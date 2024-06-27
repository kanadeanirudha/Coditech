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
    public class HospitalPatientAppointmentPurposeAgent : BaseAgent, IHospitalPatientAppointmentPurposeAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IHospitalPatientAppointmentPurposeClient _hospitalPatientAppointmentPurposeClient;
        #endregion

        #region Public Constructor
        public HospitalPatientAppointmentPurposeAgent(ICoditechLogging coditechLogging, IHospitalPatientAppointmentPurposeClient hospitalPatientAppointmentPurposeClient)
        {
            _coditechLogging = coditechLogging;
            _hospitalPatientAppointmentPurposeClient = GetClient<IHospitalPatientAppointmentPurposeClient>(hospitalPatientAppointmentPurposeClient);
        }
        #endregion

        #region Public Methods
        public virtual HospitalPatientAppointmentPurposeListViewModel GetHospitalPatientAppointmentPurposeList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("HospitalPatientAppointmentPurpose", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            HospitalPatientAppointmentPurposeListResponse response = _hospitalPatientAppointmentPurposeClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            HospitalPatientAppointmentPurposeListModel PatientAppointmentPurposeList = new HospitalPatientAppointmentPurposeListModel { HospitalPatientAppointmentPurposeList = response?.HospitalPatientAppointmentPurposeList };
            HospitalPatientAppointmentPurposeListViewModel listViewModel = new HospitalPatientAppointmentPurposeListViewModel();
            listViewModel.HospitalPatientAppointmentPurposeList = PatientAppointmentPurposeList?.HospitalPatientAppointmentPurposeList?.ToViewModel<HospitalPatientAppointmentPurposeViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.HospitalPatientAppointmentPurposeList.Count, BindColumns());
            return listViewModel;
        }

        //Create Hospital Patient Appointment Purpose.
        public virtual HospitalPatientAppointmentPurposeViewModel CreateHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeViewModel hospitalPatientAppointmentPurposeViewModel)
        {
            try
            {
                HospitalPatientAppointmentPurposeResponse response = _hospitalPatientAppointmentPurposeClient.CreateHospitalPatientAppointmentPurpose(hospitalPatientAppointmentPurposeViewModel.ToModel<HospitalPatientAppointmentPurposeModel>());
                HospitalPatientAppointmentPurposeModel hospitalPatientAppointmentPurposeModel = response?.HospitalPatientAppointmentPurposeModel;
                return IsNotNull(hospitalPatientAppointmentPurposeModel) ? hospitalPatientAppointmentPurposeModel.ToViewModel<HospitalPatientAppointmentPurposeViewModel>() : new HospitalPatientAppointmentPurposeViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (HospitalPatientAppointmentPurposeViewModel)GetViewModelWithErrorMessage(hospitalPatientAppointmentPurposeViewModel, ex.ErrorMessage);
                    default:
                        return (HospitalPatientAppointmentPurposeViewModel)GetViewModelWithErrorMessage(hospitalPatientAppointmentPurposeViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Error);
                return (HospitalPatientAppointmentPurposeViewModel)GetViewModelWithErrorMessage(hospitalPatientAppointmentPurposeViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get Hospital Patient Appointment Purpose by hospitalPatientAppointmentPurposeId.
        public virtual HospitalPatientAppointmentPurposeViewModel GetHospitalPatientAppointmentPurpose(short hospitalPatientAppointmentPurposeId)
        {
            HospitalPatientAppointmentPurposeResponse response = _hospitalPatientAppointmentPurposeClient.GetHospitalPatientAppointmentPurpose(hospitalPatientAppointmentPurposeId);
            return response?.HospitalPatientAppointmentPurposeModel.ToViewModel<HospitalPatientAppointmentPurposeViewModel>();
        }

        //Update HospitalPatientAppointmentPurpose.
        public virtual HospitalPatientAppointmentPurposeViewModel UpdateHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeViewModel hospitalPatientAppointmentPurposeViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Info);
                HospitalPatientAppointmentPurposeResponse response = _hospitalPatientAppointmentPurposeClient.UpdateHospitalPatientAppointmentPurpose(hospitalPatientAppointmentPurposeViewModel.ToModel<HospitalPatientAppointmentPurposeModel>());
                HospitalPatientAppointmentPurposeModel hospitalPatientAppointmentPurposeModel = response?.HospitalPatientAppointmentPurposeModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Info);
                return IsNotNull(hospitalPatientAppointmentPurposeModel) ? hospitalPatientAppointmentPurposeModel.ToViewModel<HospitalPatientAppointmentPurposeViewModel>() : (HospitalPatientAppointmentPurposeViewModel)GetViewModelWithErrorMessage(new HospitalPatientAppointmentPurposeViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Error);
                return (HospitalPatientAppointmentPurposeViewModel)GetViewModelWithErrorMessage(hospitalPatientAppointmentPurposeViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete hospitalPatientAppointmentPurpose.
        public virtual bool DeleteHospitalPatientAppointmentPurpose(string hospitalPatientAppointmentPurposeId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _hospitalPatientAppointmentPurposeClient.DeleteHospitalPatientAppointmentPurpose(new ParameterModel { Ids = hospitalPatientAppointmentPurposeId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteHospitalPatientAppointmentPurpose;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Error);
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
                ColumnName = "Appointment Purpose",
                ColumnCode = "AppointmentPurpose",
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

