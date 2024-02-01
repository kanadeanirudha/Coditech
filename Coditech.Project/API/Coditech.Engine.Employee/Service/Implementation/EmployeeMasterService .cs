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
    public class EmployeeMasterService : IEmployeeMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<EmployeeMaster> _employeeMasterRepository;
        public EmployeeMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _employeeMasterRepository = new CoditechRepository<EmployeeMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual EmployeeMasterListModel GetEmployeeList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<EmployeeMasterModel> objStoredProc = new CoditechViewRepository<EmployeeMasterModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<EmployeeMasterModel> EmployeeList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetEmployeeMasterList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
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
            EmployeeMaster employeeMaster = _employeeMasterRepository.Table.FirstOrDefault(x => x.EmployeeId == employeeId);
            EmployeeMasterModel employeeMasterModel = employeeMaster?.FromEntityToModel<EmployeeMasterModel>();
            return employeeMasterModel;
        }

        //Update Employee.
        public virtual bool UpdateEmployeeOtherDetail(EmployeeMasterModel employeeMasterModel)
        {
            if (IsNull(employeeMasterModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (employeeMasterModel.EmployeeId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EmployeeID"));

            EmployeeMaster employeeMaster = employeeMasterModel.FromModelToEntity<EmployeeMaster>();
                        //Update Employee
            bool isEmployeeUpdated = _employeeMasterRepository.Update(employeeMaster);
            if (!isEmployeeUpdated)
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
       
        #endregion
    }
}
