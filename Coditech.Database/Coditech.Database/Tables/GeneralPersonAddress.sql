CREATE TABLE [dbo].[GeneralPersonAddress](
	[GeneralPersonAddressId] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressTypeEnum] [varchar](50) NOT NULL,
	[PersonId] [bigint] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[AddressLine1] [nvarchar](200) NOT NULL,
	[AddressLine2] [nvarchar](200) NULL,
	[CompanyName] [nvarchar](100) NULL,
	[GeneralCountryMasterId] [smallint] NOT NULL,
	[GeneralRegionMasterId] [smallint] NOT NULL,
	[GeneralCityMasterId] [int] NOT NULL,
	[Postalcode] [varchar](10) NOT NULL,
	[PhoneNumber] [varchar](50) NULL,
	[MobileNumber] [varchar](15) NULL,
	[EmailAddress] [varchar](250) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[IsCorrespondanceAddressSameAsPermanentAddress] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GeneralPersonAddress] PRIMARY KEY CLUSTERED 
(
	[GeneralPersonAddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GeneralPersonAddress] ADD  CONSTRAINT [DF_GeneralPersonAddress_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[GeneralPersonAddress] ADD  CONSTRAINT [DF_GeneralPersonAddress_IsDefault]  DEFAULT ((1)) FOR [IsDefault]
GO

ALTER TABLE [dbo].[GeneralPersonAddress] ADD  CONSTRAINT [DF_GeneralPersonAddress_IsPermanentAddressSameAsCorrespondanceAddress]  DEFAULT ((0)) FOR [IsCorrespondanceAddressSameAsPermanentAddress]
GO

ALTER TABLE [dbo].[GeneralPersonAddress]  WITH CHECK ADD  CONSTRAINT [FK_GeneralPersonAddress_GeneralCityMaster] FOREIGN KEY([GeneralCityMasterId])
REFERENCES [dbo].[GeneralCityMaster] ([GeneralCityMasterId])
GO

ALTER TABLE [dbo].[GeneralPersonAddress] CHECK CONSTRAINT [FK_GeneralPersonAddress_GeneralCityMaster]
GO

ALTER TABLE [dbo].[GeneralPersonAddress]  WITH CHECK ADD  CONSTRAINT [FK_GeneralPersonAddress_GeneralCountryMaster] FOREIGN KEY([GeneralCountryMasterId])
REFERENCES [dbo].[GeneralCountryMaster] ([GeneralCountryMasterId])
GO

ALTER TABLE [dbo].[GeneralPersonAddress] CHECK CONSTRAINT [FK_GeneralPersonAddress_GeneralCountryMaster]
GO

ALTER TABLE [dbo].[GeneralPersonAddress]  WITH CHECK ADD  CONSTRAINT [FK_GeneralPersonAddress_GeneralRegionMaster] FOREIGN KEY([GeneralRegionMasterId])
REFERENCES [dbo].[GeneralRegionMaster] ([GeneralRegionMasterId])
GO

ALTER TABLE [dbo].[GeneralPersonAddress] CHECK CONSTRAINT [FK_GeneralPersonAddress_GeneralRegionMaster]
GO
