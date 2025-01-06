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
    public class GeneralMeasurementUnitAgent : BaseAgent, IGeneralMeasurementUnitAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralMeasurementUnitClient _generalMeasurementUnitClient;
        #endregion

        #region Public Constructor
        public GeneralMeasurementUnitAgent(ICoditechLogging coditechLogging, IGeneralMeasurementUnitClient generalMeasurementUnitClient)
        {
            _coditechLogging = coditechLogging;
            _generalMeasurementUnitClient = GetClient<IGeneralMeasurementUnitClient>(generalMeasurementUnitClient);
        }
        #endregion

        #region Public Methods
        public virtual GeneralMeasurementUnitListViewModel GetMeasurementUnitList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("MeasurementUnitDisplayName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("MeasurementUnitShortCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "MeasurementUnitDisplayName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralMeasurementUnitListResponse response = _generalMeasurementUnitClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralMeasurementUnitListModel MeasurementUnitList = new GeneralMeasurementUnitListModel { GeneralMeasurementUnitList = response?.GeneralMeasurementUnitList };
            GeneralMeasurementUnitListViewModel listViewModel = new GeneralMeasurementUnitListViewModel();
            listViewModel.GeneralMeasurementUnitList = MeasurementUnitList?.GeneralMeasurementUnitList?.ToViewModel<GeneralMeasurementUnitViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralMeasurementUnitList.Count, BindColumns());
            return listViewModel;
        }

        //Create General MeasurementUnit.
        public virtual GeneralMeasurementUnitViewModel CreateMeasurementUnit(GeneralMeasurementUnitViewModel generalMeasurementUnitViewModel)
        {
            try
            {
                GeneralMeasurementUnitResponse response = _generalMeasurementUnitClient.CreateMeasurementUnit(generalMeasurementUnitViewModel.ToModel<GeneralMeasurementUnitModel>());
                GeneralMeasurementUnitModel generalMeasurementUnitModel = response?.GeneralMeasurementUnitModel;
                return IsNotNull(generalMeasurementUnitModel) ? generalMeasurementUnitModel.ToViewModel<GeneralMeasurementUnitViewModel>() : new GeneralMeasurementUnitViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MeasurementUnitMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralMeasurementUnitViewModel)GetViewModelWithErrorMessage(generalMeasurementUnitViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralMeasurementUnitViewModel)GetViewModelWithErrorMessage(generalMeasurementUnitViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MeasurementUnitMaster.ToString(), TraceLevel.Error);
                return (GeneralMeasurementUnitViewModel)GetViewModelWithErrorMessage(generalMeasurementUnitViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general MeasurementUnit by general MeasurementUnit master id.
        public virtual GeneralMeasurementUnitViewModel GetMeasurementUnit(short generalMeasurementUnitId)
        {
            GeneralMeasurementUnitResponse response = _generalMeasurementUnitClient.GetMeasurementUnit(generalMeasurementUnitId);
            return response?.GeneralMeasurementUnitModel.ToViewModel<GeneralMeasurementUnitViewModel>();
        }

        //Update generalMeasurementUnit.
        public virtual GeneralMeasurementUnitViewModel UpdateMeasurementUnit(GeneralMeasurementUnitViewModel generalMeasurementUnitViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.MeasurementUnitMaster.ToString(), TraceLevel.Info);
                GeneralMeasurementUnitResponse response = _generalMeasurementUnitClient.UpdateMeasurementUnit(generalMeasurementUnitViewModel.ToModel<GeneralMeasurementUnitModel>());
                GeneralMeasurementUnitModel generalMeasurementUnitModel = response?.GeneralMeasurementUnitModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.MeasurementUnitMaster.ToString(), TraceLevel.Info);
                return IsNotNull(generalMeasurementUnitModel) ? generalMeasurementUnitModel.ToViewModel<GeneralMeasurementUnitViewModel>() : (GeneralMeasurementUnitViewModel)GetViewModelWithErrorMessage(new GeneralMeasurementUnitViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MeasurementUnitMaster.ToString(), TraceLevel.Error);
                return (GeneralMeasurementUnitViewModel)GetViewModelWithErrorMessage(generalMeasurementUnitViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalMeasurementUnit.
        public virtual bool DeleteMeasurementUnit(string generalMeasurementUnitId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.MeasurementUnitMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalMeasurementUnitClient.DeleteMeasurementUnit(new ParameterModel { Ids = generalMeasurementUnitId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MeasurementUnitMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralMeasurementUnitMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MeasurementUnitMaster.ToString(), TraceLevel.Error);
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
                ColumnName = "Unit Name",
                ColumnCode = "MeasurementUnitDisplayName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Unit Code",
                ColumnCode = "MeasurementUnitShortCode",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all MeasurementUnit list from database 
        public virtual GeneralMeasurementUnitListResponse GetMeasurementUnitList()
        {
            GeneralMeasurementUnitListResponse MeasurementUnitList = _generalMeasurementUnitClient.List(null, null, null, 1, int.MaxValue);
            return MeasurementUnitList?.GeneralMeasurementUnitList?.Count > 0 ? MeasurementUnitList : new GeneralMeasurementUnitListResponse();
        }
        #endregion
    }
}
