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
    public class GeneralDesignationMasterService : IGeneralDesignationMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralDesignationMaster> _generalDesignationMasterRepository;
        public GeneralDesignationMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalDesignationMasterRepository = new CoditechRepository<GeneralDesignationMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralDesignationListModel GetDesignationList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralDesignationMasterModel> objStoredProc = new CoditechViewRepository<GeneralDesignationMasterModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", null/*pageListModel?.SPWhereClause*/, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralDesignationMasterModel> designationList = objStoredProc.ExecuteStoredProcedureList("RARIndia_GetEmployeeDesignationList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralDesignationListModel listModel = new GeneralDesignationListModel();

            listModel.GeneralDesignationList = designationList?.Count > 0 ? designationList : new List<GeneralDesignationMasterModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create Designation.
        public GeneralDesignationMasterModel CreateDesignation(GeneralDesignationMasterModel generalDesignationMasterModel)
        {
            if (IsNull(generalDesignationMasterModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);
            if (IsNameAlreadyExist(generalDesignationMasterModel.DesignationName))
            {
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Designation Name"));
            }
            GeneralDesignationMaster generalDesignationMaster = generalDesignationMasterModel.FromModelToEntity<GeneralDesignationMaster>();

            //Create new Designation and return it.
            GeneralDesignationMaster designationData = _generalDesignationMasterRepository.Insert(generalDesignationMaster);
            if (designationData?.GeneralDesignationMasterId > 0)
            {
                generalDesignationMasterModel.GeneralDesignationMasterId = designationData.GeneralDesignationMasterId;
            }
            else
            {
                generalDesignationMasterModel.HasError = true;
                generalDesignationMasterModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalDesignationMasterModel;
        }

        //Get Designation by GeneralDesignationMasterId.
        public GeneralDesignationMasterModel GetDesignation(int designationId)
        {
            if (designationId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DesignationId"));

            //Get the Designation Details based on id.
            GeneralDesignationMaster designationData = _generalDesignationMasterRepository.Table.FirstOrDefault(x => x.GeneralDesignationMasterId == designationId);
            GeneralDesignationMasterModel generalDesignationMasterModel = designationData.FromEntityToModel<GeneralDesignationMasterModel>();
            return generalDesignationMasterModel;
        }

        //Update Designation.
        public virtual bool UpdateDesignation(GeneralDesignationMasterModel generalDesignationMasterModel)
        {
            if (IsNull(generalDesignationMasterModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalDesignationMasterModel.GeneralDesignationMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DesignationId"));

            GeneralDesignationMaster generalDesignationMaster = generalDesignationMasterModel.FromModelToEntity<GeneralDesignationMaster>();

            //Update Designation
            bool isDesignationUpdated = _generalDesignationMasterRepository.Update(generalDesignationMaster);
            if (!isDesignationUpdated)
            {
                generalDesignationMasterModel.HasError = true;
                generalDesignationMasterModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isDesignationUpdated;
        }

        //Delete Designation.
        public virtual bool DeleteDesignation(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DesignationId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("DesignationId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("RARIndia_DeleteEmployeeDesignationMaster @DesignationId,  @Status OUT", 1, out status);
            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Designation Name is already present or not.
        protected virtual bool IsNameAlreadyExist(string designationName)
         => _generalDesignationMasterRepository.Table.Any(x => x.DesignationName == designationName);
        #endregion
    }
}

