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
    public class HospitalDoctorsService : IHospitalDoctorsService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<HospitalDoctors> _hospitalDoctorsRepository;
        public HospitalDoctorsService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _hospitalDoctorsRepository = new CoditechRepository<HospitalDoctors>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual HospitalDoctorsListModel GetHospitalDoctorsList(string selectedCentreCode, short selectedDepartmentId, bool isAssociated, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<HospitalDoctorsModel> objStoredProc = new CoditechViewRepository<HospitalDoctorsModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@DepartmentId", selectedDepartmentId, ParameterDirection.Input, DbType.Int16);
            objStoredProc.SetParameter("@IsAssociated", isAssociated, ParameterDirection.Input, DbType.Boolean);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<HospitalDoctorsModel> hospitalDoctorsList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetHospitalDoctorList @CentreCode,@DepartmentId,@IsAssociated,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();
            HospitalDoctorsListModel listModel = new HospitalDoctorsListModel();

            listModel.HospitalDoctorsList = hospitalDoctorsList?.Count > 0 ? hospitalDoctorsList : new List<HospitalDoctorsModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create Hospital Doctors.
        public virtual HospitalDoctorsModel CreateHospitalDoctors(HospitalDoctorsModel hospitalDoctorsModel)
        {
            if (IsNull(hospitalDoctorsModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsEmployeeAlreadyExist(hospitalDoctorsModel.EmployeeId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "EmployeeId"));

            HospitalDoctors hospitalDoctors = hospitalDoctorsModel.FromModelToEntity<HospitalDoctors>();

            //Create new Hospital Doctors and return it.
            HospitalDoctors hospitalDoctorsData = _hospitalDoctorsRepository.Insert(hospitalDoctors);
            if (hospitalDoctorsData?.HospitalDoctorId > 0)
            {
                hospitalDoctorsModel.HospitalDoctorId = hospitalDoctorsData.HospitalDoctorId;
            }
            else
            {
                hospitalDoctorsModel.HasError = true;
                hospitalDoctorsModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return hospitalDoctorsModel;
        }

        //Get HospitalDoctors by  hospital Doctor id.
        public virtual HospitalDoctorsModel GetHospitalDoctors(int hospitalDoctorId)
        {
            if (hospitalDoctorId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalDoctorId"));

            //Get the HospitalDoctors Details based on id.
            HospitalDoctors hospitalDoctors = _hospitalDoctorsRepository.Table.FirstOrDefault(x => x.HospitalDoctorId == hospitalDoctorId);
            HospitalDoctorsModel hospitalDoctorsModel = hospitalDoctors?.FromEntityToModel<HospitalDoctorsModel>();
            return hospitalDoctorsModel;
        }

        //Update HospitalDoctors.
        public virtual bool UpdateHospitalDoctors(HospitalDoctorsModel hospitalDoctorsModel)
        {
            if (IsNull(hospitalDoctorsModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (hospitalDoctorsModel.HospitalDoctorId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalDoctorId"));

            if (IsEmployeeAlreadyExist(hospitalDoctorsModel.EmployeeId, hospitalDoctorsModel.HospitalDoctorId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "EmployeeId"));

            HospitalDoctors hospitalDoctors = hospitalDoctorsModel.FromModelToEntity<HospitalDoctors>();
            //Update Hospital Doctors
            bool isHospitalDoctorsUpdated = _hospitalDoctorsRepository.Update(hospitalDoctors);
            if (!isHospitalDoctorsUpdated)
            {
                hospitalDoctorsModel.HasError = true;
                hospitalDoctorsModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isHospitalDoctorsUpdated;
        }

        //Delete Hospital Doctors.
        public virtual bool DeleteHospitalDoctors(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EmployeeID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("HospitalDoctorId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteEmployee @HospitalDoctorId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if EmployeeId is already present or not.
        protected virtual bool IsEmployeeAlreadyExist(long employeeId, long hospitalDoctorId = 0)
         => _hospitalDoctorsRepository.Table.Any(x => x.EmployeeId == employeeId && (x.HospitalDoctorId != hospitalDoctorId || hospitalDoctorId == 0));
        #endregion
    }
}
