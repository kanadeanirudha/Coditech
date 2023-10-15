using Coditech.API.Data;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;

using System.Collections.Specialized;
using System.Data;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Service
{
    public class AdminRoleMasterService : BaseService, IAdminRoleMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<AdminRoleMaster> _adminRoleMasterRepository;
        private readonly ICoditechRepository<AdminRoleCentreRights> _adminRoleCentreRightsRepository;
        private readonly ICoditechRepository<AdminSanctionPost> _adminSanctionPostRepository;
        public AdminRoleMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _adminRoleMasterRepository = new CoditechRepository<AdminRoleMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _adminRoleCentreRightsRepository = new CoditechRepository<AdminRoleCentreRights>(_serviceProvider.GetService<Coditech_Entities>());
            _adminSanctionPostRepository = new CoditechRepository<AdminSanctionPost>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual AdminRoleListModel GetAdminRoleList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            int selectedDepartmentId = 0;
            int.TryParse(filters?.Find(x => string.Equals(x.FilterName, FilterKeys.SelectedDepartmentId, StringComparison.CurrentCultureIgnoreCase))?.FilterValue, out selectedDepartmentId);

            string selectedCentreCode = filters?.Find(x => string.Equals(x.FilterName, FilterKeys.SelectedCentreCode, StringComparison.CurrentCultureIgnoreCase))?.FilterValue;

            filters.RemoveAll(x => x.FilterName == FilterKeys.SelectedDepartmentId || x.FilterName == FilterKeys.SelectedCentreCode);

            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<AdminRoleModel> objStoredProc = new CoditechViewRepository<AdminRoleModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@DepartmentId", selectedDepartmentId, ParameterDirection.Input, DbType.Int16);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<AdminRoleModel> adminRoleMasterList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetAdminRoleList @CentreCode,@DepartmentId,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();
            AdminRoleListModel listModel = new AdminRoleListModel();

            listModel.AdminRoleList = adminRoleMasterList?.Count > 0 ? adminRoleMasterList : new List<AdminRoleModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Get adminRoleMaster by adminRoleMaster id.
        public AdminRoleModel GetAdminRoleDetailsById(int adminRoleMasterId)
        {
            if (adminRoleMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminRoleMasterID"));

            //Get the adminRoleMaster Details based on id.
            AdminRoleMaster adminRoleMasterData = _adminRoleMasterRepository.Table.FirstOrDefault(x => x.AdminRoleMasterId == adminRoleMasterId);
            AdminRoleModel adminRoleMasterModel = adminRoleMasterData.FromEntityToModel<AdminRoleModel>();
            adminRoleMasterModel.SelectedRoleWiseCentres = _adminRoleCentreRightsRepository.Table.Where(x => x.AdminRoleMasterId == adminRoleMasterId && x.IsActive == true)?.Select(y => y.CentreCode)?.Distinct().ToList();
            AdminSanctionPost adminSanctionPost = _adminSanctionPostRepository.GetById(adminRoleMasterData.AdminSanctionPostId);
            adminRoleMasterModel.SelectedCentreCode = adminSanctionPost.CentreCode;
            adminRoleMasterModel.SelectedDepartmentId = Convert.ToString(adminSanctionPost.DepartmentId);
            adminRoleMasterModel.SelectedCentreCodeForSelf = adminSanctionPost.CentreCode;
            adminRoleMasterModel.AllCentreList = OrganisationCentreList();
            return adminRoleMasterModel;
        }

        //Update adminRoleMaster.
        public bool UpdateAdminRole(AdminRoleModel adminRoleMasterModel)
        {
            bool isAdminRoleMasterUpdated = false;
            if (IsNull(adminRoleMasterModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);
            if (adminRoleMasterModel.AdminRoleMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminRoleMasterID"));

            AdminRoleMaster adminRoleMasterData = _adminRoleMasterRepository.Table.FirstOrDefault(x => x.AdminRoleMasterId == adminRoleMasterModel.AdminRoleMasterId);
            adminRoleMasterData.MonitoringLevel = adminRoleMasterModel.MonitoringLevel;
            adminRoleMasterData.OthCentreLevel = adminRoleMasterModel.MonitoringLevel == APIConstant.Self ? string.Empty : "Selected";
            adminRoleMasterData.IsLoginAllowFromOutside = adminRoleMasterModel.IsLoginAllowFromOutside;
            adminRoleMasterData.IsAttendaceAllowFromOutside = adminRoleMasterModel.IsAttendaceAllowFromOutside;
            adminRoleMasterData.IsActive = adminRoleMasterModel.IsActive;
            adminRoleMasterData.ModifiedBy = adminRoleMasterModel.ModifiedBy;

            //Update adminRoleMaster
            isAdminRoleMasterUpdated = _adminRoleMasterRepository.Update(adminRoleMasterData);
            if (!isAdminRoleMasterUpdated)
            {
                adminRoleMasterModel.HasError = true;
                adminRoleMasterModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
                return isAdminRoleMasterUpdated;
            }

            //update  Admin Role Centre Right
            List<AdminRoleCentreRights> adminRoleCentreRightList = _adminRoleCentreRightsRepository.Table.Where(x => x.AdminRoleMasterId == adminRoleMasterModel.AdminRoleMasterId && x.CentreCode != adminRoleMasterModel.SelectedCentreCodeForSelf)?.ToList();

            if (adminRoleMasterModel.MonitoringLevel == APIConstant.Self || (adminRoleMasterModel.MonitoringLevel == APIConstant.Other && adminRoleMasterModel?.SelectedRoleWiseCentres?.Count == 0))
            {
                adminRoleCentreRightList = adminRoleCentreRightList.Where(x => x.IsActive == true)?.ToList();
                if (adminRoleCentreRightList?.Count > 0 && adminRoleCentreRightList.Any(x => x.IsActive == true))
                {
                    adminRoleCentreRightList.ForEach(x => { x.IsActive = false; x.ModifiedBy = adminRoleMasterModel.ModifiedBy; });
                    _adminRoleCentreRightsRepository.BatchUpdate(adminRoleCentreRightList);
                }
            }
            else
            {
                adminRoleMasterModel.AllCentreList = OrganisationCentreList();
                foreach (UserAccessibleCentreModel item in adminRoleMasterModel?.AllCentreList?.Where(x => x.CentreCode != adminRoleMasterModel.SelectedCentreCodeForSelf))
                {
                    string selectedCentreCode = adminRoleMasterModel?.SelectedRoleWiseCentres?.FirstOrDefault(x => x == item.CentreCode);
                    AdminRoleCentreRights adminRoleCentreRight = adminRoleCentreRightList?.FirstOrDefault(x => x.CentreCode == item.CentreCode && x.AdminRoleMasterId == adminRoleMasterModel.AdminRoleMasterId);

                    if (adminRoleCentreRight == null && !string.IsNullOrEmpty(selectedCentreCode))
                    {
                        adminRoleCentreRight = new AdminRoleCentreRights()
                        {
                            AdminRoleMasterId = adminRoleMasterModel.AdminRoleMasterId,
                            CentreCode = selectedCentreCode,
                            IsActive = true,
                            CreatedBy = adminRoleMasterModel.CreatedBy,
                            ModifiedBy = adminRoleMasterModel.ModifiedBy
                        };
                        _adminRoleCentreRightsRepository.Insert(adminRoleCentreRight);
                    }
                    else if (adminRoleCentreRight?.CentreCode == selectedCentreCode && adminRoleCentreRight?.IsActive == false)
                    {
                        adminRoleCentreRight.IsActive = true;
                        adminRoleCentreRight.ModifiedBy = adminRoleMasterModel.ModifiedBy;
                        _adminRoleCentreRightsRepository.Update(adminRoleCentreRight);
                    }
                    else if (selectedCentreCode == null && adminRoleCentreRight?.IsActive == true)
                    {
                        adminRoleCentreRight.IsActive = false;
                        adminRoleCentreRight.ModifiedBy = adminRoleMasterModel.ModifiedBy;
                        _adminRoleCentreRightsRepository.Update(adminRoleCentreRight);
                    }
                }
            }
            return isAdminRoleMasterUpdated;
        }

        //Delete AdminRoleMaster.
        public virtual bool DeleteAdminRoleMaster(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminRoleMasterID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("AdminRoleMasterID", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteAdminRoleMaster @AdminRoleMasterID,  @Status OUT", 1, out status);
            return status == 1 ? true : false;
        }
    }
}
