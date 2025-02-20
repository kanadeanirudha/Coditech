CREATE TABLE [dbo].[OrganisationCentrewiseWhatsAppSetting](
	[OrganisationCentrewiseWhatsAppSettingId] [int] IDENTITY(1,1) NOT NULL,
	[GeneralWhatsAppProviderId] [tinyint] NOT NULL,
	[CentreCode] [nvarchar](15) NOT NULL,
	[WhatsAppPortalAccountId] [varchar](50) NULL,
	[AuthToken] [varchar](50) NULL,
	[FromMobileNumber] [varchar](15) NULL,
	[IsWhatsAppSettingEnabled] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_OrganisationCentrewiseWhatsAppSetting] PRIMARY KEY CLUSTERED 
(
	[OrganisationCentrewiseWhatsAppSettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrganisationCentrewiseWhatsAppSetting] ADD  CONSTRAINT [DF_OrganisationCentrewiseWhatsAppSetting_IsSMSSettingEnabled]  DEFAULT ((0)) FOR [IsWhatsAppSettingEnabled]
GO
