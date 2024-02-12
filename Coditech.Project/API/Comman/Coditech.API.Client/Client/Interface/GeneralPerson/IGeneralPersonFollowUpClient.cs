using Coditech.Common.API.Model;
using Coditech.Common.API.Model.GeneralPerson.GeneralPersonFollowUp;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralPersonFollowUpClient : IBaseClient
    {
        /// <summary>
        /// Get list of General PersonFollowUp.
        /// </summary>
        /// <returns>GeneralPersonFollowUpListResponse</returns>
        GeneralPersonFollowUpListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create PersonFollowUp.
        /// </summary>
        /// <param name="GeneralPersonFollowUpModel">GeneralPersonFollowUpModel.</param>
        /// <returns>Returns GeneralPersonFollowUpResponse.</returns>
        GeneralPersonFollowUpResponse CreatePersonFollowUp(GeneralPersonFollowUpModel body);

        /// <summary>
        /// Get PersonFollowUp by generalPersonFollowUpId.
        /// </summary>
        /// <param name="generalPersonFollowUpId">generalPersonFollowUpId</param>
        /// <returns>Returns GeneralPersonFollowUpResponse.</returns>
        GeneralPersonFollowUpResponse GetPersonFollowUp(long generalPersonFollowUpId);

        /// <summary>
        /// Update PersonFollowUp.
        /// </summary>
        /// <param name="GeneralPersonFollowUpModel">GeneralPersonFollowUpModel.</param>
        /// <returns>Returns updated GeneralPersonFollowUpResponse</returns>
        GeneralPersonFollowUpResponse UpdatePersonFollowUp(GeneralPersonFollowUpModel body);

        /// <summary>
        /// Delete PersonFollowUp.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeletePersonFollowUp(ParameterModel body);
    }
}
