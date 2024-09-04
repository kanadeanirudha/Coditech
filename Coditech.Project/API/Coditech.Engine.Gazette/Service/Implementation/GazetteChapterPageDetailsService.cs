
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
    public class GazetteChaptersPageDetailService : IGazetteChaptersPageDetailService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GazetteChapters> _gazetteChaptersRepository;
        private readonly ICoditechRepository<GazetteChaptersPageDetail> _gazetteChaptersPageDetailRepository;
        private readonly ICoditechRepository<GeneralDistrictMaster> _generalDistrictMasterRepository;
        private readonly ICoditechRepository<GeneralRegionMaster> _generalRegionMasterRepository;
        public GazetteChaptersPageDetailService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _gazetteChaptersRepository = new CoditechRepository<GazetteChapters>(_serviceProvider.GetService<Coditech_Entities>());
            _gazetteChaptersPageDetailRepository = new CoditechRepository<GazetteChaptersPageDetail>(_serviceProvider.GetService<Coditech_Entities>());
            _generalDistrictMasterRepository = new CoditechRepository<GeneralDistrictMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _generalRegionMasterRepository = new CoditechRepository<GeneralRegionMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GazetteChaptersPageDetailListModel GetGazetteChaptersPageDetailList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GazetteChaptersPageDetailModel> objStoredProc = new CoditechViewRepository<GazetteChaptersPageDetailModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GazetteChaptersPageDetailModel> GazetteChaptersPageDetailList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetGazetteChapterPageDetailsList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GazetteChaptersPageDetailListModel listModel = new GazetteChaptersPageDetailListModel();

            listModel.GazetteChaptersPageDetailList = GazetteChaptersPageDetailList?.Count > 0 ? GazetteChaptersPageDetailList : new List<GazetteChaptersPageDetailModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create GazetteChaptersPageDetail.
        public virtual GazetteChaptersPageDetailModel CreateGazetteChaptersPageDetail(GazetteChaptersPageDetailModel gazetteChaptersPageDetailModel)
        {
            if (IsNull(gazetteChaptersPageDetailModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsChapterNameAlreadyExist(gazetteChaptersPageDetailModel.GazetteChapterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Chapter Name"));

            GazetteChaptersPageDetail gazetteChaptersPageDetail = gazetteChaptersPageDetailModel.FromModelToEntity<GazetteChaptersPageDetail>();

            //Create new GazetteChaptersPageDetail and return it.
            GazetteChaptersPageDetail gazetteChaptersPageDetailData = _gazetteChaptersPageDetailRepository.Insert(gazetteChaptersPageDetail);
            if (gazetteChaptersPageDetailData?.GazetteChapterPageDetailId > 0)
            {
                gazetteChaptersPageDetailModel.GazetteChapterPageDetailId = gazetteChaptersPageDetailData.GazetteChapterPageDetailId;
            }
            else
            {
                gazetteChaptersPageDetailModel.HasError = true;
                gazetteChaptersPageDetailModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return gazetteChaptersPageDetailModel;
        }

        //Get GazetteChaptersPageDetail by GazetteChapterPageDetail id.
        public virtual GazetteChaptersPageDetailModel GetGazetteChaptersPageDetail(int gazetteChaptersPageDetailId)
        {
            if (gazetteChaptersPageDetailId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GazetteChapterPageDetailID"));

            //Get the GazetteChapterPageDetail Details based on id.
            GazetteChaptersPageDetail gazetteChaptersPageDetail = _gazetteChaptersPageDetailRepository.Table.FirstOrDefault(x => x.GazetteChapterPageDetailId == gazetteChaptersPageDetailId);
            GazetteChaptersPageDetailModel gazetteChaptersPageDetailModel = gazetteChaptersPageDetail?.FromEntityToModel<GazetteChaptersPageDetailModel>();
            if (gazetteChaptersPageDetailModel?.GazetteChapterPageDetailId > 0)
            {
                gazetteChaptersPageDetailModel.GeneralRegionMasterId = Convert.ToInt16(_generalDistrictMasterRepository.Table.Where(x => x.GeneralDistrictMasterId == gazetteChaptersPageDetailModel.GeneralDistrictMasterId)?.Select(y => y.GeneralRegionMasterId)?.FirstOrDefault());
            }
            if (gazetteChaptersPageDetailModel.GeneralRegionMasterId > 0)
            {
                gazetteChaptersPageDetailModel.GeneralCountryMasterId = Convert.ToInt16(_generalRegionMasterRepository.Table.Where(x => x.GeneralRegionMasterId == gazetteChaptersPageDetailModel.GeneralRegionMasterId).Select(y => y.GeneralCountryMasterId).FirstOrDefault());
            }
            return gazetteChaptersPageDetailModel;
        }

        //Update GazetteChaptersPageDetail.
        public virtual bool UpdateGazetteChaptersPageDetail(GazetteChaptersPageDetailModel gazetteChaptersPageDetailModel)
        {
            if (IsNull(gazetteChaptersPageDetailModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (gazetteChaptersPageDetailModel.GazetteChapterPageDetailId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GazetteChapterID"));

            if (IsChapterNameAlreadyExist(gazetteChaptersPageDetailModel.GazetteChapterId, gazetteChaptersPageDetailModel.GazetteChapterPageDetailId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Chapter Name"));

            GazetteChaptersPageDetail gazetteChaptersPageDetail = gazetteChaptersPageDetailModel.FromModelToEntity<GazetteChaptersPageDetail>();

            //Update GazetteChaptersPageDetail.
            bool isGazetteChaptersPageDetailUpdated = _gazetteChaptersPageDetailRepository.Update(gazetteChaptersPageDetail);
            if (!isGazetteChaptersPageDetailUpdated)
            {
                gazetteChaptersPageDetailModel.HasError = true;
                gazetteChaptersPageDetailModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isGazetteChaptersPageDetailUpdated;
        }

        //Delete GazetteChaptersPageDetail.
        public virtual bool DeleteGazetteChaptersPageDetail(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GazetteChapterPageDetailID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("GazetteChapterPageDetailId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteGazetteChaptersPageDetail @GazetteChapterId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Chapter Name is already present or not.
        protected virtual bool IsChapterNameAlreadyExist(int gazetteChapterId, int gazetteChaptersPageDetailId = 0)
         => _gazetteChaptersPageDetailRepository.Table.Any(x => x.GazetteChapterId == gazetteChapterId && (x.GazetteChapterPageDetailId != gazetteChaptersPageDetailId || gazetteChaptersPageDetailId == 0));
        #endregion
    }
}
