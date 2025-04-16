using Coditech.Common.API.Model;

namespace Coditech.API.Service
{
    public interface IAccGLOpeningBalanceService
    {
        ACCGLOpeningBalanceListModel GetNonControlHeadType(int accSetupBalanceSheetId, short glCategory, byte controlNonControl);
        ACCGLOpeningBalanceModel UpdateNonControlHeadType(ACCGLOpeningBalanceModel model);
    }
}

