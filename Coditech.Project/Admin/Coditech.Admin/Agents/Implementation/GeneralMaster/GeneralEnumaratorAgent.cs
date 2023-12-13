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
    public class GeneralEnumaratorAgent : BaseAgent, IGeneralEnumaratorAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralEnumaratorClient _generalEnumaratorClient;
        #endregion

        #region Public Constructor
        public GeneralEnumaratorAgent(ICoditechLogging coditechLogging, IGeneralEnumaratorClient GeneralEnumaratorClient)
        {
            _coditechLogging = coditechLogging;
            _generalEnumaratorClient = GetClient<IGeneralEnumaratorClient>(GeneralEnumaratorClient);
        }
        #endregion

        #region Public Methods
        public GeneralEnumaratorListViewModel GetEnumaratorList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("EnumaratorName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("EnumaratorCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "EnumaratorName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralEnumaratorListResponse response = _generalEnumaratorClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralEnumaratorListModel EnumaratorList = new GeneralEnumaratorListModel { GeneralEnumaratorList = response?.GeneralEnumaratorList };
            GeneralEnumaratorListViewModel listViewModel = new GeneralEnumaratorListViewModel();
            listViewModel.GeneralEnumaratorList = EnumaratorList?.GeneralEnumaratorList?.ToViewModel<GeneralEnumaratorListViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralEnumaratorList.Count, BindColumns());
            return listViewModel;
        }

        //Create General Enumarator.
        public virtual GeneralEnumaratorViewModel CreateEnumarator(GeneralEnumaratorViewModel GeneralEnumaratorViewModel)
        {
            try
            {
                GeneralEnumaratorResponse response = _generalEnumaratorClient.CreateEnumarator(GeneralEnumaratorViewModel.ToModel<GeneralEnumaratorModel>());
                GeneralEnumaratorModel GeneralEnumaratorModel = response?.GeneralEnumaratorModel;
                return IsNotNull(GeneralEnumaratorModel) ? GeneralEnumaratorModel.ToViewModel<GeneralEnumaratorViewModel>() : new GeneralEnumaratorViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralEnumaratorViewModel)GetViewModelWithErrorMessage(GeneralEnumaratorViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralEnumaratorViewModel)GetViewModelWithErrorMessage(GeneralEnumaratorViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorMaster.ToString(), TraceLevel.Error);
                return (GeneralEnumaratorViewModel)GetViewModelWithErrorMessage(GeneralEnumaratorViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general Enumarator by general Enumarator master id.
        public virtual GeneralEnumaratorViewModel GetEnumarator(short GeneralEnumaratorMasterId)
        {
            GeneralEnumaratorResponse response = _generalEnumaratorClient.GetEnumarator(GeneralEnumaratorMasterId);
            return response?.GeneralEnumaratorModel.ToViewModel<GeneralEnumaratorViewModel>();
        }

        //Update GeneralEnumarator.
        public virtual GeneralEnumaratorViewModel UpdateEnumarator(GeneralEnumaratorViewModel GeneralEnumaratorViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.EnumaratorMaster.ToString(), TraceLevel.Info);
                GeneralEnumaratorResponse response = _generalEnumaratorClient.UpdateEnumarator(GeneralEnumaratorViewModel.ToModel<GeneralEnumaratorModel>());
                GeneralEnumaratorModel GeneralEnumaratorModel = response?.GeneralEnumaratorModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.EnumaratorMaster.ToString(), TraceLevel.Info);
                return IsNotNull(GeneralEnumaratorModel) ? GeneralEnumaratorModel.ToViewModel<GeneralEnumaratorViewModel>() : (GeneralEnumaratorViewModel)GetViewModelWithErrorMessage(new GeneralEnumaratorViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorMaster.ToString(), TraceLevel.Error);
                return (GeneralEnumaratorViewModel)GetViewModelWithErrorMessage(GeneralEnumaratorViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete GeneralEnumarator.
        public virtual bool DeleteEnumarator(string GeneralEnumaratorMasterId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.EnumaratorMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalEnumaratorClient.DeleteEnumarator(new ParameterModel { Ids = GeneralEnumaratorMasterId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralEnumaratorMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorMaster.ToString(), TraceLevel.Error);
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
                ColumnName = "Enumarator Name",
                ColumnCode = "EnumaratorName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Enumarator Code",
                ColumnCode = "EnumaratorCode",
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
        // it will return get all Enumarator list from database 
        public GeneralEnumaratorListResponse GetEnumaratorList()
        {
            GeneralEnumaratorListResponse EnumaratorList = _generalEnumaratorClient.List(null, null, null, 1, int.MaxValue);
            return EnumaratorList?.GeneralEnumaratorList?.Count > 0 ? EnumaratorList : new GeneralEnumaratorListResponse();
        }
        #endregion
    }
}
