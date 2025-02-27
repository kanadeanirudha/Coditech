CREATE TABLE [dbo].[HospitalPatientAppointmentPurpose](
	[HospitalPatientAppointmentPurposeId] [smallint] IDENTITY(1,1) NOT NULL,
	[AppointmentPurpose] [nvarchar](500) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_HospitalPatientAppointmentPurpose] PRIMARY KEY CLUSTERED 
(
	[HospitalPatientAppointmentPurposeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[HospitalPatientAppointmentPurpose] ADD  CONSTRAINT [DF_HospitalPatientAppointmentPurpose_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
