CREATE TABLE [dbo].[GeneralCityMaster](
	[GeneralCityMasterId] [int] IDENTITY(1,1) NOT NULL,
	[CityName] [nvarchar](100) NOT NULL,
	[DefaultFlag] [bit] NOT NULL,
	[GeneralRegionMasterId] [smallint] NOT NULL,
	[IsUserDefined] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GeneralCityMaster] PRIMARY KEY CLUSTERED 
(
	[GeneralCityMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GeneralCityMaster]  WITH CHECK ADD  CONSTRAINT [FK_GeneralCityMaster_GeneralRegionMaster] FOREIGN KEY([GeneralRegionMasterId])
REFERENCES [dbo].[GeneralRegionMaster] ([GeneralRegionMasterId])
GO

ALTER TABLE [dbo].[GeneralCityMaster] CHECK CONSTRAINT [FK_GeneralCityMaster_GeneralRegionMaster]
GO