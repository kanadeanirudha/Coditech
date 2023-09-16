CREATE TABLE [dbo].[GeneralCityMaster](
	[GeneralCityMasterId] [int] IDENTITY(1,1) NOT NULL,
	[CityName] [nvarchar](100) NOT NULL,
	[DefaultFlag] [bit] NOT NULL DEFAULT 0,
	[GeneralRegionMasterId] [int] NOT NULL,
	[IsUserDefined] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKGeneralCityMasterId] PRIMARY KEY CLUSTERED 
(
	[GeneralCityMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
