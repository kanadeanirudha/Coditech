
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
    public class GeneralDistrictMasterService : IGeneralDistrictMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralDistrictMaster> _generalDistrictMasterRepository;
        public GeneralDistrictMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalDistrictMasterRepository = new CoditechRepository<GeneralDistrictMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralDistrictListModel GetDistrictList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralDistrictModel> objStoredProc = new CoditechViewRepository<GeneralDistrictModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralDistrictModel> DistrictList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetDistrictList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralDistrictListModel listModel = new GeneralDistrictListModel();

            listModel.GeneralDistrictList = DistrictList?.Count > 0 ? DistrictList : new List<GeneralDistrictModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create District.
        public virtual GeneralDistrictModel CreateDistrict(GeneralDistrictModel generalDistrictModel)
        {
            if (IsNull(generalDistrictModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsDistrictNameAlreadyExist(generalDistrictModel.DistrictName))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "District Name"));

            GeneralDistrictMaster generalDistrictMaster = generalDistrictModel.FromModelToEntity<GeneralDistrictMaster>();

            //Create new District and return it.
            GeneralDistrictMaster districtData = _generalDistrictMasterRepository.Insert(generalDistrictMaster);
            if (districtData?.GeneralDistrictMasterId > 0)
            {
                generalDistrictModel.GeneralDistrictMasterId = districtData.GeneralDistrictMasterId;
            }
            else
            {
                generalDistrictModel.HasError = true;
                generalDistrictModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalDistrictModel;
        }

        //Get District by District id.
        public virtual GeneralDistrictModel GetDistrict(short districtId)
        {
            if (districtId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DistrictID"));

            //Get the District Details based on id.
            GeneralDistrictMaster generalDistrictMaster = _generalDistrictMasterRepository.Table.FirstOrDefault(x => x.GeneralDistrictMasterId == districtId);
            GeneralDistrictModel generalDistrictModel = generalDistrictMaster?.FromEntityToModel<GeneralDistrictModel>();
            return generalDistrictModel;
        }

        //Update District.
        public virtual bool UpdateDistrict(GeneralDistrictModel generalDistrictModel)
        {
            if (IsNull(generalDistrictModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalDistrictModel.GeneralDistrictMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DistrictID"));

            if (IsDistrictNameAlreadyExist(generalDistrictModel.DistrictName, generalDistrictModel.GeneralDistrictMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "District Name"));

            GeneralDistrictMaster generalDistrictMaster = generalDistrictModel.FromModelToEntity<GeneralDistrictMaster>();

            //Update District
            bool isDistrictUpdated = _generalDistrictMasterRepository.Update(generalDistrictMaster);
            if (!isDistrictUpdated)
            {
                generalDistrictModel.HasError = true;
                generalDistrictModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isDistrictUpdated;
        }

        //Delete District.
        public virtual bool DeleteDistrict(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DistrictID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("DistrictId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteDistrict @DistrictId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        //Get District list.
        public virtual GeneralDistrictListModel GetDistrictByRegionWise(int generalRegionMasterId)
        {
            GeneralDistrictListModel list = new GeneralDistrictListModel();
            list.GeneralDistrictList = (from a in _generalDistrictMasterRepository.Table
                                      where (a.GeneralRegionMasterId == generalRegionMasterId)
                                      select new GeneralDistrictModel()
                                      {
                                          GeneralDistrictMasterId = a.GeneralDistrictMasterId,
                                          DistrictName = a.DistrictName,                                        
                                      })?.ToList();
            return list;
        }

        #region Protected Method
        //Check if District Name is already present or not.
        protected virtual bool IsDistrictNameAlreadyExist(string districtName, short generalDistrictMasterId = 0)
         => _generalDistrictMasterRepository.Table.Any(x => x.DistrictName == districtName && (x.GeneralDistrictMasterId != generalDistrictMasterId || generalDistrictMasterId == 0));
        #endregion
    }
}
