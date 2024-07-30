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
    public class GeneralSmsProviderAgent : BaseAgent, IGeneralSmsProviderAgent
    {

        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralSmsProviderClient _generalSmsProviderClient;
        #endregion

        #region Public Constructor
        public GeneralSmsProviderAgent(ICoditechLogging coditechLogging, IGeneralSmsProviderClient generalSmsProviderClient)
        {
            _coditechLogging = coditechLogging;
            _generalSmsProviderClient = GetClient<IGeneralSmsProviderClient>(generalSmsProviderClient);
        }
        #endregion

        #region Public Methods
        public virtual GeneralSmsProviderListViewModel GetSmsProviderList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("ProviderName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ProviderCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "ProviderName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralSmsProviderListResponse response = _generalSmsProviderClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralSmsProviderListModel SmsProviderList = new GeneralSmsProviderListModel { GeneralSmsProviderList = response?.GeneralSmsProviderList };
            GeneralSmsProviderListViewModel listViewModel = new GeneralSmsProviderListViewModel();
            listViewModel.GeneralSmsProviderList = SmsProviderList?.GeneralSmsProviderList?.ToViewModel<GeneralSmsProviderViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralSmsProviderList.Count, BindColumns());
            return listViewModel;
        }
        //Create General SmsProvider.
        public virtual GeneralSmsProviderViewModel CreateSmsProvider(GeneralSmsProviderViewModel generalSmsProviderViewModel)
        {
            try
            {
                GeneralSmsProviderResponse response = _generalSmsProviderClient.CreateSmsProvider(generalSmsProviderViewModel.ToModel<GeneralSmsProviderModel>());
                GeneralSmsProviderModel generalSmsProviderModel = response?.GeneralSmsProviderModel;
                return IsNotNull(generalSmsProviderModel) ? generalSmsProviderModel.ToViewModel<GeneralSmsProviderViewModel>() : new GeneralSmsProviderViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SmsProviderMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralSmsProviderViewModel)GetViewModelWithErrorMessage(generalSmsProviderViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralSmsProviderViewModel)GetViewModelWithErrorMessage(generalSmsProviderViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SmsProviderMaster.ToString(), TraceLevel.Error);
                return (GeneralSmsProviderViewModel)GetViewModelWithErrorMessage(generalSmsProviderViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }
        //Get general SmsProvider by general SmsProvider master id.
        public virtual GeneralSmsProviderViewModel GetSmsProvider(short generalSmsProviderId)
        {
            GeneralSmsProviderResponse response = _generalSmsProviderClient.GetSmsProvider(generalSmsProviderId);
            return response?.GeneralSmsProviderModel.ToViewModel<GeneralSmsProviderViewModel>();
        }

        //Update generalSmsProvider.
        public virtual GeneralSmsProviderViewModel UpdateSmsProvider(GeneralSmsProviderViewModel generalSmsProviderViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.SmsProviderMaster.ToString(), TraceLevel.Info);
                GeneralSmsProviderResponse response = _generalSmsProviderClient.UpdateSmsProvider(generalSmsProviderViewModel.ToModel<GeneralSmsProviderModel>());
                GeneralSmsProviderModel generalSmsProviderModel = response?.GeneralSmsProviderModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.SmsProviderMaster.ToString(), TraceLevel.Info);
                return IsNotNull(generalSmsProviderModel) ? generalSmsProviderModel.ToViewModel<GeneralSmsProviderViewModel>() : (GeneralSmsProviderViewModel)GetViewModelWithErrorMessage(new GeneralSmsProviderViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SmsProviderMaster.ToString(), TraceLevel.Error);
                return (GeneralSmsProviderViewModel)GetViewModelWithErrorMessage(generalSmsProviderViewModel, GeneralResources.UpdateErrorMessage);
            }
        }
        //Delete generalSmsProvider.
        public virtual bool DeleteSmsProvider(string generalSmsProviderId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.SmsProviderMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalSmsProviderClient.DeleteSmsProvider(new ParameterModel { Ids = generalSmsProviderId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SmsProviderMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralSmsProviderMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SmsProviderMaster.ToString(), TraceLevel.Error);
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
                ColumnName = "SmsProvider Name",
                ColumnCode = "SmsProviderName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "SmsProvider Code",
                ColumnCode = "SmsProviderCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Default",
                ColumnCode = "DefaultFlag",
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return all SmsProvider list from database 
        public virtual GeneralSmsProviderListResponse GetSmsProviderList()
        {
            GeneralSmsProviderListResponse SmsProviderList = _generalSmsProviderClient.List(null, null, null, 1, int.MaxValue);
            return SmsProviderList?.GeneralSmsProviderList?.Count > 0 ? SmsProviderList : new GeneralSmsProviderListResponse();
        }
        #endregion
    }
}
