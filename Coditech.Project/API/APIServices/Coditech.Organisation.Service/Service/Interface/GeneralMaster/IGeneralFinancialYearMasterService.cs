using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralFinancialYearMasterService
    {
        GeneralFinancialYearListModel GetFinancialYearList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralFinancialYearModel CreateFinancialYear(GeneralFinancialYearModel model);
        GeneralFinancialYearModel GetFinancialYear(short generalFinancialYearMasterId);
        bool UpdateFinancialYear(GeneralFinancialYearModel model);
        bool DeleteFinancialYear(ParameterModel parameterModel);
    }
}
