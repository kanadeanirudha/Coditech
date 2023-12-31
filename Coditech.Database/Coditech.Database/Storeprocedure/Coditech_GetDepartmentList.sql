Create  PROCEDURE [dbo].[Coditech_GetDepartmentList]
(   @WhereClause  VARCHAR(MAX),
    @Rows         INT          = 100,
    @PageNo       INT          = 1,
    @Order_BY     VARCHAR(100) = '',
	@RowsCount    INT OUT
)
	--exec Coditech_GetDepartmentList @WhereClause=null,@Rows=10,@PageNo=1,@Order_BY='',@RowsCount= null
AS
   
     BEGIN
         SET NOCOUNT ON;
		 
         BEGIN TRY
             DECLARE @SQL NVARCHAR(MAX);
             DECLARE @TBL_DepartmentDetail TABLE
			 (
				 GeneralDepartmentMasterId		smallint,
				 DepartmentName		NVARCHAR(100),
				 DepartmentShortCode		NVARCHAR(100),
				 PrintShortDesc		NVARCHAR(100),
				 RowId				INT,
				 CountNo			INT
			 )

             SET @SQL = '
						;with Cte_filterDepartment AS 
						(
							SELECT GeneralDepartmentMasterId,DepartmentName,DepartmentShortCode,PrintShortDesc,'
							+dbo.Fn_GetPagingRowId(@Order_By,'DepartmentShortCode')+',Count(*)Over() CountNo 
							FROM GeneralDepartmentMaster where 1=1  '+[dbo].[Fn_GetFilterWhereClause](@WhereClause)+'
						)
						SELECT  GeneralDepartmentMasterId,DepartmentName,DepartmentShortCode,PrintShortDesc,RowId,CountNo
						FROM Cte_filterDepartment
						'+dbo.Fn_GetPaginationWhereClause(@PageNo,@rows)
						print @SQL
			 INSERT INTO @TBL_DepartmentDetail (GeneralDepartmentMasterId,DepartmentName,DepartmentShortCode,PrintShortDesc,RowId,CountNo )
			
			 EXEC(@SQL)
			
			 SET @RowsCount =ISNULL((SELECT TOP 1 CountNo  FROM @TBL_DepartmentDetail),0)
			 SELECT GeneralDepartmentMasterId,DepartmentName,DepartmentShortCode,PrintShortDesc
			 FROM @TBL_DepartmentDetail
			 	 sele
         END TRY
         BEGIN CATCH
          DECLARE @Status BIT ;
		  SET @Status = 0;
		  DECLARE @Error_procedure VARCHAR(1000)= ERROR_PROCEDURE(), @ErrorMessage NVARCHAR(MAX)= ERROR_MESSAGE(), @ErrorLine VARCHAR(100)= ERROR_LINE(), @ErrorCall NVARCHAR(MAX)= 'EXEC Coditech_GetDepartmentList @WhereClause = '+cast (@WhereClause AS VARCHAR(50))+',@Rows='+CAST(@Rows AS VARCHAR(50))+',@PageNo='+CAST(@PageNo AS VARCHAR(50))+',@Order_BY='+@Order_BY+',@RowsCount='+CAST(@RowsCount AS VARCHAR(50))+',@Status='+CAST(@Status AS VARCHAR(10));
              			 
          SELECT 0 AS ID,CAST(0 AS BIT) AS Status;                    
		  

          --EXEC Znode_InsertProcedureErrorLog
          --  @ProcedureName = 'Coditech_GetDepartmentList',
          --  @ErrorInProcedure = @Error_procedure,
          --  @ErrorMessage = @ErrorMessage,
          --  @ErrorLine = @ErrorLine,
          --  @ErrorCall = @ErrorCall;
         END CATCH;
     END;
