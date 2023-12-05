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
    public class GeneralDesignationAgent : BaseAgent, IGeneralDesignationAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralDesignationClient _generalDesignationClient;
        #endregion

        #region Public Constructor
        public GeneralDesignationAgent(ICoditechLogging coditechLogging, IGeneralDesignationClient generalDesignationClient)
        {
            _coditechLogging = coditechLogging;
            _generalDesignationClient = GetClient<IGeneralDesignationClient>(generalDesignationClient);
        }
        #endregion

        #region Public Methods
        public virtual GeneralDesignationListViewModel GetDesignationList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("Description", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ShortCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "Description" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralDesignationListResponse response = _generalDesignationClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralDesignationListModel designationList = new GeneralDesignationListModel { GeneralDesignationList = response?.GeneralDesignationList };
            GeneralDesignationListViewModel listViewModel = new GeneralDesignationListViewModel();
            listViewModel.GeneralDesignationList = designationList?.GeneralDesignationList?.ToViewModel<GeneralDesignationViewModel>().ToList();
            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralDesignationList.Count, BindColumns());
            return listViewModel;
        }

        //Create General Designation.
        public virtual GeneralDesignationViewModel CreateDesignation(GeneralDesignationViewModel generalDesignationViewModel)
        {
            try
            {
                GeneralDesignationResponse response = _generalDesignationClient.CreateDesignation(generalDesignationViewModel.ToModel<GeneralDesignationModel>());
                GeneralDesignationModel generalDesignationModel = response?.GeneralDesignationModel;
                return IsNotNull(generalDesignationModel) ? generalDesignationModel.ToViewModel<GeneralDesignationViewModel>() : new GeneralDesignationViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Designation.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralDesignationViewModel)GetViewModelWithErrorMessage(generalDesignationViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralDesignationViewModel)GetViewModelWithErrorMessage(generalDesignationViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Designation.ToString(), TraceLevel.Error);
                return (GeneralDesignationViewModel)GetViewModelWithErrorMessage(generalDesignationViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general Designation by general designation master id.
        public virtual GeneralDesignationViewModel GetDesignation(short designationId)
        {
            GeneralDesignationResponse response = _generalDesignationClient.GetDesignation(designationId);
            return response?.GeneralDesignationModel.ToViewModel<GeneralDesignationViewModel>();
        }

        //Update Designation.
        public virtual GeneralDesignationViewModel UpdateDesignation(GeneralDesignationViewModel generalDesignationViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.Designation.ToString(), TraceLevel.Info);
                GeneralDesignationResponse response = _generalDesignationClient.UpdateDesignation(generalDesignationViewModel.ToModel<GeneralDesignationModel>());
                GeneralDesignationModel generalDesignationModel = response?.GeneralDesignationModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.Designation.ToString(), TraceLevel.Info);
                return IsNotNull(generalDesignationModel) ? generalDesignationModel.ToViewModel<GeneralDesignationViewModel>() : (GeneralDesignationViewModel)GetViewModelWithErrorMessage(new GeneralDesignationViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Designation.ToString(), TraceLevel.Error);
                return (GeneralDesignationViewModel)GetViewModelWithErrorMessage(generalDesignationViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete Designation.
        public virtual bool DeleteDesignation(string designationId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.Designation.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalDesignationClient.DeleteDesignation(new ParameterModel { Ids = designationId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Designation.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralDesignationMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Designation.ToString(), TraceLevel.Error);
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
                ColumnName = "Designation Name",
                ColumnCode = "Description",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Print Short Desc",
                ColumnCode = "DesignationLevel",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Grade",
                ColumnCode = "Grade",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Dept Short Code",
                ColumnCode = "ShortCode",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Active",
                ColumnCode = "IsActive",
            });
            return datatableColumnList;
        }
        #endregion
    }
}
