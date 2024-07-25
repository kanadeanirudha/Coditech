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
    public class MediaSettingMasterService : IMediaSettingMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<MediaTypeMaster> _mediaTypeMasterRepository;
        private readonly ICoditechRepository<MediaSettingMaster> _mediaSettingMasterRepository;
        public MediaSettingMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _mediaTypeMasterRepository = new CoditechRepository<MediaTypeMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _mediaSettingMasterRepository = new CoditechRepository<MediaSettingMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual MediaSettingMasterListModel GetMediaSettingMasterList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<MediaSettingMasterModel> objStoredProc = new CoditechViewRepository<MediaSettingMasterModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<MediaSettingMasterModel> MediaSettingMasterList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetMediaSettingMasterList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            MediaSettingMasterListModel listModel = new MediaSettingMasterListModel();

            listModel.MediaSettingMasterList = MediaSettingMasterList?.Count > 0 ? MediaSettingMasterList : new List<MediaSettingMasterModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Get MediaSettingMaster by MediaSettingMaster id.
        public virtual MediaSettingMasterModel GetMediaSettingMaster(byte mediaTypeMasterId)
        {
            if (mediaTypeMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "mediaTypeMasterId"));

            //Get the MediaSettingMaster Details based on id.
            MediaSettingMaster mediaSettingMaster = _mediaSettingMasterRepository.Table.FirstOrDefault(x => x.MediaTypeMasterId == mediaTypeMasterId);
            MediaSettingMasterModel mediaSettingMasterModel = new MediaSettingMasterModel();
            if (IsNotNull(mediaSettingMaster))
            {
                mediaSettingMasterModel = mediaSettingMaster?.FromEntityToModel<MediaSettingMasterModel>();
            }
            mediaSettingMasterModel.MediaType = _mediaTypeMasterRepository.Table.Where(x => x.MediaTypeMasterId == mediaTypeMasterId)?.FirstOrDefault()?.MediaType;
            return mediaSettingMasterModel;
        }

        //Update MediaSettingMaster.
        public virtual bool UpdateMediaSettingMaster(MediaSettingMasterModel mediaSettingMasterModel)
        {
            if (IsNull(mediaSettingMasterModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            MediaSettingMaster mediaSettingMaster = mediaSettingMasterModel.FromModelToEntity<MediaSettingMaster>();
            bool isMediaSettingMasterUpdated = false;
            if (mediaSettingMasterModel.MediaSettingMasterId > 0)
            {
                mediaSettingMaster = _mediaSettingMasterRepository.Insert(mediaSettingMaster);
                if(mediaSettingMaster.MediaSettingMasterId > 0)
                {
                    isMediaSettingMasterUpdated = true;
                }
            }
            else
            {
                //Update MediaSettingMaster
                isMediaSettingMasterUpdated = _mediaSettingMasterRepository.Update(mediaSettingMaster);
            }
            if (!isMediaSettingMasterUpdated)
            {
                mediaSettingMasterModel.HasError = true;
                mediaSettingMasterModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isMediaSettingMasterUpdated;
        }

        //Delete MediaSettingMaster.
        public virtual bool DeleteMediaSettingMaster(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "MediaSettingMasterID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("MediaSettingMasterId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteMediaSettingMaster @MediaSettingMasterId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if MediaSettingMaster code is already present or not.
        //protected virtual bool IsMediaSettingMasterCodeAlreadyExist(string mediasettingmasterName, short mediaSettingMasterId = 0)
        // => _mediaSettingMasterRepository.Table.Any(x => x.MediaSettingMasterName == mediasettingmasterName && (x.MediaSettingMasterId != mediaSettingMasterId || mediaSettingMasterId == 0));
        #endregion
    }
}
