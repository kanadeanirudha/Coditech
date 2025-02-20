CREATE TABLE [dbo].[AdminSanctionPost](
	[AdminSanctionPostId] [int] IDENTITY(1,1) NOT NULL,
	[DesignationId] [smallint] NOT NULL,
	[NoOfPost] [smallint] NOT NULL,
	[DepartmentId] [smallint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CentreCode] [nvarchar](15) NOT NULL,
	[DesignationType] [nvarchar](15) NOT NULL,
	[SanctionPostCode] [nvarchar](100) NOT NULL,
	[PostType] [nvarchar](15) NOT NULL,
	[SanctionedPostDescription] [nvarchar](200) NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_AdminSanctionPost] PRIMARY KEY CLUSTERED 
(
	[AdminSanctionPostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AdminSanctionPost] ADD  CONSTRAINT [DF__AdminSanc__IsAct__76619304]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[AdminSanctionPost]  WITH CHECK ADD  CONSTRAINT [FK_AdminSanctionPost_EmployeeDesignationMaster] FOREIGN KEY([DesignationId])
REFERENCES [dbo].[EmployeeDesignationMaster] ([EmployeeDesignationMasterId])
GO

ALTER TABLE [dbo].[AdminSanctionPost] CHECK CONSTRAINT [FK_AdminSanctionPost_EmployeeDesignationMaster]
GO

ALTER TABLE [dbo].[AdminSanctionPost]  WITH CHECK ADD  CONSTRAINT [FK_AdminSanctionPost_GeneralDepartmentMaster] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[GeneralDepartmentMaster] ([GeneralDepartmentMasterId])
GO

ALTER TABLE [dbo].[AdminSanctionPost] CHECK CONSTRAINT [FK_AdminSanctionPost_GeneralDepartmentMaster]
GO

ALTER TABLE [dbo].[AdminSanctionPost]  WITH CHECK ADD  CONSTRAINT [FK_AdminSanctionPost_OrganisationCentreMaster] FOREIGN KEY([CentreCode])
REFERENCES [dbo].[OrganisationCentreMaster] ([CentreCode])
GO

ALTER TABLE [dbo].[AdminSanctionPost] CHECK CONSTRAINT [FK_AdminSanctionPost_OrganisationCentreMaster]
GO
