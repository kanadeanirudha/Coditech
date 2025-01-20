using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralMeasurementUnitMasterService
    {
        GeneralMeasurementUnitListModel GetMeasurementUnitList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralMeasurementUnitModel CreateMeasurementUnit(GeneralMeasurementUnitModel model);
        GeneralMeasurementUnitModel GetMeasurementUnit(short generalMeasurementUnitMasterId);
        bool UpdateMeasurementUnit(GeneralMeasurementUnitModel model);
        bool DeleteMeasurementUnit(ParameterModel parameterModel);
    }
}
