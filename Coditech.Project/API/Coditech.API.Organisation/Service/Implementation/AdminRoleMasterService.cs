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
        private readonly ICoditechRepository<AdminRoleCentreRight> _adminRoleCentreRightsRepository;
        private readonly ICoditechRepository<AdminSanctionPost> _adminSanctionPostRepository;
        public AdminRoleMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _adminRoleMasterRepository = new CoditechRepository<AdminRoleMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _adminRoleCentreRightsRepository = new CoditechRepository<AdminRoleCentreRight>();
            _adminSanctionPostRepository = new CoditechRepository<AdminSanctionPost>();
        }

        public virtual AdminRoleMasterListModel GetAdminRoleMasterList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<AdminRoleModel> objStoredProc = new CoditechViewRepository<AdminRoleModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<AdminRoleModel> adminRoleMasterList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetAdminRoleList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            AdminRoleMasterListModel listModel = new AdminRoleMasterListModel();

            listModel.AdminRoleMasterList = adminRoleMasterList?.Count > 0 ? adminRoleMasterList : new List<AdminRoleModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create adminRoleMaster .
        public AdminRoleModel CreateAdminRoleMaster(AdminRoleModel adminRoleMasterModel)
        {
            if (IsNull(adminRoleMasterModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsAdminRoleCodeAlreadyExist(adminRoleMasterModel.AdminRoleCode, 0))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "AdminRole Code"));

            AdminRoleMaster adminRoleMaster = adminRoleMasterModel.FromModelToEntity<AdminRoleMaster>();

            //Create new adminRoleMaster and return it.
            AdminRoleMaster adminRoleMasterData = _adminRoleMasterRepository.Insert(adminRoleMaster);
            if (adminRoleMasterData?.AdminRoleMasterId > 0)
            {
                adminRoleMasterModel.AdminRoleMasterId = adminRoleMasterData.AdminRoleMasterId;
            }
            else
            {
                adminRoleMasterModel.HasError = true;
                adminRoleMasterModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return adminRoleMasterModel;
        }

        //Get adminRoleMaster by adminRoleMaster id.
        public AdminRoleModel GetAdminRoleMasterDetailsById(int adminRoleMasterId)
        {
            if (adminRoleMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminRoleMasterID"));

            //Get the adminRoleMaster Details based on id.
            AdminRoleMaster adminRoleMasterData = _adminRoleMasterRepository.Table.FirstOrDefault(x => x.AdminRoleMasterId == adminRoleMasterId);
            AdminRoleModel adminRoleMasterModel = adminRoleMasterData.FromEntityToModel<AdminRoleModel>();
            adminRoleMasterModel.SelectedRoleWiseCentres = _adminRoleCentreRightsRepository.Table.Where(x => x.AdminRoleMasterId == adminRoleMasterId && x.IsActive == true)?.Select(y => y.CentreCode)?.Distinct().ToList();
            AdminSanctionPost adminSanctionPost = _adminSanctionPostRepository.GetById(adminRoleMasterData.AdminSanctionPostId);
            adminRoleMasterModel.SelectedCentreCodeForSelf = adminSanctionPost.CentreCode;
            adminRoleMasterModel.AllCentreList = OrganisationCentreList();
            return adminRoleMasterModel;
        }

        //Update adminRoleMaster.
        public bool UpdateAdminRoleMaster(AdminRoleModel adminRoleMasterModel)
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
            adminRoleMasterData.ModifiedDate = DateTime.Now;

            //Update adminRoleMaster
            isAdminRoleMasterUpdated = _adminRoleMasterRepository.Update(adminRoleMasterData);
            if (!isAdminRoleMasterUpdated)
            {
                adminRoleMasterModel.HasError = true;
                adminRoleMasterModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
                return isAdminRoleMasterUpdated;
            }

            //update  Admin Role Centre Right
            List<AdminRoleCentreRight> adminRoleCentreRightList = _adminRoleCentreRightsRepository.Table.Where(x => x.AdminRoleMasterId == adminRoleMasterModel.AdminRoleMasterId && x.CentreCode != adminRoleMasterModel.SelectedCentreCodeForSelf)?.ToList();

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
                    AdminRoleCentreRight adminRoleCentreRight = adminRoleCentreRightList?.FirstOrDefault(x => x.CentreCode == item.CentreCode && x.AdminRoleMasterId == adminRoleMasterModel.AdminRoleMasterId);

                    if (adminRoleCentreRight == null && !string.IsNullOrEmpty(selectedCentreCode))
                    {
                        adminRoleCentreRight = new AdminRoleCentreRight()
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

        #region Protected Method
        //Check if adminRole code is already present or not.
        protected virtual bool IsAdminRoleCodeAlreadyExist(string adminRoleCode, int adminRoleMasterId)
         => _adminRoleMasterRepository.Table.Any(x => x.AdminRoleCode == adminRoleCode && (x.AdminRoleMasterId != adminRoleMasterId || adminRoleMasterId == 0));
        #endregion
    }
}
