CREATE   PROCEDURE [dbo].[Coditech_DeleteCountry]
(
       @CountryId VARCHAR(max) ,
       @Status      BIT OUT
)
AS
/*
Summary: This Procedure is used to delete Country details
Unit Testing:
EXEC Coditech_DeleteCountry @CountryId='1'
*/
     BEGIN
         BEGIN TRAN;
         BEGIN TRY
             SET NOCOUNT ON;
             DECLARE @Country TABLE (
                                      CountryId INT
                                      );
             INSERT INTO @Country SELECT item FROM dbo.Split ( @CountryId , ',');
           
             DECLARE @DeleteCountryId TABLE (
                                              CountryId INT
                                              );
             INSERT INTO @DeleteCountryId
                    SELECT a.GeneralCountryMasterId
                    FROM [dbo].GeneralCountryMaster AS a INNER JOIN @Country AS b ON ( a.GeneralCountryMasterId = b.CountryId );
           
             DELETE FROM GeneralCountryMaster
             WHERE EXISTS ( SELECT TOP 1 1
                            FROM @DeleteCountryId AS b
                            WHERE b.CountryId = GeneralCountryMaster.GeneralCountryMasterId
                          );
           
             SET @Status = 1;
             IF ( SELECT COUNT(1)
                  FROM @DeleteCountryId
                ) = ( SELECT COUNT(1)
                      FROM @Country
                    )
                 BEGIN
                     SELECT 1 AS ID , CAST(1 AS BIT) AS Status;
                 END;
             ELSE
                 BEGIN
                     SELECT 0 AS ID , CAST(0 AS BIT) AS Status;
                 END;
             COMMIT TRAN;
         END TRY
         BEGIN CATCH
             --DECLARE @Error_procedure VARCHAR(1000)= ERROR_PROCEDURE(), @ErrorMessage NVARCHAR(MAX)= ERROR_MESSAGE(), @ErrorLine VARCHAR(100)= ERROR_LINE(), @ErrorCall NVARCHAR(MAX)= 'EXEC Coditech_DeleteCountry @CountryId = '+@CountryId+',@Status='+CAST(@Status AS VARCHAR(200));
             SET @Status = 0;
             SELECT 0 AS ID,
                    CAST(0 AS BIT) AS Status;
			 ROLLBACK TRAN DeleteAccount;
             --EXEC Znode_InsertProcedureErrorLog
             --     @ProcedureName = 'Coditech_DeleteCountry',
             --     @ErrorInProcedure = @Error_procedure,
             --     @ErrorMessage = @ErrorMessage,
             --     @ErrorLine = @ErrorLine,
             --     @ErrorCall = @ErrorCall;
            
         END CATCH;
     END;
