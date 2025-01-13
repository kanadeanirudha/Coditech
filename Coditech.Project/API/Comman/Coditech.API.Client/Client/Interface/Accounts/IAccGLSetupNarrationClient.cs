using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
namespace Coditech.API.Client
{
    public interface IAccGLSetupNarrationClient : IBaseClient
    {
        /// <summary>
        /// Get list of AccGLSetupNarration.
        /// </summary>
        /// <returns>AccGLSetupNarrationListResponse</returns>
        AccGLSetupNarrationListResponse List(string selectedCentreCode);

        /// <summary>
        /// Get list of Narration.
        /// </summary>
        ///<param name="AccGLSetupNarrationModel"> AccGLSetupNarrationModel.</param>
        /// <returns>AccGLSetupNarrationListViewModel</returns>

        AccGLSetupNarrationResponse CreateNarration(AccGLSetupNarrationModel body);

        /// <summary>
        /// Get Narration by accGLSetupNarrationId.
        /// </summary>
        /// <param name="accGLSetupNarrationId">accGLSetupNarrationId</param>
        /// <returns>Returns AccGLSetupNarrationViewModel.</returns>
        AccGLSetupNarrationResponse GetNarration(int accGLSetupNarrationId);

        /// <summary>
        /// Update Narration.
        /// </summary>
        /// <param name="accGLSetupNarrationViewModel">AccGLSetupNarrationModel.</param>
        /// <returns>Returns updated AccGLSetupNarrationResponse</returns>
        AccGLSetupNarrationResponse UpdateNarration(AccGLSetupNarrationModel model);
    }
}
   