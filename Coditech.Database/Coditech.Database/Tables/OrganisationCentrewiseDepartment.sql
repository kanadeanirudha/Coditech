CREATE TABLE [dbo].[OrganisationCentrewiseDepartment](
	[OrganisationCentrewiseDepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[GeneralDepartmentMasterId] [smallint] NOT NULL,
	[CentreCode] [nvarchar](15) NOT NULL,
	[ActiveFlag] [bit] NOT NULL,
	[DepartmentSeqNo] [int] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKOrganisationCentrewiseDepartmentId] PRIMARY KEY CLUSTERED 
(
	[OrganisationCentrewiseDepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrganisationCentrewiseDepartment]  WITH CHECK ADD  CONSTRAINT [FKOrganisationCentrewiseDepartmentDepartmentId] FOREIGN KEY([GeneralDepartmentMasterId])
REFERENCES [dbo].[GeneralDepartmentMaster] ([GeneralDepartmentMasterId])
GO

ALTER TABLE [dbo].[OrganisationCentrewiseDepartment] CHECK CONSTRAINT [FKOrganisationCentrewiseDepartmentDepartmentId]
GO