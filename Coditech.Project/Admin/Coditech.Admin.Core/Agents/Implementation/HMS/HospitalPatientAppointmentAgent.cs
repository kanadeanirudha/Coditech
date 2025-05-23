﻿using Coditech.Admin.ViewModel;
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
    public class HospitalPatientAppointmentAgent : BaseAgent, IHospitalPatientAppointmentAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IHospitalPatientAppointmentClient _hospitalPatientAppointmentClient;
        #endregion

        #region Public Constructor
        public HospitalPatientAppointmentAgent(ICoditechLogging coditechLogging, IHospitalPatientAppointmentClient hospitalPatientAppointmentClient)
        {
            _coditechLogging = coditechLogging;
            _hospitalPatientAppointmentClient = GetClient<IHospitalPatientAppointmentClient>(hospitalPatientAppointmentClient);
        }
        #endregion

        #region Public Methods
        public virtual HospitalPatientAppointmentListViewModel GetHospitalPatientAppointmentList(/*string selectedCentreCode, short selectedDepartmentId,*/ DataTableViewModel dataTableModel)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            filters.Add(FilterKeys.SelectedCentreCode, ProcedureFilterOperators.Equals, dataTableModel.SelectedCentreCode);
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters.Add("FirstName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("LastName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("MobileNumber", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("AppointmentType", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            HospitalPatientAppointmentListResponse response = _hospitalPatientAppointmentClient.List(/*selectedCentreCode, selectedDepartmentId, null,*/ null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            HospitalPatientAppointmentListModel hospitalPatientAppointmentList = new HospitalPatientAppointmentListModel { HospitalPatientAppointmentList = response?.HospitalPatientAppointmentList };
            HospitalPatientAppointmentListViewModel listViewModel = new HospitalPatientAppointmentListViewModel();
            listViewModel.HospitalPatientAppointmentList = hospitalPatientAppointmentList?.HospitalPatientAppointmentList?.ToViewModel<HospitalPatientAppointmentViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.HospitalPatientAppointmentList.Count, BindColumns());
            return listViewModel;
        }

        //Create Hospital Patient Appointment.
        public virtual HospitalPatientAppointmentViewModel CreateHospitalPatientAppointment(HospitalPatientAppointmentViewModel hospitalPatientAppointmentViewModel)
        {
            try
            {
                HospitalPatientAppointmentResponse response = _hospitalPatientAppointmentClient.CreateHospitalPatientAppointment(hospitalPatientAppointmentViewModel.ToModel<HospitalPatientAppointmentModel>());
                HospitalPatientAppointmentModel hospitalPatientAppointmentModel = response?.HospitalPatientAppointmentModel;
                return IsNotNull(hospitalPatientAppointmentModel) ? hospitalPatientAppointmentModel.ToViewModel<HospitalPatientAppointmentViewModel>() : new HospitalPatientAppointmentViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointment.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (HospitalPatientAppointmentViewModel)GetViewModelWithErrorMessage(hospitalPatientAppointmentViewModel, ex.ErrorMessage);
                    default:
                        return (HospitalPatientAppointmentViewModel)GetViewModelWithErrorMessage(hospitalPatientAppointmentViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointment.ToString(), TraceLevel.Error);
                return (HospitalPatientAppointmentViewModel)GetViewModelWithErrorMessage(hospitalPatientAppointmentViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //GetHospitalPatientAppointment by hospital Patient Appointment Id.
        public virtual HospitalPatientAppointmentViewModel GetHospitalPatientAppointment(long hospitalPatientAppointmentId)
        {
            HospitalPatientAppointmentResponse response = _hospitalPatientAppointmentClient.GetHospitalPatientAppointment(hospitalPatientAppointmentId);
            return response?.HospitalPatientAppointmentModel.ToViewModel<HospitalPatientAppointmentViewModel>();
        }

        //Update HospitalPatientAppointment.
        public virtual HospitalPatientAppointmentViewModel UpdateHospitalPatientAppointment(HospitalPatientAppointmentViewModel hospitalPatientAppointmentViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalPatientAppointment.ToString(), TraceLevel.Info);
                HospitalPatientAppointmentResponse response = _hospitalPatientAppointmentClient.UpdateHospitalPatientAppointment(hospitalPatientAppointmentViewModel.ToModel<HospitalPatientAppointmentModel>());
                HospitalPatientAppointmentModel hospitalPatientAppointmentModel = response?.HospitalPatientAppointmentModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.HospitalPatientAppointment.ToString(), TraceLevel.Info);
                return IsNotNull(hospitalPatientAppointmentModel) ? hospitalPatientAppointmentModel.ToViewModel<HospitalPatientAppointmentViewModel>() : (HospitalPatientAppointmentViewModel)GetViewModelWithErrorMessage(new HospitalPatientAppointmentViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointment.ToString(), TraceLevel.Error);
                return (HospitalPatientAppointmentViewModel)GetViewModelWithErrorMessage(hospitalPatientAppointmentViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete HospitalPatientAppointment.
        public virtual bool DeleteHospitalPatientAppointment(string hospitalPatientAppointmentId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalPatientAppointment.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _hospitalPatientAppointmentClient.DeleteHospitalPatientAppointment(new ParameterModel { Ids = hospitalPatientAppointmentId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointment.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteHospitalPatientAppointment;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointment.ToString(), TraceLevel.Error);
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
                ColumnName = "Image",
                ColumnCode = "Image",
            });
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
                ColumnName = "Contact",
                ColumnCode = "MobileNumber",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Appointment Type",
                ColumnCode = "AppointmentTypeEnumId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = " Appointment Date",
                ColumnCode = "AppointmentDate",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Requested Time Slot",
                ColumnCode = "RequestedTimeSlot",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Approval Status",
                ColumnCode = "ApprovalStatusEnumId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Attended",
                ColumnCode = "IsAttended",
                IsSortable = true,

            });
            return datatableColumnList;
        }
        #endregion
    }
}
