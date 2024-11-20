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
    public class GeneralTaxGroupAgent : BaseAgent, IGeneralTaxGroupAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralTaxGroupClient _generalTaxGroupClient;
        #endregion

        #region Public Constructor
        public GeneralTaxGroupAgent(ICoditechLogging coditechLogging, IGeneralTaxGroupClient generalTaxGroupClient)
        {
            _coditechLogging = coditechLogging;
            _generalTaxGroupClient = GetClient<IGeneralTaxGroupClient>(generalTaxGroupClient);
        }
        #endregion

        #region Public Methods
        public virtual GeneralTaxGroupMasterListViewModel GetTaxGroupMasterList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("TaxGroupName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("TaxGroupRate", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "TaxGroupName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralTaxGroupListResponse response = _generalTaxGroupClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralTaxGroupMasterListModel taxGroupMasterList = new GeneralTaxGroupMasterListModel { GeneralTaxGroupMasterList = response?.GeneralTaxGroupMasterList };
            GeneralTaxGroupMasterListViewModel listViewModel = new GeneralTaxGroupMasterListViewModel();
            listViewModel.GeneralTaxGroupMasterList = taxGroupMasterList?.GeneralTaxGroupMasterList?.ToViewModel<GeneralTaxGroupMasterViewModel>().ToList();
            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralTaxGroupMasterList.Count, BindColumns());
            return listViewModel;
        }

        //Create General TaxGroupMaster.
        public virtual GeneralTaxGroupMasterViewModel CreateTaxGroupMaster(GeneralTaxGroupMasterViewModel generalTaxGroupMasterViewModel)
        {
            try
            {
                GeneralTaxGroupResponse response = _generalTaxGroupClient.CreateTaxGroupMaster(generalTaxGroupMasterViewModel.ToModel<GeneralTaxGroupModel>());
                GeneralTaxGroupModel generalTaxGroupModel = response?.GeneralTaxGroupModel;
                return IsNotNull(generalTaxGroupModel) ? generalTaxGroupModel.ToViewModel<GeneralTaxGroupMasterViewModel>() : new GeneralTaxGroupMasterViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralTaxGroupMasterViewModel)GetViewModelWithErrorMessage(generalTaxGroupMasterViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralTaxGroupMasterViewModel)GetViewModelWithErrorMessage(generalTaxGroupMasterViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Error);
                return (GeneralTaxGroupMasterViewModel)GetViewModelWithErrorMessage(generalTaxGroupMasterViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general TaxGroupMaster by general taxGroup master id.
        public virtual GeneralTaxGroupMasterViewModel GetTaxGroupMaster(short taxGroupMasterId)
        {
            GeneralTaxGroupResponse response = _generalTaxGroupClient.GetTaxGroupMaster(taxGroupMasterId);
            return response?.GeneralTaxGroupModel.ToViewModel<GeneralTaxGroupMasterViewModel>();
        }

        //Update TaxGroupMaster.
        public virtual GeneralTaxGroupMasterViewModel UpdateTaxGroupMaster(GeneralTaxGroupMasterViewModel generalTaxGroupMasterViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Info);
                GeneralTaxGroupResponse response = _generalTaxGroupClient.UpdateTaxGroupMaster(generalTaxGroupMasterViewModel.ToModel<GeneralTaxGroupModel>());
                GeneralTaxGroupModel generalTaxGroupModel = response?.GeneralTaxGroupModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Info);
                return IsNotNull(generalTaxGroupModel) ? generalTaxGroupModel.ToViewModel<GeneralTaxGroupMasterViewModel>() : (GeneralTaxGroupMasterViewModel)GetViewModelWithErrorMessage(new GeneralTaxGroupMasterViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Error);
                return (GeneralTaxGroupMasterViewModel)GetViewModelWithErrorMessage(generalTaxGroupMasterViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete TaxGroupMaster.
        public virtual bool DeleteTaxGroupMaster(string taxGroupMasterId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalTaxGroupClient.DeleteTaxGroupMaster(new ParameterModel { Ids = taxGroupMasterId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralTaxGroupMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Error);
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
                ColumnName = "Tax Group Name",
                ColumnCode = "TaxGroupName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Tax Group Rate",
                ColumnCode = "TaxGroupRate",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Other State",
                ColumnCode = "IsOtherState",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
    }
}
