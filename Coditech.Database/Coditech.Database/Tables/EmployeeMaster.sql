CREATE TABLE [dbo].[EmployeeMaster](
	[EmployeeId] [bigint] IDENTITY(1,1) NOT NULL,
	[PersonId] [bigint] NOT NULL,
	[PersonCode] [nvarchar](200) NOT NULL,
	[UserType] [varchar](30) NOT NULL,
	[CentreCode] [nvarchar](15) NOT NULL,
	[GeneralDepartmentMasterId] [smallint] NOT NULL,
	[EmployeeDesignationMasterId] [smallint] NULL,
	[IsEmployeeSmoker] [bit] NOT NULL,
	[ReportingEmployeeId] [bigint] NULL,
	[PANCardNumber] [varchar](10) NULL,
	[UANNumber] [varchar](12) NULL,
	[PassportNumber] [varchar](10) NULL,
	[AdharCardNumber] [varchar](12) NULL,
	[BankName] [nvarchar](50) NULL,
	[BankAccountNumber] [varchar](20) NULL,
	[BankIFSCCode] [varchar](20) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_EmployeeMaster] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[EmployeeMaster] ADD  CONSTRAINT [DF_EmployeeMaster_IsEmployeeSmoker]  DEFAULT ((0)) FOR [IsEmployeeSmoker]
GO

ALTER TABLE [dbo].[EmployeeMaster] ADD  CONSTRAINT [DF_EmployeeMaster_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO

ALTER TABLE [dbo].[EmployeeMaster]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeMaster_GeneralDepartmentMaster] FOREIGN KEY([GeneralDepartmentMasterId])
REFERENCES [dbo].[GeneralDepartmentMaster] ([GeneralDepartmentMasterId])
GO

ALTER TABLE [dbo].[EmployeeMaster] CHECK CONSTRAINT [FK_EmployeeMaster_GeneralDepartmentMaster]
GO

ALTER TABLE [dbo].[EmployeeMaster]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeMaster_GeneralPerson] FOREIGN KEY([PersonId])
REFERENCES [dbo].[GeneralPerson] ([PersonId])
GO

ALTER TABLE [dbo].[EmployeeMaster] CHECK CONSTRAINT [FK_EmployeeMaster_GeneralPerson]
GO

ALTER TABLE [dbo].[EmployeeMaster]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeMaster_OrganisationCentreMaster] FOREIGN KEY([CentreCode])
REFERENCES [dbo].[OrganisationCentreMaster] ([CentreCode])
GO

ALTER TABLE [dbo].[EmployeeMaster] CHECK CONSTRAINT [FK_EmployeeMaster_OrganisationCentreMaster]
GO