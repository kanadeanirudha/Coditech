using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralEmailTemplateClient : IBaseClient
    {
        /// <summary>
        /// Get list of General EmailTemplate.
        /// </summary>
        /// <returns>GeneralEmailTemplateListResponse</returns>
        GeneralEmailTemplateListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create CountryEmailTemplate
        /// </summary>
        /// <param name="GeneralEmailTemplateModel">GeneralEmailTemplateModel.</param>
        /// <returns>Returns GeneralEmailTemplateResponse.</returns>
        GeneralEmailTemplateResponse CreateEmailTemplate(GeneralEmailTemplateModel body);

        /// <summary>
        /// Get Email by generalEmailTemplateId.
        /// </summary>
        /// <param name="generalEmailTemplateId">generalEmailTemplateId</param>
        /// <returns>Returns GeneralEmailTemplateResponse.</returns>
        GeneralEmailTemplateResponse GetEmailTemplate(short generalEmailTemplateId);

        /// <summary>
        /// Update Email.
        /// </summary>
        /// <param name="GeneralEmailTemplateModel">GeneralEmailTemplateModel.</param>
        /// <returns>Returns updated GeneralEmailTemplateResponse</returns>
        GeneralEmailTemplateResponse UpdateEmailTemplate(GeneralEmailTemplateModel body);

        /// <summary>
        /// Delete Email.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteEmailTemplate(ParameterModel body);
    }
}
