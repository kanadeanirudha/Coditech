using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IOrganisationCentrewisePolicyService
    {
        GeneralPolicyDetailsListModel GetOrganisationCentrewisePolicyList(string centreCode, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralPolicyDetailsModel GetCentrewisePolicyDetails(string centreCode, short generalPolicyRulesId);
        bool CentrewisePolicyDetails(GeneralPolicyDetailsModel model);
        bool DeleteCentrewisePolicy(ParameterModel parameterModel);
    }
}

