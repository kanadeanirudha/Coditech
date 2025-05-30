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
    public class HospitalPatientTypeAgent : BaseAgent, IHospitalPatientTypeAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IHospitalPatientTypeClient _hospitalPatientTypeClient;

        #endregion

        #region Public Constructor
        public HospitalPatientTypeAgent(ICoditechLogging coditechLogging, IHospitalPatientTypeClient hospitalPatientTypeClient)
        {
            _coditechLogging = coditechLogging;
            _hospitalPatientTypeClient = GetClient<IHospitalPatientTypeClient>(hospitalPatientTypeClient);

        }
        #endregion

        #region Public Methods
        #region HospitalPatientType
        public virtual HospitalPatientTypeListViewModel GetHospitalPatientTypeList(DataTableViewModel dataTableModel)
        {

            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("PatientType", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }


            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "PatientType" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            HospitalPatientTypeListResponse response = _hospitalPatientTypeClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            HospitalPatientTypeListModel hospitalPatientTypeList = new HospitalPatientTypeListModel { HospitalPatientTypeList = response?.HospitalPatientTypeList };
            HospitalPatientTypeListViewModel listViewModel = new HospitalPatientTypeListViewModel();
            listViewModel.HospitalPatientTypeList = hospitalPatientTypeList?.HospitalPatientTypeList?.ToViewModel<HospitalPatientTypeViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.HospitalPatientTypeList.Count, BindColumns());
            return listViewModel;

        }

        //Create Hospital PatientType.
        public virtual HospitalPatientTypeViewModel CreateHospitalPatientType(HospitalPatientTypeViewModel hospitalPatientTypeViewModel)
        {
            try
            {
                HospitalPatientTypeResponse response = _hospitalPatientTypeClient.CreateHospitalPatientType(hospitalPatientTypeViewModel.ToModel<HospitalPatientTypeModel>());
                HospitalPatientTypeModel hospitalPatientTypeModel = response?.HospitalPatientTypeModel;
                return IsNotNull(hospitalPatientTypeModel) ? hospitalPatientTypeModel.ToViewModel<HospitalPatientTypeViewModel>() : new HospitalPatientTypeViewModel();

            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientType.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (HospitalPatientTypeViewModel)GetViewModelWithErrorMessage(hospitalPatientTypeViewModel, ex.ErrorMessage);
                    default:
                        return (HospitalPatientTypeViewModel)GetViewModelWithErrorMessage(hospitalPatientTypeViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientType.ToString(), TraceLevel.Error);
                return (HospitalPatientTypeViewModel)GetViewModelWithErrorMessage(hospitalPatientTypeViewModel, GeneralResources.ErrorFailedToCreate);
            }

        }

        //Get HospitalPatientType by hospital Patient Type id.
        public virtual HospitalPatientTypeViewModel GetHospitalPatientType(byte hospitalPatientTypeId)
        {
            HospitalPatientTypeResponse response = _hospitalPatientTypeClient.GetHospitalPatientType(hospitalPatientTypeId);
            return response?.HospitalPatientTypeModel.ToViewModel<HospitalPatientTypeViewModel>();

        }

        //Update  Hospital PatientType.
        public virtual HospitalPatientTypeViewModel UpdateHospitalPatientType(HospitalPatientTypeViewModel hospitalPatientTypeViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalPatientType.ToString(), TraceLevel.Info);
                HospitalPatientTypeResponse response = _hospitalPatientTypeClient.UpdateHospitalPatientType(hospitalPatientTypeViewModel.ToModel<HospitalPatientTypeModel>());
                HospitalPatientTypeModel hospitalPatientTypeModel = response?.HospitalPatientTypeModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.HospitalPatientType.ToString(), TraceLevel.Info);
                return IsNotNull(hospitalPatientTypeModel) ? hospitalPatientTypeModel.ToViewModel<HospitalPatientTypeViewModel>() : (HospitalPatientTypeViewModel)GetViewModelWithErrorMessage(new HospitalPatientTypeViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientType.ToString(), TraceLevel.Error);
                return (HospitalPatientTypeViewModel)GetViewModelWithErrorMessage(hospitalPatientTypeViewModel, GeneralResources.UpdateErrorMessage);
            }

        }

        //Delete HospitalPatientType.
        public virtual bool DeleteHospitalPatientType(string hospitalPatientTypeIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalPatientType.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _hospitalPatientTypeClient.DeleteHospitalPatientType(new ParameterModel { Ids = hospitalPatientTypeIds });
                return trueFalseResponse.IsSuccess;

            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientType.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteHospitalPatientTypeDetails;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientType.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }

        }
        #endregion
        #endregion

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Patient Type",
                ColumnCode = "PatientType",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Active",
                ColumnCode = "IsActive",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all country list from database 
        public virtual HospitalPatientTypeListResponse GetHospitalPatientTypeList()
        {
            HospitalPatientTypeListResponse hospitalPatientTypeList = _hospitalPatientTypeClient.List(null, null, null, 1, int.MaxValue);
            return hospitalPatientTypeList?.HospitalPatientTypeList?.Count > 0 ? hospitalPatientTypeList : new HospitalPatientTypeListResponse();
        }
        #endregion
    }
}
