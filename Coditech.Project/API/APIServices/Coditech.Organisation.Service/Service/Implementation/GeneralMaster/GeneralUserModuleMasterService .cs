using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Specialized;
using System.Data;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class GeneralUserModuleMasterService : IGeneralUserModuleMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<UserModuleMaster> _generalUserModuleMasterRepository;
        public GeneralUserModuleMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalUserModuleMasterRepository = new CoditechRepository<UserModuleMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual UserModuleListModel GetUserModuleList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<UserModuleModel> objStoredProc = new CoditechViewRepository<UserModuleModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<UserModuleModel> userModuleList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetUserModuleList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            UserModuleListModel listModel = new UserModuleListModel();

            listModel.ModuleList = userModuleList?.Count > 0 ? userModuleList : new List<UserModuleModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create UserModule.
        public virtual UserModuleModel CreateUserModule(UserModuleModel generalUserModuleModel)
        {
            if (IsNull(generalUserModuleModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsUserModuleCodeAlreadyExist(generalUserModuleModel.ModuleCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Module Code"));

            UserModuleMaster generalUserModuleMaster = generalUserModuleModel.FromModelToEntity<UserModuleMaster>();

            //Create new UserModule and return it.
            UserModuleMaster userModuleData = _generalUserModuleMasterRepository.Insert(generalUserModuleMaster);
            if (userModuleData?.UserModuleMasterId > 0)
            {
                generalUserModuleModel.UserModuleMasterId = userModuleData.UserModuleMasterId;
            }
            else
            {
                generalUserModuleModel.HasError = true;
                generalUserModuleModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalUserModuleModel;
        }

        //Get UserModule by UserModule id.
        public virtual UserModuleModel GetUserModule(short userModuleMasterId)
        {
            if (userModuleMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "UserModuleMasterID"));

            //Get the UserModule Details based on id.
            UserModuleMaster generalUserModuleMaster = _generalUserModuleMasterRepository.Table.FirstOrDefault(x => x.UserModuleMasterId == userModuleMasterId);
            UserModuleModel generalUserModuleModel = generalUserModuleMaster?.FromEntityToModel<UserModuleModel>();
            return generalUserModuleModel;
        }

        //Update UserModuleMaster.
        public virtual bool UpdateUserModule(UserModuleModel generalUserModuleModel)
        {
            if (IsNull(generalUserModuleModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalUserModuleModel.UserModuleMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "UserModuleMasterID"));

            if (IsUserModuleCodeAlreadyExist(generalUserModuleModel.ModuleCode, generalUserModuleModel.UserModuleMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Module Code"));

            UserModuleMaster generalUserModuleMaster = generalUserModuleModel.FromModelToEntity<UserModuleMaster>();

            //Update UserMainMenu
            bool isUserModuleUpdated = _generalUserModuleMasterRepository.Update(generalUserModuleMaster);
            if (!isUserModuleUpdated)
            {
                generalUserModuleModel.HasError = true;
                generalUserModuleModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isUserModuleUpdated;
        }

        //Delete UserModule.
        public virtual bool DeleteUserModule(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "UserModuleMasterID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("UserModuleMasterId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteUserModule @UserModuleMasterId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if UserModule code is already present or not.
        protected virtual bool IsUserModuleCodeAlreadyExist(string userModuleCode, short userModuleMasterId = 0)
         => _generalUserModuleMasterRepository.Table.Any(x => x.ModuleCode == userModuleCode && (x.UserModuleMasterId != userModuleMasterId || userModuleMasterId == 0));
        #endregion
    }
}
