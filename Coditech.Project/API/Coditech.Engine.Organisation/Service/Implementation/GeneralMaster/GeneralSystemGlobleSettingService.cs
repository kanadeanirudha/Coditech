
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
    public class GeneralSystemGlobleSettingService : IGeneralSystemGlobleSettingService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralSystemGlobleSettingMaster> _generalSystemGlobleSettingRepository;
        public GeneralSystemGlobleSettingService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalSystemGlobleSettingRepository = new CoditechRepository<GeneralSystemGlobleSettingMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralSystemGlobleSettingListModel GetSystemGlobleSettingList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralSystemGlobleSettingModel> objStoredProc = new CoditechViewRepository<GeneralSystemGlobleSettingModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralSystemGlobleSettingModel> systemGlobleSettingList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetGlobleSettingList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralSystemGlobleSettingListModel listModel = new GeneralSystemGlobleSettingListModel();

            listModel.GeneralSystemGlobleSettingList = systemGlobleSettingList?.Count > 0 ? systemGlobleSettingList : new List<GeneralSystemGlobleSettingModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create System Globle Setting.
        public virtual GeneralSystemGlobleSettingModel CreateSystemGlobleSetting(GeneralSystemGlobleSettingModel generalSystemGlobleSettingModel)
        {
            if (IsNull(generalSystemGlobleSettingModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsFeatureNameAlreadyExist(generalSystemGlobleSettingModel.FeatureName))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Feature Name"));

            GeneralSystemGlobleSettingMaster generalSystemGlobleSettingMaster = generalSystemGlobleSettingModel.FromModelToEntity<GeneralSystemGlobleSettingMaster>();

            //Create new  Globle Setting and return it.
            GeneralSystemGlobleSettingMaster globleSettingData = _generalSystemGlobleSettingRepository.Insert(generalSystemGlobleSettingMaster);
            if (globleSettingData?.GeneralSystemGlobleSettingMasterId > 0)
            {
                generalSystemGlobleSettingModel.GeneralSystemGlobleSettingMasterId = globleSettingData.GeneralSystemGlobleSettingMasterId;
            }
            else
            {
                generalSystemGlobleSettingModel.HasError = true;
                generalSystemGlobleSettingModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalSystemGlobleSettingModel;
        }

        //Get System Globle Setting by generalSystemGlobleSetting id.
        public virtual GeneralSystemGlobleSettingModel GetSystemGlobleSetting(short generalSystemGlobleSettingId)
        {
            if (generalSystemGlobleSettingId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralSystemGlobleSettingID"));

            //Get the Globle Setting Details based on id.
            GeneralSystemGlobleSettingMaster generalSystemGlobleSettingMaster = _generalSystemGlobleSettingRepository.Table.FirstOrDefault(x => x.GeneralSystemGlobleSettingMasterId == generalSystemGlobleSettingId);
            GeneralSystemGlobleSettingModel generalSystemGlobleSettingModel = generalSystemGlobleSettingMaster?.FromEntityToModel<GeneralSystemGlobleSettingModel>();
            return generalSystemGlobleSettingModel;
        }

        //Update System Globle Setting.
        public virtual bool UpdateSystemGlobleSetting(GeneralSystemGlobleSettingModel generalSystemGlobleSettingModel)
        {
            if (IsNull(generalSystemGlobleSettingModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalSystemGlobleSettingModel.GeneralSystemGlobleSettingMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GlobleID"));

            if (IsFeatureNameAlreadyExist(generalSystemGlobleSettingModel.FeatureName, generalSystemGlobleSettingModel.GeneralSystemGlobleSettingMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Feature Name"));

            GeneralSystemGlobleSettingMaster generalSystemGlobleSettingMaster = generalSystemGlobleSettingModel.FromModelToEntity<GeneralSystemGlobleSettingMaster>();

            //Update System Globle Setting
            bool isGlobleSettingUpdated = _generalSystemGlobleSettingRepository.Update(generalSystemGlobleSettingMaster);
            if (!isGlobleSettingUpdated)
            {
                generalSystemGlobleSettingModel.HasError = true;
                generalSystemGlobleSettingModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isGlobleSettingUpdated;
        }

        //Delete System Globle Setting.
        public virtual bool DeleteSystemGlobleSetting(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GlobleID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("GlobleId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteGlobleSetting @GlobleId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Feature Name is already present or not.
        protected virtual bool IsFeatureNameAlreadyExist(string featureName, short generalSystemGlobleSettingMasterId = 0)
         => _generalSystemGlobleSettingRepository.Table.Any(x => x.FeatureName == featureName && (x.GeneralSystemGlobleSettingMasterId != generalSystemGlobleSettingMasterId || generalSystemGlobleSettingMasterId == 0));
        #endregion
    }
}
