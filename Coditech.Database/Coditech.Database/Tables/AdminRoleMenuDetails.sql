CREATE TABLE [dbo].[AdminRoleMenuDetails](
	[AdminRoleMenuDetailId] [int] IDENTITY(1,1) NOT NULL,
	[AdminRoleMasterId] [int] NOT NULL,
	[AdminRoleCode] [nvarchar](100) NOT NULL,
	[ModuleCode] [nvarchar](50) NOT NULL,
	[MenuCode] [nvarchar](255) NOT NULL,
	[EnableDate] [datetime] NULL,
	[DisableDate] [datetime] NULL,
	[DisablePurpose] [nvarchar](150) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_AdminRoleMenuDetails] PRIMARY KEY CLUSTERED 
(
	[AdminRoleMenuDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AdminRoleMenuDetails] ADD  CONSTRAINT [DF__AdminRole__IsAct__15DA3E5D]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[AdminRoleMenuDetails]  WITH CHECK ADD  CONSTRAINT [FK_AdminRoleMenuDetails_AdminRoleMaster] FOREIGN KEY([AdminRoleMasterId])
REFERENCES [dbo].[AdminRoleMaster] ([AdminRoleMasterId])
GO

ALTER TABLE [dbo].[AdminRoleMenuDetails] CHECK CONSTRAINT [FK_AdminRoleMenuDetails_AdminRoleMaster]
GO

ALTER TABLE [dbo].[AdminRoleMenuDetails]  WITH CHECK ADD  CONSTRAINT [FK_AdminRoleMenuDetails_UserMainMenuMaster] FOREIGN KEY([MenuCode])
REFERENCES [dbo].[UserMainMenuMaster] ([MenuCode])
GO

ALTER TABLE [dbo].[AdminRoleMenuDetails] CHECK CONSTRAINT [FK_AdminRoleMenuDetails_UserMainMenuMaster]
GO

ALTER TABLE [dbo].[AdminRoleMenuDetails]  WITH CHECK ADD  CONSTRAINT [FK_AdminRoleMenuDetails_UserModuleMaster] FOREIGN KEY([ModuleCode])
REFERENCES [dbo].[UserModuleMaster] ([ModuleCode])
GO

ALTER TABLE [dbo].[AdminRoleMenuDetails] CHECK CONSTRAINT [FK_AdminRoleMenuDetails_UserModuleMaster]
GO
