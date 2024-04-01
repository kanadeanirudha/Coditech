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
    public class AdminSanctionPostAgent : BaseAgent, IAdminSanctionPostAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IAdminSanctionPostClient _adminSanctionPostClient;
        #endregion

        #region Public Constructor
        public AdminSanctionPostAgent(ICoditechLogging coditechLogging, IAdminSanctionPostClient adminSanctionPostClient)
        {
            _coditechLogging = coditechLogging;
            _adminSanctionPostClient = GetClient<IAdminSanctionPostClient>(adminSanctionPostClient);
        }
        #endregion

        #region Public Methods

        public virtual AdminSanctionPostListViewModel GetAdminSanctionPostList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters.Add("SanctionedPostDescription", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("SanctionPostCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            filters.Add(FilterKeys.SelectedCentreCode, ProcedureFilterOperators.Equals, dataTableModel.SelectedCentreCode);
            filters.Add(FilterKeys.SelectedDepartmentId, ProcedureFilterOperators.Equals, Convert.ToString(dataTableModel.SelectedDepartmentId));

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "SanctionedPostDescription" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            AdminSanctionPostListResponse response = _adminSanctionPostClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            AdminSanctionPostListModel departmentList = new AdminSanctionPostListModel { AdminSanctionPostList = response?.AdminSanctionPostList };
            AdminSanctionPostListViewModel listViewModel = new AdminSanctionPostListViewModel();
            listViewModel.AdminSanctionPostList = departmentList?.AdminSanctionPostList?.ToViewModel<AdminSanctionPostViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.AdminSanctionPostList.Count, BindColumns());
            return listViewModel;
        }

        //Create AdminSanctionPost.
        public virtual AdminSanctionPostViewModel CreateAdminSanctionPost(AdminSanctionPostViewModel adminSanctionPostViewModel)
        {
            try
            {
                adminSanctionPostViewModel.CentreCode = SpiltCentreCode(adminSanctionPostViewModel.SelectedCentreCode);
                adminSanctionPostViewModel.DepartmentId = Convert.ToInt16(adminSanctionPostViewModel.SelectedDepartmentId);
                adminSanctionPostViewModel.IsActive = true;
                AdminSanctionPostResponse response = _adminSanctionPostClient.CreateAdminSanctionPost(adminSanctionPostViewModel.ToModel<AdminSanctionPostModel>());
                AdminSanctionPostModel adminSanctionPostModel = response?.AdminSanctionPostModel;
                return IsNotNull(adminSanctionPostModel) ? adminSanctionPostModel.ToViewModel<AdminSanctionPostViewModel>() : new AdminSanctionPostViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSanctionPost.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (AdminSanctionPostViewModel)GetViewModelWithErrorMessage(adminSanctionPostViewModel, ex.ErrorMessage);
                    default:
                        return (AdminSanctionPostViewModel)GetViewModelWithErrorMessage(adminSanctionPostViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSanctionPost.ToString(), TraceLevel.Error);
                return (AdminSanctionPostViewModel)GetViewModelWithErrorMessage(adminSanctionPostViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general AdminSanctionPost by general department master id.
        public virtual AdminSanctionPostViewModel GetAdminSanctionPost(int adminSanctionPostId)
        {
            AdminSanctionPostResponse response = _adminSanctionPostClient.GetAdminSanctionPost(adminSanctionPostId);
            return response?.AdminSanctionPostModel.ToViewModel<AdminSanctionPostViewModel>();
        }

        //Update adminSanctionPost.
        public virtual AdminSanctionPostViewModel UpdateAdminSanctionPost(AdminSanctionPostViewModel adminSanctionPostViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AdminSanctionPost.ToString(), TraceLevel.Info);
                AdminSanctionPostResponse response = _adminSanctionPostClient.UpdateAdminSanctionPost(adminSanctionPostViewModel.ToModel<AdminSanctionPostModel>());
                AdminSanctionPostModel adminSanctionPostModel = response?.AdminSanctionPostModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.AdminSanctionPost.ToString(), TraceLevel.Info);
                return IsNotNull(adminSanctionPostModel) ? adminSanctionPostModel.ToViewModel<AdminSanctionPostViewModel>() : (AdminSanctionPostViewModel)GetViewModelWithErrorMessage(new AdminSanctionPostViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSanctionPost.ToString(), TraceLevel.Error);
                return (AdminSanctionPostViewModel)GetViewModelWithErrorMessage(adminSanctionPostViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete adminSanctionPost.
        public virtual bool DeleteAdminSanctionPost(string adminSanctionPostId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AdminSanctionPost.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _adminSanctionPostClient.DeleteAdminSanctionPost(new ParameterModel { Ids = adminSanctionPostId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSanctionPost.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteAdminSanctionPost;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSanctionPost.ToString(), TraceLevel.Error);
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
                ColumnName = "Role Code",
                ColumnCode = "SanctionPostCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Sanctioned Post Description",
                ColumnCode = "SanctionedPostDescription",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "No Of Post",
                ColumnCode = "NoOfPost",
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
    }
}
