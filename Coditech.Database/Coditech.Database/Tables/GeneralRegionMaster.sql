CREATE TABLE [dbo].[GeneralRegionMaster](
	[GeneralRegionMasterId] [smallint] IDENTITY(1,1) NOT NULL,
	[RegionName] [nvarchar](50) NOT NULL,
	[CountryId] [int] NOT NULL,
	[ShortName] [nvarchar](50) NOT NULL,
	[DefaultFlag] [bit] NOT NULL DEFAULT 0,
	[IsUserDefined] [bit] NOT NULL DEFAULT 0,
	[TinNumber] [smallint] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKGeneralRegionMasterID] PRIMARY KEY CLUSTERED 
(
	[GeneralRegionMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UKGeneralRegionMasterRegionName] UNIQUE NONCLUSTERED 
(
	[RegionName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


