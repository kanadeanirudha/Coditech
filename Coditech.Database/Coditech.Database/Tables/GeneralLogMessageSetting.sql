CREATE TABLE [dbo].[GeneralLogMessageSetting](
	[GeneralLogMessageSettingId] [tinyint] IDENTITY(1,1) NOT NULL,
	[TraceLevel] [varchar](20) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_GeneralLogMessageSetting] PRIMARY KEY CLUSTERED 
(
	[GeneralLogMessageSettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
