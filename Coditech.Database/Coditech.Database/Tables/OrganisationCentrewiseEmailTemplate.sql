CREATE TABLE [dbo].[OrganisationCentrewiseEmailTemplate](
	[OrganisationCentrewiseEmailTemplateId] [int] IDENTITY(1,1) NOT NULL,
	[CentreCode] [nvarchar](15) NOT NULL,
	[EmailTemplateCode] [varchar](100) NOT NULL,
	[Subject] [varchar](200) NOT NULL,
	[EmailTemplate] [nvarchar](max) NOT NULL,
	[IsSmsTemplate] [bit] NOT NULL,
	[IsWhatsAppTemplate] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_OrganisationCentrewiseEmailTemplate] PRIMARY KEY CLUSTERED 
(
	[OrganisationCentrewiseEmailTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrganisationCentrewiseEmailTemplate] ADD  CONSTRAINT [DF_OrganisationCentrewiseEmailTemplate_IsSmsTemplate]  DEFAULT ((0)) FOR [IsSmsTemplate]
GO

ALTER TABLE [dbo].[OrganisationCentrewiseEmailTemplate] ADD  CONSTRAINT [DF_OrganisationCentrewiseEmailTemplate_IsWhatsUpTemplate]  DEFAULT ((0)) FOR [IsWhatsAppTemplate]
GO

ALTER TABLE [dbo].[OrganisationCentrewiseEmailTemplate] ADD  CONSTRAINT [DF_OrganisationCentrewiseEmailTemplate_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[OrganisationCentrewiseEmailTemplate]  WITH CHECK ADD  CONSTRAINT [FK_OrganisationCentrewiseEmailTemplate_OrganisationCentreMaster] FOREIGN KEY([CentreCode])
REFERENCES [dbo].[OrganisationCentreMaster] ([CentreCode])
GO

ALTER TABLE [dbo].[OrganisationCentrewiseEmailTemplate] CHECK CONSTRAINT [FK_OrganisationCentrewiseEmailTemplate_OrganisationCentreMaster]
GO
