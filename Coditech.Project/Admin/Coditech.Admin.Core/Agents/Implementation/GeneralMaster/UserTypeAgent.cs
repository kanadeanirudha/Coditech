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
    public class UserTypeAgent : BaseAgent, IUserTypeAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IUserTypeClient _userTypeClient;
        #endregion

        #region Public Constructor
        public UserTypeAgent(ICoditechLogging coditechLogging, IUserTypeClient UserTypeClient)
        {
            _coditechLogging = coditechLogging;
            _userTypeClient = GetClient<IUserTypeClient>(UserTypeClient);
        }
        #endregion

        #region Public Methods
        public virtual UserTypeListViewModel GetUserTypeList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();

            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("UserTypeCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("UserDescription", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "UserTypeCode" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            UserTypeListResponse response = _userTypeClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            UserTypeListModel userTypeList = new UserTypeListModel { TypeList = response?.TypeList };
            UserTypeListViewModel listViewModel = new UserTypeListViewModel();
            listViewModel.TypeList = userTypeList?.TypeList?.ToViewModel<UserTypeViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.TypeList.Count, BindColumns());
            return listViewModel;
        }

        //Create User Type.
        public virtual UserTypeViewModel CreateUserType(UserTypeViewModel userTypeViewModel)
        {
            try
            {
                UserTypeResponse response = _userTypeClient.CreateUserType(userTypeViewModel.ToModel<UserTypeModel>());
                UserTypeModel userTypeModel = response?.UserTypeModel;
                return IsNotNull(userTypeModel) ? userTypeModel.ToViewModel<UserTypeViewModel>() : new UserTypeViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserType.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (UserTypeViewModel)GetViewModelWithErrorMessage(userTypeViewModel, ex.ErrorMessage);
                    default:
                        return (UserTypeViewModel)GetViewModelWithErrorMessage(userTypeViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserType.ToString(), TraceLevel.Error);
                return (UserTypeViewModel)GetViewModelWithErrorMessage(userTypeViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get user type by user type Id.
        public virtual UserTypeViewModel GetUserType(short userTypeId)
        {
            UserTypeResponse response = _userTypeClient.GetUserType(userTypeId);
            return response?.UserTypeModel.ToViewModel<UserTypeViewModel>();
        }

        //Update user type.
        public virtual UserTypeViewModel UpdateUserType(UserTypeViewModel userTypeViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.UserType.ToString(), TraceLevel.Info);
                UserTypeResponse response = _userTypeClient.UpdateUserType(userTypeViewModel.ToModel<UserTypeModel>());
                UserTypeModel userTypeModel = response?.UserTypeModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.UserType.ToString(), TraceLevel.Info);
                return IsNotNull(userTypeModel) ? userTypeModel.ToViewModel<UserTypeViewModel>() : (UserTypeViewModel)GetViewModelWithErrorMessage(new UserTypeViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserType.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (UserTypeViewModel)GetViewModelWithErrorMessage(userTypeViewModel, ex.ErrorMessage);
                    default:
                        return (UserTypeViewModel)GetViewModelWithErrorMessage(userTypeViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserType.ToString(), TraceLevel.Error);
                return (UserTypeViewModel)GetViewModelWithErrorMessage(userTypeViewModel, GeneralResources.UpdateErrorMessage);
            }
        }


        //public virtual bool DeleteUserType(string userTypeId, out string errorMessage)
        //{
        //    errorMessage = GeneralResources.ErrorFailedToDelete;

        //    try
        //    {
        //        _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.UserType.ToString(), TraceLevel.Info);
        //        TrueFalseResponse trueFalseResponse = _userTypeClient.DeleteUserType(new ParameterModel { Ids = userTypeId });
        //        return trueFalseResponse.IsSuccess;
        //    }
        //    catch (CoditechException ex)
        //    {
        //        _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserType.ToString(), TraceLevel.Warning);
        //        switch (ex.ErrorCode)
        //        {
        //            case ErrorCodes.AssociationDeleteError:
        //                errorMessage = AdminResources.ErrorDeleteUserType;
        //                return false;
        //            default:
        //                errorMessage = GeneralResources.ErrorFailedToDelete;
        //                return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserType.ToString(), TraceLevel.Error);
        //        errorMessage = GeneralResources.ErrorFailedToDelete;
        //        return false;
        //    }
        //}
        #endregion

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "User Type Code",
                ColumnCode = "UserTypeCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "User Description ",
                ColumnCode = "UserDescription",
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
        //#region

        //// it will return get all user type list from database 
        //public virtual UserTypeListResponse GetUserTypeList()
        //{
        //    UserTypeListResponse userTypeList = _userTypeClient.List(null, null, null, 1, int.MaxValue);
        //    return userTypeList?.UserTypeList?.Count > 0 ? userTypeList : new UserTypeListResponse();
        //}
        //#endregion
    }
}
