using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Responses;
namespace Coditech.API.Client
{
    public interface IAccSetupGLClient : IBaseClient
    {
        /// <summary>
        /// //Get GetAccSetupGL
        /// </summary>
        /// <param name="centreCode">centreCode</param>
        /// <param name="accSetupBalanceSheetId">accSetupBalanceSheetId</param>
        /// <returns>Returns GetAccSetupGLResponse.</returns>
        AccSetupGLResponse GetAccSetupGLTree(string selectedcentreCode, byte accSetupBalanceSheetTypeId, int accSetupBalanceSheetId);

        /// <summary>
        /// Create CreateAccountSetupGL.
        /// </summary>
        /// <param name="AccSetupGLModel">AccSetupGLModel.</param>
        /// <returns>Returns AccSetupGLResponse.</returns>
        AccSetupGLResponse CreateAccountSetupGL(AccSetupGLModel body);

        /// <summary>
        /// Get AccountSetupGL by accSetupGLId.
        /// </summary>
        /// <param name="accSetupGLId">accSetupGLId</param>
        /// <returns>Returns AccSetupGLResponse.</returns>
        AccSetupGLResponse GetAccountSetupGL(int accSetupGLId);

        /// <summary>
        /// Update UpdateAccountSetupGL.
        /// </summary>
        /// <param name="AccSetupGLModel">AccSetupGLModel.</param>
        /// <returns>Returns updated AccSetupGLResponse</returns>
        AccSetupGLResponse UpdateAccountSetupGL(AccSetupGLModel body);

        /// <summary>
        /// AddChild AccountSetupGL.
        /// </summary>
        /// <param name="AccSetupGLModel">AccSetupGLModel.</param>
        /// <returns>Returns updated AccSetupGLResponse</returns>
        AccSetupGLResponse AddChild(AccSetupGLModel body);
    }
}
