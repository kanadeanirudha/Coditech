CREATE TABLE [dbo].[GeneralTraineeAssociatedToTrainer](
	[GeneralTraineeAssociatedToTrainerId] [bigint] IDENTITY(1,1) NOT NULL,
	[EntityId] [bigint] NOT NULL,
	[UserType] [varchar](30) NOT NULL,
	[GeneralTrainerMasterId] [bigint] NOT NULL,
	[IsCurrentTrainer] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GeneralTraineeAssociatedToTrainer] PRIMARY KEY CLUSTERED 
(
	[GeneralTraineeAssociatedToTrainerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GeneralTraineeAssociatedToTrainer] ADD  CONSTRAINT [DF_GeneralTraineeAssociatedToTrainer_IsCurrentTrainer]  DEFAULT ((0)) FOR [IsCurrentTrainer]
GO
