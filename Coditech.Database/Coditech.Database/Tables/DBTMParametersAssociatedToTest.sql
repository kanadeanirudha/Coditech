CREATE TABLE [dbo].[DBTMParametersAssociatedToTest](
	[DBTMParametersAssociatedToTestId] [int] IDENTITY(1,1) NOT NULL,
	[DBTMTestMasterId] [int] NOT NULL,
	[DBTMTestParameterId] [tinyint] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_DBTMParametersAssociatedToTest] PRIMARY KEY CLUSTERED 
(
	[DBTMParametersAssociatedToTestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO