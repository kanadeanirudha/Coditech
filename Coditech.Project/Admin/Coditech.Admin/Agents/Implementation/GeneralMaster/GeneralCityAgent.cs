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
    public class GeneralCityAgent : BaseAgent, IGeneralCityAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralCityClient _generalCityClient;
        #endregion

        #region Public Constructor
        public GeneralCityAgent(ICoditechLogging coditechLogging, IGeneralCityClient generalCityClient)
        {
            _coditechLogging = coditechLogging;
            _generalCityClient = GetClient<IGeneralCityClient>(generalCityClient);
        }
        #endregion

        #region Public Methods
        public GeneralCityListViewModel GetCityList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("CityName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("RegionName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }
            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "CityName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralCityListResponse response = _generalCityClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralCityListModel cityList = new GeneralCityListModel { GeneralCityList = response?.GeneralCityList };
            GeneralCityListViewModel listViewModel = new GeneralCityListViewModel();
            listViewModel.GeneralCityList = cityList?.GeneralCityList?.ToViewModel<GeneralCityViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralCityList.Count, BindColumns());
            return listViewModel;
        }

        //Create General City.
        public virtual GeneralCityViewModel CreateCity(GeneralCityViewModel generalCityViewModel)
        {
            try
            {
                GeneralCityResponse response = _generalCityClient.CreateCity(generalCityViewModel.ToModel<GeneralCityModel>());
                GeneralCityModel generalCityModel = response?.GeneralCityModel;
                return IsNotNull(generalCityModel) ? generalCityModel.ToViewModel<GeneralCityViewModel>() : new GeneralCityViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.City.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralCityViewModel)GetViewModelWithErrorMessage(generalCityViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralCityViewModel)GetViewModelWithErrorMessage(generalCityViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.City.ToString(), TraceLevel.Error);
                return (GeneralCityViewModel)GetViewModelWithErrorMessage(generalCityViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general City by general city master id.
        public virtual GeneralCityViewModel GetCity(int cityId)
        {
            GeneralCityResponse response = _generalCityClient.GetCity(cityId);
            return response?.GeneralCityModel.ToViewModel<GeneralCityViewModel>();
        }

        //Update City.
        public virtual GeneralCityViewModel UpdateCity(GeneralCityViewModel generalCityViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.City.ToString(), TraceLevel.Info);
                GeneralCityResponse response = _generalCityClient.UpdateCity(generalCityViewModel.ToModel<GeneralCityModel>());
                GeneralCityModel generalCityModel = response?.GeneralCityModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.City.ToString(), TraceLevel.Info);
                return IsNotNull(generalCityModel) ? generalCityModel.ToViewModel<GeneralCityViewModel>() : (GeneralCityViewModel)GetViewModelWithErrorMessage(new GeneralCityViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.City.ToString(), TraceLevel.Error);
                return (GeneralCityViewModel)GetViewModelWithErrorMessage(generalCityViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete City.
        public virtual bool DeleteCity(string cityId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.City.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalCityClient.DeleteCity(new ParameterModel { Ids = cityId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.City.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralCityMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.City.ToString(), TraceLevel.Error);
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
                ColumnName = "City",
                ColumnCode = "CityName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Region",
                ColumnCode = "RegionName",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Default",
                ColumnCode = "DefaultFlag",
            });
            return datatableColumnList;
        }
        #endregion
        
    }
}
