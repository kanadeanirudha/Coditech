using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;
using Coditech.Model;

namespace Coditech.API.Client
{
    public interface IOrganisationCentrewiseBuildingClient : IBaseClient
    {
        /// <summary>
        /// Get list of OrganisationCentre.
        /// </summary>
        /// <returns>OrganisationCentrewiseBuildingListResponse</returns>
        OrganisationCentrewiseBuildingListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create OrganisationCentre.
        /// </summary>
        /// <param name="OrganisationCentrewiseBuildingModel">OrganisationCentrewiseBuildingModel.</param>
        /// <returns>Returns OrganisationCentrewiseBuildingResponse.</returns>
        OrganisationCentrewiseBuildingResponse CreateOrganisationCentrewiseBuilding(OrganisationCentrewiseBuildingModel body);

        /// <summary>
        /// Get OrganisationCentre by organisationCentreId.
        /// </summary>
        /// <param name="organisationCentrewiseBuildingId">organisationCentreId</param>
        /// <returns>Returns OrganisationCentrewiseBuildingResponse.</returns>
        OrganisationCentrewiseBuildingResponse GetOrganisationCentrewiseBuilding(short organisationCentrewiseBuildingId);

        /// <summary>
        /// Update OrganisationCentre.
        /// </summary>
        /// <param name="OrganisationCentrewiseBuildingModel">OrganisationCentreModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseBuildingResponse</returns>
        OrganisationCentrewiseBuildingResponse UpdateOrganisationCentrewiseBuilding(OrganisationCentrewiseBuildingModel body);

        /// <summary>
        /// Delete OrganisationCentrewiseBuilding.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteOrganisationCentrewiseBuilding(ParameterModel body);

        
    }
}
