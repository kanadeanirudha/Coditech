using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;
namespace Coditech.API.Service
{
    public interface IAccSetupGLBankService
    {
        AccSetupGLBankListModel GetAccSetupGLBankList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        AccSetupGLBankModel CreateAccSetupGLBank(AccSetupGLBankModel model);
        AccSetupGLBankModel GetAccSetupGLBank(int accSetupGLBankId);
        bool UpdateAccSetupGLBank(AccSetupGLBankModel model);
        bool DeleteAccSetupGLBank(ParameterModel parameterModel);
    }
}
