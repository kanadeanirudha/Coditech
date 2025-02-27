CREATE TABLE [dbo].[AccGLTransactionDetails](
	[AccGLTransactionDetailsId] [bigint] IDENTITY(1,1) NOT NULL,
	[AccGLTransactionId] [bigint] NOT NULL,
	[AccSetupGLId] [int] NOT NULL,
	[TransactionAmount] [money] NULL,
	[DebitCreditEnum] [int] NOT NULL,
	[ChequeNo] [nvarchar](20) NULL,
	[ChequeDatetime] [datetime] NULL,
	[NarrationDescription] [nvarchar](500) NULL,
	[BankName] [nvarchar](120) NULL,
	[BranchName] [nvarchar](120) NULL,
	[BankClearingDatetime] [datetime] NULL,
	[PersonId] [bigint] NULL,
	[UserType] [varchar](30) NULL,
	[SubmitSlipNo] [varchar](20) NULL,
	[SubmitDatetime] [datetime] NULL,
	[ReconcilationDatetime] [datetime] NOT NULL,
	[ModeCode] [varchar](30) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKAccGLTransactionDetailsId] PRIMARY KEY CLUSTERED 
(
	[AccGLTransactionDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AccGLTransactionDetails] ADD  CONSTRAINT [DEF_AccGLTransactionDetails_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO

ALTER TABLE [dbo].[AccGLTransactionDetails]  WITH CHECK ADD  CONSTRAINT [FKAccGLTransactionDetailsAccGLTransactionId] FOREIGN KEY([AccGLTransactionId])
REFERENCES [dbo].[AccGLTransaction] ([AccGLTransactionId])
GO

ALTER TABLE [dbo].[AccGLTransactionDetails] CHECK CONSTRAINT [FKAccGLTransactionDetailsAccGLTransactionId]
GO

ALTER TABLE [dbo].[AccGLTransactionDetails]  WITH CHECK ADD  CONSTRAINT [FKAccGLTransactionDetailsAccSetupGLId] FOREIGN KEY([AccSetupGLId])
REFERENCES [dbo].[AccSetupGL] ([AccSetupGLId])
GO

ALTER TABLE [dbo].[AccGLTransactionDetails] CHECK CONSTRAINT [FKAccGLTransactionDetailsAccSetupGLId]
GO
