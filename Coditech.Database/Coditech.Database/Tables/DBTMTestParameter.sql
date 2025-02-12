CREATE TABLE [dbo].[DBTMTestParameter](
	[DBTMTestParameterId] [tinyint] IDENTITY(1,1) NOT NULL,
	[ParameterName] [nvarchar](200) NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_DBTMTestParameter] PRIMARY KEY CLUSTERED 
(
	[DBTMTestParameterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO