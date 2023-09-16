CREATE TABLE [dbo].[GeneralCountryMaster](
	[GeneralCountryMasterId] [smallint] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](60) NOT NULL,
	[CountryCode] [nvarchar](50) NOT NULL,
	[IsUserDefined] [bit] NOT NULL DEFAULT 0,
	[DefaultFlag] [bit] NULL DEFAULT 0,
	[SeqNo] [int] NULL,
	[CurrencyCode] [nvarchar](5) NULL,
	[currencySymbol] [nchar](10) NULL,
	[CountryFlag] [varbinary](max) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKGeneralCountryMasterID] PRIMARY KEY CLUSTERED 
(
	[GeneralCountryMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UKGeneralCountryMasterCountryName] UNIQUE NONCLUSTERED 
(
	[CountryName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



