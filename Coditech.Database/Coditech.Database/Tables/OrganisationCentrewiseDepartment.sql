CREATE TABLE [dbo].[OrganisationCentrewiseDepartment](
	[OrganisationCentrewiseDepartmentId] [smallint] IDENTITY(1,1) NOT NULL,
	[DepartmentId] [smallint] NOT NULL,
	[CentreCode] [nvarchar](15) NOT NULL,
	[ActiveFlag] [bit] NOT NULL,
	[DepartmentSeqNo] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKOrganisationCentrewiseDepartmentId] PRIMARY KEY CLUSTERED 
(
	[OrganisationCentrewiseDepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrganisationCentrewiseDepartment]  WITH CHECK ADD  CONSTRAINT [FKOrganisationCentrewiseDepartmentDepartmentId] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[GeneralDepartmentMaster] ([GeneralDepartmentMasterId])
GO

ALTER TABLE [dbo].[OrganisationCentrewiseDepartment] CHECK CONSTRAINT [FKOrganisationCentrewiseDepartmentDepartmentId]
GO


