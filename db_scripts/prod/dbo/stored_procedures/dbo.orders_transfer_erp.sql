SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:        Dadi Arnason
-- Create date: 22.06.2017
-- Description:    A placeholder procedure to customize for writing orders to the ERP system
-- =============================================
CREATE PROCEDURE dbo.orders_transfer_erp
    -- Add the parameters for the stored procedure here
    @order_id_list AS NVARCHAR(MAX)

AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here

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
    /* -- Inserting into staging table
    INSERT INTO [agr5_stg].log.erp_actions (action_type, reference_id, user_id, status, created_at, updated_at)
    select 'create_po_to', o.id, o.user_id, 0, GETDATE(), GETDATE()
    from #order_list list
    join dbo.orders o on o.id = list.order_id
    */

END
GO