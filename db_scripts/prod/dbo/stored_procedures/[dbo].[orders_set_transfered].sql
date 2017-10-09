-- ===============================================================================
-- Author:      Dadi Arnason
-- Create date: 01.09.2017
-- Description: Sets order to be transfered
--
--  1.09.2017.DA - Created 
-- ===============================================================================

CREATE PROCEDURE [dbo].[orders_set_transfered]
    (
        @order_id AS int
    ) AS
    BEGIN

    UPDATE orders
		SET [status] = 2,
        updated_at = GETDATE()
        WHERE id = @order_id

    END



GO