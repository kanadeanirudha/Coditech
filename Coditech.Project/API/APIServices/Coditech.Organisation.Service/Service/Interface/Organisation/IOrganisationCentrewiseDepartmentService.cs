using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IOrganisationCentrewiseDepartmentService
    {
        OrganisationCentrewiseDepartmentListModel GetOrganisationCentrewiseDepartmentList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);

        bool AssociateUnAssociateCentrewiseDepartment(OrganisationCentrewiseDepartmentModel model);
    }
}

