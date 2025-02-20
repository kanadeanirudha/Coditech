
CREATE TABLE [dbo].[GeneralCountryMaster](
	[GeneralCountryMasterId] [smallint] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](60) NOT NULL,
	[CountryCode] [nvarchar](50) NOT NULL,
	[IsUserDefined] [bit] NOT NULL,
	[CallingCode] [varchar](5) NULL,
	[DefaultFlag] [bit] NULL,
	[SeqNo] [int] NULL,
	[CurrencyCode] [nvarchar](5) NULL,
	[currencySymbol] [nchar](10) NULL,
	[CountryFlag] [varbinary](max) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
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

ALTER TABLE [dbo].[GeneralCountryMaster] ADD  CONSTRAINT [DF__GeneralCo__IsUse__38996AB5]  DEFAULT ((0)) FOR [IsUserDefined]
GO

ALTER TABLE [dbo].[GeneralCountryMaster] ADD  CONSTRAINT [DF_GeneralCountryMaster_CallingCode]  DEFAULT ((0)) FOR [CallingCode]
GO

ALTER TABLE [dbo].[GeneralCountryMaster] ADD  CONSTRAINT [DF__GeneralCo__Defau__398D8EEE]  DEFAULT ((0)) FOR [DefaultFlag]
GO
