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
    public class GeneralDistrictAgent : BaseAgent, IGeneralDistrictAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralDistrictClient _generalDistrictClient;
        #endregion

        #region Public Constructor
        public GeneralDistrictAgent(ICoditechLogging coditechLogging, IGeneralDistrictClient generalDistrictClient)
        {
            _coditechLogging = coditechLogging;
            _generalDistrictClient = GetClient<IGeneralDistrictClient>(generalDistrictClient);
        }
        #endregion

        #region Public Methods
        public virtual GeneralDistrictListViewModel GetDistrictList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("DistrictName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("RegionName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "DistrictName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralDistrictListResponse response = _generalDistrictClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralDistrictListModel districtList = new GeneralDistrictListModel { GeneralDistrictList = response?.GeneralDistrictList };
            GeneralDistrictListViewModel listViewModel = new GeneralDistrictListViewModel();
            listViewModel.GeneralDistrictList = districtList?.GeneralDistrictList?.ToViewModel<GeneralDistrictViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralDistrictList.Count, BindColumns());
            return listViewModel;
        }

        //Create General District.
        public virtual GeneralDistrictViewModel CreateDistrict(GeneralDistrictViewModel generalDistrictViewModel)
        {
            try
            {
                GeneralDistrictResponse response = _generalDistrictClient.CreateDistrict(generalDistrictViewModel.ToModel<GeneralDistrictModel>());
                GeneralDistrictModel generalDistrictModel = response?.GeneralDistrictModel;
                return IsNotNull(generalDistrictModel) ? generalDistrictModel.ToViewModel<GeneralDistrictViewModel>() : new GeneralDistrictViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.District.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralDistrictViewModel)GetViewModelWithErrorMessage(generalDistrictViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralDistrictViewModel)GetViewModelWithErrorMessage(generalDistrictViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.District.ToString(), TraceLevel.Error);
                return (GeneralDistrictViewModel)GetViewModelWithErrorMessage(generalDistrictViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general District by general district master id.
        public virtual GeneralDistrictViewModel GetDistrict(short generalDistrictId)
        {
            GeneralDistrictResponse response = _generalDistrictClient.GetDistrict(generalDistrictId);
            return response?.GeneralDistrictModel.ToViewModel<GeneralDistrictViewModel>();
        }

        //Update generalDistrict.
        public virtual GeneralDistrictViewModel UpdateDistrict(GeneralDistrictViewModel generalDistrictViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.District.ToString(), TraceLevel.Info);
                GeneralDistrictResponse response = _generalDistrictClient.UpdateDistrict(generalDistrictViewModel.ToModel<GeneralDistrictModel>());
                GeneralDistrictModel generalDistrictModel = response?.GeneralDistrictModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.District.ToString(), TraceLevel.Info);
                return IsNotNull(generalDistrictModel) ? generalDistrictModel.ToViewModel<GeneralDistrictViewModel>() : (GeneralDistrictViewModel)GetViewModelWithErrorMessage(new GeneralDistrictViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.District.ToString(), TraceLevel.Error);
                return (GeneralDistrictViewModel)GetViewModelWithErrorMessage(generalDistrictViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalDistrict.
        public virtual bool DeleteDistrict(string generalDistrictId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.District.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalDistrictClient.DeleteDistrict(new ParameterModel { Ids = generalDistrictId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.District.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralDistrictMaster;
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
                ColumnName = "District Name",
                ColumnCode = "DistrictName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Region",
                ColumnCode = "GeneralRegionMasterId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Country",
                ColumnCode = "GeneralCountryMasterId",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion       
    }
}
