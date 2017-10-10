SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [log].[erp_actions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[action_type] [nvarchar](50) NOT NULL,
	[reference_id] [int] NULL,
	[user_id] [int] NULL,
	[status] [int] NOT NULL,
	[created_at] [datetime] NOT NULL,
	[updated_at] [datetime] NOT NULL,
	[error_message] [nvarchar](max) NULL,
	[error_stack_trace] [nvarchar](max) NULL,
 CONSTRAINT [PK_erp_actions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
