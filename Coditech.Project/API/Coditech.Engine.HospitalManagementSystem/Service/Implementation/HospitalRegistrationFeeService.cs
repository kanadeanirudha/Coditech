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
    public class HospitalRegistrationFeeService : BaseService, IHospitalRegistrationFeeService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<HospitalRegistrationFee> _hospitalRegistrationFeeRepository;
        public HospitalRegistrationFeeService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _hospitalRegistrationFeeRepository = new CoditechRepository<HospitalRegistrationFee>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual HospitalRegistrationFeeListModel GetRegistrationFeeList(string selectedCentreCode, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<HospitalRegistrationFeeModel> objStoredProc = new CoditechViewRepository<HospitalRegistrationFeeModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<HospitalRegistrationFeeModel> RegistrationFeeList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetHospitalRegistrationFeeList @CentreCode,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
            HospitalRegistrationFeeListModel listModel = new HospitalRegistrationFeeListModel();

            listModel.HospitalRegistrationFeeList = RegistrationFeeList?.Count > 0 ? RegistrationFeeList : new List<HospitalRegistrationFeeModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Get RegistrationFee by RegistrationFee id.
        public virtual HospitalRegistrationFeeModel GetRegistrationFee(int hospitalRegistrationFeeId)
        {
            if (hospitalRegistrationFeeId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalRegistrationFeeId"));

            //Get the PatientRegistration Details based on id.
            HospitalRegistrationFee hospitalRegistrationFee = _hospitalRegistrationFeeRepository.Table.Where(x => x.HospitalRegistrationFeeId == hospitalRegistrationFeeId)?.FirstOrDefault();
            HospitalRegistrationFeeModel hospitalRegistrationFeeModel = IsNotNull(hospitalRegistrationFee) ? hospitalRegistrationFee?.FromEntityToModel<HospitalRegistrationFeeModel>() : new HospitalRegistrationFeeModel();
            if (IsNotNull(hospitalRegistrationFeeModel))
            {
                GeneralPersonModel generalPersonModel = GetGeneralPersonDetails(hospitalRegistrationFeeModel.PersonId);
                if (IsNotNull(hospitalRegistrationFeeModel))
                {
                    hospitalRegistrationFeeModel.FirstName = generalPersonModel.FirstName;
                    hospitalRegistrationFeeModel.LastName = generalPersonModel.LastName;

                }
            }
            return hospitalRegistrationFeeModel;
        }

        //Delete RegistrationFee.
        public virtual bool DeleteRegistrationFee(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "RegistrationFeeID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("HospitalRegistrationFeeId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteHospitalRegistrationFee @HospitalRegistrationFeeId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Person code is already present or not.
      //  protected virtual bool IsPersonCodeAlreadyExist(string personCode, int hospitalRegistrationFeeId = 0)
      //   => _hospitalRegistrationFeeRepository.Table.Any(x => x.UAHNumber == personCode && (x.HospitalRegistrationFeeId != hospitalRegistrationFeeId || hospitalRegistrationFeeId == 0));

        #endregion
    }
}
