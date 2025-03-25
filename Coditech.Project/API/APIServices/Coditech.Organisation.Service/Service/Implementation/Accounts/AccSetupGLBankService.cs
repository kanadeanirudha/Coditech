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
    public class AccSetupGLBankService : IAccSetupGLBankService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<AccSetupGLBank> _accSetupGLBankRepository;
        private readonly ICoditechRepository<AccSetupBalanceSheet> _accSetupBalanceSheetRepository;
        private readonly ICoditechRepository<AccSetupBalanceSheetType> _accSetupBalanceSheetTypeRepository;
        public AccSetupGLBankService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _accSetupGLBankRepository = new CoditechRepository<AccSetupGLBank>(_serviceProvider.GetService<Coditech_Entities>());
            _accSetupBalanceSheetRepository = new CoditechRepository<AccSetupBalanceSheet>(_serviceProvider.GetService<Coditech_Entities>());
            _accSetupBalanceSheetTypeRepository = new CoditechRepository<AccSetupBalanceSheetType>(_serviceProvider.GetService<Coditech_Entities>());
        }
        public virtual AccSetupGLBankListModel GetAccSetupGLBankList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<AccSetupGLBankModel> objStoredProc = new CoditechViewRepository<AccSetupGLBankModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<AccSetupGLBankModel> AccSetupGLBankList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetAccSetupGLBankList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            AccSetupGLBankListModel listModel = new AccSetupGLBankListModel();

            listModel.AccSetupGLBankList = AccSetupGLBankList?.Count > 0 ? AccSetupGLBankList : new List<AccSetupGLBankModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create AccSetupGLBank.
        public virtual AccSetupGLBankModel CreateAccSetupGLBank(AccSetupGLBankModel accSetupGLBankModel)
        {
            //accSetupGLBankModel.SelectedCentreCode = accSetupGLBankModel.SelectedCentreCode;
            //accSetupGLBankModel.AccSetupBalanceSheetId = accSetupGLBankModel.AccSetupBalanceSheetId;

            if (IsNull(accSetupGLBankModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            //if (IsAccSetupMasterEntryAlreadyExist(accSetupMasterModel.FiscalYearDay, accSetupMasterModel.FiscalYearMonth, accSetupMasterModel.AccSetupMasterId))
            //    throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Fiscal Year"));


            AccSetupGLBank accSetupGLBank = accSetupGLBankModel.FromModelToEntity<AccSetupGLBank>();

            //Create new AccSetupMaster and return it.
            AccSetupGLBank AccSetupGLBankData = _accSetupGLBankRepository.Insert(accSetupGLBank);
            if (AccSetupGLBankData?.AccSetupGLBankId > 0)
            {
                accSetupGLBankModel.AccSetupGLBankId = AccSetupGLBankData.AccSetupGLBankId;
            }
            else
            {
                accSetupGLBankModel.HasError = true;
                accSetupGLBankModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return accSetupGLBankModel;
        }

        //Get AccSetupGLBank by AccSetupGLBank id.
        public virtual AccSetupGLBankModel GetAccSetupGLBank(int accSetupGLBankId)
        {
            if (accSetupGLBankId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "accSetupGLBankId"));

            //Get the AccSetupMaster Details based on id.
            AccSetupGLBank accSetupGLBank = _accSetupGLBankRepository.Table.FirstOrDefault(x => x.AccSetupGLBankId == accSetupGLBankId);
            AccSetupGLBankModel accSetupGLBankModel = accSetupGLBank?.FromEntityToModel<AccSetupGLBankModel>();
            return accSetupGLBankModel;
        }
        //Update AccSetupGLBank.
        public virtual bool UpdateAccSetupGLBank(AccSetupGLBankModel accSetupGLBankModel)
        {
            if (IsNull(accSetupGLBankModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            //if (IsAccSetupMasterEntryAlreadyExist(accSetupMasterModel.FiscalYearDay, accSetupMasterModel.FiscalYearMonth, accSetupMasterModel.AccSetupMasterId))
            //    throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Financial Year"));


            if (accSetupGLBankModel.AccSetupGLBankId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AccSetupGLBankID"));


            AccSetupGLBank accSetupGLBank = accSetupGLBankModel.FromModelToEntity<AccSetupGLBank>();

            //Update AccSetupGLBank
            bool isAccSetupMasterUpdated = _accSetupGLBankRepository.Update(accSetupGLBank);
            if (!isAccSetupMasterUpdated)
            {
                accSetupGLBankModel.HasError = true;
                accSetupGLBankModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isAccSetupMasterUpdated;
        }
        //Delete AccSetupGLBank.
        public virtual bool DeleteAccSetupGLBank(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AccSetupGLBankID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("AccSetupGLBankId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteAccSetupGLBank @AccSetupGLBankId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }
    }
}
