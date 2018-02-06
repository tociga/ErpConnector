-- =============================================
-- Author:		Dadi Arnason
-- Create date: 31.08.2017
-- Description:	Sets status of items that have been created in erp
-- =============================================
CREATE PROCEDURE [bm].[products_created_update_status]
	-- Add the parameters for the stored procedure here
	@temp_id int,
	@option_id int,
	@status int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update bm.products_created set status = @status
	where temp_id = @temp_id

	update bm.options_created set status = @status
	where product_id = @temp_id

	update oic
	set oic.status= @status
	from bm.option_items_created oic
	join bm.options_created oc on oc.temp_id = oic.option_id
	where oc.product_id = @temp_id

	update bm.options set option_status_id = @status
	where id = @option_id

END

GO