CREATE TABLE [dbo].[GeneralBatchUser](
	[GeneralBatchUserId] [bigint] IDENTITY(1,1) NOT NULL,
	[GeneralBatchMasterId] [int] NOT NULL,
	[EntityId] [bigint] NOT NULL,
	[UserType] [varchar](15) NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GeneralBatchUser] PRIMARY KEY CLUSTERED 
(
	[GeneralBatchUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO