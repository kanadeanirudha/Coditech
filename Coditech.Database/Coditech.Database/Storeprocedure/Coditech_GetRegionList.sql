create      PROCEDURE [dbo].[Coditech_GetRegionList]
(   @WhereClause  VARCHAR(MAX),
    @Rows         INT          = 100,
    @PageNo       INT          = 1,
    @Order_BY     VARCHAR(100) = '',
	@RowsCount    INT OUT
    )
	--exec Coditech_GetRegionList @WhereClause=null,@Rows=10,@PageNo=1,@Order_BY='',@RowsCount= null
AS
   
     BEGIN
         SET NOCOUNT ON;
		 
         BEGIN TRY
             DECLARE @SQL NVARCHAR(MAX);
             DECLARE @TBL_RegionDetail TABLE
			 (
				 RegionId			smallint,
				 RegionName			NVARCHAR(60),
				 ShortName			NVARCHAR(50),
				 CountryCode		NVARCHAR(60),
				 TinNumber			smallint,
				 DefaultFlag		BIT,
				 RowId				INT,
				 CountNo			INT
			 )

             SET @SQL = '
						;with Cte_filterRegion AS 
						(
							SELECT ID RegionId,RegionName,ShortName, CountryCode,TinNumber,DefaultFlag,'
							+dbo.Fn_GetPagingRowId(@Order_By,'RegionName')+',Count(*)Over() CountNo 
							FROM GeneralRegionMaster where 1=1  '+[dbo].[Fn_GetFilterWhereClause](@WhereClause)+'
						)
						SELECT  RegionId,RegionName,ShortName,CountryCode,TinNumber,DefaultFlag,RowId,CountNo
						FROM Cte_filterRegion
						'+dbo.Fn_GetPaginationWhereClause(@PageNo,@rows)
						print @SQL
			 INSERT INTO @TBL_RegionDetail (RegionId,RegionName,ShortName,CountryCode,TinNumber,DefaultFlag,RowId,CountNo )
			 EXEC(@SQL)

			 SELECT RegionId,RegionName,ShortName,CountryCode,TinNumber,DefaultFlag
			 FROM @TBL_RegionDetail

			 SELECT TOP 1 CountNo  FROM @TBL_RegionDetail

			 SET @RowsCount =ISNULL((SELECT TOP 1 CountNo  FROM @TBL_RegionDetail),0)
				 select @RowsCount
         END TRY
         BEGIN CATCH
          DECLARE @Status BIT ;
		  SET @Status = 0;
		  DECLARE @Error_procedure VARCHAR(1000)= ERROR_PROCEDURE(), @ErrorMessage NVARCHAR(MAX)= ERROR_MESSAGE(), @ErrorLine VARCHAR(100)= ERROR_LINE(), @ErrorCall NVARCHAR(MAX)= 'EXEC Coditech_GetRegionList @WhereClause = '+cast (@WhereClause AS VARCHAR(50))+',@Rows='+CAST(@Rows AS VARCHAR(50))+',@PageNo='+CAST(@PageNo AS VARCHAR(50))+',@Order_BY='+@Order_BY+',@RowsCount='+CAST(@RowsCount AS VARCHAR(50))+',@Status='+CAST(@Status AS VARCHAR(10));
              			 
          SELECT 0 AS ID,CAST(0 AS BIT) AS Status;                    
		  

          --EXEC Znode_InsertProcedureErrorLog
          --  @ProcedureName = 'Coditech_GetRegionList',
          --  @ErrorInProcedure = @Error_procedure,
          --  @ErrorMessage = @ErrorMessage,
          --  @ErrorLine = @ErrorLine,
          --  @ErrorCall = @ErrorCall;
         END CATCH;
     END;
GO


