CREATE TABLE [dbo].[GeneralWhatsAppProvider](
	[GeneralWhatsAppProviderId] [tinyint] IDENTITY(1,1) NOT NULL,
	[ProviderName] [varchar](50) NOT NULL,
	[ProviderCode] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GeneralWhatsAppProvider] PRIMARY KEY CLUSTERED 
(
	[GeneralWhatsAppProviderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GeneralWhatsAppProvider] ADD  CONSTRAINT [DF_GeneralWhatsAppProvider_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
