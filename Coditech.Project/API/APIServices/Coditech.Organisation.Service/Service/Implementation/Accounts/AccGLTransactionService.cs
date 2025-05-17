using System.Data;
using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace Coditech.API.Service
{
    public class AccGLTransactionService : BaseService, IAccGLTransactionService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<AccGLTransaction> _accGLTransactionRepository;
        private readonly ICoditechRepository<AccSetupGL> _accSetupGLRepository;
        private readonly ICoditechRepository<GeneralFinancialYear> _generalFinancialYearMasterRepository;
        public AccGLTransactionService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _accGLTransactionRepository = new CoditechRepository<AccGLTransaction>(_serviceProvider.GetService<Coditech_Entities>());
            _accSetupGLRepository = new CoditechRepository<AccSetupGL>(_serviceProvider.GetService<Coditech_Entities>());
            _generalFinancialYearMasterRepository = new CoditechRepository<GeneralFinancialYear>(_serviceProvider.GetService<Coditech_Entities>());
        }
        //Create GLTransaction.
        public virtual bool CreateGLTransaction(AccGLTransactionModel accGLTransactionModel)
        {
            if (accGLTransactionModel.AccSetupBalanceSheetId > 0)
            {
                CoditechViewRepository<AccGLTransactionModel> objStoredProc = new CoditechViewRepository<AccGLTransactionModel>(_serviceProvider.GetService<Coditech_Entities>());
                objStoredProc.SetParameter("@TransactionXmlString", accGLTransactionModel.TransactionDetailsData, ParameterDirection.Input, DbType.Xml);
                objStoredProc.SetParameter("@AccGLTransactionId", accGLTransactionModel.AccGLTransactionId, ParameterDirection.Input, DbType.Int64);
                objStoredProc.SetParameter("@AccSetupBalanceSheetId", accGLTransactionModel.AccSetupBalanceSheetId, ParameterDirection.Input, DbType.Int32);
                objStoredProc.SetParameter("@GeneralFinancialYearId", accGLTransactionModel.GeneralFinancialYearId, ParameterDirection.Input, DbType.Int16);
                objStoredProc.SetParameter("@AccGLTransactionTypeId", accGLTransactionModel.AccSetupTransactionTypeId, ParameterDirection.Input, DbType.Byte);
                objStoredProc.SetParameter("@NarrationDescription", accGLTransactionModel.NarrationDescription, ParameterDirection.Input, DbType.String);
                objStoredProc.SetParameter("@TransactionDate", accGLTransactionModel.TransactionDate, ParameterDirection.Input, DbType.DateTime);
                objStoredProc.SetParameter("@PolicyType", "Auto", ParameterDirection.Input, DbType.String);
                objStoredProc.SetParameter("@AuthorityLevel", "ApprovalLevel", ParameterDirection.Input, DbType.String);// EntryLevel AuthorityLevel 
                objStoredProc.SetParameter("@CreatedBy", accGLTransactionModel.CreatedBy, ParameterDirection.Input, DbType.Int64);
                objStoredProc.SetParameter("@ModeCode", accGLTransactionModel.ModeCode, ParameterDirection.Input, DbType.String);
                objStoredProc.SetParameter("@VoucherAmount", accGLTransactionModel.TransactionAmount, ParameterDirection.Input, DbType.Decimal);
                objStoredProc.SetParameter("@ErrorMessage", accGLTransactionModel.ErrorMessage, ParameterDirection.Output, DbType.Int32);
                objStoredProc.SetParameter("@ErrorCode", accGLTransactionModel.HasError, ParameterDirection.Output, DbType.Int32);
                objStoredProc.SetParameter("@Status", accGLTransactionModel.Status, ParameterDirection.Output, DbType.Int32);
                int statusOutput = 0;
                // Execute the stored procedure
                objStoredProc.ExecuteStoredProcedureList(
                    "Coditech_AccountVoucherInsert @TransactionXmlString,@AccGLTransactionId,@AccSetupBalanceSheetId,@GeneralFinancialYearId,@AccGLTransactionTypeId,@NarrationDescription,@TransactionDate,@PolicyType,@AuthorityLevel,@CreatedBy,@ModeCode,@VoucherAmount,@ErrorMessage,@ErrorCode,@Status OUT",
                    14,
                    out statusOutput // Use the local variable to capture the output value
                );
                if (statusOutput == 0)
                {
                    accGLTransactionModel.HasError = true;
                    accGLTransactionModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
                    return false;
                }
                return true;
            }
            else
            {
                accGLTransactionModel.HasError = true;
                accGLTransactionModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
                return false;
            }
        }
        #region Auto Search 
        public virtual List<AccGLTransactionModel> GetAccSetupGLAccountList(string searchKeyword, int accSetupGLId, string userType, string transactionTypeCode, int balaceSheet)
        {
            short generalFinancialYearId = _generalFinancialYearMasterRepository.Table.Where(x => x.IsCurrentFinancialYear).Select(x => x.GeneralFinancialYearId).FirstOrDefault();

            AccGLTransactionModel accGLTransactionModel = new AccGLTransactionModel();
            // Validate the input parameters
            if (string.IsNullOrEmpty(searchKeyword))
                throw new CoditechException(ErrorCodes.InvalidData, "Search keyword cannot be empty.");
            _coditechLogging.LogMessage($"Searching AccSetupGL for name: {searchKeyword}");
            CoditechViewRepository<AccSetupGLModel> objStoredProc = new CoditechViewRepository<AccSetupGLModel>(_serviceProvider.GetService<Coditech_Entities>());
            PageListModel pageListModel = new PageListModel(null, null, 0, 0);
            objStoredProc.SetParameter("@AccSetupChartOfAccountTemplateId", null, ParameterDirection.Input, DbType.Byte);
            objStoredProc.SetParameter("@AccSetupBalancesheetId", balaceSheet, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@ActionMode", "update", ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@GeneralFinancialYearId", generalFinancialYearId, ParameterDirection.Input, DbType.Int16);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<AccSetupGLModel> accSetupGLRecords = objStoredProc
                .ExecuteStoredProcedureList("Coditech_GetAccSetupGLTree @AccSetupChartOfAccountTemplateId, @AccSetupBalancesheetId, @ActionMode,@GeneralFinancialYearId, @RowsCount OUT",
                4,
                out pageListModel.TotalRowCount)
                ?.ToList() ?? new List<AccSetupGLModel>();
            AccGLTransactionModel transactionModel = new AccGLTransactionModel
            {
                AccSetupGLList = accSetupGLRecords
            };
            return new List<AccGLTransactionModel> { transactionModel };
        }
        public virtual List<AccGLTransactionModel> GetPersons(string searchKeyword, int userTypeId, int balanceSheet)
        {
            short generalFinancialYearId = _generalFinancialYearMasterRepository.Table.Where(x => x.IsCurrentFinancialYear).Select(x => x.GeneralFinancialYearId).FirstOrDefault();


            AccGLTransactionModel accGLTransactionModel = new AccGLTransactionModel();
            if (string.IsNullOrEmpty(searchKeyword))
                throw new CoditechException(ErrorCodes.InvalidData, "Search keyword cannot be empty.");
            _coditechLogging.LogMessage($"Searching AccSetupGL for name: {searchKeyword}");
            CoditechViewRepository<AccGLIndividualOpeningBalanceModel> objStoredProc = new CoditechViewRepository<AccGLIndividualOpeningBalanceModel>(_serviceProvider.GetService<Coditech_Entities>());
            PageListModel pageListModel = new PageListModel(null, null, 0, 0);
            objStoredProc.SetParameter("@AccSetupBalancesheetId", balanceSheet, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@GeneralFinancialYearId", generalFinancialYearId, ParameterDirection.Input, DbType.Int16);
            objStoredProc.SetParameter("@UserTypeId", userTypeId, ParameterDirection.Input, DbType.Int16);
            List<AccGLIndividualOpeningBalanceModel> GetpersonList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetIndividualOpeningBalanceByUserType @AccSetupBalancesheetId,@GeneralFinancialYearId,@UserTypeId")?.ToList();
            accGLTransactionModel.Personlist = GetpersonList;
            AccGLTransactionModel personTransactionModel = new AccGLTransactionModel
            {
                Personlist = GetpersonList,
            };
            return new List<AccGLTransactionModel> { personTransactionModel };
        }
    }

}

#endregion