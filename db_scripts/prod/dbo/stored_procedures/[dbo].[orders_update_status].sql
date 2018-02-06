SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================================
-- Author:      Thordur Oskarsson
-- Create date: 29.10.2015
-- Description: Update order status or delete orders
--
--  29.10.2015.TO - Created
--  08.09.2017.DA - Added a functionality to trigger order transfer via erp connector
-- ===============================================================================

ALTER PROCEDURE [dbo].[orders_update_status]
    (
        @order_id_list AS NVARCHAR(MAX),
        @action AS NVARCHAR(100) = '' -- 'delete', 'confirm', 'unconfirm'
    ) AS
    BEGIN

            IF OBJECT_ID('tempdb..#order_list') IS NOT NULL
            DROP TABLE #order_list
            CREATE TABLE #order_list(order_id INT)

                DECLARE @start_position INT = 0
                DECLARE @stop_position INT = 0
                DECLARE @sub_string NVARCHAR(MAX)
                DECLARE @sql NVARCHAR(MAX)
                SELECT @stop_position = dbo.fn_get_nth_char_occurrence(@order_id_list,',',1000)
                IF @stop_position = 0
                    SET @stop_position = len(@order_id_list)+1

                WHILE len(@order_id_list) > 0
                BEGIN
                        SELECT @sub_string = SUBSTRING(@order_id_list,@start_position,@stop_position)
                        SELECT @order_id_list = SUBSTRING(@order_id_list,@stop_position+1,len(@order_id_list))

                        SET @sql =
                        'INSERT INTO #order_list
                        SELECT id AS order_id from dbo.orders WHERE id in ('+@sub_string+')'
                        EXEC sp_executesql @sql

                        select @stop_position = dbo.fn_get_nth_char_occurrence(@order_id_list,',',1000)
                        IF len(@order_id_list) < 10000
                            SET @stop_position = len(@order_id_list)+1
                END

                IF @action = 'delete'
                BEGIN
                    UPDATE o
                    SET deleted = 1,
                    updated_at = GETDATE()
                    FROM dbo.orders o
                    INNER JOIN #order_list ol ON o.id = ol.order_id
                    WHERE deleted = 0
                END
                IF @action = 'confirm'
                BEGIN
                    UPDATE o
                    SET [status] = 1,
                    updated_at = GETDATE()
                    FROM dbo.orders o
                    INNER JOIN #order_list ol ON o.id = ol.order_id
                    WHERE deleted = 0 AND [status] = 0
                    
                    EXEC dbo.orders_transfer_erp @order_id_list = @order_id_list
                END
                IF @action = 'unconfirm'
                BEGIN
                    UPDATE o
                    SET [status] = 0,
                    updated_at = GETDATE()
                    FROM dbo.orders o
                    INNER JOIN #order_list ol ON o.id = ol.order_id
                    WHERE deleted = 0 AND [status] = 1
                END

    END



GO