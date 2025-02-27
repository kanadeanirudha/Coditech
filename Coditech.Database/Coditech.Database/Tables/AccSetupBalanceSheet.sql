CREATE TABLE [dbo].[AccSetupBalanceSheet](
	[AccSetupBalanceSheetId] [int] IDENTITY(1,1) NOT NULL,
	[AccSetupBalanceSheetTypeId] [tinyint] NOT NULL,
	[AccBalancesheetCode] [nvarchar](20) NOT NULL,
	[AccBalancesheetHeadDesc] [nvarchar](50) NOT NULL,
	[CentreCode] [nvarchar](15) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsSystemGenerated] [bit] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKAccSetupBalanceSheetId] PRIMARY KEY CLUSTERED 
(
	[AccSetupBalanceSheetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UKAccBalancesheetCode] UNIQUE NONCLUSTERED 
(
	[AccBalancesheetCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AccSetupBalanceSheet] ADD  CONSTRAINT [DEF_AccSetupBalanceSheet_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO

ALTER TABLE [dbo].[AccSetupBalanceSheet] ADD  CONSTRAINT [DF__AccSetupB__IsSys__29EC2402]  DEFAULT ((0)) FOR [IsSystemGenerated]
GO

ALTER TABLE [dbo].[AccSetupBalanceSheet]  WITH CHECK ADD  CONSTRAINT [FKAccSetupBalanceSheetAccSetupBalanceSheetTypeId] FOREIGN KEY([AccSetupBalanceSheetTypeId])
REFERENCES [dbo].[AccSetupBalanceSheetType] ([AccSetupBalanceSheetTypeId])
GO

ALTER TABLE [dbo].[AccSetupBalanceSheet] CHECK CONSTRAINT [FKAccSetupBalanceSheetAccSetupBalanceSheetTypeId]
GO
