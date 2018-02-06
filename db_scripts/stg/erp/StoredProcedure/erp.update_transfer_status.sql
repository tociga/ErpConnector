-- =============================================
-- Author:		Dadi Arnason
-- Create date: 15.08.2017
-- Description:	Updates the status of an erp action
-- =============================================
CREATE PROCEDURE [erp].[update_transfer_status]
	@action_id int,
	@status int,
	@error_message nvarchar(MAX) = null,
	@error_stack_trace nvarchar(MAX) = null
AS
BEGIN

	SET NOCOUNT ON;

	update log.erp_actions
	SET status = @status, updated_at = GETDATE(), error_message = @error_message, error_stack_trace = @error_stack_trace
	WHERE id = @action_id													

END

GO

