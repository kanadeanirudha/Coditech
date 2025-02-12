CREATE TABLE [dbo].[GeneralNotification](
	[GeneralNotificationId] [bigint] IDENTITY(1,1) NOT NULL,
	[NotificationDetails] [nvarchar](max) NOT NULL,
	[FromDate] [datetime] NULL,
	[UptoDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GeneralNotificationMaster] PRIMARY KEY CLUSTERED 
(
	[GeneralNotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[GeneralNotification] ADD  CONSTRAINT [DF_GeneralNotificationMaster_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
