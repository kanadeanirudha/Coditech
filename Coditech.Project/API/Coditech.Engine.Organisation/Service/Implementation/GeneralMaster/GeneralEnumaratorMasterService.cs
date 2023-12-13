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
    public class GeneralEnumaratorMasterService : IGeneralEnumaratorMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralEnumaratorMaster> _generalEnumaratorRepository;
        //private readonly ICoditechRepository<GeneralRegionMaster> _generalRegionMasterRepository;
        public GeneralEnumaratorMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalEnumaratorRepository = new CoditechRepository<GeneralEnumaratorMaster>(_serviceProvider.GetService<Coditech_Entities>());
           // _generalRegionMasterRepository = new CoditechRepository<GeneralRegionMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralEnumaratorListModel GetEnumaratorList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralEnumaratorModel> objStoredProc = new CoditechViewRepository<GeneralEnumaratorModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralEnumaratorModel> EnumaratorList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetEnumaratorList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralEnumaratorListModel listModel = new GeneralEnumaratorListModel();

            listModel.GeneralEnumaratorList = EnumaratorList?.Count > 0 ? EnumaratorList : new List<GeneralEnumaratorModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create Enumarator.
        public virtual GeneralEnumaratorModel CreateEnumarator(GeneralEnumaratorModel generalEnumaratorModel)
        {
            if (IsNull(generalEnumaratorModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);
            if (IsEnumaratorNameAlreadyExist(generalEnumaratorModel.EnumGroupCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Enumarator Name"));
            GeneralEnumaratorMaster generalEnumarator = generalEnumaratorModel.FromModelToEntity<GeneralEnumaratorMaster>();

            //Create new Enumarator and return it.
            GeneralEnumaratorMaster EnumaratorData = _generalEnumaratorRepository.Insert(generalEnumarator);
            if (EnumaratorData?.GeneralEnumaratorId > 0)
            {
                generalEnumaratorModel.GeneralEnumaratorId = EnumaratorData.GeneralEnumaratorId;
            }
            else
            {
                generalEnumaratorModel.HasError = true;
                generalEnumaratorModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalEnumaratorModel;
        }

        //Get Enumarator by Enumarator id.
        public virtual GeneralEnumaratorModel GetEnumarator(int EnumaratorId)
        {
            if (EnumaratorId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EnumaratorID"));

            //Get the Enumarator Details based on id.
            GeneralEnumaratorMaster EnumaratorData = _generalEnumaratorRepository.Table.FirstOrDefault(x => x.GeneralEnumaratorId == EnumaratorId);
            GeneralEnumaratorModel generalEnumaratorModel = EnumaratorData.FromEntityToModel<GeneralEnumaratorModel>();
            if (IsNotNull(generalEnumaratorModel))
            {
                generalEnumaratorModel.GeneralEnumaratorId = _generalEnumaratorRepository.Table.FirstOrDefault(x => x.GeneralEnumaratorId == generalEnumaratorModel.GeneralEnumaratorId).GeneralEnumaratorId;
            }
            return generalEnumaratorModel;
        }

        //Update Enumarator.
        public virtual bool UpdateEnumarator(GeneralEnumaratorModel generalEnumaratorModel)
        {
            if (IsNull(generalEnumaratorModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalEnumaratorModel.GeneralEnumaratorId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EnumaratorID"));

            if (IsEnumaratorNameAlreadyExist(generalEnumaratorModel.EnumGroupCode, generalEnumaratorModel.GeneralEnumaratorId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Enumarator Name"));

            GeneralEnumaratorMaster GeneralEnumarator = generalEnumaratorModel.FromModelToEntity<GeneralEnumaratorMaster>();

            //Update Enumarator
            bool isEnumaratorUpdated = _generalEnumaratorRepository.Update(GeneralEnumarator);
            if (!isEnumaratorUpdated)
            {
                generalEnumaratorModel.HasError = true;
                generalEnumaratorModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isEnumaratorUpdated;
        }

        //Delete Enumarator.
        public virtual bool DeleteEnumarator(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EnumaratorID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("EnumaratorId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteEnumarator @EnumaratorId,  @Status OUT", 1, out status);
            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Enumarator code is already present or not.
        protected virtual bool IsEnumaratorNameAlreadyExist(string EnumaratorName, int GeneralEnumaratorId = 0)
         => _generalEnumaratorRepository.Table.Any(x => x.EnumGroupCode == EnumaratorName && (x.GeneralEnumaratorId != GeneralEnumaratorId || GeneralEnumaratorId == 0));
        #endregion
    }
}
