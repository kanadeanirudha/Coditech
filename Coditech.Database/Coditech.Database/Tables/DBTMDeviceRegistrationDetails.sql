CREATE TABLE [dbo].[DBTMDeviceRegistrationDetails](
	[DBTMDeviceRegistrationDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[DBTMDeviceMasterId] [bigint] NOT NULL,
	[UserType] [varchar](30) NOT NULL,
	[EntityId] [bigint] NOT NULL,
	[PurchaseDate] [datetime] NOT NULL,
	[WarrantyExpirationDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_DBTMDeviceRegistrationDetails] PRIMARY KEY CLUSTERED 
(
	[DBTMDeviceRegistrationDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO