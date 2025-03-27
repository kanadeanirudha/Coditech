
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
    public class UserTypeService : IUserTypeService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<UserType> _userTypeRepository;
        public UserTypeService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _userTypeRepository = new CoditechRepository<UserType>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual UserTypeListModel GetUserTypeList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<UserTypeModel> objStoredProc = new CoditechViewRepository<UserTypeModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<UserTypeModel> UserTypeList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetUserTypeList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            UserTypeListModel listModel = new UserTypeListModel();

            listModel.TypeList = UserTypeList?.Count > 0 ? UserTypeList : new List<UserTypeModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create User Type
        public virtual UserTypeModel CreateUserType(UserTypeModel userTypeModel)
        {
            if (IsNull(userTypeModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            UserType userType = userTypeModel.FromModelToEntity<UserType>();
            UserType userTypeData = _userTypeRepository.Insert(userType);
            if (userTypeData?.UserTypeId > 0)
            {
                userTypeModel.UserTypeId = userTypeData.UserTypeId;
            }
            else
            {
                userTypeModel.HasError = true;
                userTypeModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return userTypeModel;
        }

        //Get User Type by User Type id.
        public virtual UserTypeModel GetUserType(short userTypeId)
        {
            if (userTypeId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "UserTypeId"));

            
            UserType userType = _userTypeRepository.Table.FirstOrDefault(x => x.UserTypeId == userTypeId);
            UserTypeModel userTypeModel = userType?.FromEntityToModel<UserTypeModel>();
            return userTypeModel;
        }

        //Update User Type
        public virtual bool UpdateUserType(UserTypeModel userTypeModel)
        {
            if (IsNull(userTypeModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (userTypeModel.UserTypeId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "UserTypeId"));


            UserType userType = userTypeModel.FromModelToEntity<UserType>();

           
            bool isUserTypeUpdated = _userTypeRepository.Update(userType);
            if (!isUserTypeUpdated)
            {
                userTypeModel.HasError = true;
                userTypeModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isUserTypeUpdated;
        }


        public virtual bool DeleteUserType(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "UserTypeId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("UserTypeId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteUserType @UserTypeId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method

        #endregion
    }
}
