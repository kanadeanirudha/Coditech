Create      PROCEDURE [dbo].[Coditech_GetNationalityList]
(   @WhereClause  VARCHAR(MAX),
    @Rows         INT          = 100,
    @PageNo       INT          = 1,
    @Order_BY     VARCHAR(100) = '',
	@RowsCount    INT OUT
    )
	--exec Coditech_GetNationalityList @WhereClause=null,@Rows=10,@PageNo=1,@Order_BY='',@RowsCount= null
AS
   
     BEGIN
         SET NOCOUNT ON;
		 
         BEGIN TRY
             DECLARE @SQL NVARCHAR(MAX);
             DECLARE @TBL_NationalityDetail TABLE
			 (
				 GeneralNationalityMasterId		int,
				 Description     	NVARCHAR(100),
				 DefaultFlag		BIT,
				 RowId				INT,
				 CountNo			INT
			 )

             SET @SQL = '
						;with Cte_filterNationality AS 
						(
							SELECT GeneralNationalityMasterId,Description,DefaultFlag,'
							+dbo.Fn_GetPagingRowId(@Order_By,'Description')+',Count(*)Over() CountNo 
							FROM GeneralNationalityMaster where 1=1  '+[dbo].[Fn_GetFilterWhereClause](@WhereClause)+'
						)
						SELECT  GeneralNationalityMasterId,Description,DefaultFlag,RowId,CountNo
						FROM Cte_filterNationality
						'+dbo.Fn_GetPaginationWhereClause(@PageNo,@rows)
						print @SQL
			 INSERT INTO @TBL_NationalityDetail (GeneralNationalityMasterId,Description,DefaultFlag,RowId,CountNo )
			 EXEC(@SQL)

			 SELECT GeneralNationalityMasterId,Description,DefaultFlag
			 FROM @TBL_NationalityDetail

			 SELECT TOP 1 CountNo  FROM @TBL_NationalityDetail

			 SET @RowsCount =ISNULL((SELECT TOP 1 CountNo  FROM @TBL_NationalityDetail),0)
				 select @RowsCount
         END TRY
         BEGIN CATCH
          DECLARE @Status BIT ;
		  SET @Status = 0;
		  DECLARE @Error_procedure VARCHAR(1000)= ERROR_PROCEDURE(), @ErrorMessage NVARCHAR(MAX)= ERROR_MESSAGE(), @ErrorLine VARCHAR(100)= ERROR_LINE(), @ErrorCall NVARCHAR(MAX)= 'EXEC Coditech_GetNationalityList @WhereClause = '+cast (@WhereClause AS VARCHAR(50))+',@Rows='+CAST(@Rows AS VARCHAR(50))+',@PageNo='+CAST(@PageNo AS VARCHAR(50))+',@Order_BY='+@Order_BY+',@RowsCount='+CAST(@RowsCount AS VARCHAR(50))+',@Status='+CAST(@Status AS VARCHAR(10));
              			 
          SELECT 0 AS ID,CAST(0 AS BIT) AS Status;                    
		  

          --EXEC Znode_InsertProcedureErrorLog
          --  @ProcedureName = 'Coditech_GetNationalityList',
          --  @ErrorInProcedure = @Error_procedure,
          --  @ErrorMessage = @ErrorMessage,
          --  @ErrorLine = @ErrorLine,
          --  @ErrorCall = @ErrorCall;
         END CATCH;
     END;
