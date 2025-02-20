CREATE TABLE [dbo].[MediaGlobalDisplaySetting](
	[MediaGlobalDisplaySettingsId] [int] IDENTITY(1,1) NOT NULL,
	[MediaId] [bigint] NULL,
	[MaxDisplayItems] [int] NULL,
	[MaxSmallThumbnailWidth] [int] NULL,
	[MaxSmallWidth] [int] NULL,
	[MaxMediumWidth] [int] NULL,
	[MaxThumbnailWidth] [int] NULL,
	[MaxLargeWidth] [int] NULL,
	[MaxCrossSellWidth] [int] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MediaGlobalDisplaySetting] PRIMARY KEY CLUSTERED 
(
	[MediaGlobalDisplaySettingsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
