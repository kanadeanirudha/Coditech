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
    public class GeneralRegionAgent : BaseAgent, IGeneralRegionAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralRegionClient _generalRegionClient;
        #endregion

        #region Public Constructor
        public GeneralRegionAgent(ICoditechLogging coditechLogging, IGeneralRegionClient generalRegionClient)
        {
            _coditechLogging = coditechLogging;
            _generalRegionClient = GetClient<IGeneralRegionClient>(generalRegionClient);
        }
        #endregion

        #region Public Methods
        public virtual GeneralRegionListViewModel GetRegionList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("RegionName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ShortName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("CountryName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "RegionName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralRegionListResponse response = _generalRegionClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralRegionListModel regionList = new GeneralRegionListModel { GeneralRegionList = response?.GeneralRegionList };
            GeneralRegionListViewModel listViewModel = new GeneralRegionListViewModel();
            listViewModel.GeneralRegionList = regionList?.GeneralRegionList?.ToViewModel<GeneralRegionViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralRegionList.Count, BindColumns());
            return listViewModel;
        }

        //Create General Region.
        public virtual GeneralRegionViewModel CreateRegion(GeneralRegionViewModel generalRegionViewModel)
        {
            try
            {
                GeneralRegionResponse response = _generalRegionClient.CreateRegion(generalRegionViewModel.ToModel<GeneralRegionModel>());
                GeneralRegionModel generalRegionModel = response?.GeneralRegionModel;
                return IsNotNull(generalRegionModel) ? generalRegionModel.ToViewModel<GeneralRegionViewModel>() : new GeneralRegionViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Region.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralRegionViewModel)GetViewModelWithErrorMessage(generalRegionViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralRegionViewModel)GetViewModelWithErrorMessage(generalRegionViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Region.ToString(), TraceLevel.Error);
                return (GeneralRegionViewModel)GetViewModelWithErrorMessage(generalRegionViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general Region by general region master id.
        public virtual GeneralRegionViewModel GetRegion(short generalRegionId)
        {
            GeneralRegionResponse response = _generalRegionClient.GetRegion(generalRegionId);
            return response?.GeneralRegionModel.ToViewModel<GeneralRegionViewModel>();
        }

        //Update generalRegion.
        public virtual GeneralRegionViewModel UpdateRegion(GeneralRegionViewModel generalRegionViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.Region.ToString(), TraceLevel.Info);
                GeneralRegionResponse response = _generalRegionClient.UpdateRegion(generalRegionViewModel.ToModel<GeneralRegionModel>());
                GeneralRegionModel generalRegionModel = response?.GeneralRegionModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.Region.ToString(), TraceLevel.Info);
                return IsNotNull(generalRegionModel) ? generalRegionModel.ToViewModel<GeneralRegionViewModel>() : (GeneralRegionViewModel)GetViewModelWithErrorMessage(new GeneralRegionViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Region.ToString(), TraceLevel.Error);
                return (GeneralRegionViewModel)GetViewModelWithErrorMessage(generalRegionViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalRegion.
        public virtual bool DeleteRegion(string generalRegionId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.Region.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalRegionClient.DeleteRegion(new ParameterModel { Ids = generalRegionId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Region.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralRegionMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Region.ToString(), TraceLevel.Error);
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
                ColumnName = "State",
                ColumnCode = "RegionName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "State Code",
                ColumnCode = "ShortName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Country",
                ColumnCode = "CountryName",
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
    }
}
