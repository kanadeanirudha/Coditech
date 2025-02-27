CREATE TABLE [dbo].[GymWorkoutPlanDetails](
	[GymWorkoutPlanDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[GymWorkoutPlanId] [bigint] NOT NULL,
	[WorkoutName] [nvarchar](200) NOT NULL,
	[WorkoutWeek] [smallint] NOT NULL,
	[WorkoutDay] [tinyint] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GymWorkoutPlanDetails] PRIMARY KEY CLUSTERED 
(
	[GymWorkoutPlanDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
