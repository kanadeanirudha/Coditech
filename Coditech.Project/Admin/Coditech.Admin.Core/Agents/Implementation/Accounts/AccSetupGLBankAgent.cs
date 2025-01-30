using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using System.Diagnostics;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.Admin.Agents
{
    public class AccSetupGLBankAgent : BaseAgent, IAccSetupGLBankAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IAccSetupGLBankClient _accSetupGLBankClient;
        #endregion

        #region Public Constructor
        public AccSetupGLBankAgent(ICoditechLogging coditechLogging, IAccSetupGLBankClient accSetupGLBankClient)
        {
            _coditechLogging = coditechLogging;
            _accSetupGLBankClient = GetClient<IAccSetupGLBankClient>(accSetupGLBankClient);
        }
        #endregion

        #region Public Methods
        public virtual AccSetupGLBankListViewModel GetAccSetupGLBankList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("CountryName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("CountryCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("CallingCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            AccSetupGLBankListResponse response = _accSetupGLBankClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            AccSetupGLBankListModel accSetupGLBankList = new AccSetupGLBankListModel { AccSetupGLBankList = response?.AccSetupGLBankList };
            AccSetupGLBankListViewModel listViewModel = new AccSetupGLBankListViewModel();
            listViewModel.AccSetupGLBankList = accSetupGLBankList?.AccSetupGLBankList?.ToViewModel<AccSetupGLBankViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.AccSetupGLBankList.Count, BindColumns());
            return listViewModel;
        }
        //Create AccSetupGLBank.
        public virtual AccSetupGLBankViewModel CreateAccSetupGLBank(AccSetupGLBankViewModel accSetupGLBankViewModel)
        {
            try
            {
                AccSetupGLBankResponse response = _accSetupGLBankClient.CreateAccSetupGLBank(accSetupGLBankViewModel.ToModel<AccSetupGLBankModel>());
                AccSetupGLBankModel accSetupGLBankModel = response?.AccSetupGLBankModel;
                return IsNotNull(accSetupGLBankViewModel) ? accSetupGLBankModel.ToViewModel<AccSetupGLBankViewModel>() : new AccSetupGLBankViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupGLBank.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (AccSetupGLBankViewModel)GetViewModelWithErrorMessage(accSetupGLBankViewModel, ex.ErrorMessage);
                    default:
                        return (AccSetupGLBankViewModel)GetViewModelWithErrorMessage(accSetupGLBankViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupGLBank.ToString(), TraceLevel.Error);
                return (AccSetupGLBankViewModel)GetViewModelWithErrorMessage(accSetupGLBankViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get AccSetupGLBank by accSetupGLBankId.
        public virtual AccSetupGLBankViewModel GetAccSetupGLBank(int accSetupGLBankId)
        {
            AccSetupGLBankResponse response = _accSetupGLBankClient.GetAccSetupGLBank(accSetupGLBankId);
            return response?.AccSetupGLBankModel.ToViewModel<AccSetupGLBankViewModel>();
        }
        //Update AccSetupGLBank.
        public virtual AccSetupGLBankViewModel UpdateAccSetupGLBank(AccSetupGLBankViewModel accSetupGLBankViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AccSetupGLBank.ToString(), TraceLevel.Info);
                AccSetupGLBankResponse response = _accSetupGLBankClient.UpdateAccSetupGLBank(accSetupGLBankViewModel.ToModel<AccSetupGLBankModel>());
                AccSetupGLBankModel accSetupGLBankModel = response?.AccSetupGLBankModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.AccSetupGLBank.ToString(), TraceLevel.Info);
                return IsNotNull(accSetupGLBankModel) ? accSetupGLBankModel.ToViewModel<AccSetupGLBankViewModel>() : (AccSetupGLBankViewModel)GetViewModelWithErrorMessage(new AccSetupGLBankViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupGLBank.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (AccSetupGLBankViewModel)GetViewModelWithErrorMessage(accSetupGLBankViewModel, ex.ErrorMessage);
                    default:
                        return (AccSetupGLBankViewModel)GetViewModelWithErrorMessage(accSetupGLBankViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupGLBank.ToString(), TraceLevel.Error);
                return (AccSetupGLBankViewModel)GetViewModelWithErrorMessage(accSetupGLBankViewModel, GeneralResources.UpdateErrorMessage);
            }
        }
        //Delete AccSetupGLBank.
        public virtual bool DeleteAccSetupGLBank(string accSetupGLBankId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AccSetupGLBank.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _accSetupGLBankClient.DeleteAccSetupGLBank(new ParameterModel { Ids = accSetupGLBankId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupGLBank.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteAccSetupGLBank;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupGLBank.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }
        #endregion

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Bank Account Number",
                ColumnCode = "Bank AccountNumber",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Bank Account Name",
                ColumnCode = "Bank AccountName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Bank Branch Name",
                ColumnCode = "BankBranchName",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
    }
}
