CREATE TABLE [dbo].[DBTMDeviceDataDetails](
	[DBTMDeviceDataDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[DBTMDeviceDataId] [bigint] NOT NULL,
	[Weight] [decimal](3, 3) NOT NULL,
	[Height] [decimal](3, 3) NOT NULL,
	[Time] [bigint] NOT NULL,
	[Distance] [decimal](10, 3) NOT NULL,
	[Force] [decimal](10, 3) NOT NULL,
	[Acceleration] [decimal](10, 3) NOT NULL,
	[Angle] [decimal](10, 3) NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_DBTMDeviceDataDetails] PRIMARY KEY CLUSTERED 
(
	[DBTMDeviceDataDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO