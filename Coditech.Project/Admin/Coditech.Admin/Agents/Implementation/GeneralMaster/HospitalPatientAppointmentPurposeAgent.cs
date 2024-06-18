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
        public HospitalPatientAppointmentPurposeAgent(ICoditechLogging coditechLogging, IHospitalPatientAppointmentPurposeClient HospitalPatientAppointmentPurposeClient)
        {
            _coditechLogging = coditechLogging;
            _hospitalPatientAppointmentPurposeClient = GetClient<IHospitalPatientAppointmentPurposeClient>(HospitalPatientAppointmentPurposeClient);
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

        //Create General Country.
        public virtual HospitalPatientAppointmentPurposeViewModel CreateHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeViewModel PatientAppointmentPurposeViewModel)
        {
            try
            {
                HospitalPatientAppointmentPurposeResponse response = _hospitalPatientAppointmentPurposeClient.CreateHospitalPatientAppointmentPurpose(PatientAppointmentPurposeViewModel.ToModel<HospitalPatientAppointmentPurposeModel>());
                HospitalPatientAppointmentPurposeModel hospitalPatientAppointmentPurposeModel = response?.hospitalPatientAppointmentPurposeModel;
                return IsNotNull(hospitalPatientAppointmentPurposeModel) ? hospitalPatientAppointmentPurposeModel.ToViewModel<HospitalPatientAppointmentPurposeViewModel>() : new HospitalPatientAppointmentPurposeViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (HospitalPatientAppointmentPurposeViewModel)GetViewModelWithErrorMessage(PatientAppointmentPurposeViewModel, ex.ErrorMessage);
                    default:
                        return (HospitalPatientAppointmentPurposeViewModel)GetViewModelWithErrorMessage(PatientAppointmentPurposeViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Error);
                return (HospitalPatientAppointmentPurposeViewModel)GetViewModelWithErrorMessage(PatientAppointmentPurposeViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general Country by general country master id.
        public virtual HospitalPatientAppointmentPurposeViewModel GetHospitalPatientAppointmentPurpose(short HospitalPatientAppointmentPurposeId)
        {
            HospitalPatientAppointmentPurposeResponse response = _hospitalPatientAppointmentPurposeClient.GetHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeId);
            return response?.hospitalPatientAppointmentPurposeModel.ToViewModel<HospitalPatientAppointmentPurposeViewModel>();
        }

        //Update generalCountry.
        public virtual HospitalPatientAppointmentPurposeViewModel UpdateHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeViewModel PatientAppointmentPurposeViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Info);
                HospitalPatientAppointmentPurposeResponse response = _hospitalPatientAppointmentPurposeClient.UpdateHospitalPatientAppointmentPurpose(PatientAppointmentPurposeViewModel.ToModel<HospitalPatientAppointmentPurposeModel>());
                HospitalPatientAppointmentPurposeModel hospitalPatientAppointmentPurposeModel = response?.hospitalPatientAppointmentPurposeModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Info);
                return IsNotNull(hospitalPatientAppointmentPurposeModel) ? hospitalPatientAppointmentPurposeModel.ToViewModel<HospitalPatientAppointmentPurposeViewModel>() : (HospitalPatientAppointmentPurposeViewModel)GetViewModelWithErrorMessage(new HospitalPatientAppointmentPurposeViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Error);
                return (HospitalPatientAppointmentPurposeViewModel)GetViewModelWithErrorMessage(PatientAppointmentPurposeViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalCountry.
        public virtual bool DeleteHospitalPatientAppointmentPurpose(string HospitalPatientAppointmentPurposeId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _hospitalPatientAppointmentPurposeClient.DeleteHospitalPatientAppointmentPurpose(new ParameterModel { Ids = HospitalPatientAppointmentPurposeId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralCountryMaster;
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
                ColumnName = "Patient Appointment Name",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Default",
            });
            return datatableColumnList;
        }
        #endregion
    }
}

