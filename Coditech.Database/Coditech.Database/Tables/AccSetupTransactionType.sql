CREATE TABLE [dbo].[AccSetupTransactionType](
	[AccSetupTransactionTypeId] [smallint] IDENTITY(1,1) NOT NULL,
	[TransactionTypeCode] [varchar](10) NOT NULL,
	[TransactionTypeName] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKAccSetupTransactionTypeId] PRIMARY KEY CLUSTERED 
(
	[AccSetupTransactionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AccSetupTransactionType] ADD  CONSTRAINT [DEF_AccSetupTransactionType_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
