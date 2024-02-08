using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralLeadGenerationClient : IBaseClient
    {
        /// <summary>
        /// Get list of General LeadGeneration.
        /// </summary>
        /// <returns>GeneralLeadGenerationListResponse</returns>
        GeneralLeadGenerationListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create LeadGeneration.
        /// </summary>
        /// <param name="GeneralLeadGenerationModel">GeneralLeadGenerationModel.</param>
        /// <returns>Returns GeneralLeadGenerationResponse.</returns>
        GeneralLeadGenerationResponse CreateLeadGeneration(GeneralLeadGenerationModel body);

        /// <summary>
        /// Get LeadGeneration by generalLeadGenerationId.
        /// </summary>
        /// <param name="generalLeadGenerationId">generalLeadGenerationId</param>
        /// <returns>Returns GeneralLeadGenerationResponse.</returns>
        GeneralLeadGenerationResponse GetLeadGeneration(long generalLeadGenerationId);

        /// <summary>
        /// Update LeadGeneration.
        /// </summary>
        /// <param name="GeneralLeadGenerationModel">GeneralLeadGenerationModel.</param>
        /// <returns>Returns updated GeneralLeadGenerationResponse</returns>
        GeneralLeadGenerationResponse UpdateLeadGeneration(GeneralLeadGenerationModel body);

        /// <summary>
        /// Delete LeadGeneration.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteLeadGeneration(ParameterModel body);
    }
}
