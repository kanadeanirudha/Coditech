CREATE TABLE [dbo].[AdminRoleMaster](
	[AdminRoleMasterId] [int] IDENTITY(1,1) NOT NULL,
	[AdminSanctionPostId] [int] NOT NULL,
	[SanctionPostName] [nvarchar](100) NOT NULL,
	[MonitoringLevel] [varchar](12) NOT NULL,
	[AdminRoleCode] [nvarchar](100) NOT NULL,
	[OthCentreLevel] [nvarchar](30) NULL,
	[IsLoginAllowFromOutside] [bit] NOT NULL,
	[IsAttendaceAllowFromOutside] [bit] NOT NULL,
	[DashboardFormEnumId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_AdminRoleMaster] PRIMARY KEY CLUSTERED 
(
	[AdminRoleMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AdminRoleMaster] ADD  CONSTRAINT [DF__AdminRole__IsLog__719CDDE7]  DEFAULT ((0)) FOR [IsLoginAllowFromOutside]
GO

ALTER TABLE [dbo].[AdminRoleMaster] ADD  CONSTRAINT [DF__AdminRole__IsAtt__72910220]  DEFAULT ((0)) FOR [IsAttendaceAllowFromOutside]
GO

ALTER TABLE [dbo].[AdminRoleMaster] ADD  CONSTRAINT [DF_AdminRoleMaster_DashboardFormEnumId]  DEFAULT ((0)) FOR [DashboardFormEnumId]
GO

ALTER TABLE [dbo].[AdminRoleMaster] ADD  CONSTRAINT [DF__AdminRole__IsAct__73852659]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[AdminRoleMaster]  WITH CHECK ADD  CONSTRAINT [FK_AdminRoleMaster_AdminSanctionPost] FOREIGN KEY([AdminSanctionPostId])
REFERENCES [dbo].[AdminSanctionPost] ([AdminSanctionPostId])
GO
