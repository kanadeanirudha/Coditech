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
    public class GymMemberBodyMeasurementAgent : BaseAgent, IGymMemberBodyMeasurementAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGymMemberBodyMeasurementClient _GymMemberBodyMeasurementClient;
        private readonly IUserClient _userClient;
        #endregion

        #region Public Constructor
        public GymMemberBodyMeasurementAgent(ICoditechLogging coditechLogging, IGymMemberBodyMeasurementClient GymMemberBodyMeasurementClient, IUserClient userClient)
        {
            _coditechLogging = coditechLogging;
            _GymMemberBodyMeasurementClient = GetClient<IGymMemberBodyMeasurementClient>(GymMemberBodyMeasurementClient);
            _userClient = GetClient<IUserClient>(userClient);
        }
        #endregion

        #region Public Methods

        public virtual GymMemberBodyMeasurementListViewModel GetBodyMeasurementTypeListByMemberId(int gymMemberDetailId, long personId, short pageSize)
        {
            GymMemberBodyMeasurementListResponse response = _GymMemberBodyMeasurementClient.GetBodyMeasurementTypeListByMemberId(gymMemberDetailId, personId, pageSize);
            GymMemberBodyMeasurementListModel MemberBodyMeasurementList = new GymMemberBodyMeasurementListModel { GymMemberBodyMeasurementList = response?.GymMemberBodyMeasurementList };
            GymMemberBodyMeasurementListViewModel listViewModel = new GymMemberBodyMeasurementListViewModel();
            listViewModel.GymMemberBodyMeasurementList = MemberBodyMeasurementList?.GymMemberBodyMeasurementList?.ToViewModel<GymMemberBodyMeasurementViewModel>().ToList();
            return listViewModel;
        }

        public virtual GymMemberBodyMeasurementListViewModel GetMemberBodyMeasurementList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("GymMemberDetailId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("GymBodyMeasurementTypeId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GymMemberBodyMeasurementListResponse response = _GymMemberBodyMeasurementClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GymMemberBodyMeasurementListModel MemberBodyMeasurementList = new GymMemberBodyMeasurementListModel { GymMemberBodyMeasurementList = response?.GymMemberBodyMeasurementList };
            GymMemberBodyMeasurementListViewModel listViewModel = new GymMemberBodyMeasurementListViewModel();
            listViewModel.GymMemberBodyMeasurementList = MemberBodyMeasurementList?.GymMemberBodyMeasurementList?.ToViewModel<GymMemberBodyMeasurementViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GymMemberBodyMeasurementList.Count, BindColumns());
            return listViewModel;
        }

        //Create General MemberBodyMeasurement.
        public virtual GymMemberBodyMeasurementViewModel CreateMemberBodyMeasurement(GymMemberBodyMeasurementViewModel GymMemberBodyMeasurementViewModel)
        {
            try
            {
                GymMemberBodyMeasurementResponse response = _GymMemberBodyMeasurementClient.CreateMemberBodyMeasurement(GymMemberBodyMeasurementViewModel.ToModel<GymMemberBodyMeasurementModel>());
                GymMemberBodyMeasurementModel GymMemberBodyMeasurementModel = response?.GymMemberBodyMeasurementModel;
                return IsNotNull(GymMemberBodyMeasurementModel) ? GymMemberBodyMeasurementModel.ToViewModel<GymMemberBodyMeasurementViewModel>() : new GymMemberBodyMeasurementViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MemberBodyMeasurement.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GymMemberBodyMeasurementViewModel)GetViewModelWithErrorMessage(GymMemberBodyMeasurementViewModel, ex.ErrorMessage);
                    default:
                        return (GymMemberBodyMeasurementViewModel)GetViewModelWithErrorMessage(GymMemberBodyMeasurementViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MemberBodyMeasurement.ToString(), TraceLevel.Error);
                return (GymMemberBodyMeasurementViewModel)GetViewModelWithErrorMessage(GymMemberBodyMeasurementViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general MemberBodyMeasurement by general MemberBodyMeasurement  id.
        public virtual GymMemberBodyMeasurementViewModel GetMemberBodyMeasurement(long GymMemberBodyMeasurementId)
        {
            GymMemberBodyMeasurementResponse response = _GymMemberBodyMeasurementClient.GetMemberBodyMeasurement(GymMemberBodyMeasurementId);
            return response?.GymMemberBodyMeasurementModel.ToViewModel<GymMemberBodyMeasurementViewModel>();
        }

        //Update GymMemberBodyMeasurement.
        public virtual GymMemberBodyMeasurementViewModel UpdateMemberBodyMeasurement(GymMemberBodyMeasurementViewModel GymMemberBodyMeasurementViewModel)
        {
            try
            {
                //if (GymMemberBodyMeasurementViewModel.IsConverted)
                //{
                //    GeneralPersonModel generalPersonModel = GymMemberBodyMeasurementViewModel.ToModel<GeneralPersonModel>();
                //    generalPersonModel.UserType = GymMemberBodyMeasurementViewModel.UserTypeCode;
                //    GeneralPersonResponse generalPersonResponse = _userClient.InsertPersonInformation(generalPersonModel);
                //    if (generalPersonResponse?.GeneralPersonModel.PersonId <= 0)
                //    {
                //        return (GymMemberBodyMeasurementViewModel)GetViewModelWithErrorMessage(GymMemberBodyMeasurementViewModel, GeneralResources.UpdateErrorMessage);
                //    }
                //}
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.MemberBodyMeasurement.ToString(), TraceLevel.Info);
                GymMemberBodyMeasurementResponse response = _GymMemberBodyMeasurementClient.UpdateMemberBodyMeasurement(GymMemberBodyMeasurementViewModel.ToModel<GymMemberBodyMeasurementModel>());
                GymMemberBodyMeasurementModel GymMemberBodyMeasurementModel = response?.GymMemberBodyMeasurementModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.MemberBodyMeasurement.ToString(), TraceLevel.Info);
                return IsNotNull(GymMemberBodyMeasurementModel) ? GymMemberBodyMeasurementModel.ToViewModel<GymMemberBodyMeasurementViewModel>() : (GymMemberBodyMeasurementViewModel)GetViewModelWithErrorMessage(new GymMemberBodyMeasurementViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MemberBodyMeasurement.ToString(), TraceLevel.Error);
                return (GymMemberBodyMeasurementViewModel)GetViewModelWithErrorMessage(GymMemberBodyMeasurementViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete GymMemberBodyMeasurement.
        public virtual bool DeleteMemberBodyMeasurement(string GymMemberBodyMeasurementId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.MemberBodyMeasurement.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _GymMemberBodyMeasurementClient.DeleteMemberBodyMeasurement(new ParameterModel { Ids = GymMemberBodyMeasurementId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MemberBodyMeasurement.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGymMemberBodyMeasurement;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MemberBodyMeasurement.ToString(), TraceLevel.Error);
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
                ColumnName = "Gym Member Body Measurement Id",
                ColumnCode = "GymMemberBodyMeasurementId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Gym Member Detail Id",
                ColumnCode = "GymMemberDetailId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Gym Body Measurement Type Id",
                ColumnCode = "GymBodyMeasurementTypeId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Body Measurement Value",
                ColumnCode = "BodyMeasurementValue",
                IsSortable = true,
            });            
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all MemberBodyMeasurement list from database 
        public virtual GymMemberBodyMeasurementListResponse GetMemberBodyMeasurementList()
        {
            GymMemberBodyMeasurementListResponse MemberBodyMeasurementList = _GymMemberBodyMeasurementClient.List(null, null, null, 1, int.MaxValue);
            return MemberBodyMeasurementList?.GymMemberBodyMeasurementList?.Count > 0 ? MemberBodyMeasurementList : new GymMemberBodyMeasurementListResponse();
        }
        #endregion
    }
}
