CREATE TABLE [dbo].[TaskApprovalTransaction](
	[TaskApprovalTransactionId] [bigint] IDENTITY(1,1) NOT NULL,
	[TaskApprovalSettingId] [int] NOT NULL,
	[TaskApprovalStatusEnumId] [int] NOT NULL,
	[IsCurrentStatus] [bit] NOT NULL,
	[TablePrimaryColumnId] [bigint] NOT NULL,
	[Comments] [nvarchar](1000) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_TaskApprovalTransaction] PRIMARY KEY CLUSTERED 
(
	[TaskApprovalTransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
