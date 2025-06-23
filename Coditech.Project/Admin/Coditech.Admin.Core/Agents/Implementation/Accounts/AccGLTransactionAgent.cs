using Coditech.Admin.Helpers;
using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using System.Diagnostics;
using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.Admin.Agents
{
    public class AccGLTransactionAgent : BaseAgent, IAccGLTransactionAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IAccGLTransactionClient _accGLTransactionClient;
        private readonly IGeneralFinancialYearClient _generalFinancialYearClient;
        #endregion

        #region Public Constructor
        public AccGLTransactionAgent(ICoditechLogging coditechLogging, IAccGLTransactionClient accGLTransactionClient, IGeneralFinancialYearClient generalFinancialYearClient)
        {
            _coditechLogging = coditechLogging;
            _accGLTransactionClient = GetClient<IAccGLTransactionClient>(accGLTransactionClient);
            _generalFinancialYearClient = GetClient<IGeneralFinancialYearClient>(generalFinancialYearClient);
        }
        #endregion

        #region Public Methods


        public virtual GeneralFinancialYearModel GetCurrentFinancialYear()
        {
            int accSetupBalanceSheetId = AdminGeneralHelper.GetSelectedBalanceSheetId();
            GeneralFinancialYearResponse financialyearresponse = _generalFinancialYearClient.GetCurrentFinancialYear(accSetupBalanceSheetId);
            return financialyearresponse?.GeneralFinancialYearModel.ToViewModel<GeneralFinancialYearModel>();
        }
        //Create General Designation.
        public virtual AccGLTransactionViewModel CreateGLTransaction(AccGLTransactionViewModel accGLTransactionViewModel)
        {


            accGLTransactionViewModel.AccSetupBalanceSheetId = AdminGeneralHelper.GetSelectedBalanceSheetId();
            try
            {
                AccGLTransactionResponse response = _accGLTransactionClient.CreateGLTransaction(accGLTransactionViewModel.ToModel<AccGLTransactionModel>());
                AccGLTransactionModel accGLTransactionModel = response?.AccGLTransactionModel;
                return IsNotNull(accGLTransactionModel) ? accGLTransactionModel.ToViewModel<AccGLTransactionViewModel>() : new AccGLTransactionViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLTransaction.ToString(), TraceLevel.Warning);

                return (AccGLTransactionViewModel)GetViewModelWithErrorMessage(accGLTransactionViewModel,
                    ex.ErrorCode == ErrorCodes.AlreadyExist ? ex.ErrorMessage : GeneralResources.ErrorFailedToCreate);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLTransaction.ToString(), TraceLevel.Error);
                return (AccGLTransactionViewModel)GetViewModelWithErrorMessage(accGLTransactionViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        public List<AccGLTransactionViewModel> GetAccSetupGLAccountList(string searchKeyword, int accSetupGLId, string userType, string transactionTypeCode, int balanceSheet)
        {
            AccGLTransactionViewModel accGLTransactionViewModel = new AccGLTransactionViewModel();
            balanceSheet = AdminGeneralHelper.GetSelectedBalanceSheetId();
            AccGLTransactionListResponse response = _accGLTransactionClient.GetAccSetupGLAccountList(searchKeyword, accSetupGLId, userType, transactionTypeCode, balanceSheet);

            return response?.AccGLTransactionList?.ToViewModel<AccGLTransactionViewModel>().ToList()
                   ?? new List<AccGLTransactionViewModel>(); // Ensure returning a valid list
        }
        public List<AccGLTransactionViewModel> GetPersons(string searchKeyword, int userTypeId, int balanceSheet)
        {
            //Set the AccSetupBalanceSheetId from the AdminGeneralHelper
            balanceSheet = AdminGeneralHelper.GetSelectedBalanceSheetId();
            AccGLTransactionListResponse response = _accGLTransactionClient.GetPersons(searchKeyword, userTypeId, balanceSheet);

            return response?.AccGLTransactionList?.ToViewModel<AccGLTransactionViewModel>().ToList()
                   ?? new List<AccGLTransactionViewModel>(); // Ensure returning a valid list
        }

        #endregion

        #region protected

        #endregion
    }
}
