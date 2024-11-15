
using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;

using System.Collections.Specialized;
using System.Data;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class InventoryCategoryService : IInventoryCategoryService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<InventoryCategory> _inventoryCategoryRepository;
        public InventoryCategoryService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _inventoryCategoryRepository = new CoditechRepository<InventoryCategory>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual InventoryCategoryListModel GetInventoryCategoryList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<InventoryCategoryModel> objStoredProc = new CoditechViewRepository<InventoryCategoryModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<InventoryCategoryModel> InventoryCategoryList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetInventoryCategoryList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            InventoryCategoryListModel listModel = new InventoryCategoryListModel();

            listModel.InventoryCategoryList = InventoryCategoryList?.Count > 0 ? InventoryCategoryList : new List<InventoryCategoryModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create InventoryCategory.
        public virtual InventoryCategoryModel CreateInventoryCategory(InventoryCategoryModel inventoryCategoryModel)
        {
            if (IsNull(inventoryCategoryModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsInventoryCategoryCodeAlreadyExist(inventoryCategoryModel.CategoryCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "InventoryCategory Code"));

            InventoryCategory inventoryCategory = inventoryCategoryModel.FromModelToEntity<InventoryCategory>();

            //Create new InventoryCategory and return it.
            InventoryCategory inventoryCategoryData = _inventoryCategoryRepository.Insert(inventoryCategory);
            if (inventoryCategoryData?.InventoryCategoryId > 0)
            {
                inventoryCategoryModel.InventoryCategoryId = inventoryCategoryData.InventoryCategoryId;
            }
            else
            {
                inventoryCategoryModel.HasError = true;
                inventoryCategoryModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return inventoryCategoryModel;
        }

        //Get InventoryCategory by InventoryCategory id.
        public virtual InventoryCategoryModel GetInventoryCategory(short inventoryCategoryId)
        {
            if (inventoryCategoryId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryCategoryID"));

            //Get the InventoryCategory Details based on id.
            InventoryCategory inventoryCategory = _inventoryCategoryRepository.Table.FirstOrDefault(x => x.InventoryCategoryId == inventoryCategoryId);
            InventoryCategoryModel inventoryCategoryModel = inventoryCategory?.FromEntityToModel<InventoryCategoryModel>();
            return inventoryCategoryModel;
        }

        //Update InventoryCategory.
        public virtual bool UpdateInventoryCategory(InventoryCategoryModel inventoryCategoryModel)
        {
            if (IsNull(inventoryCategoryModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (inventoryCategoryModel.InventoryCategoryId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryCategoryID"));

            if (IsInventoryCategoryCodeAlreadyExist(inventoryCategoryModel.CategoryCode, inventoryCategoryModel.InventoryCategoryId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "InventoryCategory Code"));

            InventoryCategory inventoryCategory = inventoryCategoryModel.FromModelToEntity<InventoryCategory>();

            //Update InventoryCategory
            bool isInventoryCategoryUpdated = _inventoryCategoryRepository.Update(inventoryCategory);
            if (!isInventoryCategoryUpdated)
            {
                inventoryCategoryModel.HasError = true;
                inventoryCategoryModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isInventoryCategoryUpdated;
        }

        //Delete InventoryCategory.
        public virtual bool DeleteInventoryCategory(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryCategoryID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("InventoryCategoryId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteInventoryCategory @InventoryCategoryId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if InventoryCategory code is already present or not.
        protected virtual bool IsInventoryCategoryCodeAlreadyExist(string categoryCode, short inventoryCategoryId = 0)
         => _inventoryCategoryRepository.Table.Any(x => x.CategoryCode == categoryCode && (x.InventoryCategoryId != inventoryCategoryId || inventoryCategoryId == 0));
        #endregion
    }
}
