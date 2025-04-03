using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralPolicyMasterService
    {
        GeneralPolicyListModel GetPolicyList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralPolicyModel CreatePolicy(GeneralPolicyModel model);
        GeneralPolicyModel GetPolicy(short generalPolicyMasterId);
        bool UpdatePolicy(GeneralPolicyModel model);
        bool DeletePolicy(ParameterModel parameterModel);
    }
}
