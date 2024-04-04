using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Model;
using Coditech.Resources;

using System.Collections.Specialized;
using System.Data;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class HospitalDoctorsService : BaseService, IHospitalDoctorsService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<HospitalDoctors> _hospitalDoctorsRepository;
        private readonly ICoditechRepository<HospitalDoctorsSchedules> _hospitalDoctorsSchedulesRepository;
        public HospitalDoctorsService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _hospitalDoctorsRepository = new CoditechRepository<HospitalDoctors>(_serviceProvider.GetService<Coditech_Entities>());
            _hospitalDoctorsSchedulesRepository = new CoditechRepository<HospitalDoctorsSchedules>(_serviceProvider.GetService<Coditech_Entities>());
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
            if (hospitalDoctorsModel?.EmployeeId > 0)
            {
                GeneralPersonModel generalPersonModel = GetGeneralPersonDetailsByEntityType(hospitalDoctorsModel.EmployeeId, UserTypeEnum.Employee.ToString());
                if (IsNotNull(generalPersonModel))
                {
                    hospitalDoctorsModel.FirstName = generalPersonModel.FirstName;
                    hospitalDoctorsModel.LastName = generalPersonModel.LastName;
                    hospitalDoctorsModel.SelectedCentreCode = generalPersonModel.SelectedCentreCode;
                    hospitalDoctorsModel.SelectedDepartmentId = generalPersonModel.SelectedDepartmentId;
                }
                hospitalDoctorsModel.OrganisationCentrewiseBuildingMasterId = Convert.ToInt16(new CoditechRepository<OrganisationCentrewiseBuildingRooms>(_serviceProvider.GetService<Coditech_Entities>()).Table.Where(x => x.OrganisationCentrewiseBuildingRoomId == hospitalDoctorsModel.OrganisationCentrewiseBuildingRoomId)?.FirstOrDefault()?.OrganisationCentrewiseBuildingMasterId);
            }
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

        //Get HospitalDoctorSchedule by hospitalDoctorScheduleId.
        public virtual HospitalDoctorScheduleModel GetHospitalDoctorSchedule(int hospitalDoctorId)
        {
            if (hospitalDoctorId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalDoctorId"));

            //Get the DoctorSchedule Details based on id.
            HospitalDoctorsSchedules hospitalDoctorsSchedules = _hospitalDoctorsSchedulesRepository.Table.FirstOrDefault(x => x.HospitalDoctorId == hospitalDoctorId);
            HospitalDoctorScheduleModel hospitalDoctorScheduleModel = IsNotNull(hospitalDoctorsSchedules) ? hospitalDoctorsSchedules?.FromEntityToModel<HospitalDoctorScheduleModel>() : new HospitalDoctorScheduleModel();
            //if (IsNotNull(hospitalDoctorScheduleModel))
            //{
            //    GeneralPersonModel generalPersonModel = GetGeneralPersonDetails(hospitalDoctorScheduleModel.PersonId);
            //    if (IsNotNull(hospitalDoctorScheduleModel))
            //    {
            //        hospitalDoctorScheduleModel.FirstName = generalPersonModel.FirstName;
            //        hospitalDoctorScheduleModel.LastName = generalPersonModel.LastName;
            //    }
            //}
            
            return hospitalDoctorScheduleModel;
        }

        //Update HospitalDoctorSchedule.
        public virtual bool UpdateHospitalDoctorSchedule(HospitalDoctorScheduleModel hospitalDoctorScheduleModel)
        {
            if (IsNull(hospitalDoctorScheduleModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (hospitalDoctorScheduleModel.HospitalDoctorId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalDoctorId"));

            if (IsEmployeeAlreadyExist(hospitalDoctorScheduleModel.EmployeeId, hospitalDoctorScheduleModel.HospitalDoctorId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "EmployeeId"));

            bool isHospitalDoctorScheduleUpdated = false;
            HospitalDoctorsSchedules hospitalDoctorsSchedules = hospitalDoctorScheduleModel.FromModelToEntity<HospitalDoctorsSchedules>();

            if (hospitalDoctorScheduleModel.HospitalDoctorScheduleId > 0)
                isHospitalDoctorScheduleUpdated = _hospitalDoctorsSchedulesRepository.Update(hospitalDoctorsSchedules);
            else
            {
                hospitalDoctorsSchedules = _hospitalDoctorsSchedulesRepository.Insert(hospitalDoctorsSchedules);
                isHospitalDoctorScheduleUpdated = hospitalDoctorsSchedules.HospitalDoctorScheduleId > 0;
            }

            if (!isHospitalDoctorScheduleUpdated)
            {
                hospitalDoctorScheduleModel.HasError = true;
                hospitalDoctorScheduleModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isHospitalDoctorScheduleUpdated;
        }

        #region Protected Method
        //Check if EmployeeId is already present or not.
        protected virtual bool IsEmployeeAlreadyExist(long employeeId, int hospitalDoctorId = 0)
         => _hospitalDoctorsRepository.Table.Any(x => x.EmployeeId == employeeId && (x.HospitalDoctorId != hospitalDoctorId || hospitalDoctorId == 0));
        #endregion
    }
}
