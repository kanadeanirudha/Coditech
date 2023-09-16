
CREATE  PROCEDURE [dbo].[Coditech_GetEmployeeDesignationList]
(   @WhereClause  VARCHAR(MAX),
    @Rows         INT          = 100,
    @PageNo       INT          = 1,
    @Order_BY     VARCHAR(100) = '',
	@RowsCount    INT OUT
    )
	--exec Coditech_GetEmployeeDesignationList @WhereClause=null,@Rows=25,@PageNo=1,@Order_BY='',@RowsCount= null

AS
   
     BEGIN
         SET NOCOUNT ON;
		 
         BEGIN TRY
             DECLARE @SQL NVARCHAR(MAX);
             DECLARE @TBL_EmployeeDesignationDetail TABLE
			 (
				 EmployeeDesignationMasterId			smallint,
				 Description		NVARCHAR(50),
				 ShortCode		NVARCHAR(10),
				 IsActive		BIT,
				 RowId				INT,
				 CountNo			INT
			 )

             SET @SQL = '
						;with Cte_filterEmployeeDesignation AS 
						(
							SELECT EmployeeDesignationMasterId,ShortCode,Description,IsActive,'
							+dbo.Fn_GetPagingRowId(@Order_By,'Description')+',Count(*)Over() CountNo 
							FROM EmployeeDesignationMaster where 1=1  '+[dbo].[Fn_GetFilterWhereClause](@WhereClause)+'
						)
						SELECT  EmployeeDesignationMasterId,ShortCode,Description,IsActive,RowId,CountNo
						FROM Cte_filterEmployeeDesignation
						'+dbo.Fn_GetPaginationWhereClause(@PageNo,@rows)
						
			 INSERT INTO @TBL_EmployeeDesignationDetail (EmployeeDesignationMasterId,ShortCode,Description,IsActive,RowId,CountNo )
			 EXEC(@SQL)
			 print @SQL
			 SET @RowsCount =ISNULL((SELECT TOP 1 CountNo  FROM @TBL_EmployeeDesignationDetail),0)
			 SELECT EmployeeDesignationMasterId,ShortCode,Description,IsActive
			 FROM @TBL_EmployeeDesignationDetail
			 	 
         END TRY
         BEGIN CATCH
		 select ERROR_MESSAGE()
          DECLARE @Status BIT ;
		  SET @Status = 0;
		  DECLARE @Error_procedure VARCHAR(1000)= ERROR_PROCEDURE(), @ErrorMessage NVARCHAR(MAX)= ERROR_MESSAGE(), @ErrorLine VARCHAR(100)= ERROR_LINE(), @ErrorCall NVARCHAR(MAX)= 'EXEC Coditech_GetEmployeeDesignationList @WhereClause = '+cast (@WhereClause AS VARCHAR(50))+',@Rows='+CAST(@Rows AS VARCHAR(50))+',@PageNo='+CAST(@PageNo AS VARCHAR(50))+',@Order_BY='+@Order_BY+',@RowsCount='+CAST(@RowsCount AS VARCHAR(50))+',@Status='+CAST(@Status AS VARCHAR(10));
              			 
          SELECT 0 AS ID,CAST(0 AS BIT) AS Status;                    
		  

          --EXEC Znode_InsertProcedureErrorLog
          --  @ProcedureName = 'Coditech_GetEmployeeDesignationList',
          --  @ErrorInProcedure = @Error_procedure,
          --  @ErrorMessage = @ErrorMessage,
          --  @ErrorLine = @ErrorLine,
          --  @ErrorCall = @ErrorCall;
         END CATCH;
     END;

GO

