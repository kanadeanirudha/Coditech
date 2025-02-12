CREATE TABLE [dbo].[GeneralPersonFollowUp](
	[GeneralPersonFollowUpId] [bigint] IDENTITY(1,1) NOT NULL,
	[FollowTakenFor] [varchar](200) NOT NULL,
	[FollowTakenId] [bigint] NOT NULL,
	[FollowupTypesEnumId] [int] NOT NULL,
	[FollowupComment] [nvarchar](500) NOT NULL,
	[IsSetReminder] [bit] NOT NULL,
	[ReminderDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GeneralPersonFollowUp] PRIMARY KEY CLUSTERED 
(
	[GeneralPersonFollowUpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GeneralPersonFollowUp] ADD  CONSTRAINT [DF_GeneralPersonFollowUp_IsSetReminder]  DEFAULT ((0)) FOR [IsSetReminder]
GO
