CREATE TABLE [dbo].[UserModuleMaster](
	[UserModuleMasterId] [tinyint] IDENTITY(1,1) NOT NULL,
	[ModuleCode] [nvarchar](50) NOT NULL,
	[ModuleName] [nvarchar](60) NOT NULL,
	[ModuleInstalledFlag] [bit] NULL,
	[ModuleActiveFlag] [bit] NULL,
	[ModuleSeqNumber] [int] NULL,
	[ModuleRelatedWith] [nvarchar](50) NULL,
	[ModuleTooltip] [nvarchar](50) NULL,
	[ModuleIconName] [nvarchar](50) NULL,
	[ModuleIconPath] [nvarchar](100) NULL,
	[ModuleFormName] [nvarchar](50) NULL,
	[ModuleColorClass] [varchar](50) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_UserModuleMaster] PRIMARY KEY CLUSTERED 
(
	[UserModuleMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UKUserModuleMasterModuleCode] UNIQUE NONCLUSTERED 
(
	[ModuleCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
