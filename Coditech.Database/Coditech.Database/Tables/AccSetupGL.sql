CREATE TABLE [dbo].[AccSetupGL](
	[AccSetupGLId] [int] IDENTITY(1,1) NOT NULL,
	[AccSetupCategoryId] [smallint] NOT NULL,
	[AltSetupGLId] [int] NULL,
	[ParentAccSetupGLId] [int] NULL,
	[AccSetupGLTypeId] [smallint] NULL,
	[AccSetupBalanceSheetId] [int] NOT NULL,
	[GLName] [nvarchar](50) NOT NULL,
	[GLCode] [varchar](15) NOT NULL,
	[IsGroup] [bit] NOT NULL,
	[UserType] [varchar](30) NULL,
	[DebitCreditEnum] [int] NOT NULL,
	[IsOpBalRequired] [bit] NOT NULL,
	[PrintingSequence] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[IsSystemGenerated] [bit] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKAccSetupGLId] PRIMARY KEY CLUSTERED 
(
	[AccSetupGLId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AccSetupGL] ADD  CONSTRAINT [DEF_AccSetupGL_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO

ALTER TABLE [dbo].[AccSetupGL] ADD  CONSTRAINT [DF__AccSetupG__IsSys__16D94F8E]  DEFAULT ((1)) FOR [IsSystemGenerated]
GO

ALTER TABLE [dbo].[AccSetupGL]  WITH CHECK ADD  CONSTRAINT [FKAccSetupGLAccSetupBalanceSheetId] FOREIGN KEY([AccSetupBalanceSheetId])
REFERENCES [dbo].[AccSetupBalanceSheet] ([AccSetupBalanceSheetId])
GO

ALTER TABLE [dbo].[AccSetupGL] CHECK CONSTRAINT [FKAccSetupGLAccSetupBalanceSheetId]
GO

ALTER TABLE [dbo].[AccSetupGL]  WITH CHECK ADD  CONSTRAINT [FKAccSetupGLAccSetupCategoryId] FOREIGN KEY([AccSetupCategoryId])
REFERENCES [dbo].[AccSetupCategory] ([AccSetupCategoryId])
GO

ALTER TABLE [dbo].[AccSetupGL] CHECK CONSTRAINT [FKAccSetupGLAccSetupCategoryId]
GO

ALTER TABLE [dbo].[AccSetupGL]  WITH CHECK ADD  CONSTRAINT [FKAccSetupGLAccSetupGLTypeId] FOREIGN KEY([AccSetupGLTypeId])
REFERENCES [dbo].[AccSetupGLType] ([AccSetupGLTypeId])
GO

ALTER TABLE [dbo].[AccSetupGL] CHECK CONSTRAINT [FKAccSetupGLAccSetupGLTypeId]
GO

ALTER TABLE [dbo].[AccSetupGL]  WITH CHECK ADD  CONSTRAINT [FKAccSetupGLAltSetupGLId] FOREIGN KEY([AltSetupGLId])
REFERENCES [dbo].[AccSetupGL] ([AccSetupGLId])
GO

ALTER TABLE [dbo].[AccSetupGL] CHECK CONSTRAINT [FKAccSetupGLAltSetupGLId]
GO

ALTER TABLE [dbo].[AccSetupGL]  WITH CHECK ADD  CONSTRAINT [FKAccSetupGLParentAccSetupGLId] FOREIGN KEY([ParentAccSetupGLId])
REFERENCES [dbo].[AccSetupGL] ([AccSetupGLId])
GO

ALTER TABLE [dbo].[AccSetupGL] CHECK CONSTRAINT [FKAccSetupGLParentAccSetupGLId]
GO
