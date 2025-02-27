CREATE TABLE [dbo].[HospitalDoctorAllocatedRoom](
	[HospitalDoctorAllocatedOPDRoomId] [int] IDENTITY(1,1) NOT NULL,
	[HospitalDoctorId] [int] NOT NULL,
	[OrganisationCentrewiseBuildingRoomId] [int] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_HospitalDoctorsAllocatedRoom] PRIMARY KEY CLUSTERED 
(
	[HospitalDoctorAllocatedOPDRoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[HospitalDoctorAllocatedRoom]  WITH CHECK ADD  CONSTRAINT [FK_HospitalDoctorsAllocatedRoom_HospitalDoctors] FOREIGN KEY([HospitalDoctorId])
REFERENCES [dbo].[HospitalDoctors] ([HospitalDoctorId])
GO

ALTER TABLE [dbo].[HospitalDoctorAllocatedRoom] CHECK CONSTRAINT [FK_HospitalDoctorsAllocatedRoom_HospitalDoctors]
GO

ALTER TABLE [dbo].[HospitalDoctorAllocatedRoom]  WITH CHECK ADD  CONSTRAINT [FK_HospitalDoctorsAllocatedRoom_OrganisationCentrewiseBuildingRooms] FOREIGN KEY([OrganisationCentrewiseBuildingRoomId])
REFERENCES [dbo].[OrganisationCentrewiseBuildingRooms] ([OrganisationCentrewiseBuildingRoomId])
GO

ALTER TABLE [dbo].[HospitalDoctorAllocatedRoom] CHECK CONSTRAINT [FK_HospitalDoctorsAllocatedRoom_OrganisationCentrewiseBuildingRooms]
GO
