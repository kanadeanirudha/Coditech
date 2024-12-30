
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;
namespace Coditech.API.Client
{
    public interface IAccGLSetupNarrationClient : IBaseClient
    {
        /// <summary>
        /// Get list of AccGLSetupNarration.
        /// </summary>
        /// <returns>AccGLSetupNarrationListResponse</returns>
        AccGLSetupNarrationListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

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

        /// <summary>
        /// 
        /// Delete Narration.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteNarration(ParameterModel body);
    }
}
   