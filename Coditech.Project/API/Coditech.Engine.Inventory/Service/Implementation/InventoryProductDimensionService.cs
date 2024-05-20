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
    public class InventoryProductDimensionService : IInventoryProductDimensionService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<InventoryProductDimension> _inventoryProductDimensionRepository;
        public InventoryProductDimensionService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _inventoryProductDimensionRepository = new CoditechRepository<InventoryProductDimension>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual InventoryProductDimensionListModel GetInventoryProductDimensionList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<InventoryProductDimensionModel> objStoredProc = new CoditechViewRepository<InventoryProductDimensionModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<InventoryProductDimensionModel> InventoryProductDimensionList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetInventoryProductDimensionList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            InventoryProductDimensionListModel listModel = new InventoryProductDimensionListModel();

            listModel.InventoryProductDimensionList = InventoryProductDimensionList?.Count > 0 ? InventoryProductDimensionList : new List<InventoryProductDimensionModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create InventoryProductDimension.
        public virtual InventoryProductDimensionModel CreateInventoryProductDimension(InventoryProductDimensionModel inventoryProductDimensionModel)
        {
            if (IsNull(inventoryProductDimensionModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsInventoryProductDimensionCodeAlreadyExist(inventoryProductDimensionModel.ProductDimensionCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "InventoryProductDimension Code"));

            InventoryProductDimension inventoryProductDimension = inventoryProductDimensionModel.FromModelToEntity<InventoryProductDimension>();

            //Create new InventoryProductDimension and return it.
            InventoryProductDimension inventoryProductDimensionData = _inventoryProductDimensionRepository.Insert(inventoryProductDimension);
            if (inventoryProductDimensionData?.InventoryProductDimensionId > 0)
            {
                inventoryProductDimensionModel.InventoryProductDimensionId = inventoryProductDimensionData.InventoryProductDimensionId;
            }
            else
            {
                inventoryProductDimensionModel.HasError = true;
                inventoryProductDimensionModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return inventoryProductDimensionModel;
        }

        //Get InventoryProductDimension by InventoryProductDimension id.
        public virtual InventoryProductDimensionModel GetInventoryProductDimension(short inventoryProductDimensionId)
        {
            if (inventoryProductDimensionId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryProductDimensionID"));

            //Get the InventoryProductDimension Details based on id.
            InventoryProductDimension inventoryProductDimension = _inventoryProductDimensionRepository.Table.FirstOrDefault(x => x.InventoryProductDimensionId == inventoryProductDimensionId);
            InventoryProductDimensionModel inventoryProductDimensionModel = inventoryProductDimension?.FromEntityToModel<InventoryProductDimensionModel>();
            return inventoryProductDimensionModel;
        }

        //Update InventoryProductDimension.
        public virtual bool UpdateInventoryProductDimension(InventoryProductDimensionModel inventoryProductDimensionModel)
        {
            if (IsNull(inventoryProductDimensionModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (inventoryProductDimensionModel.InventoryProductDimensionId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryProductDimensionID"));

            if (IsInventoryProductDimensionCodeAlreadyExist(inventoryProductDimensionModel.ProductDimensionCode, inventoryProductDimensionModel.InventoryProductDimensionId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "InventoryProductDimension Code"));

            InventoryProductDimension inventoryProductDimension = inventoryProductDimensionModel.FromModelToEntity<InventoryProductDimension>();

            //Update InventoryProductDimension
            bool isInventoryProductDimensionUpdated = _inventoryProductDimensionRepository.Update(inventoryProductDimension);
            if (!isInventoryProductDimensionUpdated)
            {
                inventoryProductDimensionModel.HasError = true;
                inventoryProductDimensionModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isInventoryProductDimensionUpdated;
        }

        //Delete InventoryProductDimension.
        public virtual bool DeleteInventoryProductDimension(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryProductDimensionID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("InventoryProductDimensionId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteInventoryProductDimension @InventoryProductDimensionId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if InventoryProductDimension code is already present or not.
        protected virtual bool IsInventoryProductDimensionCodeAlreadyExist(string productDimensionCode, short inventoryProductDimensionId = 0)
         => _inventoryProductDimensionRepository.Table.Any(x => x.ProductDimensionCode == productDimensionCode && (x.InventoryProductDimensionId != inventoryProductDimensionId || inventoryProductDimensionId == 0));
        #endregion
    }
}
