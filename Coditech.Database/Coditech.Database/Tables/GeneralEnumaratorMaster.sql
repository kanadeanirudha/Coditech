CREATE TABLE [dbo].[GeneralEnumaratorMaster](
	[GeneralEnumaratorId] [int] IDENTITY(1,1) NOT NULL,
	[GeneralEnumaratorGroupId] [int] NOT NULL,
	[EnumName] [nvarchar](50) NOT NULL,
	[EnumDisplayText] [nvarchar](250) NOT NULL,
	[RelatedWith] [varchar](50) NULL,
	[EnumValue] [smallint] NULL,
	[SequenceNumber] [smallint] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GeneralEnumaratorMaster] PRIMARY KEY CLUSTERED 
(
	[GeneralEnumaratorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GeneralEnumaratorMaster] ADD  CONSTRAINT [DF_GeneralEnumarator_IsDefault]  DEFAULT ((0)) FOR [IsDefault]
GO

ALTER TABLE [dbo].[GeneralEnumaratorMaster] ADD  CONSTRAINT [DF_GeneralEnumaratorMaster_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[GeneralEnumaratorMaster]  WITH CHECK ADD  CONSTRAINT [FK_GeneralEnumaratorMaster_GeneralEnumaratorGroup] FOREIGN KEY([GeneralEnumaratorGroupId])
REFERENCES [dbo].[GeneralEnumaratorGroup] ([GeneralEnumaratorGroupId])
GO

ALTER TABLE [dbo].[GeneralEnumaratorMaster] CHECK CONSTRAINT [FK_GeneralEnumaratorMaster_GeneralEnumaratorGroup]
GO
