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
    public class GeneralRegionMasterService : IGeneralRegionMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralRegionMaster> _generalRegionMasterRepository;
        public GeneralRegionMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalRegionMasterRepository = new CoditechRepository<GeneralRegionMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralRegionListModel GetRegionList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralRegionModel> objStoredProc = new CoditechViewRepository<GeneralRegionModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralRegionModel> regionList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetRegionList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralRegionListModel listModel = new GeneralRegionListModel();

            listModel.GeneralRegionList = regionList?.Count > 0 ? regionList : new List<GeneralRegionModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create Region.
        public virtual GeneralRegionModel CreateRegion(GeneralRegionModel generalRegionModel)
        {
            if (IsNull(generalRegionModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsRegionNameAlreadyExist(generalRegionModel.RegionName))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Region Name"));

            GeneralRegionMaster generalRegionMaster = generalRegionModel.FromModelToEntity<GeneralRegionMaster>();

            //Create new Region and return it.
            GeneralRegionMaster regionData = _generalRegionMasterRepository.Insert(generalRegionMaster);
            if (regionData?.GeneralRegionMasterId > 0)
            {
                generalRegionModel.GeneralRegionMasterId = regionData.GeneralRegionMasterId;
            }
            else
            {
                generalRegionModel.HasError = true;
                generalRegionModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalRegionModel;
        }

        //Get Region by Region id.
        public virtual GeneralRegionModel GetRegion(short regionId)
        {
            if (regionId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "Region Name"));

            //Get the Region Details based on id.
            GeneralRegionMaster generalRegionMaster = _generalRegionMasterRepository.Table.FirstOrDefault(x => x.GeneralRegionMasterId == regionId);
            GeneralRegionModel generalRegionModel = generalRegionMaster?.FromEntityToModel<GeneralRegionModel>();
            return generalRegionModel;
        }

        //Update Region.
        public virtual bool UpdateRegion(GeneralRegionModel generalRegionModel)
        {
            if (IsNull(generalRegionModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalRegionModel.GeneralRegionMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "RegionID"));

            if (IsRegionNameAlreadyExist(generalRegionModel.RegionName, generalRegionModel.GeneralRegionMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Region Name"));

            GeneralRegionMaster generalRegionMaster = generalRegionModel.FromModelToEntity<GeneralRegionMaster>();

            //Update Region
            bool isRegionUpdated = _generalRegionMasterRepository.Update(generalRegionMaster);
            if (!isRegionUpdated)
            {
                generalRegionModel.HasError = true;
                generalRegionModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isRegionUpdated;
        }

        //Delete Region.
        public virtual bool DeleteRegion(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "RegionID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("RegionId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteRegion @RegionId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        //Get region list.
        public virtual GeneralRegionListModel GetRegionByCountryWise(int generalCountryMasterId)
        {
            GeneralRegionListModel list = new GeneralRegionListModel();
            list.GeneralRegionList =  (from a in _generalRegionMasterRepository.Table
                                      where (a.GeneralCountryMasterId == generalCountryMasterId)
                                      select new GeneralRegionModel()
                                      {
                                          GeneralRegionMasterId = a.GeneralRegionMasterId,
                                          RegionName = a.RegionName,
                                          ShortName = a.ShortName,
                                      })?.ToList();
            return list;
        }

        #region Protected Method
        //Check if Region Name is already present or not.
        protected virtual bool IsRegionNameAlreadyExist(string regionName, short generalRegionMasterId = 0)
         => _generalRegionMasterRepository.Table.Any(x => x.RegionName == regionName && (x.GeneralRegionMasterId != generalRegionMasterId || generalRegionMasterId == 0));
        #endregion
    }
}
