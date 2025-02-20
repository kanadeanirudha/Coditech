CREATE TABLE [dbo].[OrganisationCentrewiseUserNameRegistration](
	[OrganisationCentrewiseUserNameRegistrationId] [int] IDENTITY(1,1) NOT NULL,
	[CentreCode] [nvarchar](15) NOT NULL,
	[UserType] [varchar](20) NOT NULL,
	[UserNameBasedOn] [varchar](20) NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_OrganisationCentrewiseUserNameRegistration] PRIMARY KEY CLUSTERED 
(
	[OrganisationCentrewiseUserNameRegistrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
