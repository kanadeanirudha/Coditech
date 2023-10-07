CREATE   PROCEDURE [dbo].[Coditech_DeleteTaxGroup]
(
       @TaxGroupId VARCHAR(max) ,
       @Status      BIT OUT
)
AS
/*
Summary: This Procedure is used to delete TaxGroup details
Unit Testing:
EXEC Coditech_DeleteTaxGroup @TaxGroupId='1'
*/
     BEGIN
         BEGIN TRAN;
         BEGIN TRY
             SET NOCOUNT ON;
             DECLARE @TaxGroup TABLE (
                                      TaxGroupId INT
                                      );
             INSERT INTO @TaxGroup SELECT item FROM dbo.Split ( @TaxGroupId , ',');
           
             DECLARE @DeleteTaxGroupId TABLE (
                                              TaxGroupId INT
                                              );
             INSERT INTO @DeleteTaxGroupId
                    SELECT a.GeneralTaxGroupMasterId
                    FROM [dbo].GeneralTaxGroupMaster AS a INNER JOIN @TaxGroup AS b ON ( a.GeneralTaxGroupMasterId = b.TaxGroupId );
           
		     DELETE FROM GeneralTaxGroupMasterDetails
             WHERE EXISTS ( SELECT TOP 1 1
                            FROM @DeleteTaxGroupId AS b
                            WHERE b.TaxGroupId = GeneralTaxGroupMasterDetails.GeneralTaxGroupMasterId
                          );

             DELETE FROM GeneralTaxGroupMaster
             WHERE EXISTS ( SELECT TOP 1 1
                            FROM @DeleteTaxGroupId AS b
                            WHERE b.TaxGroupId = GeneralTaxGroupMaster.GeneralTaxGroupMasterId
                          );
           
             SET @Status = 1;
				 IF ( SELECT COUNT(1)
					  FROM @DeleteTaxGroupId
					) = ( SELECT COUNT(1)
						  FROM @TaxGroup
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
             --DECLARE @Error_procedure VARCHAR(1000)= ERROR_PROCEDURE(), @ErrorMessage NVARCHAR(MAX)= ERROR_MESSAGE(), @ErrorLine VARCHAR(100)= ERROR_LINE(), @ErrorCall NVARCHAR(MAX)= 'EXEC Coditech_DeleteTaxGroup @TaxGroupId = '+@TaxGroupId+',@Status='+CAST(@Status AS VARCHAR(200));
             SET @Status = 0;
             SELECT 0 AS ID,
                    CAST(0 AS BIT) AS Status;
			 ROLLBACK TRAN DeleteAccount;
             --EXEC Znode_InsertProcedureErrorLog
             --     @ProcedureName = 'Coditech_DeleteTaxGroup',
             --     @ErrorInProcedure = @Error_procedure,
             --     @ErrorMessage = @ErrorMessage,
             --     @ErrorLine = @ErrorLine,
             --     @ErrorCall = @ErrorCall;
            
         END CATCH;
     END;
GO


