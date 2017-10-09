-- =============================================
-- Author:		Dadi Arnason
-- Create date: 06.09.2017
-- Description:	Gets a date given a date_id, used for the erp connector
-- =============================================
CREATE PROCEDURE [dbo].[get_date_by_id]
	-- Add the parameters for the stored procedure here
	@date_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select date from date_table where date_id = @date_id
END
GO