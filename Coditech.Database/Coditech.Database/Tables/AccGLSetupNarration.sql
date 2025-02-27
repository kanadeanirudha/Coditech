CREATE TABLE [dbo].[AccGLSetupNarration](
	[AccGLSetupNarrationId] [int] IDENTITY(1,1) NOT NULL,
	[NarrationDescription] [nvarchar](500) NOT NULL,
	[NarrationType] [nvarchar](50) NOT NULL,
	[CentreCode] [varchar](15) NULL,
	[IsActive] [bit] NOT NULL,
	[IsSystemGenerated] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKAccGLSetupNarrationId] PRIMARY KEY CLUSTERED 
(
	[AccGLSetupNarrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AccGLSetupNarration] ADD  CONSTRAINT [DEF_AccGLSetupNarration_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
