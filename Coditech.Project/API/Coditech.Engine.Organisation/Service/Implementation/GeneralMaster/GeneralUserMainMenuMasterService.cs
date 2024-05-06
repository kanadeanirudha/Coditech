
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
    public class GeneralUserMainMenuMasterService : IGeneralUserMainMenuMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<UserMainMenuMaster> _generalUserMainMenuMasterRepository;
        public GeneralUserMainMenuMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalUserMainMenuMasterRepository = new CoditechRepository<UserMainMenuMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual UserMainMenuListModel GetUserMainMenuList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<UserMainMenuModel> objStoredProc = new CoditechViewRepository<UserMainMenuModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<UserMainMenuModel> UserMainMenuList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetUserMainMenuList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            UserMainMenuListModel listModel = new UserMainMenuListModel();

            listModel.GeneralUserMainMenuList = UserMainMenuList?.Count > 0 ? UserMainMenuList : new List<UserMainMenuModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create UserMainMenu.
        public virtual UserMainMenuModel CreateUserMainMenu(UserMainMenuModel generalUserMainMenuModel)
        {
            if (IsNull(generalUserMainMenuModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsUserMainMenuCodeAlreadyExist(generalUserMainMenuModel.MenuCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Menu Code"));

            UserMainMenuMaster generalUserMainMenuMaster = generalUserMainMenuModel.FromModelToEntity<UserMainMenuMaster>();

            //Create new UserMainMenu and return it.
            UserMainMenuMaster userMainMenuData = _generalUserMainMenuMasterRepository.Insert(generalUserMainMenuMaster);
            if (userMainMenuData?.UserMainMenuMasterId > 0)
            {
                generalUserMainMenuModel.UserMainMenuMasterId = userMainMenuData.UserMainMenuMasterId;
            }
            else
            {
                generalUserMainMenuModel.HasError = true;
                generalUserMainMenuModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalUserMainMenuModel;
        }

        //Get UserMainMenu by UserMainMenu id.
        public virtual UserMainMenuModel GetUserMainMenu(short userMainMenuId)
        {
            if (userMainMenuId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "UserMainMenuID"));

            //Get the UserMainMenu Details based on id.
            UserMainMenuMaster generalUserMainMenuMaster = _generalUserMainMenuMasterRepository.Table.FirstOrDefault(x => x.UserMainMenuMasterId == userMainMenuId);
            UserMainMenuModel generalUserMainMenuModel = generalUserMainMenuMaster?.FromEntityToModel<UserMainMenuModel>();
            return generalUserMainMenuModel;
        }

        //Update UserMainMenu.
        public virtual bool UpdateUserMainMenu(UserMainMenuModel generalUserMainMenuModel)
        {
            if (IsNull(generalUserMainMenuModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalUserMainMenuModel.UserMainMenuMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "UserMainMenuID"));

            if (IsUserMainMenuCodeAlreadyExist(generalUserMainMenuModel.MenuCode, generalUserMainMenuModel.UserMainMenuMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Menu Code"));

            UserMainMenuMaster generalUserMainMenuMaster = generalUserMainMenuModel.FromModelToEntity<UserMainMenuMaster>();

            //Update UserMainMenu
            bool isUserMainMenuUpdated = _generalUserMainMenuMasterRepository.Update(generalUserMainMenuMaster);
            if (!isUserMainMenuUpdated)
            {
                generalUserMainMenuModel.HasError = true;
                generalUserMainMenuModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isUserMainMenuUpdated;
        }

        //Delete UserMainMenu.
        public virtual bool DeleteUserMainMenu(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "UserMainMenuID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("UserMainMenuId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteUserMainMenu @UserMainMenuId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if UserMainMenu code is already present or not.
        protected virtual bool IsUserMainMenuCodeAlreadyExist(string userMainMenuCode, short generalUserMainMenuMasterId = 0)
         => _generalUserMainMenuMasterRepository.Table.Any(x => x.MenuCode == userMainMenuCode && (x.UserMainMenuMasterId != generalUserMainMenuMasterId || generalUserMainMenuMasterId == 0));
        #endregion
    }
}
