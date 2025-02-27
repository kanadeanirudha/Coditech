CREATE TABLE [dbo].[AccSetupGLType](
	[AccSetupGLTypeId] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Code] [nvarchar](20) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsSystemGenerated] [bit] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKAccSetupGLTypeId] PRIMARY KEY CLUSTERED 
(
	[AccSetupGLTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AccSetupGLType] ADD  CONSTRAINT [DF_AccSetupGLType_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO

ALTER TABLE [dbo].[AccSetupGLType] ADD  CONSTRAINT [DF__AccSetupG__IsSys__15E52B55]  DEFAULT ((1)) FOR [IsSystemGenerated]
GO
