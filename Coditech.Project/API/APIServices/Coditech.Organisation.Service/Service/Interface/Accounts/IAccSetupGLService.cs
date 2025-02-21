using Coditech.Common.API.Model;
namespace Coditech.API.Service
{
    public interface IAccSetupGLService
    {
        AccSetupGLModel GetAccSetupGLTree(string selectedcentreCode, byte accSetupBalanceSheetTypeId, int accSetupBalanceSheetId);
        AccSetupGLModel CreateAccountSetupGL(AccSetupGLModel model);
        bool UpdateAccountSetupGL(AccSetupGLModel model);
    }
}
