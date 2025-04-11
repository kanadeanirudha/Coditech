using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralPolicyMasterService
    {
        GeneralPolicyListModel GetPolicyList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralPolicyModel CreatePolicy(GeneralPolicyModel model);
        GeneralPolicyModel GetPolicy(string policyCode);
        bool UpdatePolicy(GeneralPolicyModel model);
        bool DeletePolicy(ParameterModel parameterModel);
        GeneralPolicyRulesListModel GetGeneralPolicyRulesList(string policyCode, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralPolicyRulesModel CreatePolicyRules(GeneralPolicyRulesModel model);
        GeneralPolicyRulesModel GetPolicyRules(short generalPolicyRulesId, string policyApplicableStatus);
        bool UpdatePolicyRules(GeneralPolicyRulesModel model);
        bool DeletePolicyRules(ParameterModel parameterModel);
        GeneralPolicyDetailsModel GetPolicyDetails(short generalPolicyDetailsId);
        bool UpdatePolicyDetails(GeneralPolicyDetailsModel model);
    }
}
