CREATE TABLE [dbo].[OrganisationCentrewiseGSTCredential](
	[OrganisationCentrewiseGSTCredentialId] [int] IDENTITY(1,1) NOT NULL,
	[OrganisationCentreMasterId] [int] NOT NULL,
	[Version] [varchar](10) NOT NULL,
	[Urls] [varchar](max) NOT NULL,
	[EInvoiceUserName] [varchar](200) NOT NULL,
	[EInvoicePassword] [varchar](200) NOT NULL,
	[AspId] [varchar](200) NOT NULL,
	[AspUserPassword] [varchar](200) NOT NULL,
	[QrCodeSize] [int] NULL,
	[AuthToken] [varchar](200) NULL,
	[TokenExpiry] [varchar](200) NULL,
	[ClientId] [varchar](200) NULL,
	[IsLiveMode] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_OrganisationCentreWiseGSTCredential] PRIMARY KEY CLUSTERED 
(
	[OrganisationCentrewiseGSTCredentialId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrganisationCentrewiseGSTCredential]  WITH CHECK ADD  CONSTRAINT [FKOrganisationCentrewiseGSTCredentialOrganisationCentreMasterId] FOREIGN KEY([OrganisationCentreMasterId])
REFERENCES [dbo].[OrganisationCentreMaster] ([OrganisationCentreMasterId])
GO

ALTER TABLE [dbo].[OrganisationCentrewiseGSTCredential] CHECK CONSTRAINT [FKOrganisationCentrewiseGSTCredentialOrganisationCentreMasterId]
GO
