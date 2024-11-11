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

        public virtual HospitalRegistrationFeeListModel GetHospitalRegistrationFeeList(string selectedCentreCode, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
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
            List<HospitalRegistrationFeeModel> registrationFeeList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetHospitalRegistrationFeeList @CentreCode,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
            HospitalRegistrationFeeListModel listModel = new HospitalRegistrationFeeListModel();

            listModel.HospitalRegistrationFeeList = registrationFeeList?.Count > 0 ? registrationFeeList : new List<HospitalRegistrationFeeModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create HospitalRegistrationFee.
        public virtual HospitalRegistrationFeeModel CreateRegistrationFee(HospitalRegistrationFeeModel hospitalRegistrationFeeModel)
        {
            if (IsNull(hospitalRegistrationFeeModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            HospitalRegistrationFee hospitalRegistrationFee = hospitalRegistrationFeeModel.FromModelToEntity<HospitalRegistrationFee>();

            //Create new HospitalRegistrationFee and return it.
            HospitalRegistrationFee hospitalRegistrationFeeData = _hospitalRegistrationFeeRepository.Insert(hospitalRegistrationFee);
            if (hospitalRegistrationFeeData?.HospitalRegistrationFeeId > 0)
            {
                hospitalRegistrationFeeModel.HospitalRegistrationFeeId = hospitalRegistrationFeeData.HospitalRegistrationFeeId;
                hospitalRegistrationFeeModel.SelectedCentreCode = hospitalRegistrationFeeModel.CentreCode;
            }
            else
            {
                hospitalRegistrationFeeModel.HasError = true;
                hospitalRegistrationFeeModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return hospitalRegistrationFeeModel;
        }

        //Get HospitalRegistrationFee by HospitalRegistrationFee id.
        public virtual HospitalRegistrationFeeModel GetRegistrationFee(int hospitalRegistrationFeeId)
        {
            if (hospitalRegistrationFeeId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalRegistrationFeeID"));

            //Get the HospitalRegistrationFee Details based on id.
            HospitalRegistrationFee hospitalRegistrationFee = _hospitalRegistrationFeeRepository.Table.Where(x => x.HospitalRegistrationFeeId == hospitalRegistrationFeeId)?.FirstOrDefault();
            HospitalRegistrationFeeModel hospitalRegistrationFeeModel = hospitalRegistrationFee?.FromEntityToModel<HospitalRegistrationFeeModel>();
            if (hospitalRegistrationFeeModel?.HospitalRegistrationFeeId > 0)
            {
                GeneralPersonModel generalPersonModel = GetGeneralPersonDetailsByEntityType(hospitalRegistrationFeeModel.HospitalRegistrationFeeId, UserTypeEnum.Employee.ToString());
                if (IsNotNull(generalPersonModel))
                {
                    hospitalRegistrationFeeModel.FirstName = generalPersonModel.FirstName;
                    hospitalRegistrationFeeModel.LastName = generalPersonModel.LastName;


                }
            }

            return hospitalRegistrationFeeModel;
        }

        //Update HospitalRegistrationFee.
        public virtual bool UpdateRegistrationFee(HospitalRegistrationFeeModel hospitalRegistrationFeeModel)
        {
            if (IsNull(hospitalRegistrationFeeModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (hospitalRegistrationFeeModel.HospitalRegistrationFeeId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalRegistrationFeeID"));

            HospitalRegistrationFee hospitalRegistrationFee = hospitalRegistrationFeeModel.FromModelToEntity<HospitalRegistrationFee>();

            //Update HospitalRegistrationFee
            bool isHospitalRegistrationFeeUpdated = _hospitalRegistrationFeeRepository.Update(hospitalRegistrationFee);
            if (!isHospitalRegistrationFeeUpdated)
            {
                hospitalRegistrationFeeModel.HasError = true;
                hospitalRegistrationFeeModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isHospitalRegistrationFeeUpdated;
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
        #endregion
    }
}
