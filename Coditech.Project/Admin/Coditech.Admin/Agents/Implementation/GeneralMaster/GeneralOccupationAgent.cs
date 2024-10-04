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
    public class GeneralOccupationAgent : BaseAgent, IGeneralOccupationAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralOccupationClient _generalOccupationClient;
        #endregion

        #region Public Constructor
        public GeneralOccupationAgent(ICoditechLogging coditechLogging, IGeneralOccupationClient generalOccupationClient)
        {
            _coditechLogging = coditechLogging;
            _generalOccupationClient = GetClient<IGeneralOccupationClient>(generalOccupationClient);
        }
        #endregion

        #region Public Methods
        public virtual GeneralOccupationListViewModel GetOccupationList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("OccupationName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "OccupationName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralOccupationListResponse response = _generalOccupationClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralOccupationListModel OccupationList = new GeneralOccupationListModel { GeneralOccupationList = response?.GeneralOccupationList };
            GeneralOccupationListViewModel listViewModel = new GeneralOccupationListViewModel();
            listViewModel.GeneralOccupationList = OccupationList?.GeneralOccupationList?.ToViewModel<GeneralOccupationViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralOccupationList.Count, BindColumns());
            return listViewModel;
        }

        //Create General Occupation.
        public virtual GeneralOccupationViewModel CreateOccupation(GeneralOccupationViewModel generalOccupationViewModel)
        {
            try
            {
                GeneralOccupationResponse response = _generalOccupationClient.CreateOccupation(generalOccupationViewModel.ToModel<GeneralOccupationModel>());
                GeneralOccupationModel generalOccupationModel = response?.GeneralOccupationModel;
                return IsNotNull(generalOccupationModel) ? generalOccupationModel.ToViewModel<GeneralOccupationViewModel>() : new GeneralOccupationViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OccupationMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralOccupationViewModel)GetViewModelWithErrorMessage(generalOccupationViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralOccupationViewModel)GetViewModelWithErrorMessage(generalOccupationViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OccupationMaster.ToString(), TraceLevel.Error);
                return (GeneralOccupationViewModel)GetViewModelWithErrorMessage(generalOccupationViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general Occupation by general Occupation master id.
        public virtual GeneralOccupationViewModel GetOccupation(short generalOccupationId)
        {
            GeneralOccupationResponse response = _generalOccupationClient.GetOccupation(generalOccupationId);
            return response?.GeneralOccupationModel.ToViewModel<GeneralOccupationViewModel>();
        }

        //Update generalOccupation.
        public virtual GeneralOccupationViewModel UpdateOccupation(GeneralOccupationViewModel generalOccupationViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.OccupationMaster.ToString(), TraceLevel.Info);
                GeneralOccupationResponse response = _generalOccupationClient.UpdateOccupation(generalOccupationViewModel.ToModel<GeneralOccupationModel>());
                GeneralOccupationModel generalOccupationModel = response?.GeneralOccupationModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.OccupationMaster.ToString(), TraceLevel.Info);
                return IsNotNull(generalOccupationModel) ? generalOccupationModel.ToViewModel<GeneralOccupationViewModel>() : (GeneralOccupationViewModel)GetViewModelWithErrorMessage(new GeneralOccupationViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OccupationMaster.ToString(), TraceLevel.Error);
                return (GeneralOccupationViewModel)GetViewModelWithErrorMessage(generalOccupationViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalOccupation.
        public virtual bool DeleteOccupation(string generalOccupationId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.OccupationMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalOccupationClient.DeleteOccupation(new ParameterModel { Ids = generalOccupationId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OccupationMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralOccupationMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OccupationMaster.ToString(), TraceLevel.Error);
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
                ColumnName = "Occupation Name",
                ColumnCode = "OccupationName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Display Order",
                ColumnCode = "DisplayOrder",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all Occupation list from database 
        public virtual GeneralOccupationListResponse GetOccupationList()
        {
            GeneralOccupationListResponse OccupationList = _generalOccupationClient.List(null, null, null, 1, int.MaxValue);
            return OccupationList?.GeneralOccupationList?.Count > 0 ? OccupationList : new GeneralOccupationListResponse();
        }
        #endregion
    }
}
