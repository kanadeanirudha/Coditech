using Coditech.Common.API.Model;

namespace Coditech.API.Service
{
    public interface IAccGLOpeningBalanceService
    {
        ACCGLOpeningBalanceListModel GetNonControlHeadType(int accSetupBalanceSheetId, short glCategory, byte controlNonControl);
        ACCGLOpeningBalanceModel UpdateNonControlHeadType(ACCGLOpeningBalanceModel model);
        ACCGLOpeningBalanceModel GetControlHeadType(int accSetupBalanceSheetId, short glCategory, byte controlNonControl);
        AccGLIndividualOpeningBalanceModel GetIndividualOpeningBalance(int accSetupBalanceSheetId,short userTypeId, short generalFinancialYearId, int accSetupGLId);
        AccGLIndividualOpeningBalanceModel UpdateIndividualOpeningBalance(AccGLIndividualOpeningBalanceModel model);
    }
}

