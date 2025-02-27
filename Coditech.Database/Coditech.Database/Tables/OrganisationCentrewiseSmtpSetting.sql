CREATE TABLE [dbo].[OrganisationCentrewiseSmtpSetting](
	[OrganisationCentrewiseSmtpSettingId] [int] IDENTITY(1,1) NOT NULL,
	[CentreCode] [nvarchar](15) NOT NULL,
	[ServerName] [varchar](200) NOT NULL,
	[UserName] [varchar](200) NOT NULL,
	[Password] [varchar](200) NOT NULL,
	[Port] [int] NOT NULL,
	[IsEnableSsl] [bit] NOT NULL,
	[FromDisplayName] [nvarchar](256) NULL,
	[FromEmailAddress] [nvarchar](256) NULL,
	[BccEmailAddress] [nvarchar](max) NULL,
	[DisableAllEmails] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_OrganisationCentrewiseSmtpSetting] PRIMARY KEY CLUSTERED 
(
	[OrganisationCentrewiseSmtpSettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrganisationCentrewiseSmtpSetting] ADD  CONSTRAINT [DF_OrganisationCentrewiseSmtpSetting_DisableAllEmails]  DEFAULT ((1)) FOR [DisableAllEmails]
GO

ALTER TABLE [dbo].[OrganisationCentrewiseSmtpSetting]  WITH CHECK ADD  CONSTRAINT [FK_OrganisationCentrewiseSmtpSetting_OrganisationCentreMaster] FOREIGN KEY([CentreCode])
REFERENCES [dbo].[OrganisationCentreMaster] ([CentreCode])
GO

ALTER TABLE [dbo].[OrganisationCentrewiseSmtpSetting] CHECK CONSTRAINT [FK_OrganisationCentrewiseSmtpSetting_OrganisationCentreMaster]
GO
