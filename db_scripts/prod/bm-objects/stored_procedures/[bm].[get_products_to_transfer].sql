 -- =============================================
 -- Author:		Dadi Arnason
 -- Create date: 07.09.2017
 -- Description:	Gets the details about the products to transfer.
 --              If the @temp_id parameter is null it checks if any pending items are to be transferred.
 -- =============================================
 CREATE PROCEDURE [bm].[get_products_to_transfer]
 	-- Add the parameters for the stored procedure here
 	@temp_id int = null
 AS
 BEGIN
 	-- SET NOCOUNT ON added to prevent extra result sets from
 	-- interfering with SELECT statements.
 	SET NOCOUNT ON;
 
     -- Insert statements for procedure here
 	if @temp_id is null
 	begin
 		select distinct temp_id from bm.v_items_to_create
 	end
 	else
 	begin
 	SELECT
 	[product_no]
       ,[product_name]
       ,[description]
       ,[divison_no]
       ,[divison]
       ,[department_no]
       ,[department]
       ,[sub_department_no]
       ,[sub_department]
       ,[option_name_no]
       ,[option_name]
       ,[size_no]
       ,[size]
       ,[color_no]
       ,[color]
       ,[color_group_no]
       ,[color_group]
       ,[size_group_no]
       ,[size_group]
 	  ,temp_id
 	  ,master_status
 	  ,min_order_qty
 	  ,pack_size
 	  ,display_stock
 	  , option_id
 	FROM bm.v_items_to_create WHERE temp_id = @temp_id
 	end
 END
 
 GO