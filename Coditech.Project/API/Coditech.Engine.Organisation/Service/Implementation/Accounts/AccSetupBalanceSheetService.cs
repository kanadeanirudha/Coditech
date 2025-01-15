using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;
using System.Collections.Specialized;
using System.Data;
using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Service
{
    public class AccSetupBalanceSheetService : BaseService, IAccSetupBalanceSheetService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<AccSetupBalanceSheet> _accSetupBalanceSheetRepository;
        public AccSetupBalanceSheetService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _accSetupBalanceSheetRepository = new CoditechRepository<AccSetupBalanceSheet>(_serviceProvider.GetService<Coditech_Entities>());
        }
        public virtual AccSetupBalanceSheetListModel GetBalanceSheetList(string selectedCentreCode, byte accSetupBalanceSheetTypeId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<AccSetupBalanceSheetModel> objStoredProc = new CoditechViewRepository<AccSetupBalanceSheetModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<AccSetupBalanceSheetModel> balanceSheetList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetAccSetupBalanceSheetList  @CentreCode @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
            AccSetupBalanceSheetListModel listModel = new AccSetupBalanceSheetListModel();

            listModel.AccSetupBalanceSheetList = balanceSheetList?.Count > 0 ? balanceSheetList : new List<AccSetupBalanceSheetModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create BalanceSheet.
        public virtual AccSetupBalanceSheetModel CreateBalanceSheet(AccSetupBalanceSheetModel accSetupBalanceSheetModel)
        {
            if (IsNull(accSetupBalanceSheetModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);
            if (IsBalanceSheetNameAlreadyExist(accSetupBalanceSheetModel.AccBalancesheetHeadDesc))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "BalanceSheet Name"));

            AccSetupBalanceSheet accSetupBalanceSheet = accSetupBalanceSheetModel.FromModelToEntity<AccSetupBalanceSheet>();

            //Create new BalanceSheet and return it.
            AccSetupBalanceSheet balanceSheetData = _accSetupBalanceSheetRepository.Insert(accSetupBalanceSheet);
            if (balanceSheetData?.AccSetupBalanceSheetId > 0)
            {
                accSetupBalanceSheetModel.AccSetupBalanceSheetId = balanceSheetData.AccSetupBalanceSheetId;
            }
            else
            {
                accSetupBalanceSheetModel.HasError = true;
                accSetupBalanceSheetModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return accSetupBalanceSheetModel;
        }
        //Get BalanceSheet by AccSetupBalanceSheetId.
        public virtual AccSetupBalanceSheetModel GetBalanceSheet(int accSetupBalanceSheetId)
        {
            if (accSetupBalanceSheetId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "BalanceSheetId"));

            //Get the BalanceSheet Details based on id.
            AccSetupBalanceSheet balanceSheetData = _accSetupBalanceSheetRepository.Table.FirstOrDefault(x => x.AccSetupBalanceSheetId == accSetupBalanceSheetId);
            AccSetupBalanceSheetModel accSetupBalanceSheetModel = balanceSheetData.FromEntityToModel<AccSetupBalanceSheetModel>();
            return accSetupBalanceSheetModel;
        }

        //Update BalanceSheet.
        public virtual bool UpdateBalanceSheet(AccSetupBalanceSheetModel accSetupBalanceSheetModel)
        {
            if (IsNull(accSetupBalanceSheetModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (accSetupBalanceSheetModel.AccSetupBalanceSheetId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "BalanceSheetId"));

            if (IsBalanceSheetNameAlreadyExist(accSetupBalanceSheetModel.AccBalancesheetHeadDesc, accSetupBalanceSheetModel.AccSetupBalanceSheetId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "BalanceSheet Name"));

            AccSetupBalanceSheet accSetupBalanceSheet = accSetupBalanceSheetModel.FromModelToEntity<AccSetupBalanceSheet>();

            //Update BalanceSheet
            bool isBalanceSheetUpdated = _accSetupBalanceSheetRepository.Update(accSetupBalanceSheet);
            if (!isBalanceSheetUpdated)
            {
                accSetupBalanceSheetModel.HasError = true;
                accSetupBalanceSheetModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isBalanceSheetUpdated;
        }
        //Delete BalanceSheet.
        public virtual bool DeleteBalanceSheet(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AccSetupBalanceSheetId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("AccSetupBalanceSheetId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteAccSetupBalanceSheet @AccSetupBalanceSheetId,  @Status OUT", 1, out status);
            return status == 1 ? true : false;
        }
        #region Protected Method
        //Check if BalanceSheet Name is already present or not.
        protected virtual bool IsBalanceSheetNameAlreadyExist(string balancesheetName, int accSetupBalanceSheetId = 0)
         => _accSetupBalanceSheetRepository.Table.Any(x => x.AccBalancesheetHeadDesc == balancesheetName && (x.AccSetupBalanceSheetId != accSetupBalanceSheetId || accSetupBalanceSheetId == 0));
        #endregion
    }
}

