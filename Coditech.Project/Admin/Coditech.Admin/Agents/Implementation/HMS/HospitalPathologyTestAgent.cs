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
    public class HospitalPathologyTestAgent : BaseAgent, IHospitalPathologyTestAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IHospitalPathologyTestClient _hospitalPathologyTestClient;
        #endregion

        #region Public Constructor
        public HospitalPathologyTestAgent(ICoditechLogging coditechLogging, IHospitalPathologyTestClient hospitalPathologyTestClient)
        {
            _coditechLogging = coditechLogging;
            _hospitalPathologyTestClient = GetClient<IHospitalPathologyTestClient>(hospitalPathologyTestClient);
        }
        #endregion

        #region Public Methods
        public virtual HospitalPathologyTestListViewModel GetHospitalPathologyTestList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("PathologyTestName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("TestSampleType", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "PathologyTestName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            HospitalPathologyTestListResponse response = _hospitalPathologyTestClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            HospitalPathologyTestListModel hospitalPathologyTestList = new HospitalPathologyTestListModel { HospitalPathologyTestList = response?.HospitalPathologyTestList };
            HospitalPathologyTestListViewModel listViewModel = new HospitalPathologyTestListViewModel();
            listViewModel.HospitalPathologyTestList = hospitalPathologyTestList?.HospitalPathologyTestList?.ToViewModel<HospitalPathologyTestViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.HospitalPathologyTestList.Count, BindColumns());
            return listViewModel;
        }

        //Create HospitalPathologyTest.
        public virtual HospitalPathologyTestViewModel CreateHospitalPathologyTest(HospitalPathologyTestViewModel hospitalPathologyTestViewModel)
        {
            try
            {
                HospitalPathologyTestResponse response = _hospitalPathologyTestClient.CreateHospitalPathologyTest(hospitalPathologyTestViewModel.ToModel<HospitalPathologyTestModel>());
                HospitalPathologyTestModel hospitalPathologyTestModel = response?.HospitalPathologyTestModel;
                return HelperUtility.IsNotNull(hospitalPathologyTestModel) ? hospitalPathologyTestModel.ToViewModel<HospitalPathologyTestViewModel>() : new HospitalPathologyTestViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTest.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (HospitalPathologyTestViewModel)GetViewModelWithErrorMessage(hospitalPathologyTestViewModel, ex.ErrorMessage);
                    default:
                        return (HospitalPathologyTestViewModel)GetViewModelWithErrorMessage(hospitalPathologyTestViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTest.ToString(), TraceLevel.Error);
                return (HospitalPathologyTestViewModel)GetViewModelWithErrorMessage(hospitalPathologyTestViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general HospitalPathologyTest by hospital Pathology Test id.
        public virtual HospitalPathologyTestViewModel GetHospitalPathologyTest(long hospitalPathologyTestId)
        {
            HospitalPathologyTestResponse response = _hospitalPathologyTestClient.GetHospitalPathologyTest(hospitalPathologyTestId);
            return response?.HospitalPathologyTestModel.ToViewModel<HospitalPathologyTestViewModel>();
        }

        //Update  HospitalPathologyTest.
        public virtual HospitalPathologyTestViewModel UpdateHospitalPathologyTest(HospitalPathologyTestViewModel hospitalPathologyTestViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalPathologyTest.ToString(), TraceLevel.Info);
                HospitalPathologyTestResponse response = _hospitalPathologyTestClient.UpdateHospitalPathologyTest(hospitalPathologyTestViewModel.ToModel<HospitalPathologyTestModel>());
                HospitalPathologyTestModel hospitalPathologyTestModel = response?.HospitalPathologyTestModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.HospitalPathologyTest.ToString(), TraceLevel.Info);
                return HelperUtility.IsNotNull(hospitalPathologyTestModel) ? hospitalPathologyTestModel.ToViewModel<HospitalPathologyTestViewModel>() : (HospitalPathologyTestViewModel)GetViewModelWithErrorMessage(new HospitalPathologyTestViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTest.ToString(), TraceLevel.Error);
                return (HospitalPathologyTestViewModel)GetViewModelWithErrorMessage(hospitalPathologyTestViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete HospitalPathologyTest.
        public virtual bool DeleteHospitalPathologyTest(string hospitalPathologyTestIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalPathologyTest.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _hospitalPathologyTestClient.DeleteHospitalPathologyTest(new ParameterModel { Ids = hospitalPathologyTestIds });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTest.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteHospitalPathologyTest;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTest.ToString(), TraceLevel.Error);
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
                ColumnName = "Pathology Test Name",
                ColumnCode = "PathologyTestName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Pathology Test Group Name",
                ColumnCode = "PathologyTestGroupName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Test Sample Type",
                ColumnCode = "TestSampleType ",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
    }
}

