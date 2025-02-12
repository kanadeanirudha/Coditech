CREATE TABLE [dbo].[GymMemberBodyMeasurement](
	[GymMemberBodyMeasurementId] [bigint] IDENTITY(1,1) NOT NULL,
	[GymMemberDetailId] [int] NOT NULL,
	[GymBodyMeasurementTypeId] [smallint] NOT NULL,
	[BodyMeasurementValue] [nvarchar](50) NOT NULL,
	[BodyMeasurementDate] [datetime] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GymMemberBodyMeasurement] PRIMARY KEY CLUSTERED 
(
	[GymMemberBodyMeasurementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
