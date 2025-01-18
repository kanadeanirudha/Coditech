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

namespace Coditech.Admin.Agents
{
    public class HospitalPathologyTestGroupAgent : BaseAgent, IHospitalPathologyTestGroupAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IHospitalPathologyTestGroupClient _hospitalPathologyTestGroupClient;
        #endregion

        #region Public Constructor
        public HospitalPathologyTestGroupAgent(ICoditechLogging coditechLogging, IHospitalPathologyTestGroupClient hospitalPathologyTestGroupClient)
        {
            _coditechLogging = coditechLogging;
            _hospitalPathologyTestGroupClient = GetClient<IHospitalPathologyTestGroupClient>(hospitalPathologyTestGroupClient);
        }
        #endregion

        #region Public Methods
        public virtual HospitalPathologyTestGroupListViewModel GetHospitalPathologyTestGroupList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("PathologyTestGroupName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "PathologyTestGroupName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            HospitalPathologyTestGroupListResponse response = _hospitalPathologyTestGroupClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            HospitalPathologyTestGroupListModel hospitalPathologyTestGroupList = new HospitalPathologyTestGroupListModel { HospitalPathologyTestGroupList = response?.HospitalPathologyTestGroupList };
            HospitalPathologyTestGroupListViewModel listViewModel = new HospitalPathologyTestGroupListViewModel();
            listViewModel.HospitalPathologyTestGroupList = hospitalPathologyTestGroupList?.HospitalPathologyTestGroupList?.ToViewModel<HospitalPathologyTestGroupViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.HospitalPathologyTestGroupList.Count, BindColumns());
            return listViewModel;
        }

        //Create HospitalPathologyTestGroup.
        public virtual HospitalPathologyTestGroupViewModel CreateHospitalPathologyTestGroup(HospitalPathologyTestGroupViewModel hospitalPathologyTestGroupViewModel)
        {
            try
            {
                HospitalPathologyTestGroupResponse response = _hospitalPathologyTestGroupClient.CreateHospitalPathologyTestGroup(hospitalPathologyTestGroupViewModel.ToModel<HospitalPathologyTestGroupModel>());
                HospitalPathologyTestGroupModel hospitalPathologyTestGroupModel = response?.HospitalPathologyTestGroupModel;
                return HelperUtility.IsNotNull(hospitalPathologyTestGroupModel) ? hospitalPathologyTestGroupModel.ToViewModel<HospitalPathologyTestGroupViewModel>() : new HospitalPathologyTestGroupViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestGroup.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (HospitalPathologyTestGroupViewModel)GetViewModelWithErrorMessage(hospitalPathologyTestGroupViewModel, ex.ErrorMessage);
                    default:
                        return (HospitalPathologyTestGroupViewModel)GetViewModelWithErrorMessage(hospitalPathologyTestGroupViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestGroup.ToString(), TraceLevel.Error);
                return (HospitalPathologyTestGroupViewModel)GetViewModelWithErrorMessage(hospitalPathologyTestGroupViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general HospitalPathologyTestGroup by hospital Pathology TestGroup id.
        public virtual HospitalPathologyTestGroupViewModel GetHospitalPathologyTestGroup(int hospitalPathologyTestGroupId)
        {
            HospitalPathologyTestGroupResponse response = _hospitalPathologyTestGroupClient.GetHospitalPathologyTestGroup(hospitalPathologyTestGroupId);
            return response?.HospitalPathologyTestGroupModel.ToViewModel<HospitalPathologyTestGroupViewModel>();
        }

        //Update  HospitalPathologyTestGroup.
        public virtual HospitalPathologyTestGroupViewModel UpdateHospitalPathologyTestGroup(HospitalPathologyTestGroupViewModel hospitalPathologyTestGroupViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalPathologyTestGroup.ToString(), TraceLevel.Info);
                HospitalPathologyTestGroupResponse response = _hospitalPathologyTestGroupClient.UpdateHospitalPathologyTestGroup(hospitalPathologyTestGroupViewModel.ToModel<HospitalPathologyTestGroupModel>());
                HospitalPathologyTestGroupModel hospitalPathologyTestGroupModel = response?.HospitalPathologyTestGroupModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.HospitalPathologyTestGroup.ToString(), TraceLevel.Info);
                return HelperUtility.IsNotNull(hospitalPathologyTestGroupModel) ? hospitalPathologyTestGroupModel.ToViewModel<HospitalPathologyTestGroupViewModel>() : (HospitalPathologyTestGroupViewModel)GetViewModelWithErrorMessage(new HospitalPathologyTestGroupViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestGroup.ToString(), TraceLevel.Error);
                return (HospitalPathologyTestGroupViewModel)GetViewModelWithErrorMessage(hospitalPathologyTestGroupViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete HospitalPathologyTestGroup.
        public virtual bool DeleteHospitalPathologyTestGroup(string hospitalPathologyTestGroupId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalPathologyTestGroup.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _hospitalPathologyTestGroupClient.DeleteHospitalPathologyTestGroup(new ParameterModel { Ids = hospitalPathologyTestGroupId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestGroup.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteHospitalPathologyTestGroup;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestGroup.ToString(), TraceLevel.Error);
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
                ColumnName = "Pathology Test Group Name",
                ColumnCode = "PathologyTestGroupName",
                IsSortable = true,
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

