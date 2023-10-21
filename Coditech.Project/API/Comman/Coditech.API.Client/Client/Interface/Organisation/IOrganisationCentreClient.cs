using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;
using Coditech.Model;

namespace Coditech.API.Client
{
    public interface IOrganisationCentreClient : IBaseClient
    {
        /// <summary>
        /// Get list of OrganisationCentre.
        /// </summary>
        /// <returns>OrganisationCentreListResponse</returns>
        OrganisationCentreListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create OrganisationCentre.
        /// </summary>
        /// <param name="OrganisationCentreModel">OrganisationCentreModel.</param>
        /// <returns>Returns OrganisationCentreResponse.</returns>
        OrganisationCentreResponse CreateOrganisationCentre(OrganisationCentreModel body);

        /// <summary>
        /// Get OrganisationCentre by organisationCentreId.
        /// </summary>
        /// <param name="organisationCentreId">organisationCentreId</param>
        /// <returns>Returns OrganisationCentreResponse.</returns>
        OrganisationCentreResponse GetOrganisationCentre(short organisationCentreId);

        /// <summary>
        /// Update OrganisationCentre.
        /// </summary>
        /// <param name="OrganisationCentreModel">OrganisationCentreModel.</param>
        /// <returns>Returns updated OrganisationCentreResponse</returns>
        OrganisationCentreResponse UpdateOrganisationCentre(OrganisationCentreModel body);

        /// <summary>
        /// Delete OrganisationCentre.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteOrganisationCentre(ParameterModel body);

        /// <summary>
        /// Get OrganisationCentrePrintingFormat.
        /// </summary>
        /// <param name="OrganisationCentrePrintingFormatModel">OrganisationCentrePrintingFormatModel.</param>
        /// <returns>Returns OrganisationCentrePrintingFormatResponse</returns>
        OrganisationCentrePrintingFormatResponse GetPrintingFormat(OrganisationCentrePrintingFormatModel body);
    }
}
