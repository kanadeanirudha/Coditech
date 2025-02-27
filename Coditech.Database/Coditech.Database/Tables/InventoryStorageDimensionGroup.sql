CREATE TABLE [dbo].[InventoryStorageDimensionGroup](
	[InventoryStorageDimensionGroupId] [int] IDENTITY(1,1) NOT NULL,
	[StorageDimensionGroupName] [varchar](100) NOT NULL,
	[StorageDimensionGroupCode] [varchar](100) NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[WarehouseManagementProcesses] [bit] NOT NULL,
	[Mandatory] [bit] NOT NULL,
	[PrimaryStocking] [bit] NOT NULL,
 CONSTRAINT [PK_InventoryStorageDimensionGroup_1] PRIMARY KEY CLUSTERED 
(
	[InventoryStorageDimensionGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[InventoryStorageDimensionGroup] ADD  DEFAULT ((0)) FOR [WarehouseManagementProcesses]
GO

ALTER TABLE [dbo].[InventoryStorageDimensionGroup] ADD  DEFAULT ((0)) FOR [Mandatory]
GO

ALTER TABLE [dbo].[InventoryStorageDimensionGroup] ADD  DEFAULT ((0)) FOR [PrimaryStocking]
GO
