CREATE TABLE [dbo].[GeneralEmailTemplate](
	[GeneralEmailTemplateId] [smallint] IDENTITY(1,1) NOT NULL,
	[EmailTemplateCode] [varchar](100) NOT NULL,
	[EmailTemplateName] [varchar](100) NOT NULL,
	[Subject] [varchar](200) NOT NULL,
	[EmailTemplate] [nvarchar](max) NOT NULL,
	[ModuleCode] [nvarchar](50) NULL,
	[IsSmsTemplate] [bit] NOT NULL,
	[IsWhatsAppTemplate] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GeneralEmailTemplate] PRIMARY KEY CLUSTERED 
(
	[GeneralEmailTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[GeneralEmailTemplate] ADD  CONSTRAINT [DF_GeneralEmailTemplate_IsSmsTemplate]  DEFAULT ((0)) FOR [IsSmsTemplate]
GO

ALTER TABLE [dbo].[GeneralEmailTemplate] ADD  CONSTRAINT [DF_GeneralEmailTemplate_IsSmsTemplate1]  DEFAULT ((0)) FOR [IsWhatsAppTemplate]
GO

ALTER TABLE [dbo].[GeneralEmailTemplate] ADD  CONSTRAINT [DF_GeneralEmailTemplate_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
