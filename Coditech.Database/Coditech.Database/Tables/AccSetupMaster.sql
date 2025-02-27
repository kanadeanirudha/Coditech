CREATE TABLE [dbo].[AccSetupMaster](
	[AccSetupMasterId] [smallint] IDENTITY(1,1) NOT NULL,
	[FiscalYearday] [tinyint] NOT NULL,
	[FiscalYearMonth] [tinyint] NOT NULL,
	[CentreCode] [varchar](15) NULL,
	[IsActive] [bit] NOT NULL,
	[IsSystemGenerated] [bit] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKAccSetupMaster] PRIMARY KEY CLUSTERED 
(
	[AccSetupMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AccSetupMaster] ADD  CONSTRAINT [DEF_AccSetupMaster_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO

ALTER TABLE [dbo].[AccSetupMaster] ADD  CONSTRAINT [DF__AccSetupM__IsSys__19B5BC39]  DEFAULT ((1)) FOR [IsSystemGenerated]
GO
