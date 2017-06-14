USE [agr5_bm_stg_dev]
GO

/****** Object:  StoredProcedure [erp].[pending_transfers]    Script Date: 14.6.2017 11:17:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





-- ===============================================================================
-- Author:		Bjorn Vignir Magnusson
-- Create date: 12/06/2017
-- Description:	Select pending transfers to ERP system
--
-- ===============================================================================
CREATE PROCEDURE [erp].[pending_transfers]
	@pendingDataTransfer BIT OUTPUT,
	@markTransferred BIT NULL = 0
AS
BEGIN
	IF @markTransferred = 1
		UPDATE [log].[erp_actions]
		SET success = 1
	ELSE
		SELECT @pendingDataTransfer = CASE WHEN id IS NOT NULL THEN 1 ELSE 0 END FROM [log].[erp_actions]
		WHERE success != 1
		AND action_type = 'run_transfer'
END















GO


