using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IGeneralTemplateAgent
    {
        /// <summary>
        /// Get Template by generalTemplateId.
        /// </summary>
        /// <param name="generalTemplateId">generalTemplateId</param>
        /// <returns>Returns GeneralTemplateViewModel.</returns>
        GeneralTemplateViewModel GetTemplate(int generalTemplateId);     
    }
}
