CREATE TABLE [dbo].[GeneralDepartmentMaster](
	[GeneralDepartmentMasterId] [smallint] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](60) NOT NULL,
	[DepartmentShortCode] [nvarchar](50) NOT NULL,
	[PrintShortDesc] [nvarchar](50) NULL,
	[WorkActivity] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKGeneralDepartmentMasterId] PRIMARY KEY CLUSTERED 
(
	[GeneralDepartmentMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
