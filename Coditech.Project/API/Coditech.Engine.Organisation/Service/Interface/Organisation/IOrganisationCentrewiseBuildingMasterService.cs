using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IOrganisationCentrewiseBuildingMasterService
    {
        OrganisationCentrewiseBuildingListModel GetOrganisationCentrewiseBuildingList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        OrganisationCentrewiseBuildingModel CreateOrganisationCentrewiseBuilding(OrganisationCentrewiseBuildingModel model);
        OrganisationCentrewiseBuildingModel GetOrganisationCentrewiseBuilding(short organisationCentrewiseBuildingId);
        bool UpdateOrganisationCentrewiseBuilding(OrganisationCentrewiseBuildingModel model);
        bool DeleteOrganisationCentrewiseBuilding(ParameterModel parameterModel);

    }
}

