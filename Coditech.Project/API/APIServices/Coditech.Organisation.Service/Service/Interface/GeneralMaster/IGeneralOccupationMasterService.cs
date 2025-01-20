using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralOccupationMasterService
    {
        GeneralOccupationListModel GetOccupationList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralOccupationModel CreateOccupation(GeneralOccupationModel model);
        GeneralOccupationModel GetOccupation(short generalOccupationMasterId);
        bool UpdateOccupation(GeneralOccupationModel model);
        bool DeleteOccupation(ParameterModel parameterModel);
    }
}
