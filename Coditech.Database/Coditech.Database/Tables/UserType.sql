CREATE TABLE [dbo].[UserType](
	[UserTypeId] [smallint] IDENTITY(1,1) NOT NULL,
	[UserTypeCode] [varchar](30) NOT NULL,
	[UserDescription] [nvarchar](50) NOT NULL,
	[RelatedWith] [nvarchar](50) NULL,
	[IsCommon] [bit] NOT NULL,
	[IsLoginRequired] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[UserTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserType] ADD  CONSTRAINT [DF_UserType_IsLoginRequired]  DEFAULT ((0)) FOR [IsLoginRequired]
GO
