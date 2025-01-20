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
    public class GeneralMeasurementUnitMasterService : IGeneralMeasurementUnitMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralMeasurementUnitMaster> _generalMeasurementUnitMasterRepository;
        public GeneralMeasurementUnitMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalMeasurementUnitMasterRepository = new CoditechRepository<GeneralMeasurementUnitMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralMeasurementUnitListModel GetMeasurementUnitList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralMeasurementUnitModel> objStoredProc = new CoditechViewRepository<GeneralMeasurementUnitModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralMeasurementUnitModel> MeasurementUnitList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetMeasurementUnitMasterList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralMeasurementUnitListModel listModel = new GeneralMeasurementUnitListModel();

            listModel.GeneralMeasurementUnitList = MeasurementUnitList?.Count > 0 ? MeasurementUnitList : new List<GeneralMeasurementUnitModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create MeasurementUnit.
        public virtual GeneralMeasurementUnitModel CreateMeasurementUnit(GeneralMeasurementUnitModel generalMeasurementUnitModel)
        {
            if (IsNull(generalMeasurementUnitModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsMeasurementUnitCodeAlreadyExist(generalMeasurementUnitModel.MeasurementUnitShortCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "MeasurementUnit Code"));

            GeneralMeasurementUnitMaster generalMeasurementUnitMaster = generalMeasurementUnitModel.FromModelToEntity<GeneralMeasurementUnitMaster>();

            //Create new MeasurementUnit and return it.
            GeneralMeasurementUnitMaster MeasurementUnitData = _generalMeasurementUnitMasterRepository.Insert(generalMeasurementUnitMaster);
            if (MeasurementUnitData?.GeneralMeasurementUnitMasterId > 0)
            {
                generalMeasurementUnitModel.GeneralMeasurementUnitMasterId = MeasurementUnitData.GeneralMeasurementUnitMasterId;
            }
            else
            {
                generalMeasurementUnitModel.HasError = true;
                generalMeasurementUnitModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalMeasurementUnitModel;
        }

        //Get MeasurementUnit by MeasurementUnit id.
        public virtual GeneralMeasurementUnitModel GetMeasurementUnit(short MeasurementUnitId)
        {
            if (MeasurementUnitId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "MeasurementUnitID"));

            //Get the MeasurementUnit Details based on id.
            GeneralMeasurementUnitMaster generalMeasurementUnitMaster = _generalMeasurementUnitMasterRepository.Table.FirstOrDefault(x => x.GeneralMeasurementUnitMasterId == MeasurementUnitId);
            GeneralMeasurementUnitModel generalMeasurementUnitModel = generalMeasurementUnitMaster?.FromEntityToModel<GeneralMeasurementUnitModel>();
            return generalMeasurementUnitModel;
        }

        //Update MeasurementUnit.
        public virtual bool UpdateMeasurementUnit(GeneralMeasurementUnitModel generalMeasurementUnitModel)
        {
            if (IsNull(generalMeasurementUnitModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalMeasurementUnitModel.GeneralMeasurementUnitMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "MeasurementUnitID"));

            if (IsMeasurementUnitCodeAlreadyExist(generalMeasurementUnitModel.MeasurementUnitShortCode, generalMeasurementUnitModel.GeneralMeasurementUnitMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "MeasurementUnit Code"));

            GeneralMeasurementUnitMaster generalMeasurementUnitMaster = generalMeasurementUnitModel.FromModelToEntity<GeneralMeasurementUnitMaster>();

            //Update MeasurementUnit
            bool isMeasurementUnitUpdated = _generalMeasurementUnitMasterRepository.Update(generalMeasurementUnitMaster);
            if (!isMeasurementUnitUpdated)
            {
                generalMeasurementUnitModel.HasError = true;
                generalMeasurementUnitModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isMeasurementUnitUpdated;
        }

        //Delete MeasurementUnit.
        public virtual bool DeleteMeasurementUnit(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "MeasurementUnitID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("MeasurementUnitId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteMeasurementUnit @MeasurementUnitId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if MeasurementUnit code is already present or not.
        protected virtual bool IsMeasurementUnitCodeAlreadyExist(string measurementUnitShortCode, short generalMeasurementUnitMasterId = 0)
         => _generalMeasurementUnitMasterRepository.Table.Any(x => x.MeasurementUnitShortCode == measurementUnitShortCode && (x.GeneralMeasurementUnitMasterId != generalMeasurementUnitMasterId || generalMeasurementUnitMasterId == 0));
        #endregion
    }
}
