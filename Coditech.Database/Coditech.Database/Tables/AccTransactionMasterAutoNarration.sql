CREATE TABLE [dbo].[AccTransactionMasterAutoNarration](
	[AccTransactionMasterAutoNarrationId] [bigint] IDENTITY(1,1) NOT NULL,
	[AccGLTransactionId] [bigint] NOT NULL,
	[AutoNarration] [nvarchar](2000) NOT NULL,
	[ChequeDetails] [nvarchar](2000) NOT NULL,
	[IsLast] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [AccTransactionMasterAutoNarrationId] PRIMARY KEY CLUSTERED 
(
	[AccTransactionMasterAutoNarrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AccTransactionMasterAutoNarration] ADD  CONSTRAINT [DF_AccTransactionMasterAutoNarration_IsLast]  DEFAULT ((0)) FOR [IsLast]
GO

ALTER TABLE [dbo].[AccTransactionMasterAutoNarration]  WITH CHECK ADD  CONSTRAINT [AccTransactionMasterAutoNarrationAccGLTransactionId] FOREIGN KEY([AccGLTransactionId])
REFERENCES [dbo].[AccGLTransaction] ([AccGLTransactionId])
GO

ALTER TABLE [dbo].[AccTransactionMasterAutoNarration] CHECK CONSTRAINT [AccTransactionMasterAutoNarrationAccGLTransactionId]
GO
