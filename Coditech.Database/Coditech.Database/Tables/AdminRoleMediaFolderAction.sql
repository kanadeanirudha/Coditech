CREATE TABLE [dbo].[AdminRoleMediaFolderAction](
	[AdminRoleMediaFolderActionId] [int] IDENTITY(1,1) NOT NULL,
	[AdminRoleMasterId] [int] NOT NULL,
	[MediaAction] [varchar](500) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_AdminRoleMediaFolderAction] PRIMARY KEY CLUSTERED 
(
	[AdminRoleMediaFolderActionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO