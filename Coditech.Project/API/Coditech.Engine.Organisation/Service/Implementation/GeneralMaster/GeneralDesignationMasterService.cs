using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;

using System.Collections.Specialized;
using System.Data;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Service
{
    public class GeneralDesignationMasterService : BaseService, IGeneralDesignationMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<EmployeeDesignationMaster> _employeeDesignationMasterRepository;
        public GeneralDesignationMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _employeeDesignationMasterRepository = new CoditechRepository<EmployeeDesignationMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralDesignationListModel GetDesignationList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralDesignationModel> objStoredProc = new CoditechViewRepository<GeneralDesignationModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralDesignationModel> designationList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetEmployeeDesignationList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralDesignationListModel listModel = new GeneralDesignationListModel();

            listModel.GeneralDesignationList = designationList?.Count > 0 ? designationList : new List<GeneralDesignationModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create Designation.
        public virtual GeneralDesignationModel CreateDesignation(GeneralDesignationModel generalDesignationModel)
        {
            if (IsNull(generalDesignationModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);
            if (IsDesignationNameAlreadyExist(generalDesignationModel.Description))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Designation Name"));

            EmployeeDesignationMaster employeeDesignationMaster = generalDesignationModel.FromModelToEntity<EmployeeDesignationMaster>();

            //Create new Designation and return it.
            EmployeeDesignationMaster designationData = _employeeDesignationMasterRepository.Insert(employeeDesignationMaster);
            if (designationData?.EmployeeDesignationMasterId > 0)
            {
                generalDesignationModel.EmployeeDesignationMasterId = designationData.EmployeeDesignationMasterId;
            }
            else
            {
                generalDesignationModel.HasError = true;
                generalDesignationModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalDesignationModel;
        }

        //Get Designation by EmployeeDesignationMasterId.
        public virtual GeneralDesignationModel GetDesignation(short designationId)
        {
            if (designationId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DesignationId"));

            //Get the Designation Details based on id.
            EmployeeDesignationMaster designationData = _employeeDesignationMasterRepository.Table.FirstOrDefault(x => x.EmployeeDesignationMasterId == designationId);
            GeneralDesignationModel generalDesignationModel = designationData.FromEntityToModel<GeneralDesignationModel>();
            return generalDesignationModel;
        }

        //Update Designation.
        public virtual bool UpdateDesignation(GeneralDesignationModel generalDesignationModel)
        {
            if (IsNull(generalDesignationModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalDesignationModel.EmployeeDesignationMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DesignationId"));

            if (IsDesignationNameAlreadyExist(generalDesignationModel.Description, generalDesignationModel.EmployeeDesignationMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Designation Name"));

            EmployeeDesignationMaster employeeDesignationMaster = generalDesignationModel.FromModelToEntity<EmployeeDesignationMaster>();

            //Update Designation
            bool isDesignationUpdated = _employeeDesignationMasterRepository.Update(employeeDesignationMaster);
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
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteEmployeeDesignationMaster @DesignationId,  @Status OUT", 1, out status);
            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Designation Name is already present or not.
        protected virtual bool IsDesignationNameAlreadyExist(string designationName, int employeeDesignationMasterId = 0)
         => _employeeDesignationMasterRepository.Table.Any(x => x.Description == designationName && (x.EmployeeDesignationMasterId != employeeDesignationMasterId || employeeDesignationMasterId == 0));
        #endregion
    }
}

