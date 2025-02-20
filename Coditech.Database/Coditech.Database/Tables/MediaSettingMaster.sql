CREATE TABLE [dbo].[MediaSettingMaster](
	[MediaSettingMasterId] [tinyint] IDENTITY(1,1) NOT NULL,
	[MediaTypeMasterId] [tinyint] NOT NULL,
	[MediaConfigurationId] [tinyint] NULL,
	[MaxSizeInMB] [smallint] NOT NULL,
	[MediaTypeExtensionMasterIds] [varchar](200) NULL,
	[LargeImageResize] [smallint] NULL,
	[MediumImageResize] [smallint] NULL,
	[SmallImageResize] [smallint] NULL,
	[CrossSellImageResize] [smallint] NULL,
	[ThumbnailImageResize] [smallint] NULL,
	[SmallThumbnailImageResize] [smallint] NULL,
	[HelpDescription] [nvarchar](100) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MediaSettingMaster] PRIMARY KEY CLUSTERED 
(
	[MediaSettingMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MediaSettingMaster]  WITH CHECK ADD  CONSTRAINT [FK_MediaSettingMaster_MediaConfiguration] FOREIGN KEY([MediaConfigurationId])
REFERENCES [dbo].[MediaConfiguration] ([MediaConfigurationId])
GO

ALTER TABLE [dbo].[MediaSettingMaster] CHECK CONSTRAINT [FK_MediaSettingMaster_MediaConfiguration]
GO

ALTER TABLE [dbo].[MediaSettingMaster]  WITH CHECK ADD  CONSTRAINT [FK_MediaSettingMaster_MediaTypeMaster] FOREIGN KEY([MediaTypeMasterId])
REFERENCES [dbo].[MediaTypeMaster] ([MediaTypeMasterId])
GO

ALTER TABLE [dbo].[MediaSettingMaster] CHECK CONSTRAINT [FK_MediaSettingMaster_MediaTypeMaster]
GO
