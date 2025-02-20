CREATE TABLE [dbo].[UserMaster](
	[UserMasterId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserType] [varchar](30) NOT NULL,
	[EntityId] [bigint] NOT NULL,
	[UserName] [varchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[EmailId] [nvarchar](100) NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[MiddleName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DeviceToken] [nvarchar](250) NULL,
	[LastModuleCode] [nvarchar](50) NULL,
	[IsPasswordChange] [bit] NOT NULL,
	[ResetPasswordToken] [varchar](50) NULL,
	[ResetPasswordTokenExpiredDate] [datetime] NULL,
	[IsAcceptedTermsAndConditions] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_UserMaster] PRIMARY KEY CLUSTERED 
(
	[UserMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserMaster] ADD  CONSTRAINT [DF_UserMaster_PersonId]  DEFAULT ((0)) FOR [EntityId]
GO

ALTER TABLE [dbo].[UserMaster] ADD  CONSTRAINT [DF_UserMaster_IsPasswordChange]  DEFAULT ((0)) FOR [IsPasswordChange]
GO

ALTER TABLE [dbo].[UserMaster] ADD  CONSTRAINT [DF_UserMaster_AcceptTermsAndConditions]  DEFAULT ((0)) FOR [IsAcceptedTermsAndConditions]
GO
