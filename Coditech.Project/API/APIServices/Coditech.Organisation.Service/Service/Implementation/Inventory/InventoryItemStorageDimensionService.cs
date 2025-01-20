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
    public class InventoryItemStorageDimensionService : IInventoryItemStorageDimensionService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<InventoryItemStorageDimension> _inventoryItemStorageDimensionRepository;
        public InventoryItemStorageDimensionService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _inventoryItemStorageDimensionRepository = new CoditechRepository<InventoryItemStorageDimension>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual InventoryItemStorageDimensionListModel GetInventoryItemStorageDimensionList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<InventoryItemStorageDimensionModel> objStoredProc = new CoditechViewRepository<InventoryItemStorageDimensionModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<InventoryItemStorageDimensionModel> InventoryItemStorageDimensionList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetInventoryItemStorageDimensionList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            InventoryItemStorageDimensionListModel listModel = new InventoryItemStorageDimensionListModel();

            listModel.InventoryItemStorageDimensionList = InventoryItemStorageDimensionList?.Count > 0 ? InventoryItemStorageDimensionList : new List<InventoryItemStorageDimensionModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create InventoryItemStorageDimension.
        public virtual InventoryItemStorageDimensionModel CreateInventoryItemStorageDimension(InventoryItemStorageDimensionModel inventoryItemStorageDimensionModel)
        {
            if (IsNull(inventoryItemStorageDimensionModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsInventoryItemStorageDimensionCodeAlreadyExist(inventoryItemStorageDimensionModel.StorageDimensionCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "InventoryItemStorageDimension Code"));

            InventoryItemStorageDimension inventoryItemStorageDimension = inventoryItemStorageDimensionModel.FromModelToEntity<InventoryItemStorageDimension>();

            //Create new InventoryItemStorageDimension and return it.
            InventoryItemStorageDimension inventoryItemStorageDimensionData = _inventoryItemStorageDimensionRepository.Insert(inventoryItemStorageDimension);
            if (inventoryItemStorageDimensionData?.InventoryItemStorageDimensionId > 0)
            {
                inventoryItemStorageDimensionModel.InventoryItemStorageDimensionId = inventoryItemStorageDimensionData.InventoryItemStorageDimensionId;
            }
            else
            {
                inventoryItemStorageDimensionModel.HasError = true;
                inventoryItemStorageDimensionModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return inventoryItemStorageDimensionModel;
        }

        //Get InventoryItemStorageDimension by InventoryItemStorageDimension id.
        public virtual InventoryItemStorageDimensionModel GetInventoryItemStorageDimension(short inventoryItemStorageDimensionId)
        {
            if (inventoryItemStorageDimensionId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryItemStorageDimensionID"));

            //Get the InventoryItemStorageDimension Details based on id.
            InventoryItemStorageDimension inventoryItemStorageDimension = _inventoryItemStorageDimensionRepository.Table.FirstOrDefault(x => x.InventoryItemStorageDimensionId == inventoryItemStorageDimensionId);
            InventoryItemStorageDimensionModel inventoryItemStorageDimensionModel = inventoryItemStorageDimension?.FromEntityToModel<InventoryItemStorageDimensionModel>();
            return inventoryItemStorageDimensionModel;
        }

        //Update InventoryItemStorageDimension.
        public virtual bool UpdateInventoryItemStorageDimension(InventoryItemStorageDimensionModel inventoryItemStorageDimensionModel)
        {
            if (IsNull(inventoryItemStorageDimensionModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (inventoryItemStorageDimensionModel.InventoryItemStorageDimensionId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryItemStorageDimensionID"));

            if (IsInventoryItemStorageDimensionCodeAlreadyExist(inventoryItemStorageDimensionModel.StorageDimensionCode, inventoryItemStorageDimensionModel.InventoryItemStorageDimensionId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "InventoryItemStorageDimension Code"));

            InventoryItemStorageDimension inventoryItemStorageDimension = inventoryItemStorageDimensionModel.FromModelToEntity<InventoryItemStorageDimension>();

            //Update InventoryItemStorageDimension
            bool isInventoryItemStorageDimensionUpdated = _inventoryItemStorageDimensionRepository.Update(inventoryItemStorageDimension);
            if (!isInventoryItemStorageDimensionUpdated)
            {
                inventoryItemStorageDimensionModel.HasError = true;
                inventoryItemStorageDimensionModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isInventoryItemStorageDimensionUpdated;
        }

        //Delete InventoryItemStorageDimension.
        public virtual bool DeleteInventoryItemStorageDimension(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryItemStorageDimensionID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("InventoryItemStorageDimensionId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteInventoryItemStorageDimension @InventoryItemStorageDimensionId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if InventoryItemStorageDimension code is already present or not.
        protected virtual bool IsInventoryItemStorageDimensionCodeAlreadyExist(string storageDimensionCode, short inventoryItemStorageDimensionId = 0)
         => _inventoryItemStorageDimensionRepository.Table.Any(x => x.StorageDimensionCode == storageDimensionCode && (x.InventoryItemStorageDimensionId != inventoryItemStorageDimensionId || inventoryItemStorageDimensionId == 0));
        #endregion
    }
}
