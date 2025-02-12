CREATE TABLE [dbo].[DBTMTraineeAssignment](
	[DBTMTraineeAssignmentId] [bigint] IDENTITY(1,1) NOT NULL,
	[GeneralTrainerMasterId] [bigint] NOT NULL,
	[DBTMTraineeDetailId] [bigint] NOT NULL,
	[DBTMTestMasterId] [int] NOT NULL,
	[AssignmentDate] [date] NOT NULL,
	[AssignmentTime] [time](7) NULL,
	[DBTMTestStatusEnumId] [int] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_DBTMTraineeAssignment] PRIMARY KEY CLUSTERED 
(
	[DBTMTraineeAssignmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO