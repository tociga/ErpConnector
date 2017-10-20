USE [agr5_stg]

GO

/****** Object:  StoredProcedure [erp].[insert_erp_action]    Script Date: 10/19/2017 1:14:12 PM ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

-- ============================================

-- Author:		Dadi Arnason

-- Create date: 01.09.2017

-- Description:	Inserts action into erp_actions

-- =============================================

CREATE PROCEDURE [erp].[insert_erp_action] 

	@action_type nvarchar(50),

	@reference_id int = null,

	@action_id int output

AS

BEGIN

	SET NOCOUNT ON;



	INSERT INTO log.erp_actions (action_type, reference_id, user_id, status, created_at, updated_at)

	values (@action_type, @reference_id, 1, 0, getdate(),getdate())



	SET @action_id = SCOPE_IDENTITY()

END





