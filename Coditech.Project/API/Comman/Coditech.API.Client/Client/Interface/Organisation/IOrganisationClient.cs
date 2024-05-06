using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Responses;

namespace Coditech.API.Client
{
    public interface IOrganisationClient : IBaseClient
    {
        /// <summary>
        /// Get Organisation by organisationId.
        /// </summary>
        /// <returns>Returns OrganisationResponse.</returns>
        OrganisationResponse GetOrganisation();

        /// <summary>
        /// Update Organisation.
        /// </summary>
        /// <param name="OrganisationModel">OrganisationModel.</param>
        /// <returns>Returns updated OrganisationResponse</returns>
        OrganisationResponse UpdateOrganisation(OrganisationModel body);
    }
}
