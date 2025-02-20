CREATE TABLE [dbo].[DBTMDeviceData](
	[DBTMDeviceDataId] [bigint] IDENTITY(1,1) NOT NULL,
	[DeviceSerialCode] [varchar](100) NOT NULL,
	[PersonCode] [nvarchar](200) NOT NULL,
	[TestCode] [nvarchar](50) NOT NULL,
	[Comments] [nvarchar](200) NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_DBTMDeviceData] PRIMARY KEY CLUSTERED 
(
	[DBTMDeviceDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO