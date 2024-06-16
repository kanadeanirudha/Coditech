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
    public class EmployeeServiceService : BaseService, IEmployeeServiceService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<EmployeeService> _employeeServiceRepository;
        private readonly ICoditechRepository<EmployeeMaster> _employeeMasterRepository;
        public EmployeeServiceService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _employeeServiceRepository = new CoditechRepository<EmployeeService>(_serviceProvider.GetService<Coditech_Entities>());
            _employeeMasterRepository = new CoditechRepository<EmployeeMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual EmployeeServiceListModel GetEmployeeServiceList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            int employeeId = 0;
            int.TryParse(filters?.Find(x => string.Equals(x.FilterName, FilterKeys.EmployeeId, StringComparison.CurrentCultureIgnoreCase))?.FilterValue, out employeeId);
            filters.RemoveAll(x => x.FilterName == FilterKeys.EmployeeId);

            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<EmployeeServiceModel> objStoredProc = new CoditechViewRepository<EmployeeServiceModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@EmployeeId", employeeId, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<EmployeeServiceModel> EmployeeList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetEmployeeServiceList @EmployeeId,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
            EmployeeServiceListModel listModel = new EmployeeServiceListModel();

            listModel.EmployeeServiceList = EmployeeList?.Count > 0 ? EmployeeList : new List<EmployeeServiceModel>();
            listModel.BindPageListModel(pageListModel);
            GeneralPersonModel generalPersonModel = GetGeneralPersonDetailsByEntityType(employeeId, UserTypeEnum.Employee.ToString());
            if (IsNotNull(generalPersonModel))
            {
                listModel.FirstName = generalPersonModel.FirstName;
                listModel.LastName = generalPersonModel.LastName;
            }
            return listModel;
        }

        //Get Employee by Employee id.
        public virtual EmployeeServiceModel GetEmployeeService(long employeeId, long personId, long employeeServiceId)
        {
            if (employeeId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EmployeeId"));

            //Get the Employee Details based on id.
            EmployeeService employeeService = _employeeServiceRepository.Table.Where(x => x.EmployeeServiceId == employeeServiceId)?.FirstOrDefault();
            EmployeeServiceModel employeeServiceModel = IsNotNull(employeeService) ? employeeService?.FromEntityToModel<EmployeeServiceModel>() : new EmployeeServiceModel();
            employeeServiceModel.EmployeeId = employeeId;
            employeeServiceModel.PersonId = personId;
            if (employeeServiceModel?.EmployeeId > 0)
            {
                GeneralPersonModel generalPersonModel = GetGeneralPersonDetailsByEntityType(employeeServiceModel.EmployeeId, UserTypeEnum.Employee.ToString());
                if (IsNotNull(generalPersonModel))
                {
                    employeeServiceModel.FirstName = generalPersonModel.FirstName;
                    employeeServiceModel.LastName = generalPersonModel.LastName;
                }
            }
            return employeeServiceModel;
        }

        //Update Employee.
        public virtual bool UpdateEmployeeService(EmployeeServiceModel employeeServiceModel)
        {
            if (IsNull(employeeServiceModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (employeeServiceModel.EmployeeServiceId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EmployeeServiceId"));

            bool isEmployeeUpdated = false;
            EmployeeService employeeService = _employeeServiceRepository.Table.FirstOrDefault(x => x.EmployeeServiceId == employeeServiceModel.EmployeeServiceId);
            if (employeeServiceModel.EmployeeDesignationMasterId != employeeService.EmployeeDesignationMasterId || employeeServiceModel.EmployeeStageEnumId != employeeService.EmployeeStageEnumId)
            {
               
                employeeService.IsCurrentPosition = false;
                isEmployeeUpdated = _employeeServiceRepository.Update(employeeService);

                employeeService.EmployeeDesignationMasterId = employeeServiceModel.EmployeeDesignationMasterId;
                employeeService.EmployeeStageEnumId = employeeServiceModel.EmployeeStageEnumId;
                employeeService.EmployeeServiceId = 0;
                employeeService.IsCurrentPosition = true;
                _employeeServiceRepository.Insert(employeeService);
                if (employeeServiceModel.EmployeeDesignationMasterId != employeeService.EmployeeDesignationMasterId)
                {
                    EmployeeMaster employeeMaster = _employeeMasterRepository.Table.FirstOrDefault(x => x.EmployeeId == employeeServiceModel.EmployeeId);
                    employeeMaster.EmployeeDesignationMasterId = employeeServiceModel.EmployeeDesignationMasterId;
                    _employeeMasterRepository.Update(employeeMaster);
                }
            }
            else
            {
                employeeServiceModel.EmployeeCode = employeeService.EmployeeCode;
                employeeServiceModel.EmployeeId = employeeService.EmployeeId;
                employeeService = employeeServiceModel.FromEntityToModel<EmployeeService>();
                isEmployeeUpdated = _employeeServiceRepository.Update(employeeService);
            }

            if (!isEmployeeUpdated)
            {
                employeeServiceModel.HasError = true;
                employeeServiceModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isEmployeeUpdated;
        }

        //Delete Employee.
        public virtual bool DeleteEmployeeService(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EmployeeID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("EmployeeId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteEmployeeService @EmployeeId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Employee code is already present or not.
        protected virtual bool IsEmployeeCodeAlreadyExist(string employeeCode, long employeeId = 0)
         => _employeeServiceRepository.Table.Any(x => x.EmployeeCode == employeeCode && (x.EmployeeId != employeeId || employeeId == 0));
        #endregion
    }
}
