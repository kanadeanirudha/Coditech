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
    public class GeneralCountryAgent : BaseAgent, IGeneralCountryAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralCountryClient _generalCountryClient;
        #endregion

        #region Public Constructor
        public GeneralCountryAgent(ICoditechLogging coditechLogging, IGeneralCountryClient generalCountryClient)
        {
            _coditechLogging = coditechLogging;
            _generalCountryClient = GetClient<IGeneralCountryClient>(generalCountryClient);
        }
        #endregion

        #region Public Methods
        public virtual GeneralCountryListViewModel GetCountryList(DataTableViewModel dataTableModel)
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

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "CountryName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralCountryListResponse response = _generalCountryClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralCountryListModel countryList = new GeneralCountryListModel { GeneralCountryList = response?.GeneralCountryList };
            GeneralCountryListViewModel listViewModel = new GeneralCountryListViewModel();
            listViewModel.GeneralCountryList = countryList?.GeneralCountryList?.ToViewModel<GeneralCountryViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralCountryList.Count, BindColumns());
            return listViewModel;
        }

        //Create General Country.
        public virtual GeneralCountryViewModel CreateCountry(GeneralCountryViewModel generalCountryViewModel)
        {
            try
            {
                GeneralCountryResponse response = _generalCountryClient.CreateCountry(generalCountryViewModel.ToModel<GeneralCountryModel>());
                GeneralCountryModel generalCountryModel = response?.GeneralCountryModel;
                return IsNotNull(generalCountryModel) ? generalCountryModel.ToViewModel<GeneralCountryViewModel>() : new GeneralCountryViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralCountryViewModel)GetViewModelWithErrorMessage(generalCountryViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralCountryViewModel)GetViewModelWithErrorMessage(generalCountryViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Error);
                return (GeneralCountryViewModel)GetViewModelWithErrorMessage(generalCountryViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general Country by general country master id.
        public virtual GeneralCountryViewModel GetCountry(short generalCountryId)
        {
            GeneralCountryResponse response = _generalCountryClient.GetCountry(generalCountryId);
            return response?.GeneralCountryModel.ToViewModel<GeneralCountryViewModel>();
        }

        //Update generalCountry.
        public virtual GeneralCountryViewModel UpdateCountry(GeneralCountryViewModel generalCountryViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Info);
                GeneralCountryResponse response = _generalCountryClient.UpdateCountry(generalCountryViewModel.ToModel<GeneralCountryModel>());
                GeneralCountryModel generalCountryModel = response?.GeneralCountryModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Info);
                return IsNotNull(generalCountryModel) ? generalCountryModel.ToViewModel<GeneralCountryViewModel>() : (GeneralCountryViewModel)GetViewModelWithErrorMessage(new GeneralCountryViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralCountryViewModel)GetViewModelWithErrorMessage(generalCountryViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralCountryViewModel)GetViewModelWithErrorMessage(generalCountryViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Error);
                return (GeneralCountryViewModel)GetViewModelWithErrorMessage(generalCountryViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalCountry.
        public virtual bool DeleteCountry(string generalCountryId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalCountryClient.DeleteCountry(new ParameterModel { Ids = generalCountryId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralCountryMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Error);
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
                ColumnName = "Country Name",
                ColumnCode = "CountryName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Country Code",
                ColumnCode = "CountryCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Calling Code",
                ColumnCode = "CallingCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Default",
                ColumnCode = "DefaultFlag",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all country list from database 
        public virtual GeneralCountryListResponse GetCountryList()
        {
            GeneralCountryListResponse countryList = _generalCountryClient.List(null, null, null, 1, int.MaxValue);
            return countryList?.GeneralCountryList?.Count > 0 ? countryList : new GeneralCountryListResponse();
        }
        #endregion
    }
}
