CREATE TABLE [dbo].[GeneralRunningNumbers](
	[GeneralRunningNumberId] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[KeyFieldEnumId] [int] NOT NULL,
	[GeneralFinancialYearId] [int] NULL,
	[CentreCode] [nvarchar](50) NOT NULL,
	[DisplayFormat] [varchar](200) NOT NULL,
	[IsSequenceReset] [bit] NOT NULL,
	[Separator] [varchar](10) NOT NULL,
	[Prefix] [varchar](20) NOT NULL,
	[IsBackDated] [bit] NOT NULL,
	[BackDatedPrefix] [varchar](20) NULL,
	[StartSequence] [bigint] NOT NULL,
	[CurrentSequnce] [bigint] NOT NULL,
	[IsRowLock] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GeneralRunningNumbers] PRIMARY KEY CLUSTERED 
(
	[GeneralRunningNumberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GeneralRunningNumbers] ADD  CONSTRAINT [DF_GeneralRunningNumbers_IsSequenceReset]  DEFAULT ((0)) FOR [IsSequenceReset]
GO

ALTER TABLE [dbo].[GeneralRunningNumbers] ADD  CONSTRAINT [DF_GeneralRunningNumbers_SeparatorChar]  DEFAULT ('-') FOR [Separator]
GO

ALTER TABLE [dbo].[GeneralRunningNumbers] ADD  CONSTRAINT [DF_GeneralRunningNumbers_IsBackDated]  DEFAULT ((0)) FOR [IsBackDated]
GO

ALTER TABLE [dbo].[GeneralRunningNumbers] ADD  CONSTRAINT [DF_GeneralRunningNumbers_StartSequence]  DEFAULT ((1)) FOR [StartSequence]
GO

ALTER TABLE [dbo].[GeneralRunningNumbers] ADD  CONSTRAINT [DF_GeneralRunningNumbers_CurrentSequnce]  DEFAULT ((0)) FOR [CurrentSequnce]
GO

ALTER TABLE [dbo].[GeneralRunningNumbers] ADD  CONSTRAINT [DF_GeneralRunningNumbers_IsRowLock]  DEFAULT ((0)) FOR [IsRowLock]
GO

ALTER TABLE [dbo].[GeneralRunningNumbers] ADD  CONSTRAINT [DF_GeneralRunningNumbers_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
