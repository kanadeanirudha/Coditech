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
    public class InventoryUoMMasterService : IInventoryUoMMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<InventoryUoMMaster> _inventoryUoMMasterRepository;
        public InventoryUoMMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _inventoryUoMMasterRepository = new CoditechRepository<InventoryUoMMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual InventoryUoMMasterListModel GetInventoryUoMMasterList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<InventoryUoMMasterModel> objStoredProc = new CoditechViewRepository<InventoryUoMMasterModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<InventoryUoMMasterModel> InventoryUoMMasterList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetInventoryUoMMasterList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            InventoryUoMMasterListModel listModel = new InventoryUoMMasterListModel();

            listModel.InventoryUoMMasterList = InventoryUoMMasterList?.Count > 0 ? InventoryUoMMasterList : new List<InventoryUoMMasterModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create InventoryUoMMaster.
        public virtual InventoryUoMMasterModel CreateInventoryUoMMaster(InventoryUoMMasterModel inventoryUoMMasterModel)
        {
            if (IsNull(inventoryUoMMasterModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsInventoryUoMMasterCodeAlreadyExist(inventoryUoMMasterModel.UomCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "InventoryUoMMaster Code"));

            InventoryUoMMaster inventoryUoMMaster = inventoryUoMMasterModel.FromModelToEntity<InventoryUoMMaster>();

            //Create new InventoryUoMMaster and return it.
            InventoryUoMMaster inventoryUoMMasterData = _inventoryUoMMasterRepository.Insert(inventoryUoMMaster);
            if (inventoryUoMMasterData?.InventoryUoMMasterId > 0)
            {
                inventoryUoMMasterModel.InventoryUoMMasterId = inventoryUoMMasterData.InventoryUoMMasterId;
            }
            else
            {
                inventoryUoMMasterModel.HasError = true;
                inventoryUoMMasterModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return inventoryUoMMasterModel;
        }

        //Get InventoryUoMMaster by InventoryUoMMaster id.
        public virtual InventoryUoMMasterModel GetInventoryUoMMaster(short inventoryUoMMasterId)
        {
            if (inventoryUoMMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryUoMMasterID"));

            //Get the InventoryUoMMaster Details based on id.
            InventoryUoMMaster inventoryUoMMaster = _inventoryUoMMasterRepository.Table.FirstOrDefault(x => x.InventoryUoMMasterId == inventoryUoMMasterId);
            InventoryUoMMasterModel inventoryUoMMasterModel = inventoryUoMMaster?.FromEntityToModel<InventoryUoMMasterModel>();
            return inventoryUoMMasterModel;
        }

        //Update InventoryUoMMaster.
        public virtual bool UpdateInventoryUoMMaster(InventoryUoMMasterModel inventoryUoMMasterModel)
        {
            if (IsNull(inventoryUoMMasterModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (inventoryUoMMasterModel.InventoryUoMMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryUoMMasterID"));

            if (IsInventoryUoMMasterCodeAlreadyExist(inventoryUoMMasterModel.UomCode, inventoryUoMMasterModel.InventoryUoMMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "InventoryUoMMaster Code"));

            InventoryUoMMaster inventoryUoMMaster = inventoryUoMMasterModel.FromModelToEntity<InventoryUoMMaster>();

            //Update InventoryUoMMaster
            bool isInventoryUoMMasterUpdated = _inventoryUoMMasterRepository.Update(inventoryUoMMaster);
            if (!isInventoryUoMMasterUpdated)
            {
                inventoryUoMMasterModel.HasError = true;
                inventoryUoMMasterModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isInventoryUoMMasterUpdated;
        }

        //Delete InventoryUoMMaster.
        public virtual bool DeleteInventoryUoMMaster(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryUoMMasterID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("InventoryUoMMasterId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteInventoryUoMMaster @InventoryUoMMasterId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if InventoryUoMMaster code is already present or not.
        protected virtual bool IsInventoryUoMMasterCodeAlreadyExist(string uomCode, short inventoryUoMMasterId = 0)
         => _inventoryUoMMasterRepository.Table.Any(x => x.UomCode == uomCode && (x.InventoryUoMMasterId != inventoryUoMMasterId || inventoryUoMMasterId == 0));
        #endregion
    }
}
