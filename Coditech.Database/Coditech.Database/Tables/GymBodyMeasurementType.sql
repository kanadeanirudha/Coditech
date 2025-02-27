CREATE TABLE [dbo].[GymBodyMeasurementType](
	[GymBodyMeasurementTypeId] [smallint] IDENTITY(1,1) NOT NULL,
	[BodyMeasurementType] [nvarchar](50) NOT NULL,
	[GeneralMeasurementUnitMasterId] [smallint] NOT NULL,
	[DisplayOrder] [smallint] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GymBodyMeasurementType] PRIMARY KEY CLUSTERED 
(
	[GymBodyMeasurementTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GymBodyMeasurementType] ADD  CONSTRAINT [DF_GymBodyMeasurementType_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
