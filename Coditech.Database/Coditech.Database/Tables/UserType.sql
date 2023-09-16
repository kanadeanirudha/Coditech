CREATE TABLE [dbo].[UserType](
	[UserTypeId] [smallint] IDENTITY(1,1) NOT NULL,
	[UserType] [char](1) NOT NULL,
	[UserDescription] [nvarchar](50) NOT NULL,
	[RelatedWith] [nvarchar](10) NULL,
	[IsCommon] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKUserType] PRIMARY KEY CLUSTERED 
(
	[UserTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

