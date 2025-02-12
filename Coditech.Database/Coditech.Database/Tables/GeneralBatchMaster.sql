CREATE TABLE [dbo].[GeneralBatchMaster](
	[GeneralBatchMasterId] [int] IDENTITY(1,1) NOT NULL,
	[CentreCode] [varchar](15) NOT NULL,
	[BatchName] [nvarchar](100) NOT NULL,
	[BatchTime] [time](7) NOT NULL,
	[BatchStartTime] [time](7) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GeneralBatchMaster] PRIMARY KEY CLUSTERED 
(
	[GeneralBatchMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO