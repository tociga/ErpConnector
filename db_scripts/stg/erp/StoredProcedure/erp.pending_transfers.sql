-- =============================================
-- Author:		Dadi Arnason
-- Create date: 15.08.2017
-- Description:	Gets Pending erp actions, action_type can take the following values:
--              daily_refresh, full_refresh, pim_full, transaction_full, transaction_refresh, create_po_to, create_item
-- =============================================

ALTER PROCEDURE [erp].[pending_transfers]

AS
BEGIN

	SET NOCOUNT ON;

	SELECT id, action_type, reference_id, user_id, status, created_at, updated_at 
	FROM log.erp_actions 
	WHERE status = 0
END

GO