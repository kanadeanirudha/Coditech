
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
    public class CoditechApplicationSettingService : ICoditechApplicationSettingService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<CoditechApplicationSetting> _coditechApplicationSettingRepository;
        public CoditechApplicationSettingService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _coditechApplicationSettingRepository = new CoditechRepository<CoditechApplicationSetting>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual CoditechApplicationSettingListModel GetCoditechApplicationSettingList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<CoditechApplicationSettingModel> objStoredProc = new CoditechViewRepository<CoditechApplicationSettingModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<CoditechApplicationSettingModel> coditechApplicationSettingList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetCoditechApplicationSettingList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            CoditechApplicationSettingListModel listModel = new CoditechApplicationSettingListModel();

            listModel.CoditechApplicationSettingList = coditechApplicationSettingList?.Count > 0 ? coditechApplicationSettingList : new List<CoditechApplicationSettingModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create Coditech Application Setting.
        public virtual CoditechApplicationSettingModel CreateCoditechApplicationSetting(CoditechApplicationSettingModel coditechApplicationSettingModel)
        {
            if (IsNull(coditechApplicationSettingModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsApplicationCodeAlreadyExist(coditechApplicationSettingModel.ApplicationCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Application Code"));

            coditechApplicationSettingModel.ApplicationValue1 = string.IsNullOrEmpty(coditechApplicationSettingModel.ApplicationValue1) ? coditechApplicationSettingModel.ApplicationValue2 : coditechApplicationSettingModel.ApplicationValue1;
            CoditechApplicationSetting coditechApplicationSetting = coditechApplicationSettingModel.FromModelToEntity<CoditechApplicationSetting>();

            //Create new  Coditech Application Setting and return it.
            CoditechApplicationSetting coditechApplicationSettingData = _coditechApplicationSettingRepository.Insert(coditechApplicationSetting);
            if (coditechApplicationSettingData?.CoditechApplicationSettingId > 0)
            {
                coditechApplicationSettingModel.CoditechApplicationSettingId = coditechApplicationSettingData.CoditechApplicationSettingId;
            }
            else
            {
                coditechApplicationSettingModel.HasError = true;
                coditechApplicationSettingModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return coditechApplicationSettingModel;
        }

        //Get Coditech Application Setting by coditechApplicationSetting id.
        public virtual CoditechApplicationSettingModel GetCoditechApplicationSetting(short coditechApplicationSettingId)
        {
            if (coditechApplicationSettingId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "CoditechApplicationSettingID"));

            //Get the Coditech Application Setting Details based on id.
            CoditechApplicationSetting coditechApplicationSetting = _coditechApplicationSettingRepository.Table.FirstOrDefault(x => x.CoditechApplicationSettingId == coditechApplicationSettingId);
            CoditechApplicationSettingModel coditechApplicationSettingModel = coditechApplicationSetting?.FromEntityToModel<CoditechApplicationSettingModel>();
            return coditechApplicationSettingModel;
        }

        //Update Coditech Application Setting.
        public virtual bool UpdateCoditechApplicationSetting(CoditechApplicationSettingModel coditechApplicationSettingModel)
        {
            if (IsNull(coditechApplicationSettingModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (coditechApplicationSettingModel.CoditechApplicationSettingId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "CoditechApplicationID"));

            if (IsApplicationCodeAlreadyExist(coditechApplicationSettingModel.ApplicationCode, coditechApplicationSettingModel.CoditechApplicationSettingId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Application Code"));

            coditechApplicationSettingModel.ApplicationValue1 = string.IsNullOrEmpty(coditechApplicationSettingModel.ApplicationValue1) ? coditechApplicationSettingModel.ApplicationValue2 : coditechApplicationSettingModel.ApplicationValue1;

            CoditechApplicationSetting coditechApplicationSetting = coditechApplicationSettingModel.FromModelToEntity<CoditechApplicationSetting>();

            //Update Coditech Application Setting
            bool isCoditechApplicationSettingUpdated = _coditechApplicationSettingRepository.Update(coditechApplicationSetting);
            if (!isCoditechApplicationSettingUpdated)
            {
                coditechApplicationSettingModel.HasError = true;
                coditechApplicationSettingModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isCoditechApplicationSettingUpdated;
        }

        //Delete Coditech Application Setting.
        public virtual bool DeleteCoditechApplicationSetting(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "CoditechApplicationID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("CoditechApplicationId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteCoditechApplicationSetting @CoditechApplicationId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Application Code is already present or not.
        protected virtual bool IsApplicationCodeAlreadyExist(string applicationCode, short coditechApplicationSettingId = 0)
         => _coditechApplicationSettingRepository.Table.Any(x => x.ApplicationCode == applicationCode && (x.CoditechApplicationSettingId != coditechApplicationSettingId || coditechApplicationSettingId == 0));
        #endregion
    }
}
