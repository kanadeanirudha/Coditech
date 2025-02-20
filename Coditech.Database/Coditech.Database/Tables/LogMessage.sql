CREATE TABLE [dbo].[LogMessage](
	[LogMessageId] [bigint] IDENTITY(1,1) NOT NULL,
	[ErrorMessageType] [varchar](50) NOT NULL,
	[ExceptionMessage] [varchar](max) NOT NULL,
	[ComponentName] [varchar](200) NOT NULL,
	[TraceLevel] [varchar](20) NOT NULL,
	[Exception] [varchar](max) NULL,
	[MethodName] [varchar](200) NULL,
	[FileName] [varchar](200) NULL,
	[LineNumber] [int] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_LogMessage] PRIMARY KEY CLUSTERED 
(
	[LogMessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
