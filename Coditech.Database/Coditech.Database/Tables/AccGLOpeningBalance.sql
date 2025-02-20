CREATE TABLE [dbo].[AccGLOpeningBalance](
	[ACCGLOpeningBalanceId] [int] IDENTITY(1,1) NOT NULL,
	[GeneralFinancialYearId] [smallint] NOT NULL,
	[AccSetupGLId] [int] NOT NULL,
	[AccSetupBalanceSheetId] [int] NOT NULL,
	[OpeningDatetime] [datetime] NOT NULL,
	[OpeningBalance] [money] NOT NULL,
	[TotalDebitAmount] [money] NOT NULL,
	[TotalCreditAmount] [money] NOT NULL,
	[ClosingBalance] [money] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKACCGLOpeningBalanceId] PRIMARY KEY CLUSTERED 
(
	[ACCGLOpeningBalanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AccGLOpeningBalance] ADD  CONSTRAINT [DF_AccGLOpeningBalance_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO

ALTER TABLE [dbo].[AccGLOpeningBalance]  WITH CHECK ADD  CONSTRAINT [FK_AccGLOpeningBalanceGeneralFinancialYearId] FOREIGN KEY([GeneralFinancialYearId])
REFERENCES [dbo].[GeneralFinancialYear] ([GeneralFinancialYearId])
GO

ALTER TABLE [dbo].[AccGLOpeningBalance] CHECK CONSTRAINT [FK_AccGLOpeningBalanceGeneralFinancialYearId]
GO

ALTER TABLE [dbo].[AccGLOpeningBalance]  WITH CHECK ADD  CONSTRAINT [FKAccGLOpeningBalanceAccSetupBalanceSheetId] FOREIGN KEY([AccSetupBalanceSheetId])
REFERENCES [dbo].[AccSetupBalanceSheet] ([AccSetupBalanceSheetId])
GO

ALTER TABLE [dbo].[AccGLOpeningBalance] CHECK CONSTRAINT [FKAccGLOpeningBalanceAccSetupBalanceSheetId]
GO

ALTER TABLE [dbo].[AccGLOpeningBalance]  WITH CHECK ADD  CONSTRAINT [FKAccGLOpeningBalanceAccSetupGLId] FOREIGN KEY([AccSetupGLId])
REFERENCES [dbo].[AccSetupGL] ([AccSetupGLId])
GO

ALTER TABLE [dbo].[AccGLOpeningBalance] CHECK CONSTRAINT [FKAccGLOpeningBalanceAccSetupGLId]
GO
