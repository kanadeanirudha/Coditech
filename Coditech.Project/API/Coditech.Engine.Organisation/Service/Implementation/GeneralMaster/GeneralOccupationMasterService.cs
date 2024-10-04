
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
    public class GeneralOccupationMasterService : IGeneralOccupationMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralOccupationMaster> _generalOccupationMasterRepository;
        public GeneralOccupationMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalOccupationMasterRepository = new CoditechRepository<GeneralOccupationMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralOccupationListModel GetOccupationList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralOccupationModel> objStoredProc = new CoditechViewRepository<GeneralOccupationModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralOccupationModel> OccupationList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetOccupationList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralOccupationListModel listModel = new GeneralOccupationListModel();

            listModel.GeneralOccupationList = OccupationList?.Count > 0 ? OccupationList : new List<GeneralOccupationModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create Occupation.
        public virtual GeneralOccupationModel CreateOccupation(GeneralOccupationModel generalOccupationModel)
        {
            if (IsNull(generalOccupationModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsOccupationNameAlreadyExist(generalOccupationModel.OccupationName))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Occupation Name"));

            GeneralOccupationMaster generalOccupationMaster = generalOccupationModel.FromModelToEntity<GeneralOccupationMaster>();

            //Create new Occupation and return it.
            GeneralOccupationMaster OccupationData = _generalOccupationMasterRepository.Insert(generalOccupationMaster);
            if (OccupationData?.GeneralOccupationMasterId > 0)
            {
                generalOccupationModel.GeneralOccupationMasterId = OccupationData.GeneralOccupationMasterId;
            }
            else
            {
                generalOccupationModel.HasError = true;
                generalOccupationModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalOccupationModel;
        }

        //Get Occupation by Occupation id.
        public virtual GeneralOccupationModel GetOccupation(short OccupationId)
        {
            if (OccupationId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "OccupationID"));

            //Get the Occupation Details based on id.
            GeneralOccupationMaster generalOccupationMaster = _generalOccupationMasterRepository.Table.FirstOrDefault(x => x.GeneralOccupationMasterId == OccupationId);
            GeneralOccupationModel generalOccupationModel = generalOccupationMaster?.FromEntityToModel<GeneralOccupationModel>();
            return generalOccupationModel;
        }

        //Update Occupation.
        public virtual bool UpdateOccupation(GeneralOccupationModel generalOccupationModel)
        {
            if (IsNull(generalOccupationModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalOccupationModel.GeneralOccupationMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "OccupationID"));

            if (IsOccupationNameAlreadyExist(generalOccupationModel.OccupationName, generalOccupationModel.GeneralOccupationMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Occupation Name"));

            GeneralOccupationMaster generalOccupationMaster = generalOccupationModel.FromModelToEntity<GeneralOccupationMaster>();

            //Update Occupation
            bool isOccupationUpdated = _generalOccupationMasterRepository.Update(generalOccupationMaster);
            if (!isOccupationUpdated)
            {
                generalOccupationModel.HasError = true;
                generalOccupationModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isOccupationUpdated;
        }

        //Delete Occupation.
        public virtual bool DeleteOccupation(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "OccupationID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("OccupationId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteOccupation @OccupationId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Occupation code is already present or not.
        protected virtual bool IsOccupationNameAlreadyExist(string occupationName, short generalOccupationMasterId = 0)
         => _generalOccupationMasterRepository.Table.Any(x => x.OccupationName == occupationName && (x.GeneralOccupationMasterId != generalOccupationMasterId || generalOccupationMasterId == 0));
        #endregion
    }
}
