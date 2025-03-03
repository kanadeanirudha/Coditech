using Coditech.API.Data;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Specialized;
using System.Data;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Service
{
    public class AdminSanctionPostService : BaseService, IAdminSanctionPostService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<AdminSanctionPost> _adminSanctionPostRepository;
        private readonly ICoditechRepository<AdminRoleMaster> _adminRoleMasterRepository;
        private readonly ICoditechRepository<AdminRoleCentreRights> _adminRoleCentreRightsRepository;
        public AdminSanctionPostService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _adminSanctionPostRepository = new CoditechRepository<AdminSanctionPost>(_serviceProvider.GetService<Coditech_Entities>());
            _adminRoleMasterRepository = new CoditechRepository<AdminRoleMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _adminRoleCentreRightsRepository = new CoditechRepository<AdminRoleCentreRights>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual AdminSanctionPostListModel GetAdminSanctionPostList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            int selectedDepartmentId = 0;
            int.TryParse(filters?.Find(x => string.Equals(x.FilterName, FilterKeys.SelectedDepartmentId, StringComparison.CurrentCultureIgnoreCase))?.FilterValue, out selectedDepartmentId);

            string selectedCentreCode = filters?.Find(x => string.Equals(x.FilterName, FilterKeys.SelectedCentreCode, StringComparison.CurrentCultureIgnoreCase))?.FilterValue;

            filters.RemoveAll(x => x.FilterName == FilterKeys.SelectedDepartmentId || x.FilterName == FilterKeys.SelectedCentreCode);

            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<AdminSanctionPostModel> objStoredProc = new CoditechViewRepository<AdminSanctionPostModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@DepartmentId", selectedDepartmentId, ParameterDirection.Input, DbType.Int16);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<AdminSanctionPostModel> adminSanctionPostList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetAdminSanctionPostList @CentreCode,@DepartmentId,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();
            AdminSanctionPostListModel listModel = new AdminSanctionPostListModel();

            listModel.AdminSanctionPostList = adminSanctionPostList?.Count > 0 ? adminSanctionPostList : new List<AdminSanctionPostModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create adminSanctionPost.
        public virtual AdminSanctionPostModel CreateAdminSanctionPost(AdminSanctionPostModel adminSanctionPostModel)
        {
            if (IsNull(adminSanctionPostModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsNameAlreadyExist(adminSanctionPostModel))
            {
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Admin Sanction Post"));
            }
            EmployeeDesignationMaster employeeDesignationMaster = GetDesignationDetails(adminSanctionPostModel.DesignationId);
            GeneralDepartmentMaster generalDepartmentMaster = GetDepartmentDetails(adminSanctionPostModel.DepartmentId);

            adminSanctionPostModel.SanctionPostCode = $"{employeeDesignationMaster.ShortCode}-{generalDepartmentMaster.DepartmentShortCode}-{adminSanctionPostModel.CentreCode}";
            adminSanctionPostModel.SanctionedPostDescription = $"{employeeDesignationMaster.Description}-{generalDepartmentMaster.DepartmentName}-{adminSanctionPostModel.PostType}-{adminSanctionPostModel.DesignationType}";
            AdminSanctionPost adminSanctionPostEntity = adminSanctionPostModel.FromModelToEntity<AdminSanctionPost>();
            //Create new adminSanctionPost and return it.
            AdminSanctionPost adminSanctionPostData = _adminSanctionPostRepository.Insert(adminSanctionPostEntity);

            if (adminSanctionPostData?.AdminSanctionPostId > 0)
            {
                adminSanctionPostModel.AdminSanctionPostId = adminSanctionPostData.AdminSanctionPostId;

                AdminRoleMaster adminRoleMaster = new AdminRoleMaster()
                {
                    AdminSanctionPostId = adminSanctionPostModel.AdminSanctionPostId,
                    SanctionPostName = adminSanctionPostModel.SanctionedPostDescription,
                    MonitoringLevel = APIConstant.Self,
                    AdminRoleCode = adminSanctionPostModel.SanctionPostCode,
                    LimitedDataAccessEnumId = GetEnumIdByEnumCode("Self", DropdownTypeEnum.LimitedDataAccess.ToString()),
                    OthCentreLevel = string.Empty,
                    IsActive = true,
                    CreatedBy = adminSanctionPostModel.CreatedBy
                };
                //Create new adminRoleMaster
                adminRoleMaster = _adminRoleMasterRepository.Insert(adminRoleMaster);

                AdminRoleCentreRights adminRoleCentreRight = new AdminRoleCentreRights()
                {
                    AdminRoleMasterId = adminRoleMaster.AdminRoleMasterId,
                    CentreCode = adminSanctionPostModel.CentreCode,
                    IsActive = true,
                    CreatedBy = adminSanctionPostModel.CreatedBy
                };

                //Create new adminRoleCentreRight
                adminRoleCentreRight = _adminRoleCentreRightsRepository.Insert(adminRoleCentreRight);
            }
            else
            {
                adminSanctionPostModel.HasError = true;
                adminSanctionPostModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return adminSanctionPostModel;
        }

        //Get adminSanctionPost by adminSanctionPost id.
        public virtual AdminSanctionPostModel GetAdminSanctionPost(int adminSanctionPostId)
        {
            if (adminSanctionPostId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminSanctionPostID"));

            //Get the adminSanctionPost Details based on id.
            AdminSanctionPost adminSanctionPostData = _adminSanctionPostRepository.Table.FirstOrDefault(x => x.AdminSanctionPostId == adminSanctionPostId);
            AdminSanctionPostModel adminSanctionPostModel = adminSanctionPostData.FromEntityToModel<AdminSanctionPostModel>();
            if (IsNotNull(adminSanctionPostModel))
            {
                adminSanctionPostModel.CentreName = GetOrganisationCentreDetails(adminSanctionPostModel.CentreCode)?.CentreName;
                adminSanctionPostModel.DepartmentName = GetDepartmentDetails(adminSanctionPostModel.DepartmentId)?.DepartmentName;
                adminSanctionPostModel.DesignationName = GetDesignationDetails(adminSanctionPostModel.DesignationId)?.Description;
            }
            return adminSanctionPostModel;
        }

        //Update adminSanctionPost.
        public virtual bool UpdateAdminSanctionPost(AdminSanctionPostModel adminSanctionPostModel)
        {
            bool isAdminSanctionPostUpdated = false;
            if (IsNull(adminSanctionPostModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (adminSanctionPostModel.AdminSanctionPostId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminSanctionPostId"));

            AdminSanctionPost adminSanctionPostData = _adminSanctionPostRepository.Table.FirstOrDefault(x => x.AdminSanctionPostId == adminSanctionPostModel.AdminSanctionPostId);
            adminSanctionPostData.NoOfPost = adminSanctionPostModel.NoOfPost;
            adminSanctionPostData.IsActive = adminSanctionPostModel.IsActive;

            //Update adminSanctionPost
            isAdminSanctionPostUpdated = _adminSanctionPostRepository.Update(adminSanctionPostData);
            if (!isAdminSanctionPostUpdated)
            {
                adminSanctionPostModel.HasError = true;
                adminSanctionPostModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return isAdminSanctionPostUpdated;
        }

        //Delete adminSanctionPost.
        public virtual bool DeleteAdminSanctionPost(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminSanctionPostID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("AdminSanctionPostId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteAdminSanctionPost @AdminSanctionPostId,  @Status OUT", 1, out status);
            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if adminSanctionPost code is already present or not.
        protected virtual bool IsNameAlreadyExist(AdminSanctionPostModel adminSanctionPostModel)
             => _adminSanctionPostRepository.Table.Any(x => x.CentreCode == adminSanctionPostModel.CentreCode && x.DepartmentId == adminSanctionPostModel.DepartmentId && x.DesignationId == adminSanctionPostModel.DesignationId && (x.AdminSanctionPostId != adminSanctionPostModel.AdminSanctionPostId || adminSanctionPostModel.AdminSanctionPostId == 0));
        #endregion
    }
}
