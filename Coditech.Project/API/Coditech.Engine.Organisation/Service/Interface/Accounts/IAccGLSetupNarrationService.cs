using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;
namespace Coditech.API.Service
{
    public interface IAccGLSetupNarrationService
    {
        AccGLSetupNarrationListModel GetNarrationList(string selectedCentreCode);
        AccGLSetupNarrationModel CreateNarration(AccGLSetupNarrationModel model);
        AccGLSetupNarrationModel GetNarration(int generalCountryMasterId);
        bool UpdateNarration(AccGLSetupNarrationModel model);
        //bool DeleteNarration(ParameterModel parameterModel);
    }
}
