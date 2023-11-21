
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
    public class GeneralDepartmentMasterService : IGeneralDepartmentMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralDepartmentMaster> _generalDepartmentMasterRepository;
        private readonly ICoditechRepository<OrganisationCentrewiseDepartment> _organisationCentrewiseDepartmentRepository;
        public GeneralDepartmentMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalDepartmentMasterRepository = new CoditechRepository<GeneralDepartmentMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _organisationCentrewiseDepartmentRepository = new CoditechRepository<OrganisationCentrewiseDepartment>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralDepartmentListModel GetDepartmentList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralDepartmentModel> objStoredProc = new CoditechViewRepository<GeneralDepartmentModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralDepartmentModel> DepartmentList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetDepartmentList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralDepartmentListModel listModel = new GeneralDepartmentListModel();

            listModel.GeneralDepartmentList = DepartmentList?.Count > 0 ? DepartmentList : new List<GeneralDepartmentModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create Department.
        public virtual GeneralDepartmentModel CreateDepartment(GeneralDepartmentModel generalDepartmentModel)
        {
            if (IsNull(generalDepartmentModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsDepartmentCodeAlreadyExist(generalDepartmentModel.DepartmentShortCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Department Short Code"));

            GeneralDepartmentMaster generalDepartmentMaster = generalDepartmentModel.FromModelToEntity<GeneralDepartmentMaster>();

            //Create new Department and return it.
            GeneralDepartmentMaster departmentData = _generalDepartmentMasterRepository.Insert(generalDepartmentMaster);
            if (departmentData?.GeneralDepartmentMasterId > 0)
            {
                generalDepartmentModel.GeneralDepartmentMasterId = departmentData.GeneralDepartmentMasterId;
            }
            else
            {
                generalDepartmentModel.HasError = true;
                generalDepartmentModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalDepartmentModel;
        }

        //Get Department by Department id.
        public virtual GeneralDepartmentModel GetDepartment(short departmentId)
        {
            if (departmentId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DepartmentID"));

            //Get the Department Details based on id.
            GeneralDepartmentMaster generalDepartmentMaster = _generalDepartmentMasterRepository.Table.FirstOrDefault(x => x.GeneralDepartmentMasterId == departmentId);
            GeneralDepartmentModel generalDepartmentModel = generalDepartmentMaster?.FromEntityToModel<GeneralDepartmentModel>();
            return generalDepartmentModel;
        }

        //Update Department.
        public virtual bool UpdateDepartment(GeneralDepartmentModel generalDepartmentModel)
        {
            if (IsNull(generalDepartmentModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalDepartmentModel.GeneralDepartmentMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DepartmentID"));

            if (IsDepartmentCodeAlreadyExist(generalDepartmentModel.DepartmentShortCode, generalDepartmentModel.GeneralDepartmentMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Department Short Code"));

            GeneralDepartmentMaster generalDepartmentMaster = generalDepartmentModel.FromModelToEntity<GeneralDepartmentMaster>();

            //Update Department
            bool isDepartmentUpdated = _generalDepartmentMasterRepository.Update(generalDepartmentMaster);
            if (!isDepartmentUpdated)
            {
                generalDepartmentModel.HasError = true;
                generalDepartmentModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isDepartmentUpdated;
        }

        //Delete Department.
        public virtual bool DeleteDepartment(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DepartmentID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("DepartmentId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteDepartment @DepartmentId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        //Get department list.
        public virtual GeneralDepartmentListModel GetDepartmentsByCentreCode(string centreCode)
        {
            GeneralDepartmentListModel list = new GeneralDepartmentListModel();
            list.GeneralDepartmentList = (from a in _generalDepartmentMasterRepository.Table
                                          join b in _organisationCentrewiseDepartmentRepository.Table
                                          on a.GeneralDepartmentMasterId equals b.GeneralDepartmentMasterId
                                          where (b.CentreCode == centreCode || centreCode == null)
                                          select new GeneralDepartmentModel()
                                          {
                                              GeneralDepartmentMasterId = a.GeneralDepartmentMasterId,
                                              DepartmentName = a.DepartmentName,
                                              DepartmentShortCode = a.DepartmentShortCode,
                                              PrintShortDesc = a.PrintShortDesc
                                          })?.ToList();
            return list;
        }

        #region Protected Method

        //Check if Department code is already present or not.
        protected virtual bool IsDepartmentCodeAlreadyExist(string departmentShortCode, short generalDepartmentMasterId = 0)
         => _generalDepartmentMasterRepository.Table.Any(x => x.DepartmentShortCode == departmentShortCode && (x.GeneralDepartmentMasterId != generalDepartmentMasterId || generalDepartmentMasterId == 0));
        #endregion
    }
}
