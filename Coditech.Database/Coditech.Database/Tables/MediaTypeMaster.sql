CREATE TABLE [dbo].[MediaTypeMaster](
	[MediaTypeMasterId] [tinyint] IDENTITY(1,1) NOT NULL,
	[MediaType] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MediaTypeMaster] PRIMARY KEY CLUSTERED 
(
	[MediaTypeMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MediaTypeMaster] ADD  CONSTRAINT [DF_MediaTypeMaster_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
