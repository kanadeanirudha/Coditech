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
    public class GeneralEnumaratorGroupAgent : BaseAgent, IGeneralEnumaratorGroupAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralEnumaratorGroupClient _GeneralEnumaratorGroupClient;
        #endregion

        #region Public Constructor
        public GeneralEnumaratorGroupAgent(ICoditechLogging coditechLogging, IGeneralEnumaratorGroupClient GeneralEnumaratorGroupClient)
        {
            _coditechLogging = coditechLogging;
            _GeneralEnumaratorGroupClient = GetClient<IGeneralEnumaratorGroupClient>(GeneralEnumaratorGroupClient);
        }
        #endregion

        #region Public Methods
        public GeneralEnumaratorGroupViewModel GetCountryList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("CountryName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("CountryCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "CountryName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralEnumaratorGroupListResponse response = _GeneralEnumaratorGroupClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralEnumaratorGroupListModel countryList = new GeneralEnumaratorGroupListModel { GeneralEnumaratorGroupList = response?.GeneralEnumaratorGroupList };
            GeneralEnumaratorGroupViewModel listViewModel = new GeneralEnumaratorGroupListViewModel();
            listViewModel.GeneralEnumaratorGroupList = countryList?.GeneralEnumaratorGroupList?.ToViewModel<GeneralEnumaratorGroupViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralEnumaratorGroupList.Count, BindColumns());
            return listViewModel;
        }

        //Create General Country.
        public virtual GeneralEnumaratorGroupViewModel CreateCountry(GeneralEnumaratorGroupViewModel GeneralEnumaratorGroupViewModel)
        {
            try
            {
                GeneralEnumaratorGroupResponse response = _GeneralEnumaratorGroupClient.CreateCountry(GeneralEnumaratorGroupViewModel.ToModel<GeneralEnumaratorGroupModel>());
                GeneralEnumaratorGroupModel GeneralEnumaratorGroupModel = response?.GeneralEnumaratorGroupModel;
                return IsNotNull(GeneralEnumaratorGroupModel) ? GeneralEnumaratorGroupModel.ToViewModel<GeneralEnumaratorGroupViewModel>() : new GeneralEnumaratorGroupViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralEnumaratorGroupViewModel)GetViewModelWithErrorMessage(GeneralEnumaratorGroupViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralEnumaratorGroupViewModel)GetViewModelWithErrorMessage(GeneralEnumaratorGroupViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Error);
                return (GeneralEnumaratorGroupViewModel)GetViewModelWithErrorMessage(GeneralEnumaratorGroupViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general Country by general country master id.
        public virtual GeneralEnumaratorGroupViewModel GetCountry(short GeneralEnumaratorGroupId)
        {
            GeneralEnumaratorGroupResponse response = _GeneralEnumaratorGroupClient.GetCountry(GeneralEnumaratorGroupId);
            return response?.GeneralEnumaratorGroupModel.ToViewModel<GeneralEnumaratorGroupViewModel>();
        }

        //Update GeneralEnumaratorGroup.
        public virtual GeneralEnumaratorGroupViewModel UpdateCountry(GeneralEnumaratorGroupViewModel GeneralEnumaratorGroupViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Info);
                GeneralEnumaratorGroupResponse response = _GeneralEnumaratorGroupClient.UpdateCountry(GeneralEnumaratorGroupViewModel.ToModel<GeneralEnumaratorGroupModel>());
                GeneralEnumaratorGroupModel GeneralEnumaratorGroupModel = response?.GeneralEnumaratorGroupModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Info);
                return IsNotNull(GeneralEnumaratorGroupModel) ? GeneralEnumaratorGroupModel.ToViewModel<GeneralEnumaratorGroupViewModel>() : (GeneralEnumaratorGroupViewModel)GetViewModelWithErrorMessage(new GeneralEnumaratorGroupViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Error);
                return (GeneralEnumaratorGroupViewModel)GetViewModelWithErrorMessage(GeneralEnumaratorGroupViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete GeneralEnumaratorGroup.
        public virtual bool DeleteCountry(string GeneralEnumaratorGroupId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _GeneralEnumaratorGroupClient.DeleteCountry(new ParameterModel { Ids = GeneralEnumaratorGroupId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralEnumaratorGroupMaster;
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
                ColumnName = "Is Default",
                ColumnCode = "DefaultFlag",
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all country list from database 
        public GeneralEnumaratorGroupListResponse GetCountryList()
        {
            GeneralEnumaratorGroupListResponse countryList = _GeneralEnumaratorGroupClient.List(null, null, null, 1, int.MaxValue);
            return countryList?.GeneralEnumaratorGroupList?.Count > 0 ? countryList : new GeneralEnumaratorGroupListResponse();
        }
        #endregion
    }
}
