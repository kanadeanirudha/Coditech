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
    public class GeneralLeadGenerationAgent : BaseAgent, IGeneralLeadGenerationAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralLeadGenerationClient _generalLeadGenerationClient;
        #endregion

        #region Public Constructor
        public GeneralLeadGenerationAgent(ICoditechLogging coditechLogging, IGeneralLeadGenerationClient generalLeadGenerationClient)
        {
            _coditechLogging = coditechLogging;
            _generalLeadGenerationClient = GetClient<IGeneralLeadGenerationClient>(generalLeadGenerationClient);
        }
        #endregion

        #region Public Methods
        public virtual GeneralLeadGenerationListViewModel GetLeadGenerationList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("FirstName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("LastName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "LeadGenerationName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralLeadGenerationListResponse response = _generalLeadGenerationClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralLeadGenerationListModel LeadGenerationList = new GeneralLeadGenerationListModel { GeneralLeadGenerationList = response?.GeneralLeadGenerationList };
            GeneralLeadGenerationListViewModel listViewModel = new GeneralLeadGenerationListViewModel();
            listViewModel.GeneralLeadGenerationList = LeadGenerationList?.GeneralLeadGenerationList?.ToViewModel<GeneralLeadGenerationViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralLeadGenerationList.Count, BindColumns());
            return listViewModel;
        }

        //Create General LeadGeneration.
        public virtual GeneralLeadGenerationViewModel CreateLeadGeneration(GeneralLeadGenerationViewModel generalLeadGenerationViewModel)
        {
            try
            {
                GeneralLeadGenerationResponse response = _generalLeadGenerationClient.CreateLeadGeneration(generalLeadGenerationViewModel.ToModel<GeneralLeadGenerationModel>());
                GeneralLeadGenerationModel generalLeadGenerationModel = response?.GeneralLeadGenerationModel;
                return IsNotNull(generalLeadGenerationModel) ? generalLeadGenerationModel.ToViewModel<GeneralLeadGenerationViewModel>() : new GeneralLeadGenerationViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LeadGenerationMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralLeadGenerationViewModel)GetViewModelWithErrorMessage(generalLeadGenerationViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralLeadGenerationViewModel)GetViewModelWithErrorMessage(generalLeadGenerationViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LeadGenerationMaster.ToString(), TraceLevel.Error);
                return (GeneralLeadGenerationViewModel)GetViewModelWithErrorMessage(generalLeadGenerationViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general LeadGeneration by general LeadGeneration master id.
        public virtual GeneralLeadGenerationViewModel GetLeadGeneration(long generalLeadGenerationId)
        {
            GeneralLeadGenerationResponse response = _generalLeadGenerationClient.GetLeadGeneration(generalLeadGenerationId);
            return response?.GeneralLeadGenerationModel.ToViewModel<GeneralLeadGenerationViewModel>();
        }

        //Update generalLeadGeneration.
        public virtual GeneralLeadGenerationViewModel UpdateLeadGeneration(GeneralLeadGenerationViewModel generalLeadGenerationViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.LeadGenerationMaster.ToString(), TraceLevel.Info);
                GeneralLeadGenerationResponse response = _generalLeadGenerationClient.UpdateLeadGeneration(generalLeadGenerationViewModel.ToModel<GeneralLeadGenerationModel>());
                GeneralLeadGenerationModel generalLeadGenerationModel = response?.GeneralLeadGenerationModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.LeadGenerationMaster.ToString(), TraceLevel.Info);
                return IsNotNull(generalLeadGenerationModel) ? generalLeadGenerationModel.ToViewModel<GeneralLeadGenerationViewModel>() : (GeneralLeadGenerationViewModel)GetViewModelWithErrorMessage(new GeneralLeadGenerationViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LeadGenerationMaster.ToString(), TraceLevel.Error);
                return (GeneralLeadGenerationViewModel)GetViewModelWithErrorMessage(generalLeadGenerationViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalLeadGeneration.
        public virtual bool DeleteLeadGeneration(string generalLeadGenerationId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.LeadGenerationMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalLeadGenerationClient.DeleteLeadGeneration(new ParameterModel { Ids = generalLeadGenerationId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LeadGenerationMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralLeadGenerationMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LeadGenerationMaster.ToString(), TraceLevel.Error);
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
                ColumnName = "First Name",
                ColumnCode = "FirstName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Last Name",
                ColumnCode = "LastName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Email",
                ColumnCode = "EmailId",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Mobile Number",
                ColumnCode = "MobileNumber",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Lead Generation Source",
                ColumnCode = "LeadGenerationSource",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Lead Generation Status",
                ColumnCode = "LeadGenerationStatus",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Converted",
                ColumnCode = "IsConverted",
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all LeadGeneration list from database 
        public virtual GeneralLeadGenerationListResponse GetLeadGenerationList()
        {
            GeneralLeadGenerationListResponse LeadGenerationList = _generalLeadGenerationClient.List(null, null, null, 1, int.MaxValue);
            return LeadGenerationList?.GeneralLeadGenerationList?.Count > 0 ? LeadGenerationList : new GeneralLeadGenerationListResponse();
        }
        #endregion
    }
}
