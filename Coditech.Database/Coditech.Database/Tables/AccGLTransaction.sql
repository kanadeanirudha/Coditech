CREATE TABLE [dbo].[AccGLTransaction](
	[AccGLTransactionId] [bigint] IDENTITY(1,1) NOT NULL,
	[AccSetupBalanceSheetId] [int] NOT NULL,
	[GeneralFinancialYearId] [smallint] NOT NULL,
	[AccSetupTransactionTypeId] [smallint] NOT NULL,
	[TransactionDate] [datetime] NULL,
	[NarrationDescription] [nvarchar](500) NULL,
	[VoucherNumber] [varchar](100) NULL,
	[IsPosted] [bit] NULL,
	[PostedBy] [bigint] NULL,
	[PostedDate] [datetime] NULL,
	[TransactionEnum] [int] NULL,
	[TransactionRefId] [nvarchar](200) NULL,
	[ModeCode] [varchar](30) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKAccGLTransactionId] PRIMARY KEY CLUSTERED 
(
	[AccGLTransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AccGLTransaction] ADD  CONSTRAINT [DEF_AccGLTransaction_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO

ALTER TABLE [dbo].[AccGLTransaction]  WITH CHECK ADD  CONSTRAINT [FK_AccGLTransactionGeneralFinancialYearId] FOREIGN KEY([GeneralFinancialYearId])
REFERENCES [dbo].[GeneralFinancialYear] ([GeneralFinancialYearId])
GO

ALTER TABLE [dbo].[AccGLTransaction] CHECK CONSTRAINT [FK_AccGLTransactionGeneralFinancialYearId]
GO

ALTER TABLE [dbo].[AccGLTransaction]  WITH CHECK ADD  CONSTRAINT [FKAccGLTransactionAccSetupTransactionTypeId] FOREIGN KEY([AccSetupTransactionTypeId])
REFERENCES [dbo].[AccSetupTransactionType] ([AccSetupTransactionTypeId])
GO

ALTER TABLE [dbo].[AccGLTransaction] CHECK CONSTRAINT [FKAccGLTransactionAccSetupTransactionTypeId]
GO

ALTER TABLE [dbo].[AccGLTransaction]  WITH CHECK ADD  CONSTRAINT [FKAccGLTransactionSetupBalanceSheetId] FOREIGN KEY([AccSetupBalanceSheetId])
REFERENCES [dbo].[AccSetupBalanceSheet] ([AccSetupBalanceSheetId])
GO

ALTER TABLE [dbo].[AccGLTransaction] CHECK CONSTRAINT [FKAccGLTransactionSetupBalanceSheetId]
GO
