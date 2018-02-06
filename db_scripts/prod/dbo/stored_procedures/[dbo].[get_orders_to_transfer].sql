-- =============================================
-- Author:		Dadi Arnason
-- Create date: 01.09.2017
-- Description:	Gets pending po or to transfer to the erp
-- =============================================
CREATE PROCEDURE [dbo].[get_orders_to_transfer]
	-- Add the parameters for the stored procedure here
	@order_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [order_id]
      ,[article_no]
      ,[location_no]
      ,[orderfrom_location_no]
      ,[color]
      ,[size]
      ,[style]
      ,[user_id]
      ,[unit_qty_chg]
      ,[est_delivery_date]
	  ,[location_type]
      ,[SITEID]
      ,[CHANNELID]
      ,[WAREHOUSE]
  FROM [dbo].[v_orders_to_transfer]
  where order_id = @order_id
END
GO