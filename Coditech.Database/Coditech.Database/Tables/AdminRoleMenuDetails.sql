CREATE TABLE [dbo].[AdminRoleMenuDetails](
	[AdminRoleMenuDetailId] [int] IDENTITY(1,1) NOT NULL,
	[AdminRoleMasterId] [int] NOT NULL,
	[AdminRoleCode] [nvarchar](30) NOT NULL,
	[MenuCode] [varchar](50) NOT NULL,
	[EnableDate] [datetime] NULL,
	[DisableDate] [datetime] NULL,
	[DisablePurpose] [nvarchar](150) NOT NULL,
	[IsActive] [bit] NOT NULL DEFAULT 1,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PkAdminRoleMenuDetailsId] PRIMARY KEY CLUSTERED 
(
	[AdminRoleMenuDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE [dbo].[AdminRoleMenuDetails]  WITH NOCHECK ADD  CONSTRAINT [FkAdminRoleMenuDetailsAdminRoleMasterId] FOREIGN KEY([AdminRoleMasterId])
REFERENCES [dbo].[AdminRoleMaster] ([AdminRoleMasterId])
GO

ALTER TABLE [dbo].[AdminRoleMenuDetails] NOCHECK CONSTRAINT [FkAdminRoleMenuDetailsAdminRoleMasterId]
GO


