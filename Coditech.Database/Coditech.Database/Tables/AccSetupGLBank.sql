CREATE TABLE [dbo].[AccSetupGLBank](
	[AccSetupGLBankId] [int] IDENTITY(1,1) NOT NULL,
	[AccSetupBalanceSheetId] [int] NOT NULL,
	[AccSetupGLId] [int] NOT NULL,
	[BankAccountNumber] [varchar](20) NULL,
	[BankAccountName] [nvarchar](120) NULL,
	[BankBranchName] [nvarchar](120) NULL,
	[BankLimitAmount] [money] NULL,
	[RateOfInterest] [money] NULL,
	[InterestMode] [varchar](10) NULL,
	[OpenDatetime] [datetime] NULL,
	[DueDatetime] [datetime] NULL,
	[InterestTypeEnumId] [int] NOT NULL,
	[IFSCCode] [varchar](20) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKAccSetupGLBankId] PRIMARY KEY CLUSTERED 
(
	[AccSetupGLBankId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AccSetupGLBank] ADD  CONSTRAINT [DEF_AccSetupGLBank_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO

ALTER TABLE [dbo].[AccSetupGLBank]  WITH CHECK ADD  CONSTRAINT [FKAccSetupGLBankAccSetupBalanceSheetId] FOREIGN KEY([AccSetupBalanceSheetId])
REFERENCES [dbo].[AccSetupBalanceSheet] ([AccSetupBalanceSheetId])
GO

ALTER TABLE [dbo].[AccSetupGLBank] CHECK CONSTRAINT [FKAccSetupGLBankAccSetupBalanceSheetId]
GO

ALTER TABLE [dbo].[AccSetupGLBank]  WITH CHECK ADD  CONSTRAINT [FKAccSetupGLBankAccSetupGLId] FOREIGN KEY([AccSetupGLId])
REFERENCES [dbo].[AccSetupGL] ([AccSetupGLId])
GO

ALTER TABLE [dbo].[AccSetupGLBank] CHECK CONSTRAINT [FKAccSetupGLBankAccSetupGLId]
GO
