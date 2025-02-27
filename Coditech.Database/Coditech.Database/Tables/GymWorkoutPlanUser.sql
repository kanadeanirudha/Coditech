CREATE TABLE [dbo].[GymWorkoutPlanUser](
	[GymWorkoutPlanUserId] [bigint] IDENTITY(1,1) NOT NULL,
	[GymMemberDetailId] [int] NOT NULL,
	[GymWorkoutPlanId] [bigint] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GymWorkoutPlanUser] PRIMARY KEY CLUSTERED 
(
	[GymWorkoutPlanUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
