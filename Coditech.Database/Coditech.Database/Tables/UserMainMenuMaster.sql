CREATE TABLE [dbo].[UserMainMenuMaster](
	[UserMainMenuMasterId] [smallint] IDENTITY(1,1) NOT NULL,
	[ModuleCode] [nvarchar](50) NOT NULL,
	[ParentMenuCode] [nvarchar](50) NULL,
	[MenuCode] [nvarchar](255) NOT NULL,
	[MenuName] [nvarchar](100) NOT NULL,
	[MenuInnerLevel] [smallint] NULL,
	[MenuDisplaySeqNo] [int] NULL,
	[MenuInstalledFlag] [bit] NULL,
	[ControllerName] [varchar](1000) NULL,
	[ActionName] [nvarchar](1000) NULL,
	[IsEnable] [bit] NULL,
	[DisableDate] [datetime] NULL,
	[RemarkAboutDisable] [nvarchar](100) NULL,
	[MenuToolTip] [nvarchar](50) NULL,
	[MenuIconName] [nvarchar](50) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKUserMainMenuMaster] PRIMARY KEY CLUSTERED 
(
	[UserMainMenuMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UKUserMainMenuMasterMenuCode] UNIQUE NONCLUSTERED 
(
	[MenuCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
