CREATE TABLE [dbo].[OrganisationCentrewiseSmsSetting](
	[OrganisationCentrewiseSmsSettingId] [int] IDENTITY(1,1) NOT NULL,
	[GeneralSmsProviderId] [tinyint] NOT NULL,
	[CentreCode] [nvarchar](15) NOT NULL,
	[SmsPortalAccountId] [varchar](50) NULL,
	[AuthToken] [varchar](50) NULL,
	[FromMobileNumber] [varchar](15) NULL,
	[IsSMSSettingEnabled] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_OrganisationCentrewiseSmsSetting] PRIMARY KEY CLUSTERED 
(
	[OrganisationCentrewiseSmsSettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrganisationCentrewiseSmsSetting] ADD  CONSTRAINT [DF_OrganisationCentrewiseSmsSetting_IsSMSSettingEnabled]  DEFAULT ((0)) FOR [IsSMSSettingEnabled]
GO
