
-- =============================================
-- Author:		Dadi Arnason
-- Create date: 10.10.2017
-- Description:	Used to find the max rec id in a ax table in stg
-- =============================================
CREATE PROCEDURE [ax].[find_max_rec_id]
	-- Add the parameters for the stored procedure here
	@schema nvarchar(5),
	@table_name nvarchar(255)
AS
BEGIN
	-- SET NOCOUNT ON added dto prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    declare @sql nvarchar(max)
	set @sql = 'SELECT max(recId) as maxRecId FROM ' + @schema + '.'+ @table_name 

	exec (@sql)
END


GO
