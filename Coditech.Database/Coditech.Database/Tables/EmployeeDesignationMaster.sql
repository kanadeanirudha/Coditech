CREATE TABLE [dbo].[EmployeeDesignationMaster](
	[EmployeeDesignationMasterId] [smallint] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NULL,
	[DesignationLevel] [int] NULL,
	[Grade] [int] NULL,
	[ShortCode] [varchar](10) NULL,
	[CollegeID] [int] NULL,
	[EmpDesigType] [varchar](7) NULL,
	[RelatedWith] [varchar](10) NULL,
	[IsActive] [bit] NULL DEFAULT 0,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PkEmployeeDesignationID] PRIMARY KEY CLUSTERED 
(
	[EmployeeDesignationMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


