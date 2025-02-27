CREATE TABLE [dbo].[TaskApprovalSetting](
	[TaskApprovalSettingId] [int] IDENTITY(1,1) NOT NULL,
	[CentreCode] [nvarchar](15) NOT NULL,
	[TaskMasterId] [smallint] NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[ApprovalSequenceNumber] [tinyint] NOT NULL,
	[IsFinalApproval] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_TaskApprovalSetting] PRIMARY KEY CLUSTERED 
(
	[TaskApprovalSettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
