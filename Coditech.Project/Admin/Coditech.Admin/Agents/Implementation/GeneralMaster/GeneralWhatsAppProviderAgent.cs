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
    public class GeneralWhatsAppProviderAgent : BaseAgent, IGeneralWhatsAppProviderAgent
    {
        #region Private Variable 
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralWhatsAppProviderClient _generalWhatsAppProviderClient;
        #endregion

        #region public constructor 
        public GeneralWhatsAppProviderAgent(ICoditechLogging coditechLogging, IGeneralWhatsAppProviderClient generalWhatsAppProviderClient)
        {
            _coditechLogging = coditechLogging;
            _generalWhatsAppProviderClient = GetClient<IGeneralWhatsAppProviderClient>(generalWhatsAppProviderClient);
        }
        #endregion
        #region public Methods 
        public virtual GeneralWhatsAppProviderListViewModel GetWhatsAppProviderList(DataTableViewModel dataTableModel)
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

            GeneralWhatsAppProviderListResponse response = _generalWhatsAppProviderClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralWhatsAppProviderListModel WhatsAppProviderList = new GeneralWhatsAppProviderListModel { GeneralWhatsAppProviderList = response?.GeneralWhatsAppProviderList };
            GeneralWhatsAppProviderListViewModel listViewModel = new GeneralWhatsAppProviderListViewModel();
            listViewModel.GeneralWhatsAppProviderList = WhatsAppProviderList?.GeneralWhatsAppProviderList?.ToViewModel<GeneralWhatsAppProviderViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralWhatsAppProviderList.Count, BindColumns());
            return listViewModel;
        }
        public virtual GeneralWhatsAppProviderViewModel CreateWhatsAppProvider(GeneralWhatsAppProviderViewModel generalWhatsAppProviderViewModel)
        {
            try
            {
                GeneralWhatsAppProviderResponse response = _generalWhatsAppProviderClient.CreateWhatsAppProvider(generalWhatsAppProviderViewModel.ToModel<GeneralWhatsAppProviderModel>());
                GeneralWhatsAppProviderModel generalWhatsAppProviderModel = response?.GeneralWhatsAppProviderModel;
                return IsNotNull(generalWhatsAppProviderModel) ? generalWhatsAppProviderModel.ToViewModel<GeneralWhatsAppProviderViewModel>() : new GeneralWhatsAppProviderViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.WhatsAppProviderMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralWhatsAppProviderViewModel)GetViewModelWithErrorMessage(generalWhatsAppProviderViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralWhatsAppProviderViewModel)GetViewModelWithErrorMessage(generalWhatsAppProviderViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.WhatsAppProviderMaster.ToString(), TraceLevel.Error);
                return (GeneralWhatsAppProviderViewModel)GetViewModelWithErrorMessage(generalWhatsAppProviderViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general WhatsAppProvider by general WhatsAppProvider master id.
        public virtual GeneralWhatsAppProviderViewModel GetWhatsAppProvider(short generalWhatsAppProviderId)
        {
            GeneralWhatsAppProviderResponse response = _generalWhatsAppProviderClient.GetWhatsAppProvider(generalWhatsAppProviderId);
            return response?.GeneralWhatsAppProviderModel.ToViewModel<GeneralWhatsAppProviderViewModel>();
        }

        //Update generalWhatsAppProvider.
        public virtual GeneralWhatsAppProviderViewModel UpdateWhatsAppProvider(GeneralWhatsAppProviderViewModel generalWhatsAppProviderViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.WhatsAppProviderMaster.ToString(), TraceLevel.Info);
                GeneralWhatsAppProviderResponse response = _generalWhatsAppProviderClient.UpdateWhatsAppProvider(generalWhatsAppProviderViewModel.ToModel<GeneralWhatsAppProviderModel>());
                GeneralWhatsAppProviderModel generalWhatsAppProviderModel = response?.GeneralWhatsAppProviderModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.WhatsAppProviderMaster.ToString(), TraceLevel.Info);
                return IsNotNull(generalWhatsAppProviderModel) ? generalWhatsAppProviderModel.ToViewModel<GeneralWhatsAppProviderViewModel>() : (GeneralWhatsAppProviderViewModel)GetViewModelWithErrorMessage(new GeneralWhatsAppProviderViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.WhatsAppProviderMaster.ToString(), TraceLevel.Error);
                return (GeneralWhatsAppProviderViewModel)GetViewModelWithErrorMessage(generalWhatsAppProviderViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalWhatsAppProvider.
        public virtual bool DeleteWhatsAppProvider(string generalWhatsAppProviderId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.WhatsAppProviderMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalWhatsAppProviderClient.DeleteWhatsAppProvider(new ParameterModel { Ids = generalWhatsAppProviderId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.WhatsAppProviderMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralWhatsAppProviderMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.WhatsAppProviderMaster.ToString(), TraceLevel.Error);
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
                ColumnName = "Provider Name",
                ColumnCode = "ProviderName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Provider Code",
                ColumnCode = "ProviderCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "IsActive",
                ColumnCode = "DefaultFlag",
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return all WhatsAppProvider list from database 
        public virtual GeneralWhatsAppProviderListResponse GetWhatsAppProviderList()
        {
            GeneralWhatsAppProviderListResponse WhatsAppProviderList = _generalWhatsAppProviderClient.List(null, null, null, 1, int.MaxValue);
            return WhatsAppProviderList?.GeneralWhatsAppProviderList?.Count > 0 ? WhatsAppProviderList : new GeneralWhatsAppProviderListResponse();
        }
        #endregion

    }
    
}