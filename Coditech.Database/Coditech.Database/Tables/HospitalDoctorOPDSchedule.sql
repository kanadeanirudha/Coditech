CREATE TABLE [dbo].[HospitalDoctorOPDSchedule](
	[HospitalDoctorOPDScheduleId] [bigint] IDENTITY(1,1) NOT NULL,
	[HospitalDoctorId] [int] NOT NULL,
	[WeekDayEnumId] [int] NOT NULL,
	[OPDTimesOfDay] [varchar](50) NOT NULL,
	[FromTime] [time](7) NOT NULL,
	[UptoTime] [time](7) NOT NULL,
	[TimeSlotInMinute] [tinyint] NOT NULL,
	[TimeZone] [varchar](50) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_HospitalDoctorsSchedules] PRIMARY KEY CLUSTERED 
(
	[HospitalDoctorOPDScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[HospitalDoctorOPDSchedule]  WITH CHECK ADD  CONSTRAINT [FK_HospitalDoctorOPDSchedule_HospitalDoctors] FOREIGN KEY([HospitalDoctorId])
REFERENCES [dbo].[HospitalDoctors] ([HospitalDoctorId])
GO

ALTER TABLE [dbo].[HospitalDoctorOPDSchedule] CHECK CONSTRAINT [FK_HospitalDoctorOPDSchedule_HospitalDoctors]
GO
