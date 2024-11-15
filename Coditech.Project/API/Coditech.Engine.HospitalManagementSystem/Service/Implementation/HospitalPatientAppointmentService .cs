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
    public class HospitalPatientAppointmentService : BaseService, IHospitalPatientAppointmentService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<HospitalPatientAppointment> _hospitalPatientAppointmentRepository;
        private readonly ICoditechRepository<HospitalDoctors> _hospitalDoctorsRepository;
        private readonly ICoditechRepository<EmployeeMaster> _employeeMasterRepository;
        private readonly ICoditechRepository<GeneralPerson> _generalPersonRepository;

        public HospitalPatientAppointmentService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _hospitalPatientAppointmentRepository = new CoditechRepository<HospitalPatientAppointment>(_serviceProvider.GetService<Coditech_Entities>());
            _hospitalDoctorsRepository = new CoditechRepository<HospitalDoctors>(_serviceProvider.GetService<Coditech_Entities>());
            _employeeMasterRepository = new CoditechRepository<EmployeeMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _generalPersonRepository = new CoditechRepository<GeneralPerson>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual HospitalPatientAppointmentListModel GetHospitalPatientAppointmentList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            string selectedCentreCode = filters?.Find(x => string.Equals(x.FilterName, FilterKeys.SelectedCentreCode, StringComparison.CurrentCultureIgnoreCase))?.FilterValue;
            filters.RemoveAll(x => x.FilterName == FilterKeys.SelectedCentreCode);
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<HospitalPatientAppointmentModel> objStoredProc = new CoditechViewRepository<HospitalPatientAppointmentModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<HospitalPatientAppointmentModel> hospitalPatientAppointmentList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetHospitalPatientAppointmentList @CentreCode,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
            HospitalPatientAppointmentListModel listModel = new HospitalPatientAppointmentListModel();

            listModel.HospitalPatientAppointmentList = hospitalPatientAppointmentList?.Count > 0 ? hospitalPatientAppointmentList : new List<HospitalPatientAppointmentModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create HospitalPatientAppointment.
        public virtual HospitalPatientAppointmentModel CreateHospitalPatientAppointment(HospitalPatientAppointmentModel hospitalPatientAppointmentModel)
        {
            if (IsNull(hospitalPatientAppointmentModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            HospitalPatientAppointment hospitalPatientAppointment = hospitalPatientAppointmentModel.FromModelToEntity<HospitalPatientAppointment>();
            hospitalPatientAppointment.ApprovalStatusEnumId = GetEnumIdByEnumCode(HospitalApprovalStatusEnum.HospitalPending.ToString());

            //Create new HospitalPatientAppointment and return it.
            HospitalPatientAppointment hospitalPatientAppointmentData = _hospitalPatientAppointmentRepository.Insert(hospitalPatientAppointment);
            if (hospitalPatientAppointmentData?.HospitalPatientAppointmentId > 0)
            {
                hospitalPatientAppointmentModel.HospitalPatientAppointmentId = hospitalPatientAppointmentData.HospitalPatientAppointmentId;
            }
            else
            {
                hospitalPatientAppointmentModel.HasError = true;
                hospitalPatientAppointmentModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return hospitalPatientAppointmentModel;
        }

        //Get HospitalPatientAppointment by hospitalPatientAppointment Id.
        public virtual HospitalPatientAppointmentModel GetHospitalPatientAppointment(long hospitalPatientAppointmentId)
        {
            if (hospitalPatientAppointmentId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalPatientAppointmentId"));

            //Get the HospitalPatientAppointment Details based on id.
            HospitalPatientAppointment hospitalPatientAppointment = _hospitalPatientAppointmentRepository.Table.FirstOrDefault(x => x.HospitalPatientAppointmentId == hospitalPatientAppointmentId);
            HospitalPatientAppointmentModel hospitalPatientAppointmentModel = hospitalPatientAppointment?.FromEntityToModel<HospitalPatientAppointmentModel>();
            long? employeeId = _hospitalDoctorsRepository.Table.Where(x => x.HospitalDoctorId == hospitalPatientAppointmentModel.HospitalDoctorId)?.Select(x => x.EmployeeId)?.FirstOrDefault();
            if (employeeId > 0)
            {
                GeneralPersonModel generalPersonModel = GetGeneralPersonDetailsByEntityType((long)employeeId, UserTypeEnum.Employee.ToString());
                if (IsNotNull(generalPersonModel))
                {
                    hospitalPatientAppointmentModel.FirstName = generalPersonModel.FirstName;
                    hospitalPatientAppointmentModel.LastName = generalPersonModel.LastName;
                    hospitalPatientAppointmentModel.SelectedCentreCode = generalPersonModel.SelectedCentreCode;
                }
            }
            return hospitalPatientAppointmentModel;
        }

        //Update HospitalPatientAppointment.
        public virtual bool UpdateHospitalPatientAppointment(HospitalPatientAppointmentModel hospitalPatientAppointmentModel)
        {
            if (IsNull(hospitalPatientAppointmentModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (hospitalPatientAppointmentModel.HospitalPatientAppointmentId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalPatientAppointmentID"));

            HospitalPatientAppointment hospitalPatientAppointment = hospitalPatientAppointmentModel.FromModelToEntity<HospitalPatientAppointment>();

            //Update HospitalPatientAppointment
            bool isHospitalPatientAppointmentUpdated = _hospitalPatientAppointmentRepository.Update(hospitalPatientAppointment);
            if (!isHospitalPatientAppointmentUpdated)
            {
                hospitalPatientAppointmentModel.HasError = true;
                hospitalPatientAppointmentModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isHospitalPatientAppointmentUpdated;
        }

        //Delete HospitalPatientAppointment.
        public virtual bool DeleteHospitalPatientAppointment(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalPatientAppointmentID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("HospitalPatientAppointmentId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteHospitalPatientAppointment @HospitalPatientAppointmentId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        public virtual HospitalDoctorsListModel GetDoctorsByCentreCodeAndSpecialization(string selectedCentreCode, int medicalSpecializationEnumId)
        {

            HospitalDoctorsListModel listModel = new HospitalDoctorsListModel();

            listModel.HospitalDoctorsList = (from a in _hospitalDoctorsRepository.Table
                                             join b in _employeeMasterRepository.Table
                                             on a.EmployeeId equals b.EmployeeId
                                             join c in _generalPersonRepository.Table
                                             on b.PersonId equals c.PersonId
                                             where a.MedicalSpecializationEnumId == medicalSpecializationEnumId
                                             && b.CentreCode == selectedCentreCode
                                             && b.IsActive
                                             select new HospitalDoctorsModel
                                             {
                                                 HospitalDoctorId = a.HospitalDoctorId,
                                                 FirstName = c.FirstName,
                                                 LastName = c.LastName,
                                                 MedicalSpecializationEnumId = a.MedicalSpecializationEnumId,
                                             })?.ToList();
            return listModel;
        }

        public virtual HospitalPatientTimeSlotListModel GetTimeSlotByDoctorsAndAppointmentDate(int hospitalDoctorId, DateTime appointmentDate)
        {
            HospitalPatientTimeSlotListModel listModel = new HospitalPatientTimeSlotListModel();

            // Add time slots based on appointment date and doctor
            listModel.TimeSlotList.Add(new HospitalPatientTimeSlotModel()
            {
                Value = "10:00",
                Text = "10:00AM - 10:15AM"
            });
            listModel.TimeSlotList.Add(new HospitalPatientTimeSlotModel()
            {
                Value = "10:15",
                Text = "10:16 AM-10:30 AM",
            });
            listModel.TimeSlotList.Add(new HospitalPatientTimeSlotModel()
            {
                Value = "10:30",
                Text = "10:31 AM-10:45 AM",
            });
            listModel.TimeSlotList.Add(new HospitalPatientTimeSlotModel()
            {
                Value = "10:45",
                Text = "10:46 AM-11:00 AM",
            });
            listModel.TimeSlotList.Add(new HospitalPatientTimeSlotModel()
            {
                Value = "11:00",
                Text = "11:00 AM-11:15 AM",
            });
            return listModel;
        }
    }
}
