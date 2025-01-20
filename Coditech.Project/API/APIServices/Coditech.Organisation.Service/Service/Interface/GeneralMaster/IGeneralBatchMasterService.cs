using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralBatchMasterService
    {
        GeneralBatchListModel GetBatchList(string selectedCentreCode,FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralBatchModel CreateGeneralBatch(GeneralBatchModel model);
        GeneralBatchModel GetGeneralBatch(int generalBatchMasterId);
        bool UpdateGeneralBatch(GeneralBatchModel model);
        bool DeleteGeneralBatch(ParameterModel parameterModel);
        GeneralBatchUserListModel GetGeneralBatchUserList(int generalBatchMasterId, string userType, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        bool AssociateUnAssociateBatchwiseUser(GeneralBatchUserModel model);
    }
}
