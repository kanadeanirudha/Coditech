using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Specialized;
using System.Data;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class InventoryCategoryTypeService : IInventoryCategoryTypeService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<InventoryCategoryTypeMaster> _inventoryCategoryTypeRepository;
        public InventoryCategoryTypeService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _inventoryCategoryTypeRepository = new CoditechRepository<InventoryCategoryTypeMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual InventoryCategoryTypeListModel GetInventoryCategoryTypeList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<InventoryCategoryTypeModel> objStoredProc = new CoditechViewRepository<InventoryCategoryTypeModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<InventoryCategoryTypeModel> inventoryCategoryTypeList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetInventoryCategoryTypeList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            InventoryCategoryTypeListModel listModel = new InventoryCategoryTypeListModel();

            listModel.InventoryCategoryTypeList = inventoryCategoryTypeList?.Count > 0 ? inventoryCategoryTypeList : new List<InventoryCategoryTypeModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create InventoryCategoryType.
        public virtual InventoryCategoryTypeModel CreateInventoryCategoryType(InventoryCategoryTypeModel inventoryCategoryTypeModel)
        {
            if (IsNull(inventoryCategoryTypeModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsInventoryCategoryTypeNameAlreadyExist(inventoryCategoryTypeModel.CategoryTypeName))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Inventory Category Type Name"));

            InventoryCategoryTypeMaster inventoryCategoryType = inventoryCategoryTypeModel.FromModelToEntity<InventoryCategoryTypeMaster>();

            //Create new InventoryCategory and return it.
            InventoryCategoryTypeMaster inventoryCategoryTypeData = _inventoryCategoryTypeRepository.Insert(inventoryCategoryType);
            if (inventoryCategoryTypeData?.InventoryCategoryTypeMasterId > 0)
            {
                inventoryCategoryTypeModel.InventoryCategoryTypeMasterId = inventoryCategoryTypeData.InventoryCategoryTypeMasterId;
            }
            else
            {
                inventoryCategoryTypeModel.HasError = true;
                inventoryCategoryTypeModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return inventoryCategoryTypeModel;
        }

        //Get InventoryCategoryType by InventoryCategory id.
        public virtual InventoryCategoryTypeModel GetInventoryCategoryType(byte inventoryCategoryTypeMasterId)
        {
            if (inventoryCategoryTypeMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryCategoryTypeMasterId"));

            //Get the InventoryCategoryType Details based on id.
            InventoryCategoryTypeMaster inventoryCategoryType = _inventoryCategoryTypeRepository.Table.FirstOrDefault(x => x.InventoryCategoryTypeMasterId == inventoryCategoryTypeMasterId);
            InventoryCategoryTypeModel inventoryCategoryTypeModel = inventoryCategoryType?.FromEntityToModel<InventoryCategoryTypeModel>();
            return inventoryCategoryTypeModel;
        }

        //Update InventoryCategoryType.
        public virtual bool UpdateInventoryCategoryType(InventoryCategoryTypeModel inventoryCategoryTypeModel)
        {
            if (IsNull(inventoryCategoryTypeModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (inventoryCategoryTypeModel.InventoryCategoryTypeMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryCategoryTypeMasterId"));

            if (IsInventoryCategoryTypeNameAlreadyExist(inventoryCategoryTypeModel.CategoryTypeName, inventoryCategoryTypeModel.InventoryCategoryTypeMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Inventory Category Type Name"));

            InventoryCategoryTypeMaster inventoryCategoryType = inventoryCategoryTypeModel.FromModelToEntity<InventoryCategoryTypeMaster>();

            //Update InventoryCategoryType
            bool isInventoryCategoryTypeUpdated = _inventoryCategoryTypeRepository.Update(inventoryCategoryType);
            if (!isInventoryCategoryTypeUpdated)
            {
                inventoryCategoryTypeModel.HasError = true;
                inventoryCategoryTypeModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isInventoryCategoryTypeUpdated;
        }

        //Delete InventoryCategoryType.
        public virtual bool DeleteInventoryCategoryType(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryCategoryTypeMasterID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("InventoryCategoryTypeMasterId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteInventoryCategoryType @InventoryCategoryTypeMasterId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }
        #region Protected Method
        //Check if Inventory Category Type Name is already present or not.
        protected virtual bool IsInventoryCategoryTypeNameAlreadyExist(string categoryTypeName, byte inventoryCategoryTypeMasterId = 0)
         => _inventoryCategoryTypeRepository.Table.Any(x => x.CategoryTypeName == categoryTypeName && (x.InventoryCategoryTypeMasterId != inventoryCategoryTypeMasterId || inventoryCategoryTypeMasterId == 0));
        #endregion
    }
}
