CREATE TABLE [dbo].[GymWorkoutPlan](
	[GymWorkoutPlanId] [bigint] IDENTITY(1,1) NOT NULL,
	[CentreCode] [nvarchar](15) NOT NULL,
	[WorkoutPlanName] [nvarchar](200) NOT NULL,
	[Target] [nvarchar](50) NOT NULL,
	[NumberOfWeeks] [tinyint] NOT NULL,
	[NumberOfDaysPerWeek] [smallint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GymWorkoutPlan] PRIMARY KEY CLUSTERED 
(
	[GymWorkoutPlanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
