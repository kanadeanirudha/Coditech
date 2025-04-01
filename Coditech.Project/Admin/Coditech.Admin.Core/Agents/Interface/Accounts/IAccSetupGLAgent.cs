using Coditech.Common.API.Model;
namespace Coditech.Admin.Agents
{
    public interface IAccSetupGLAgent
    {
        /// <summary>
        /// Get AccSetupGL.
        /// </summary>
        /// <param name="centreCode">centreCode</param>
        /// <param name="accSetupBalanceSheetId">accSetupBalanceSheetId</param>
        /// <returns>Returns AccSetupGLModel.</returns>
        AccSetupGLModel GetAccSetupGLTree(string selectedcentreCode, byte accSetupBalanceSheetTypeId, int accSetupBalanceSheetId);
        /// <summary>
        /// Create CreateAccountSetupGL.
        /// </summary>
        /// <param name="CreateAccSetupGLModel">CreateAccountSetupGL View Model.</param>
        /// <returns>Returns created model.</returns>
        AccSetupGLModel CreateAccountSetupGL(AccSetupGLModel accSetupGLModel);

        /// <summary>
        /// Update UpdateAccountSetupGL.
        /// </summary>
        /// <param name="accSetupMasterViewModel">accSetupMasterViewModel.</param>
        /// <returns>Returns updated AccSetupMasterViewModel</returns>
        AccSetupGLModel UpdateAccountSetupGL(AccSetupGLModel accSetupGLModel);

        /// <summary>
        /// AddChild AccountSetupGL.
        /// </summary>
        /// <param name="accSetupMasterViewModel">accSetupMasterViewModel.</param>
        /// <returns>Returns updated AccSetupMasterViewModel</returns>
        AccSetupGLModel AddChild(AccSetupGLModel accSetupGLModel);
  //     AccSetupGLModel UpdateAccount(AccSetupGLModel accSetupGLModel);
        AccSetupGLModel GetAccountSetupGL(int accSetupGLId);

        bool DeleteAccountSetupGL(string accSetupGLId, out string errorMessage);

    }
}
