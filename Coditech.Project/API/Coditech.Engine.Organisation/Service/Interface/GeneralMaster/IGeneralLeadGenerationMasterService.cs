using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralLeadGenerationMasterService
    {
        GeneralLeadGenerationListModel GetLeadGenerationList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralLeadGenerationModel CreateLeadGeneration(GeneralLeadGenerationModel model);
        GeneralLeadGenerationModel GetLeadGeneration(long generalLeadGenerationMasterId);
        bool UpdateLeadGeneration(GeneralLeadGenerationModel model);
        bool DeleteLeadGeneration(ParameterModel parameterModel);
    }
}

