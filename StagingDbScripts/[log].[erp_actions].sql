USE [agr5_bm_stg_dev]
GO

/****** Object:  Table [log].[erp_actions]    Script Date: 14.6.2017 11:17:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [log].[erp_actions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[action_type] [nvarchar](255) NULL,
	[created_at] [datetime] NOT NULL,
	[updated_at] [datetime] NOT NULL,
	[user_id] [int] NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_erp_actions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


