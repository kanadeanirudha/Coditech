CREATE TABLE [dbo].[DBTMDeviceMaster](
	[DBTMDeviceMasterId] [bigint] IDENTITY(1,1) NOT NULL,
	[DeviceName] [nvarchar](200) NOT NULL,
	[DeviceSerialCode] [varchar](100) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[ManufacturedBy] [nvarchar](200) NULL,
	[StatusEnumId] [int] NOT NULL,
	[IsMasterDevice] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[RegistrationDate] [date] NOT NULL,
	[WarrantyExpirationPeriodInMonth] [smallint] NOT NULL,
	[AdditionalFeatures] [nvarchar](500) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_DBTMDeviceMaster] PRIMARY KEY CLUSTERED 
(
	[DBTMDeviceMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DBTMDeviceMaster] ADD  CONSTRAINT [DF_DBTMDeviceMaster_IsMasterDevice]  DEFAULT ((0)) FOR [IsMasterDevice]
GO

ALTER TABLE [dbo].[DBTMDeviceMaster] ADD  CONSTRAINT [DF_DBTMDeviceMaster_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO