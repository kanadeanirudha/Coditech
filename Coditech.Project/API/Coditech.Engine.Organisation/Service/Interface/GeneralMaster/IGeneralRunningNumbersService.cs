using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralRunningNumbersService
    {
        GeneralRunningNumbersListModel GetRunningNumbersList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralRunningNumbersModel CreateRunningNumbers(GeneralRunningNumbersModel model);
        GeneralRunningNumbersModel GetRunningNumbers(long generalRunningNumberId);
        bool UpdateRunningNumbers(GeneralRunningNumbersModel model);
        bool DeleteRunningNumbers(ParameterModel parameterModel);
    }
}

