CREATE TABLE [dbo].[TicketMaster](
	[TicketMasterId] [bigint] IDENTITY(1,1) NOT NULL,
	[TicketNumber] [nvarchar](30) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[TicketStatusEnumId] [int] NOT NULL,
	[Phone] [varchar](20) NULL,
	[Location] [nvarchar](50) NULL,
	[TicketDepartmentEnumId] [int] NOT NULL,
	[Subject] [varchar](200) NOT NULL,
	[TicketPriorityEnumId] [int] NULL,
	[AddCc] [nvarchar](500) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_TicketMaster] PRIMARY KEY CLUSTERED 
(
	[TicketMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
