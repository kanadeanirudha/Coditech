using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using Coditech.Model;
using System.Collections.Specialized;

namespace Coditech.API.Organisation.Service.Interface.Organisation
{
    public interface IOrganisationCentrewiseBuildingMasterService
    {
        OrganisationCentrewiseBuildingListModel GetOrganisationCentrewiseBuildingList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        OrganisationCentrewiseBuildingModel CreateOrganisationCentrewiseBuilding(OrganisationCentrewiseBuildingModel model);
        OrganisationCentrewiseBuildingModel GetOrganisationCentrewiseBuilding(short organisationCentrewiseBuildingMasterId);
        bool UpdateOrganisationCentrewiseBuilding(OrganisationCentrewiseBuildingModel model);
        bool DeleteOrganisationCentrewiseBuilding(ParameterModel parameterModel);
        
    }
}

