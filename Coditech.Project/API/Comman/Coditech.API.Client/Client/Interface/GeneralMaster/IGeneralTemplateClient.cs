using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralTemplateClient : IBaseClient
    {
        /// <summary>
        /// Get Template by generalTemplateId.
        /// </summary>
        /// <param name="generalTemplateId">generalTemplateId</param>
        /// <returns>Returns GeneralTemplateResponse.</returns>
        GeneralTemplateResponse GetTemplate(int generalTemplateId);
    }
}
