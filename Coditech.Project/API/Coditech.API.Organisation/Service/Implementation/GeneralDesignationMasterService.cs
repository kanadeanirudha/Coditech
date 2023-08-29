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
            CoditechViewRepository<GeneralDesignationModel> objStoredProc = new CoditechViewRepository<GeneralDesignationModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", null/*pageListModel?.SPWhereClause*/, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralDesignationModel> designationList = objStoredProc.ExecuteStoredProcedureList("RARIndia_GetEmployeeDesignationList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralDesignationListModel listModel = new GeneralDesignationListModel();

            listModel.GeneralDesignationList = designationList?.Count > 0 ? designationList : new List<GeneralDesignationModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create Designation.
        public GeneralDesignationModel CreateDesignation(GeneralDesignationModel generalDesignationModel)
        {
            if (IsNull(generalDesignationModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);
            if (IsNameAlreadyExist(generalDesignationModel.DesignationName))
            {
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Designation Name"));
            }
            GeneralDesignationMaster generalDesignationMaster = generalDesignationModel.FromModelToEntity<GeneralDesignationMaster>();

            //Create new Designation and return it.
            GeneralDesignationMaster designationData = _generalDesignationMasterRepository.Insert(generalDesignationMaster);
            if (designationData?.GeneralDesignationMasterId > 0)
            {
                generalDesignationModel.GeneralDesignationMasterId = designationData.GeneralDesignationMasterId;
            }
            else
            {
                generalDesignationModel.HasError = true;
                generalDesignationModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalDesignationModel;
        }

        //Get Designation by GeneralDesignationMasterId.
        public GeneralDesignationModel GetDesignation(int designationId)
        {
            if (designationId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DesignationId"));

            //Get the Designation Details based on id.
            GeneralDesignationMaster designationData = _generalDesignationMasterRepository.Table.FirstOrDefault(x => x.GeneralDesignationMasterId == designationId);
            GeneralDesignationModel generalDesignationModel = designationData.FromEntityToModel<GeneralDesignationModel>();
            return generalDesignationModel;
        }

        //Update Designation.
        public virtual bool UpdateDesignation(GeneralDesignationModel generalDesignationModel)
        {
            if (IsNull(generalDesignationModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalDesignationModel.GeneralDesignationMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DesignationId"));

            GeneralDesignationMaster generalDesignationMaster = generalDesignationModel.FromModelToEntity<GeneralDesignationMaster>();

            //Update Designation
            bool isDesignationUpdated = _generalDesignationMasterRepository.Update(generalDesignationMaster);
            if (!isDesignationUpdated)
            {
                generalDesignationModel.HasError = true;
                generalDesignationModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
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

