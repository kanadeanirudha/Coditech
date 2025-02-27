CREATE TABLE [dbo].[TicketDetails](
	[TicketDetailsId] [bigint] IDENTITY(1,1) NOT NULL,
	[TicketMasterId] [bigint] NOT NULL,
	[Details] [nvarchar](max) NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_TicketDetails] PRIMARY KEY CLUSTERED 
(
	[TicketDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
