CREATE TABLE [dbo].[HospitalPatientType](
	[HospitalPatientTypeId] [tinyint] IDENTITY(1,1) NOT NULL,
	[PatientType] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_HospitalPatientType] PRIMARY KEY CLUSTERED 
(
	[HospitalPatientTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[HospitalPatientType] ADD  CONSTRAINT [DF_HospitalPatientType_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
