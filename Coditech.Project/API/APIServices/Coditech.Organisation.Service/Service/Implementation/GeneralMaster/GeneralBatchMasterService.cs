using Coditech.API.Data;
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
    public class GeneralBatchMasterService : BaseService, IGeneralBatchMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralBatchMaster> _generalBatchMasterRepository;
        private readonly ICoditechRepository<GeneralBatchUser> _generalBatchUserRepository;
        public GeneralBatchMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalBatchMasterRepository = new CoditechRepository<GeneralBatchMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _generalBatchUserRepository = new CoditechRepository<GeneralBatchUser>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralBatchListModel GetBatchList(string selectedCentreCode, long userId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralBatchModel> objStoredProc = new CoditechViewRepository<GeneralBatchModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@UserMasterId", userId, ParameterDirection.Input, DbType.Int64);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralBatchModel> generalBatchList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetGeneralBatchList @CentreCode,@UserMasterId, @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();
            GeneralBatchListModel listModel = new GeneralBatchListModel();

            listModel.GeneralBatchList = generalBatchList?.Count > 0 ? generalBatchList : new List<GeneralBatchModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create GeneralBatch.
        public virtual GeneralBatchModel CreateGeneralBatch(GeneralBatchModel generalBatchModel)
        {
            if (IsNull(generalBatchModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);
            
            GeneralBatchMaster generalBatchMaster = generalBatchModel.FromModelToEntity<GeneralBatchMaster>();
            generalBatchMaster.WeekDays = generalBatchModel.BatchFrequency == "Weekly" && generalBatchModel.SelectedWeekDays != null && generalBatchModel.SelectedWeekDays.Any() ? string.Join(",", generalBatchModel.SelectedWeekDays) : "";

            //Create new GeneralBatchMaster and return it.
            GeneralBatchMaster generalBatchData = _generalBatchMasterRepository.Insert(generalBatchMaster);
            if (generalBatchData?.GeneralBatchMasterId > 0)
            {
                generalBatchModel.GeneralBatchMasterId = generalBatchData.GeneralBatchMasterId;
            }
            else
            {
                generalBatchModel.HasError = true;
                generalBatchModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalBatchModel;
        }

        //Get GeneralBatchMaster by generalBatchMaster id.
        public virtual GeneralBatchModel GetGeneralBatch(int generalBatchMasterId)
        {
            if (generalBatchMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralBatchMasterId"));

            //Get the GeneralBatchMaster Details based on id.
            GeneralBatchMaster generalBatchMaster = _generalBatchMasterRepository.Table.Where(x => x.GeneralBatchMasterId == generalBatchMasterId)?.FirstOrDefault();
            GeneralBatchModel generalBatchModel = generalBatchMaster?.FromEntityToModel<GeneralBatchModel>();
            if (generalBatchModel?.Duration != null)
            {
                generalBatchModel.DurationHours = generalBatchModel.Duration.Value.Hours.ToString("D2");
                generalBatchModel.DurationMinutes = generalBatchModel.Duration.Value.Minutes.ToString("D2");
            }
            return generalBatchModel;
        }

        //Update GeneralBatchMaster.
        public virtual bool UpdateGeneralBatch(GeneralBatchModel generalBatchModel)
        {
            if (IsNull(generalBatchModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalBatchModel.GeneralBatchMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralBatchMasterId"));

            GeneralBatchMaster generalBatchMaster = generalBatchModel.FromModelToEntity<GeneralBatchMaster>();
            generalBatchMaster.WeekDays = generalBatchModel.BatchFrequency == "Weekly" && generalBatchModel.SelectedWeekDays != null && generalBatchModel.SelectedWeekDays.Any() ? string.Join(",", generalBatchModel.SelectedWeekDays) : "";

            //Update GeneralBatchMaster
            bool isGeneralBatchUpdated = _generalBatchMasterRepository.Update(generalBatchMaster);
            if (!isGeneralBatchUpdated)
            {
                generalBatchModel.HasError = true;
                generalBatchModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isGeneralBatchUpdated;
        }

        //Delete GeneralBatchMaster.
        public virtual bool DeleteGeneralBatch(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralBatchMasterId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("GeneralBatchMasterId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteGeneralBatch @GeneralBatchMasterId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region GeneralBatchUser
        public virtual GeneralBatchUserListModel GetGeneralBatchUserList(int generalBatchMasterId, string userType, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralBatchUserModel> objStoredProc = new CoditechViewRepository<GeneralBatchUserModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@GeneralBatchMasterId", generalBatchMasterId, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@UserType", userType, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralBatchUserModel> batchList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetGeneralBatchUserAssociatedList @GeneralBatchMasterId,@UserType,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();
            GeneralBatchUserListModel listModel = new GeneralBatchUserListModel();

            listModel.GeneralBatchUserList = batchList?.Count > 0 ? batchList : new List<GeneralBatchUserModel>();
            listModel.BindPageListModel(pageListModel);


            if (generalBatchMasterId > 0)
            {
                GeneralBatchModel model = GetGeneralBatch(generalBatchMasterId);
                if (IsNotNull(listModel))
                {
                    listModel.BatchName = model.BatchName;
                }
            }
            listModel.GeneralBatchMasterId = generalBatchMasterId;
            return listModel;
        }

        public virtual bool AssociateUnAssociateBatchwiseUser(GeneralBatchUserModel generalBatchUserModel)
        {
            bool isAssociateUnAssociateBatchwiseUser = false;

            GeneralBatchUser generalBatchUser = new GeneralBatchUser();
            if (generalBatchUserModel.GeneralBatchUserId > 0)  
            {
                generalBatchUser = _generalBatchUserRepository.Table.Where(x=>x.GeneralBatchUserId == generalBatchUserModel.GeneralBatchUserId)?.FirstOrDefault();
                isAssociateUnAssociateBatchwiseUser = _generalBatchUserRepository.Delete(generalBatchUser);
            }
            else  
            {
                 generalBatchUser =generalBatchUserModel.FromModelToEntity<GeneralBatchUser>();
                 generalBatchUser = _generalBatchUserRepository.Insert(generalBatchUser);
                isAssociateUnAssociateBatchwiseUser = generalBatchUser.GeneralBatchUserId > 0;
            }

            if (!isAssociateUnAssociateBatchwiseUser)
            {
                generalBatchUserModel.HasError = true;
                generalBatchUserModel.ErrorMessage = GeneralResources.UpdateErrorMessage;  
            }
            return isAssociateUnAssociateBatchwiseUser;  
        }
        #endregion
    }
}
