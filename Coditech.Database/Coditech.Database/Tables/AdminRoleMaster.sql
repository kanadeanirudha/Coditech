CREATE TABLE [dbo].[AdminRoleMaster](
	[AdminRoleMasterId] [int] IDENTITY(1,1) NOT NULL,
	[AdminSanctionPostId] [int] NOT NULL,
	[SanctionPostName] [nvarchar](100) NOT NULL,
	[MonitoringLevel] [varchar](12) NOT NULL,
	[AdminRoleCode] [nvarchar](30) NOT NULL,
	[OthCentreLevel] [nvarchar](30) NULL,
	[IsLoginAllowFromOutside] [bit] NOT NULL,
	[IsAttendaceAllowFromOutside] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PkAdmRlMstId] PRIMARY KEY CLUSTERED 
(
	[AdminRoleMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AdminRoleMaster] ADD  DEFAULT ((0)) FOR [IsLoginAllowFromOutside]
GO

ALTER TABLE [dbo].[AdminRoleMaster] ADD  DEFAULT ((0)) FOR [IsAttendaceAllowFromOutside]
GO

ALTER TABLE [dbo].[AdminRoleMaster] ADD  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[AdminRoleMaster]  WITH CHECK ADD  CONSTRAINT [FKAdminRoleMasterAdminSanctionPostId] FOREIGN KEY([AdminSanctionPostId])
REFERENCES [dbo].[AdminSanctionPost] ([AdminSanctionPostId])
GO

ALTER TABLE [dbo].[AdminRoleMaster] CHECK CONSTRAINT [FKAdminRoleMasterAdminSanctionPostId]
GO


