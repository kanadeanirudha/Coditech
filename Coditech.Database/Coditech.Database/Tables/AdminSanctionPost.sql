CREATE TABLE [dbo].[AdminSanctionPost](
	[AdminSanctionPostId] [int] IDENTITY(1,1) NOT NULL,
	[DesignationId] [smallint] NOT NULL,
	[NoOfPost] [smallint] NOT NULL,
	[DepartmentId] [smallint] NOT NULL,
	[IsActive] [bit] NOT NULL DEFAULT 1,
	[CentreCode] [nvarchar](15) NOT NULL,
	[DesignationType] [nvarchar](15) NOT NULL,
	[SanctionPostCode] [nvarchar](30) NOT NULL,
	[PostType] [nvarchar](15) NOT NULL,
	[SanctionedPostDescription] [nvarchar](200) NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PkAdminSanctionPostId] PRIMARY KEY CLUSTERED 
(
	[AdminSanctionPostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE [dbo].[AdminSanctionPost]  WITH CHECK ADD  CONSTRAINT [FKAdminSanctionPostDepartmentId] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[GeneralDepartmentMaster] ([GeneralDepartmentMasterId])
GO

ALTER TABLE [dbo].[AdminSanctionPost] CHECK CONSTRAINT [FKAdminSanctionPostDepartmentId]
GO

ALTER TABLE [dbo].[AdminSanctionPost]  WITH CHECK ADD  CONSTRAINT [FKAdminSanctionPostDesignationId] FOREIGN KEY([DesignationId])
REFERENCES [dbo].[EmployeeDesignationMaster] ([EmployeeDesignationMasterId])
GO

ALTER TABLE [dbo].[AdminSanctionPost] CHECK CONSTRAINT [FKAdminSanctionPostDesignationId]
GO


