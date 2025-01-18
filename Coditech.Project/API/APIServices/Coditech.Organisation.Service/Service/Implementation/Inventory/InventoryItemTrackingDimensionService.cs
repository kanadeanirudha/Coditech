using Coditech.API.Data;
using Coditech.API.Data.DataModel.Inventory;
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
    public class InventoryItemTrackingDimensionService : IInventoryItemTrackingDimensionService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<InventoryItemTrackingDimension> _inventoryItemTrackingDimensionRepository;
        public InventoryItemTrackingDimensionService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _inventoryItemTrackingDimensionRepository = new CoditechRepository<InventoryItemTrackingDimension>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual InventoryItemTrackingDimensionListModel GetInventoryItemTrackingDimensionList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<InventoryItemTrackingDimensionModel> objStoredProc = new CoditechViewRepository<InventoryItemTrackingDimensionModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<InventoryItemTrackingDimensionModel> InventoryItemTrackingDimensionList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetInventoryItemTrackingDimensionList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            InventoryItemTrackingDimensionListModel listModel = new InventoryItemTrackingDimensionListModel();

            listModel.InventoryItemTrackingDimensionList = InventoryItemTrackingDimensionList?.Count > 0 ? InventoryItemTrackingDimensionList : new List<InventoryItemTrackingDimensionModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create InventoryItemTrackingDimension.
        public virtual InventoryItemTrackingDimensionModel CreateInventoryItemTrackingDimension(InventoryItemTrackingDimensionModel inventoryItemTrackingDimensionModel)
        {
            if (IsNull(inventoryItemTrackingDimensionModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsInventoryItemTrackingDimensionCodeAlreadyExist(inventoryItemTrackingDimensionModel.TrackingDimensionCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "InventoryItemTrackingDimension Code"));

            InventoryItemTrackingDimension inventoryItemTrackingDimension = inventoryItemTrackingDimensionModel.FromModelToEntity<InventoryItemTrackingDimension>();

            //Create new InventoryItemTrackingDimension and return it.
            InventoryItemTrackingDimension inventoryItemTrackingDimensionData = _inventoryItemTrackingDimensionRepository.Insert(inventoryItemTrackingDimension);
            if (inventoryItemTrackingDimensionData?.InventoryItemTrackingDimensionId > 0)
            {
                inventoryItemTrackingDimensionModel.InventoryItemTrackingDimensionId = inventoryItemTrackingDimensionData.InventoryItemTrackingDimensionId;
            }
            else
            {
                inventoryItemTrackingDimensionModel.HasError = true;
                inventoryItemTrackingDimensionModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return inventoryItemTrackingDimensionModel;
        }

        //Get InventoryItemTrackingDimension by InventoryItemTrackingDimension id.
        public virtual InventoryItemTrackingDimensionModel GetInventoryItemTrackingDimension(short inventoryItemTrackingDimensionId)
        {
            if (inventoryItemTrackingDimensionId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryItemTrackingDimensionID"));

            //Get the InventoryItemTrackingDimension Details based on id.
            InventoryItemTrackingDimension inventoryItemTrackingDimension = _inventoryItemTrackingDimensionRepository.Table.FirstOrDefault(x => x.InventoryItemTrackingDimensionId == inventoryItemTrackingDimensionId);
            InventoryItemTrackingDimensionModel inventoryItemTrackingDimensionModel = inventoryItemTrackingDimension?.FromEntityToModel<InventoryItemTrackingDimensionModel>();
            return inventoryItemTrackingDimensionModel;
        }

        //Update InventoryItemTrackingDimension.
        public virtual bool UpdateInventoryItemTrackingDimension(InventoryItemTrackingDimensionModel inventoryItemTrackingDimensionModel)
        {
            if (IsNull(inventoryItemTrackingDimensionModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (inventoryItemTrackingDimensionModel.InventoryItemTrackingDimensionId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryItemTrackingDimensionID"));

            if (IsInventoryItemTrackingDimensionCodeAlreadyExist(inventoryItemTrackingDimensionModel.TrackingDimensionCode, inventoryItemTrackingDimensionModel.InventoryItemTrackingDimensionId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "InventoryItemTrackingDimension Code"));

            InventoryItemTrackingDimension inventoryItemTrackingDimension = inventoryItemTrackingDimensionModel.FromModelToEntity<InventoryItemTrackingDimension>();

            //Update InventoryItemTrackingDimension
            bool isInventoryItemTrackingDimensionUpdated = _inventoryItemTrackingDimensionRepository.Update(inventoryItemTrackingDimension);
            if (!isInventoryItemTrackingDimensionUpdated)
            {
                inventoryItemTrackingDimensionModel.HasError = true;
                inventoryItemTrackingDimensionModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isInventoryItemTrackingDimensionUpdated;
        }

        //Delete InventoryItemTrackingDimension.
        public virtual bool DeleteInventoryItemTrackingDimension(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryItemTrackingDimensionID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("InventoryItemTrackingDimensionId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteInventoryItemTrackingDimension @InventoryItemTrackingDimensionId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if InventoryItemTrackingDimension code is already present or not.
        protected virtual bool IsInventoryItemTrackingDimensionCodeAlreadyExist(string trackingDimensionCode, short inventoryItemTrackingDimensionId = 0)
         => _inventoryItemTrackingDimensionRepository.Table.Any(x => x.TrackingDimensionCode == trackingDimensionCode && (x.InventoryItemTrackingDimensionId != inventoryItemTrackingDimensionId || inventoryItemTrackingDimensionId == 0));
        #endregion
    }
}
