CREATE TABLE [dbo].[UserMainMenuMaster](
	[UserMainMenuMasterId] [smallint] IDENTITY(1,1) NOT NULL,
	[ModuleCode] [nvarchar](50) NOT NULL,
	[MenuCode] [nvarchar](255) NOT NULL,
	[MenuName] [nvarchar](100) NOT NULL,
	[MenuInnerLevel] [smallint] NULL,
	[ParentMenuId] [smallint] NULL,
	[MenuDisplaySeqNo] [int] NULL,
	[MenuVerNo] [nvarchar](60) NULL,
	[MenuInstalledFlag] [bit] NULL,
	[ControllerName] [varchar](1000) NULL,
	[ActionName] [nvarchar](1000) NULL,
	[IsEnable] [bit] NOT NULL DEFAULT 1,
	[DisableDate] [datetime] NULL,
	[RemarkAboutDisable] [nvarchar](100) NULL,
	[MenuToolTip] [nvarchar](50) NULL,
	[ParentMenuName] [nvarchar](100) NULL,
	[ParentMenuCode] [nvarchar](50) NULL,
	[MenuIconName] [nvarchar](50) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKUserMainMenuMaster] PRIMARY KEY CLUSTERED 
(
	[UserMainMenuMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UKUserMainMenuMasterMenuCode] UNIQUE NONCLUSTERED 
(
	[MenuCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UKUserMainMenuMasterMenuName] UNIQUE NONCLUSTERED 
(
	[MenuName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


