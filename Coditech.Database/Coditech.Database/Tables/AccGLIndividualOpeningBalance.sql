CREATE TABLE [dbo].[AccGLIndividualOpeningBalance](
	[AccGLIndividualOpeningBalanceId] [int] IDENTITY(1,1) NOT NULL,
	[GeneralFinancialYearId] [smallint] NOT NULL,
	[AccSetupGLId] [int] NOT NULL,
	[AccSetupBalanceSheetId] [int] NOT NULL,
	[UserType] [varchar](30) NULL,
	[PersonId] [bigint] NOT NULL,
	[OpeningDatetime] [datetime] NOT NULL,
	[OpeningBalance] [money] NOT NULL,
	[TotalDebitAmount] [money] NOT NULL,
	[TotalCreditAmount] [money] NOT NULL,
	[ClosingBalance] [money] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedtBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [AccGLIndividualOpeningBalanceId] PRIMARY KEY CLUSTERED 
(
	[AccGLIndividualOpeningBalanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AccGLIndividualOpeningBalance] ADD  CONSTRAINT [DF_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO

ALTER TABLE [dbo].[AccGLIndividualOpeningBalance]  WITH CHECK ADD  CONSTRAINT [FK_AccGLIndividualOpeningBalanceGeneralFinancialYearId] FOREIGN KEY([GeneralFinancialYearId])
REFERENCES [dbo].[GeneralFinancialYear] ([GeneralFinancialYearId])
GO

ALTER TABLE [dbo].[AccGLIndividualOpeningBalance] CHECK CONSTRAINT [FK_AccGLIndividualOpeningBalanceGeneralFinancialYearId]
GO

ALTER TABLE [dbo].[AccGLIndividualOpeningBalance]  WITH CHECK ADD  CONSTRAINT [FKAccGLIndividualOpeningBalanceAccSetupBalanceSheetId] FOREIGN KEY([AccSetupBalanceSheetId])
REFERENCES [dbo].[AccSetupBalanceSheet] ([AccSetupBalanceSheetId])
GO

ALTER TABLE [dbo].[AccGLIndividualOpeningBalance] CHECK CONSTRAINT [FKAccGLIndividualOpeningBalanceAccSetupBalanceSheetId]
GO

ALTER TABLE [dbo].[AccGLIndividualOpeningBalance]  WITH CHECK ADD  CONSTRAINT [FKAccGLIndividualOpeningBalanceAccSetupGLId] FOREIGN KEY([AccSetupGLId])
REFERENCES [dbo].[AccSetupGL] ([AccSetupGLId])
GO

ALTER TABLE [dbo].[AccGLIndividualOpeningBalance] CHECK CONSTRAINT [FKAccGLIndividualOpeningBalanceAccSetupGLId]
GO
