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
    public class GymMemberDetailsAgent : BaseAgent, IGymMemberDetailsAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGymMemberDetailsClient _gymMemberDetailsClient;
        private readonly IUserClient _userClient;
        #endregion

        #region Public Constructor
        public GymMemberDetailsAgent(ICoditechLogging coditechLogging, IGymMemberDetailsClient gymMemberDetailsClient, IUserClient userClient)
        {
            _coditechLogging = coditechLogging;
            _gymMemberDetailsClient = GetClient<IGymMemberDetailsClient>(gymMemberDetailsClient);
            _userClient = GetClient<IUserClient>(userClient);
        }
        #endregion

        #region Public Methods
        public virtual GymMemberDetailsListViewModel GetGymMemberDetailsList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("FirstName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("LastName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "FirstName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GymMemberDetailsListResponse response = _gymMemberDetailsClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GymMemberDetailsListModel countryList = new GymMemberDetailsListModel { GymMemberDetailsList = response?.GymMemberDetailsList };
            GymMemberDetailsListViewModel listViewModel = new GymMemberDetailsListViewModel();
            listViewModel.GymMemberDetailsList = countryList?.GymMemberDetailsList?.ToViewModel<GymMemberDetailsViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GymMemberDetailsList.Count, BindColumns());
            return listViewModel;
        }

        //Create Member Details
        public virtual GymCreateEditMemberViewModel CreateMemberDetails(GymCreateEditMemberViewModel gymCreateEditMemberViewModel)
        {
            try
            {
                gymCreateEditMemberViewModel.UserType = UserTypeEnum.GymMember.ToString();
                GeneralPersonResponse response = _userClient.InsertPersonInformation(gymCreateEditMemberViewModel.ToModel<GeneralPersonModel>());
                GeneralPersonModel generalPersonModel = response?.GeneralPersonModel;
                return IsNotNull(generalPersonModel) ? generalPersonModel.ToViewModel<GymCreateEditMemberViewModel>() : new GymCreateEditMemberViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GymCreateEditMemberViewModel)GetViewModelWithErrorMessage(gymCreateEditMemberViewModel, ex.ErrorMessage);
                    default:
                        return (GymCreateEditMemberViewModel)GetViewModelWithErrorMessage(gymCreateEditMemberViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                return (GymCreateEditMemberViewModel)GetViewModelWithErrorMessage(gymCreateEditMemberViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get Get Member Details by personId.
        public virtual GymCreateEditMemberViewModel GetMemberPersonalDetails(long personId)
        {
            //GymMemberDetailsResponse response = _gymMemberDetailsClient.GetCountry(gymMemberDetailsId);
            //return response?.GymMemberDetailsModel.ToViewModel<GymMemberDetailsViewModel>();

            return new GymCreateEditMemberViewModel();
        }

        //Get Get Member Details by personId.
        public virtual GymCreateEditMemberViewModel GetMemberDetails(long personId)
        {
            //GymMemberDetailsResponse response = _gymMemberDetailsClient.GetCountry(gymMemberDetailsId);
            //return response?.GymMemberDetailsModel.ToViewModel<GymMemberDetailsViewModel>();

            return new GymCreateEditMemberViewModel();
        }

        //Update Member Details
        public virtual GymCreateEditMemberViewModel UpdateMemberDetails(GymCreateEditMemberViewModel gymCreateEditMemberViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Info);
                //GymMemberDetailsResponse response = _gymMemberDetailsClient.UpdateCountry(gymMemberDetailsViewModel.ToModel<GymMemberDetailsModel>());
                //GymMemberDetailsModel gymMemberDetailsModel = response?.GymMemberDetailsModel;
                //_coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Info);
                //return IsNotNull(gymMemberDetailsModel) ? gymMemberDetailsModel.ToViewModel<GymMemberDetailsViewModel>() : (GymMemberDetailsViewModel)GetViewModelWithErrorMessage(new GymMemberDetailsViewModel(), GeneralResources.UpdateErrorMessage);
                return new GymCreateEditMemberViewModel();
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                return (GymCreateEditMemberViewModel)GetViewModelWithErrorMessage(gymCreateEditMemberViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete gymMemberDetails.
        public virtual bool DeleteMembers(string gymMemberDetailIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = new TrueFalseResponse();// _gymMemberDetailsClient.DeleteMembers(new ParameterModel { Ids = gymMemberDetailIds });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        //errorMessage = AdminResources.ErrorDeleteGymMemberDetailsMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
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
            return datatableColumnList;
        }
        #endregion
    }
}
