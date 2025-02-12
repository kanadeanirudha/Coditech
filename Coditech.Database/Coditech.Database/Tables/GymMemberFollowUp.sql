CREATE TABLE [dbo].[GymMemberFollowUp](
	[GymMemberFollowUpId] [bigint] IDENTITY(1,1) NOT NULL,
	[GymFollowupTypesEnumId] [int] NOT NULL,
	[GymMemberDetailId] [int] NOT NULL,
	[FollowupComment] [nvarchar](500) NOT NULL,
	[IsSetReminder] [bit] NOT NULL,
	[ReminderDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GymMemberFollowUp] PRIMARY KEY CLUSTERED 
(
	[GymMemberFollowUpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GymMemberFollowUp] ADD  CONSTRAINT [DF_GymMemberFollowUp_IsSetReminder]  DEFAULT ((0)) FOR [IsSetReminder]
GO
