using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;
namespace Coditech.API.Service
{
    public interface IAccGLSetupNarrationService
    {
        AccGLSetupNarrationListModel GetNarrationList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        AccGLSetupNarrationModel CreateNarration(AccGLSetupNarrationModel model);
        AccGLSetupNarrationModel GetNarration(int generalCountryMasterId);
        bool UpdateNarration(AccGLSetupNarrationModel model);
        bool DeleteNarration(ParameterModel parameterModel);
    }
}
