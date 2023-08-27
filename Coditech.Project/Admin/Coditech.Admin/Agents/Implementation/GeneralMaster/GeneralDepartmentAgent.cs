using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;

using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.Admin.Agents
{
    public class GeneralDepartmentAgent : BaseAgent, IGeneralDepartmentAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralDepartmentClient _generalDepartmentClient;
        #endregion

        #region Public Constructor
        public GeneralDepartmentAgent(ICoditechLogging coditechLogging, IGeneralDepartmentClient generalDepartmentClient)
        {
            _coditechLogging = coditechLogging;
            _generalDepartmentClient = GetClient<IGeneralDepartmentClient>(generalDepartmentClient);
        }
        #endregion
        #region Public Methods

        public GeneralDepartmentListViewModel GetDepartmentList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("DepartmentName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("DepartmentShortCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn, dataTableModel.SortBy);
            GeneralDepartmentListResponse response = _generalDepartmentClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralDepartmentListModel departmentList = new GeneralDepartmentListModel { GeneralDepartmentList = response?.GeneralDepartmentList };
            GeneralDepartmentListViewModel listViewModel = new GeneralDepartmentListViewModel();
            listViewModel.GeneralDepartmentList = departmentList?.GeneralDepartmentList?.ToViewModel<GeneralDepartmentViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralDepartmentList.Count);

            return listViewModel;
        }

        //Create General Department.
        public virtual GeneralDepartmentViewModel CreateDepartment(GeneralDepartmentViewModel generalDepartmentViewModel)
        {
            try
            {
                GeneralDepartmentResponse response = _generalDepartmentClient.CreateDepartment(generalDepartmentViewModel.ToModel<GeneralDepartmentModel>());
                GeneralDepartmentModel generalDepartmentModel = response?.GeneralDepartmentModel;
                return IsNotNull(generalDepartmentModel) ? generalDepartmentModel.ToViewModel<GeneralDepartmentViewModel>() : new GeneralDepartmentViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralDepartmentViewModel)GetViewModelWithErrorMessage(generalDepartmentViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralDepartmentViewModel)GetViewModelWithErrorMessage(generalDepartmentViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Error);
                return (GeneralDepartmentViewModel)GetViewModelWithErrorMessage(generalDepartmentViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general Department by general department master id.
        public virtual GeneralDepartmentViewModel GetDepartment(int generalDepartmentId)
        {
            GeneralDepartmentResponse response = _generalDepartmentClient.GetDepartment(generalDepartmentId);
            return response?.GeneralDepartmentModel.ToViewModel<GeneralDepartmentViewModel>();
        }

        //Update generalDepartment.
        public virtual GeneralDepartmentViewModel UpdateDepartment(GeneralDepartmentViewModel generalDepartmentViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Info);
                GeneralDepartmentResponse response = _generalDepartmentClient.UpdateDepartment(generalDepartmentViewModel.ToModel<GeneralDepartmentModel>());
                GeneralDepartmentModel generalDepartmentModel = response?.GeneralDepartmentModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Info);
                return IsNotNull(generalDepartmentModel) ? generalDepartmentModel.ToViewModel<GeneralDepartmentViewModel>() : (GeneralDepartmentViewModel)GetViewModelWithErrorMessage(new GeneralDepartmentViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Error);
                return (GeneralDepartmentViewModel)GetViewModelWithErrorMessage(generalDepartmentViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalDepartment.
        public virtual bool DeleteDepartment(string generalDepartmentId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalDepartmentClient.DeleteDepartment(new ParameterModel { Ids = generalDepartmentId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralDepartmentMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }
        #endregion
    }
}
