CREATE TABLE [dbo].[GeneralSystemGlobleSettingMaster](
	[GeneralSystemGlobleSettingMasterId] [smallint] IDENTITY(1,1) NOT NULL,
	[FeatureName] [nvarchar](50) NOT NULL,
	[FeatureDefaultValue] [varchar](max) NOT NULL,
	[FeatureValue] [varchar](max) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GeneralSystemGlobleSettingMaster] PRIMARY KEY CLUSTERED 
(
	[GeneralSystemGlobleSettingMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
