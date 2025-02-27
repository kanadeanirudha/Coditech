CREATE TABLE [dbo].[HospitalDoctorLeaveSchedule](
	[HospitalDoctorLeaveScheduleId] [bigint] IDENTITY(1,1) NOT NULL,
	[HospitalDoctorId] [int] NOT NULL,
	[LeaveDate] [date] NOT NULL,
	[IsFullDay] [bit] NOT NULL,
	[FromTime] [time](7) NULL,
	[UptoTime] [time](7) NULL,
	[Remark] [nvarchar](500) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_HospitalDoctorLeaveSchedule] PRIMARY KEY CLUSTERED 
(
	[HospitalDoctorLeaveScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[HospitalDoctorLeaveSchedule]  WITH CHECK ADD  CONSTRAINT [FK_HospitalDoctorLeaveSchedule_HospitalDoctors] FOREIGN KEY([HospitalDoctorId])
REFERENCES [dbo].[HospitalDoctors] ([HospitalDoctorId])
GO

ALTER TABLE [dbo].[HospitalDoctorLeaveSchedule] CHECK CONSTRAINT [FK_HospitalDoctorLeaveSchedule_HospitalDoctors]
GO
