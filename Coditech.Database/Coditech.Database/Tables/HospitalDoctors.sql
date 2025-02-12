CREATE TABLE [dbo].[HospitalDoctors](
	[HospitalDoctorId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[MedicalSpecializationEnumId] [int] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_HospitalDoctors] PRIMARY KEY CLUSTERED 
(
	[HospitalDoctorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[HospitalDoctors]  WITH CHECK ADD  CONSTRAINT [FK_HospitalDoctors_EmployeeMaster] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[EmployeeMaster] ([EmployeeId])
GO

ALTER TABLE [dbo].[HospitalDoctors] CHECK CONSTRAINT [FK_HospitalDoctors_EmployeeMaster]
GO
