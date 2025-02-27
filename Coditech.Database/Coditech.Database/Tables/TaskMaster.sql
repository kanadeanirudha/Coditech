CREATE TABLE [dbo].[TaskMaster](
	[TaskMasterId] [smallint] IDENTITY(1,1) NOT NULL,
	[TaskCode] [varchar](100) NOT NULL,
	[TableName] [varchar](200) NOT NULL,
	[TaskDescription] [varchar](200) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_TaskMaster] PRIMARY KEY CLUSTERED 
(
	[TaskMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
