using Coditech.Admin.Helpers;
using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Coditech.Admin.Agents
{
    public class AccGLOpeningBalanceAgent : BaseAgent, IAccGLOpeningBalanceAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IAccGLOpeningBalanceClient _accGLOpeningBalanceClient;
        private readonly IGeneralFinancialYearClient _generalFinancialYearClient;
        #endregion

        #region Public Constructor
        public AccGLOpeningBalanceAgent(ICoditechLogging coditechLogging, IAccGLOpeningBalanceClient accGLOpeningBalanceClient, IGeneralFinancialYearClient generalFinancialYearClient)
        {
            _coditechLogging = coditechLogging;
            _accGLOpeningBalanceClient = GetClient<IAccGLOpeningBalanceClient>(accGLOpeningBalanceClient);
            _generalFinancialYearClient = GetClient<IGeneralFinancialYearClient>(generalFinancialYearClient);

        }
        #endregion

        #region Public Methods
        public virtual ACCGLOpeningBalanceListViewModel GetNonControlHeadType(short accSetupCategoryId)
        {
            int accSetupBalanceSheetId = AdminGeneralHelper.GetSelectedBalanceSheetId();
            ACCGLOpeningBalanceListResponse response = _accGLOpeningBalanceClient.GetNonControlHeadType(accSetupBalanceSheetId, accSetupCategoryId, 0);
            ACCGLOpeningBalanceListModel accGLTransactionList = new ACCGLOpeningBalanceListModel { ACCGLOpeningBalanceList = response?.ACCGLOpeningBalanceList };
            ACCGLOpeningBalanceListViewModel listViewModel = new ACCGLOpeningBalanceListViewModel();
            listViewModel.ACCGLOpeningBalanceList = accGLTransactionList?.ACCGLOpeningBalanceList?.ToViewModel<ACCGLOpeningBalanceViewModel>().ToList();
            return listViewModel;
        }

        public virtual GeneralFinancialYearModel GetCurrentFinancialYear()
        {
            int accSetupBalanceSheetId = AdminGeneralHelper.GetSelectedBalanceSheetId();
            GeneralFinancialYearResponse financialyearresponse = _generalFinancialYearClient.GetCurrentFinancialYear(accSetupBalanceSheetId);
            return financialyearresponse?.GeneralFinancialYearModel.ToViewModel<GeneralFinancialYearModel>();
        }

        //Update  ACCGLOpeningBalance.
        public virtual ACCGLOpeningBalanceListViewModel UpdateNonControlHeadType(ACCGLOpeningBalanceListViewModel aCCGLOpeningBalanceViewModel)
        {
            try
            {
                List<ACCGLOpeningBalanceModel> accGLBalanceList = new List<ACCGLOpeningBalanceModel>();

                if (!string.IsNullOrEmpty(aCCGLOpeningBalanceViewModel.AccGLOpeningBalanceData))
                {
                    accGLBalanceList = JsonConvert.DeserializeObject<List<ACCGLOpeningBalanceModel>>(aCCGLOpeningBalanceViewModel.AccGLOpeningBalanceData);
                    aCCGLOpeningBalanceViewModel.ACCGLBalanceList = accGLBalanceList;
                }
                foreach (var item in accGLBalanceList)
                {
                    item.AccSetupBalanceSheetId = AdminGeneralHelper.GetSelectedBalanceSheetId();
                    item.AccSetupCategoryId = aCCGLOpeningBalanceViewModel.AccSetupCategoryId;
                }
                ACCGLOpeningBalanceModel model = new ACCGLOpeningBalanceModel
                {
                    AccSetupBalanceSheetId = AdminGeneralHelper.GetSelectedBalanceSheetId(),
                    AccSetupCategoryId = aCCGLOpeningBalanceViewModel.AccSetupCategoryId,
                    ACCGLBalanceList = accGLBalanceList
                };
                ACCGLOpeningBalanceResponse response = _accGLOpeningBalanceClient.UpdateNonControlHeadType(model);
                if (response != null)
                {
                    aCCGLOpeningBalanceViewModel.HasError = response.HasError;
                }
                else
                {
                    aCCGLOpeningBalanceViewModel.HasError = true;
                }
                return aCCGLOpeningBalanceViewModel;
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLOpeningBalance.ToString(), TraceLevel.Error);
                return (ACCGLOpeningBalanceListViewModel)GetViewModelWithErrorMessage(aCCGLOpeningBalanceViewModel, GeneralResources.UpdateErrorMessage);
            }
        }
        #endregion
    }
}
