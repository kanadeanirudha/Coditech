using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;
namespace Coditech.API.Service
{
    public interface IAccSetupTransactionTypeService
    {
        AccSetupTransactionTypeListModel GetTransactionTypeList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        AccSetupTransactionTypeModel CreateTransactionType(AccSetupTransactionTypeModel model);
        AccSetupTransactionTypeModel GetTransactionType(short accSetupTransactionTypeId);
        bool UpdateTransactionType(AccSetupTransactionTypeModel model);
        bool DeleteTransactionType(ParameterModel parameterModel);
    }
}
