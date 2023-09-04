using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;

using System.Collections.Specialized;
using System.Data;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Service
{
    public class AdminSnPostsMasterService : IAdminSnPostsMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<AdminSactionPost> _adminSnPostsRepository;
        private readonly ICoditechRepository<AdminRoleMaster> _adminRoleMasterRepository;
        private readonly ICoditechRepository<AdminRoleCentreRight> _adminRoleCentreRightsRepository;
        public AdminSnPostsMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _adminSnPostsRepository = new CoditechRepository<AdminSactionPost>();
            _adminRoleMasterRepository = new CoditechRepository<AdminRoleMaster>();
            _adminRoleCentreRightsRepository = new CoditechRepository<AdminRoleCentreRight>();
        }

        public virtual AdminSnPostsListModel GetAdminSnPostsList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<AdminSnPostsModel> objStoredProc = new CoditechViewRepository<AdminSnPostsModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<AdminSnPostsModel> adminSnPostsList = objStoredProc.ExecuteStoredProcedureList("RARIndia_GetAdminSactionPostsList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            AdminSnPostsListModel listModel = new AdminSnPostsListModel();

            listModel.AdminSnPostsList = adminSnPostsList?.Count > 0 ? adminSnPostsList : new List<AdminSnPostsModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create adminSnPosts.
        public AdminSnPostsModel CreateAdminSnPosts(AdminSnPostsModel adminSnPostsModel)
        {
            if (IsNull(adminSnPostsModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);
            
            if (IsNameAlreadyExist(adminSnPostsModel))
            {
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "AdminSnPosts code"));
            }
            EmployeeDesignationMaster employeeDesignationMaster = GetDesignationDetails(adminSnPostsModel.DesignationId);
            GeneralDepartmentMaster generalDepartmentMaster = GetDepartmentDetails(adminSnPostsModel.DepartmentId);

            adminSnPostsModel.SactionPostCode = $"{employeeDesignationMaster.ShortCode}-{generalDepartmentMaster.DepartmentShortCode}-{adminSnPostsModel.CentreCode}";
            adminSnPostsModel.SactionedPostDescription = $"{employeeDesignationMaster.Description}-{generalDepartmentMaster.DepartmentName}-{adminSnPostsModel.PostType}-{adminSnPostsModel.DesignationType}";
            AdminSactionPost adminSnPostEntity = adminSnPostsModel.FromModelToEntity<AdminSactionPost>();
            //Create new adminSnPosts and return it.
            AdminSactionPost adminSnPostsData = _adminSnPostsRepository.Insert(adminSnPostEntity);

            if (adminSnPostsData?.AdminSactionPostId > 0)
            {
                adminSnPostsModel.AdminSactionPostId = adminSnPostsData.AdminSactionPostId;

                AdminRoleMaster adminRoleMaster = new AdminRoleMaster()
                {
                    AdminSactionPostId = adminSnPostsModel.AdminSactionPostId,
                    SanctPostName = adminSnPostsModel.SactionedPostDescription,
                    MonitoringLevel = CoditechConstant.Self,
                    AdminRoleCode = adminSnPostsModel.SactionPostCode,
                    OthCentreLevel = string.Empty,
                    IsActive = true,
                    CreatedBy = adminSnPostsModel.CreatedBy
                };
                //Create new adminRoleMaster
                adminRoleMaster = _adminRoleMasterRepository.Insert(adminRoleMaster);

                AdminRoleCentreRight adminRoleCentreRight = new AdminRoleCentreRight()
                {
                    AdminRoleMasterId = adminRoleMaster.AdminRoleMasterId,
                    CentreCode = adminSnPostsModel.CentreCode,
                    IsActive = true,
                    CreatedBy = adminSnPostsModel.CreatedBy
                };

                //Create new adminRoleCentreRight
                adminRoleCentreRight = _adminRoleCentreRightsRepository.Insert(adminRoleCentreRight);
            }
            else
            {
                adminSnPostsModel.HasError = true;
                adminSnPostsModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return adminSnPostsModel;
        }

        //Get adminSnPosts by adminSnPosts id.
        public AdminSnPostsModel GetAdminSnPosts(int adminSactionPostId)
        {
            if (adminSactionPostId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminSnPostsID"));

            //Get the adminSnPosts Details based on id.
            AdminSactionPost adminSnPostsData = _adminSnPostsRepository.Table.FirstOrDefault(x => x.AdminSactionPostId == adminSactionPostId);
            AdminSnPostsModel adminSnPostsModel = adminSnPostsData.FromEntityToModel<AdminSnPostsModel>();
            if (IsNotNull(adminSnPostsModel))
            {
                adminSnPostsModel.CentreName = GetOrganisationCentreDetails(adminSnPostsModel.CentreCode)?.CentreName;
                adminSnPostsModel.DepartmentName = GetDepartmentDetails(adminSnPostsModel.DepartmentId)?.DepartmentName;
                adminSnPostsModel.DesignationName = GetDesignationDetails(adminSnPostsModel.DesignationId)?.Description;
            }
            return adminSnPostsModel;
        }

        //Update adminSnPosts.
        public AdminSnPostsModel UpdateAdminSnPosts(AdminSnPostsModel adminSnPostsModel)
        {
            bool isAdminSnPostsUpdated = false;
            if (IsNull(adminSnPostsModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (adminSnPostsModel.AdminSactionPostId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminSnPostsID"));

            AdminSactionPost adminSnPostsData = _adminSnPostsRepository.Table.FirstOrDefault(x => x.AdminSactionPostId == adminSnPostsModel.AdminSactionPostId);
            adminSnPostsData.NoOfPost = adminSnPostsModel.NoOfPost;
            adminSnPostsData.IsActive = adminSnPostsModel.IsActive;
            adminSnPostsData.ModifiedBy = adminSnPostsModel.ModifiedBy;
            
            //Update adminSnPosts
            isAdminSnPostsUpdated = _adminSnPostsRepository.Update(adminSnPostsData);
            if (!isAdminSnPostsUpdated)
            {
                adminSnPostsModel.HasError = true;
                adminSnPostsModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return adminSnPostsModel;
        }

        //Delete adminSnPosts.
        public virtual bool DeleteAdminSnPosts(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminSnPostsID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("AdminSnPostsId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("RARIndia_DeleteAdminSnPosts @AdminSnPostsId,  @Status OUT", 1, out status);
            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if adminSnPosts code is already present or not.
        protected virtual bool IsNameAlreadyExist(AdminSnPostsModel adminSnPostsModel)
             => _adminSnPostsRepository.Table.Any(x => x.CentreCode == adminSnPostsModel.CentreCode && x.DepartmentId == adminSnPostsModel.DepartmentId && x.DesignationId == adminSnPostsModel.DesignationId);
        #endregion
    }
}
