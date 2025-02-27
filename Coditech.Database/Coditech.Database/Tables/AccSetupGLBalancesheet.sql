CREATE TABLE [dbo].[AccSetupGLBalancesheet](
	[AccSetupGLBalancesheetId] [int] IDENTITY(1,1) NOT NULL,
	[AccSetupBalanceSheetId] [int] NOT NULL,
	[AccSetupGLId] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKAccSetupGLBalancesheetId] PRIMARY KEY CLUSTERED 
(
	[AccSetupGLBalancesheetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AccSetupGLBalancesheet] ADD  CONSTRAINT [DEF_AccSetupGLBalancesheet_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO

ALTER TABLE [dbo].[AccSetupGLBalancesheet]  WITH CHECK ADD  CONSTRAINT [FKAccSetupGLBalancesheetAccSetupBalanceSheetId] FOREIGN KEY([AccSetupBalanceSheetId])
REFERENCES [dbo].[AccSetupBalanceSheet] ([AccSetupBalanceSheetId])
GO

ALTER TABLE [dbo].[AccSetupGLBalancesheet] CHECK CONSTRAINT [FKAccSetupGLBalancesheetAccSetupBalanceSheetId]
GO

ALTER TABLE [dbo].[AccSetupGLBalancesheet]  WITH CHECK ADD  CONSTRAINT [FKAccSetupGLBalancesheetAccSetupGLId] FOREIGN KEY([AccSetupGLId])
REFERENCES [dbo].[AccSetupGL] ([AccSetupGLId])
GO

ALTER TABLE [dbo].[AccSetupGLBalancesheet] CHECK CONSTRAINT [FKAccSetupGLBalancesheetAccSetupGLId]
GO


