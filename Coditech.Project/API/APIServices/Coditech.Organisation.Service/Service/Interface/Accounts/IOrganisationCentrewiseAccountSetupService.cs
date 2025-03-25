using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using Coditech.Model;
using System.Collections.Specialized;
namespace Coditech.API.Service
{
    public interface IOrganisationCentrewiseAccountSetupService
    {
        OrganisationCentrewiseAccountSetupModel GetOrganisationCentrewiseAccountSetup(string centreCode);
        bool UpdateOrganisationCentrewiseAccountSetup(OrganisationCentrewiseAccountSetupModel model);
    }
}
