
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
    public class GazetteChaptersService : IGazetteChaptersService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GazetteChapters> _gazetteChaptersRepository;
        private readonly ICoditechRepository<GeneralDistrictMaster> _generalDistrictMasterRepository;
        private readonly ICoditechRepository<GeneralRegionMaster> _generalRegionMasterRepository;
        public GazetteChaptersService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _gazetteChaptersRepository = new CoditechRepository<GazetteChapters>(_serviceProvider.GetService<Coditech_Entities>());
            _generalDistrictMasterRepository = new CoditechRepository<GeneralDistrictMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _generalRegionMasterRepository = new CoditechRepository<GeneralRegionMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GazetteChaptersListModel GetGazetteChaptersList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GazetteChaptersModel> objStoredProc = new CoditechViewRepository<GazetteChaptersModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GazetteChaptersModel> GazetteChaptersList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetGazetteChaptersList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GazetteChaptersListModel listModel = new GazetteChaptersListModel();

            listModel.GazetteChaptersList = GazetteChaptersList?.Count > 0 ? GazetteChaptersList : new List<GazetteChaptersModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create GazetteChapters.
        public virtual GazetteChaptersModel CreateGazetteChapters(GazetteChaptersModel gazetteChaptersModel)
        {
            if (IsNull(gazetteChaptersModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsChapterNameAlreadyExist(gazetteChaptersModel.ChapterName))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Chapter Name"));

            GazetteChapters gazetteChapters = gazetteChaptersModel.FromModelToEntity<GazetteChapters>();

            //Create new GazetteChapters and return it.
            GazetteChapters gazetteChaptersData = _gazetteChaptersRepository.Insert(gazetteChapters);
            if (gazetteChaptersData?.GazetteChapterId > 0)
            {
                gazetteChaptersModel.GazetteChapterId = gazetteChaptersData.GazetteChapterId;
            }
            else
            {
                gazetteChaptersModel.HasError = true;
                gazetteChaptersModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return gazetteChaptersModel;
        }

        //Get GazetteChapters by GazetteChapter id.
        public virtual GazetteChaptersModel GetGazetteChapters(int gazetteChaptersId)
        {
            if (gazetteChaptersId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GazetteChapterID"));

            //Get the GazetteChapter Details based on id.
            GazetteChapters gazetteChapters = _gazetteChaptersRepository.Table.FirstOrDefault(x => x.GazetteChapterId == gazetteChaptersId);
            GazetteChaptersModel gazetteChaptersModel = gazetteChapters?.FromEntityToModel<GazetteChaptersModel>();
            if (gazetteChaptersModel?.GeneralDistrictMasterId > 0)
            {
                GeneralDistrictMaster generalDistrictMaster = _generalDistrictMasterRepository.Table.Where(x => x.GeneralDistrictMasterId == gazetteChaptersModel.GeneralDistrictMasterId)?.FirstOrDefault();
                gazetteChaptersModel.DistrictName = generalDistrictMaster.DistrictName;
                gazetteChaptersModel.GeneralRegionMasterId = Convert.ToInt16(generalDistrictMaster.GeneralRegionMasterId);
            }
            if (gazetteChaptersModel.GeneralRegionMasterId > 0)
            {
                gazetteChaptersModel.GeneralCountryMasterId = Convert.ToInt16(_generalRegionMasterRepository.Table.Where(x => x.GeneralRegionMasterId == gazetteChaptersModel.GeneralRegionMasterId).Select(y => y.GeneralCountryMasterId).FirstOrDefault());
            }
            return gazetteChaptersModel;
        }

        //Update GazetteChapters.
        public virtual bool UpdateGazetteChapters(GazetteChaptersModel gazetteChaptersModel)
        {
            if (IsNull(gazetteChaptersModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (gazetteChaptersModel.GazetteChapterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GazetteChapterID"));

            if (IsChapterNameAlreadyExist(gazetteChaptersModel.ChapterName, gazetteChaptersModel.GazetteChapterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Chapter Name"));

            GazetteChapters gazetteChapters = gazetteChaptersModel.FromModelToEntity<GazetteChapters>();

            //Update GazetteChapters.
            bool isGazetteChaptersUpdated = _gazetteChaptersRepository.Update(gazetteChapters);
            if (!isGazetteChaptersUpdated)
            {
                gazetteChaptersModel.HasError = true;
                gazetteChaptersModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isGazetteChaptersUpdated;
        }

        //Delete GazetteChapters.
        public virtual bool DeleteGazetteChapters(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GazetteChapterID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("GazetteChapterId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteGazetteChapters @GazetteChapterId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        //Get GazetteChapters list.
        public virtual GazetteChaptersListModel GetGazetteChaptersByDistrictWise(int generalDistrictMasterId)
        {
            GazetteChaptersListModel list = new GazetteChaptersListModel();
            list.GazetteChaptersList = (from a in _gazetteChaptersRepository.Table
                                        where (a.GeneralDistrictMasterId == generalDistrictMasterId)
                                        select new GazetteChaptersModel()
                                        {
                                            GazetteChapterId = a.GazetteChapterId,
                                            ChapterName = a.ChapterName,
                                        })?.ToList();
            return list;
        }

        #region Protected Method
        //Check if Chapter Name is already present or not.
        protected virtual bool IsChapterNameAlreadyExist(string chapterName, int gazetteChaptersId = 0)
         => _gazetteChaptersRepository.Table.Any(x => x.ChapterName == chapterName && (x.GazetteChapterId != gazetteChaptersId || gazetteChaptersId == 0));
        #endregion
    }
}
