create   PROCEDURE [dbo].[Coditech_DeleteTaxMaster]
(
       @GeneralTaxMasterId VARCHAR(max) ,
       @Status      BIT OUT
)
AS
/*
Summary: This Procedure is used to delete Tax details
Unit Testing:
EXEC Coditech_DeleteTaxMaster @GeneralTaxMasterId='1'
*/
     BEGIN
         BEGIN TRAN;
         BEGIN TRY
             SET NOCOUNT ON;
             DECLARE @Tax TABLE (
                                      GeneralTaxMasterId INT
                                      );
             INSERT INTO @Tax SELECT item FROM dbo.Split ( @GeneralTaxMasterId , ',');
           
             DECLARE @DeleteGeneralTaxMasterId TABLE (
                                              GeneralTaxMasterId INT
                                              );
             INSERT INTO @DeleteGeneralTaxMasterId
                    SELECT a.GeneralTaxMasterId
                    FROM [dbo].GeneralTaxMaster AS a INNER JOIN @Tax AS b ON ( a.GeneralTaxMasterId = b.GeneralTaxMasterId );
           
             DELETE FROM GeneralTaxMaster
             WHERE EXISTS ( SELECT TOP 1 1
                            FROM @DeleteGeneralTaxMasterId AS b
                            WHERE b.GeneralTaxMasterId = GeneralTaxMaster.GeneralTaxMasterId
                          );
           
             SET @Status = 1;
             IF ( SELECT COUNT(1)
                  FROM @DeleteGeneralTaxMasterId
                ) = ( SELECT COUNT(1)
                      FROM @Tax
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
             --DECLARE @Error_procedure VARCHAR(1000)= ERROR_PROCEDURE(), @ErrorMessage NVARCHAR(MAX)= ERROR_MESSAGE(), @ErrorLine VARCHAR(100)= ERROR_LINE(), @ErrorCall NVARCHAR(MAX)= 'EXEC Coditech_DeleteTaxMaster @GeneralTaxMasterId = '+@GeneralTaxMasterId+',@Status='+CAST(@Status AS VARCHAR(200));
             SET @Status = 0;
             SELECT 0 AS ID,
                    CAST(0 AS BIT) AS Status;
			 ROLLBACK TRAN DeleteAccount;
             --EXEC Znode_InsertProcedureErrorLog
             --     @ProcedureName = 'Coditech_DeleteTaxMaster',
             --     @ErrorInProcedure = @Error_procedure,
             --     @ErrorMessage = @ErrorMessage,
             --     @ErrorLine = @ErrorLine,
             --     @ErrorCall = @ErrorCall;
            
         END CATCH;
     END;
GO


