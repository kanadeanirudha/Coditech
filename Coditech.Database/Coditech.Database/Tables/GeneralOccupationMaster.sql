CREATE TABLE [dbo].[GeneralOccupationMaster](
	[GeneralOccupationMasterId] [smallint] IDENTITY(1,1) NOT NULL,
	[OccupationName] [nvarchar](60) NOT NULL,
	[DisplayOrder] [smallint] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GeneralOccupationMaster] PRIMARY KEY CLUSTERED 
(
	[GeneralOccupationMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
