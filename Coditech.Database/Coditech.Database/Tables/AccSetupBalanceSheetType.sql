CREATE TABLE [dbo].[AccSetupBalanceSheetType](
	[AccSetupBalanceSheetTypeId] [tinyint] IDENTITY(1,1) NOT NULL,
	[AccBalsheetTypeCode] [nvarchar](20) NOT NULL,
	[AccBalsheetTypeDesc] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsSystemGenerated] [bit] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKAccSetupBalanceSheetTypeId] PRIMARY KEY CLUSTERED 
(
	[AccSetupBalanceSheetTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AccSetupBalanceSheetType] ADD  CONSTRAINT [DEF_AccSetupBalanceSheetType_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO

ALTER TABLE [dbo].[AccSetupBalanceSheetType] ADD  CONSTRAINT [DF__AccSetupB__IsSys__17CD73C7]  DEFAULT ((1)) FOR [IsSystemGenerated]
GO
