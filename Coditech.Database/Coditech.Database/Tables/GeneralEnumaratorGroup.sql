CREATE TABLE [dbo].[GeneralEnumaratorGroup](
	[GeneralEnumaratorGroupId] [int] IDENTITY(1,1) NOT NULL,
	[EnumGroupCode] [varchar](50) NOT NULL,
	[DisplayText] [nvarchar](50) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GeneralEnumaratorGroup] PRIMARY KEY CLUSTERED 
(
	[GeneralEnumaratorGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
