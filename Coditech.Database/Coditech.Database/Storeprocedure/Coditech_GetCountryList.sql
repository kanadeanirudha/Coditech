Create     PROCEDURE [dbo].[Coditech_GetCountryList]
(   @WhereClause  VARCHAR(MAX),
    @Rows         INT          = 100,
    @PageNo       INT          = 1,
    @Order_BY     VARCHAR(100) = '',
	@RowsCount    INT OUT
    )
	--exec Coditech_GetCountryList @WhereClause=null,@Rows=25,@PageNo=1,@Order_BY='',@RowsCount= null
AS
   
     BEGIN
         SET NOCOUNT ON;
		 
         BEGIN TRY
             DECLARE @SQL NVARCHAR(MAX);
             DECLARE @TBL_CountryDetail TABLE
			 (
				 GeneralCountryMasterId			smallint,
				 CountryName		NVARCHAR(60),
				 CountryCode		NVARCHAR(50),
				 DefaultFlag		BIT,
				 RowId				INT,
				 CountNo			INT
			 )

             SET @SQL = '
						;with Cte_filterCountry AS 
						(
							SELECT GeneralCountryMasterId,CountryCode,CountryName,DefaultFlag,'
							+dbo.Fn_GetPagingRowId(@Order_By,'CountryName')+',Count(*)Over() CountNo 
							FROM GeneralCountryMaster where 1=1  '+[dbo].[Fn_GetFilterWhereClause](@WhereClause)+'
						)
						SELECT  GeneralCountryMasterId,CountryCode,CountryName,DefaultFlag,RowId,CountNo
						FROM Cte_filterCountry
						'+dbo.Fn_GetPaginationWhereClause(@PageNo,@rows)
						print @SQL
			 INSERT INTO @TBL_CountryDetail (GeneralCountryMasterId,CountryCode,CountryName,DefaultFlag,RowId,CountNo )
			 EXEC(@SQL)

			 SET @RowsCount =ISNULL((SELECT TOP 1 CountNo  FROM @TBL_CountryDetail),0)
			 SELECT GeneralCountryMasterId,CountryCode,DefaultFlag,CountryName
			 FROM @TBL_CountryDetail
			 	 
         END TRY
         BEGIN CATCH
          DECLARE @Status BIT ;
		  SET @Status = 0;
		  DECLARE @Error_procedure VARCHAR(1000)= ERROR_PROCEDURE(), @ErrorMessage NVARCHAR(MAX)= ERROR_MESSAGE(), @ErrorLine VARCHAR(100)= ERROR_LINE(), @ErrorCall NVARCHAR(MAX)= 'EXEC Coditech_GetCountryList @WhereClause = '+cast (@WhereClause AS VARCHAR(50))+',@Rows='+CAST(@Rows AS VARCHAR(50))+',@PageNo='+CAST(@PageNo AS VARCHAR(50))+',@Order_BY='+@Order_BY+',@RowsCount='+CAST(@RowsCount AS VARCHAR(50))+',@Status='+CAST(@Status AS VARCHAR(10));
              			 
          SELECT 0 AS ID,CAST(0 AS BIT) AS Status;                    
		  

          --EXEC Znode_InsertProcedureErrorLog
          --  @ProcedureName = 'Coditech_GetCountryList',
          --  @ErrorInProcedure = @Error_procedure,
          --  @ErrorMessage = @ErrorMessage,
          --  @ErrorLine = @ErrorLine,
          --  @ErrorCall = @ErrorCall;
         END CATCH;
     END;
