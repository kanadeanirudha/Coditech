using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IOrganisationCentreMasterService
    {
        OrganisationCentreListModel GetOrganisationCentreList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        OrganisationCentreMasterModel CreateOrganisationCentre(OrganisationCentreMasterModel model);
        OrganisationCentreMasterModel GetOrganisationCentre(short organisationCentreMasterId);
        bool UpdateOrganisationCentre(OrganisationCentreMasterModel model);
        bool DeleteOrganisationCentre(ParameterModel parameterModel);
    }
}

