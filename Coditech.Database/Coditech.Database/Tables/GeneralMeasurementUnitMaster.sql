CREATE TABLE [dbo].[GeneralMeasurementUnitMaster](
	[GeneralMeasurementUnitMasterId] [smallint] IDENTITY(1,1) NOT NULL,
	[MeasurementUnitDisplayName] [nvarchar](50) NOT NULL,
	[MeasurementUnitShortCode] [varchar](50) NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GeneralMeasurementUnitMaster] PRIMARY KEY CLUSTERED 
(
	[GeneralMeasurementUnitMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
