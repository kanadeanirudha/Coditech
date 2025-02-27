CREATE TABLE [dbo].[HospitalPatientAppointment](
	[HospitalPatientAppointmentId] [bigint] IDENTITY(1,1) NOT NULL,
	[AppointmentTypeEnumId] [int] NULL,
	[AppointmentDate] [date] NULL,
	[HospitalDoctorId] [int] NULL,
	[RequestedTimeSlot] [time](7) NULL,
	[HospitalPatientAppointmentPurposeId] [smallint] NULL,
	[ApprovalStatusEnumId] [int] NULL,
	[IsAttended] [bit] NOT NULL,
	[HospitalPatientRegistrationId] [bigint] NULL,
	[HospitalTempPersonId] [bigint] NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[MobileNumber] [nvarchar](15) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_HospitalPatientAppointment] PRIMARY KEY CLUSTERED 
(
	[HospitalPatientAppointmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[HospitalPatientAppointment] ADD  CONSTRAINT [DF_HospitalPatientAppointment_IsAttended]  DEFAULT ((0)) FOR [IsAttended]
GO
