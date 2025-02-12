CREATE TABLE [dbo].[CoditechApplicationSetting](
	[CoditechApplicationSettingId] [smallint] IDENTITY(1,1) NOT NULL,
	[ApplicationCode] [varchar](100) NOT NULL,
	[ApplicationValue1] [varchar](max) NOT NULL,
	[ApplicationValue2] [varchar](500) NULL,
	[ApplicationValue3] [varchar](500) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_CoditechApplicationSetting] PRIMARY KEY CLUSTERED 
(
	[CoditechApplicationSettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
