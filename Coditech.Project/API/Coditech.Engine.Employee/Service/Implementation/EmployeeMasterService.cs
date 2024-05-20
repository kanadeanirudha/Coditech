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
    public class EmployeeMasterService : BaseService, IEmployeeMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<EmployeeMaster> _employeeMasterRepository;
        public EmployeeMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _employeeMasterRepository = new CoditechRepository<EmployeeMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual EmployeeMasterListModel GetEmployeeList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            int selectedDepartmentId = 0;
            int.TryParse(filters?.Find(x => string.Equals(x.FilterName, FilterKeys.SelectedDepartmentId, StringComparison.CurrentCultureIgnoreCase))?.FilterValue, out selectedDepartmentId);

            string selectedCentreCode = filters?.Find(x => string.Equals(x.FilterName, FilterKeys.SelectedCentreCode, StringComparison.CurrentCultureIgnoreCase))?.FilterValue;

            filters.RemoveAll(x => x.FilterName == FilterKeys.SelectedDepartmentId || x.FilterName == FilterKeys.SelectedCentreCode);

            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<EmployeeMasterModel> objStoredProc = new CoditechViewRepository<EmployeeMasterModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@DepartmentId", selectedDepartmentId, ParameterDirection.Input, DbType.Int16);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<EmployeeMasterModel> EmployeeList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetEmployeeMasterList @CentreCode,@DepartmentId,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();
            EmployeeMasterListModel listModel = new EmployeeMasterListModel();

            listModel.EmployeeMasterList = EmployeeList?.Count > 0 ? EmployeeList : new List<EmployeeMasterModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Get Employee by Employee id.
        public virtual EmployeeMasterModel GetEmployeeOtherDetail(long employeeId)
        {
            if (employeeId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EmployeeId"));

            //Get the Employee Details based on id.
            EmployeeMaster employeeMaster = _employeeMasterRepository.Table.Where(x => x.EmployeeId == employeeId)?.FirstOrDefault();
            EmployeeMasterModel employeeMasterModel = IsNotNull(employeeMaster) ? employeeMaster?.FromEntityToModel<EmployeeMasterModel>() : new EmployeeMasterModel();
            if (IsNotNull(employeeMasterModel))
            {
                GeneralPersonModel generalPersonModel = GetGeneralPersonDetails(employeeMasterModel.PersonId);
                if (IsNotNull(employeeMasterModel))
                {
                    employeeMasterModel.FirstName = generalPersonModel.FirstName;
                    employeeMasterModel.LastName = generalPersonModel.LastName;
                }
            }
            return employeeMasterModel;
        }

        //Update Employee.
        public virtual bool UpdateEmployeeOtherDetail(EmployeeMasterModel employeeMasterModel)
        {
            if (IsNull(employeeMasterModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (employeeMasterModel.EmployeeId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EmployeeID"));

            EmployeeMaster employeeMaster = _employeeMasterRepository.Table.FirstOrDefault(x => x.EmployeeId == employeeMasterModel.EmployeeId);
            BindEmployeeOtherDetail(employeeMasterModel, employeeMaster);

            //Update Employee
            bool isEmployeeUpdated = _employeeMasterRepository.Update(employeeMaster);
            if (isEmployeeUpdated)
            {
                ActiveInActiveUserLogin(employeeMasterModel.IsActive, employeeMasterModel.EmployeeId, UserTypeEnum.Employee.ToString());
            }
            else
            {
                employeeMasterModel.HasError = true;
                employeeMasterModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isEmployeeUpdated;
        }

        //Delete Employee.
        public virtual bool DeleteEmployee(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EmployeeID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("EmployeeId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteEmployee @EmployeeId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Person code is already present or not.
        protected virtual bool IsPersonCodeAlreadyExist(string personCode, long employeeId = 0)
         => _employeeMasterRepository.Table.Any(x => x.PersonCode == personCode && (x.EmployeeId != employeeId || employeeId == 0));


        protected virtual void BindEmployeeOtherDetail(EmployeeMasterModel employeeMasterModel, EmployeeMaster employeeMaster)
        {
            employeeMaster.EmployeeDesignationMasterId = employeeMasterModel.EmployeeDesignationMasterId;
            employeeMaster.IsEmployeeSmoker = employeeMasterModel.IsEmployeeSmoker;
            employeeMaster.ReportingEmployeeId = employeeMasterModel.ReportingEmployeeId;
            employeeMaster.PANCardNumber = employeeMasterModel.PANCardNumber;
            employeeMaster.UANNumber = employeeMasterModel.UANNumber;
            employeeMaster.PassportNumber = employeeMasterModel.PassportNumber;
            employeeMaster.AdharCardNumber = employeeMasterModel.AdharCardNumber;
            employeeMaster.BankAccountNumber = employeeMasterModel.BankAccountNumber;
            employeeMaster.BankName = employeeMasterModel.BankName;
            employeeMaster.BankIFSCCode = employeeMasterModel.BankIFSCCode;
            employeeMaster.IsActive = employeeMasterModel.IsActive;
        }

        #endregion
    }
}
