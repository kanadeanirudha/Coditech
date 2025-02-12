CREATE TABLE [dbo].[AccTransactionTypeMaster](
	[AccTransactionTypeMasterId] [int] IDENTITY(1,1) NOT NULL,
	[TransactionTypeCode] [varchar](10) NULL,
	[TransactionTypeName] [varchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKAccTransactionTypeMasterId] PRIMARY KEY CLUSTERED 
(
	[AccTransactionTypeMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__AccTrans__2BE3AC24568637EC] UNIQUE NONCLUSTERED 
(
	[TransactionTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__AccTrans__99D415A664693722] UNIQUE NONCLUSTERED 
(
	[TransactionTypeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AccTransactionTypeMaster] ADD  CONSTRAINT [DEF_AccTransactionTypeMaster_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
