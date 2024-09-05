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
    public class HospitalDoctorOPDScheduleService : BaseService, IHospitalDoctorOPDScheduleService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<HospitalDoctorOPDSchedule> _hospitalDoctorOPDScheduleRepository;
        private readonly ICoditechRepository<HospitalDoctors> _hospitalDoctorsRepository;

        public HospitalDoctorOPDScheduleService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _hospitalDoctorOPDScheduleRepository = new CoditechRepository<HospitalDoctorOPDSchedule>(_serviceProvider.GetService<Coditech_Entities>());
            _hospitalDoctorsRepository = new CoditechRepository<HospitalDoctors>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual HospitalDoctorOPDScheduleListModel GetHospitalDoctorOPDScheduleList(string selectedCentreCode, short selectedDepartmentId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<HospitalDoctorOPDScheduleModel> objStoredProc = new CoditechViewRepository<HospitalDoctorOPDScheduleModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@DepartmentId", selectedDepartmentId, ParameterDirection.Input, DbType.Int16);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<HospitalDoctorOPDScheduleModel> hospitalDoctorOPDScheduleList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetHospitalDoctorOPDScheduleList @CentreCode,@DepartmentId,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();
            HospitalDoctorOPDScheduleListModel listModel = new HospitalDoctorOPDScheduleListModel();

            listModel.HospitalDoctorOPDScheduleList = hospitalDoctorOPDScheduleList?.Count > 0 ? hospitalDoctorOPDScheduleList : new List<HospitalDoctorOPDScheduleModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Get HospitalDoctorOPDSchedule by hospitalDoctorOPDSchedule Id.
        public virtual HospitalDoctorOPDScheduleModel GetHospitalDoctorOPDSchedule(int hospitalDoctorId, int weekDayEnumId)
        {
            if (hospitalDoctorId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "hospitalDoctorId"));

            if (weekDayEnumId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "weekDayEnumId"));

            HospitalDoctorOPDScheduleModel hospitalDoctorOPDScheduleModel = new HospitalDoctorOPDScheduleModel()
            {
                HospitalDoctorId = hospitalDoctorId,
                WeekDayEnumId = weekDayEnumId
            };
            List<HospitalDoctorOPDSchedule> scheduleListByWeekDays = _hospitalDoctorOPDScheduleRepository.Table.Where(x => x.HospitalDoctorId == hospitalDoctorId && x.WeekDayEnumId == weekDayEnumId)?.ToList();

            if (scheduleListByWeekDays?.Count > 0)
            {
                HospitalDoctorOPDSchedule hospitalDoctorOPDScheduleMorning = scheduleListByWeekDays.FirstOrDefault(x => x.OPDTimesOfDay == "morning");
                if (IsNotNull(hospitalDoctorOPDScheduleMorning))
                {
                    hospitalDoctorOPDScheduleModel.HospitalDoctorOPDScheduleMorningId = hospitalDoctorOPDScheduleMorning.HospitalDoctorOPDScheduleId;
                    hospitalDoctorOPDScheduleModel.FromTimeMorning = hospitalDoctorOPDScheduleMorning.FromTime;
                    hospitalDoctorOPDScheduleModel.UptoTimeMorning = hospitalDoctorOPDScheduleMorning.UptoTime;
                    hospitalDoctorOPDScheduleModel.TimeSlotInMinuteMorning = hospitalDoctorOPDScheduleMorning.TimeSlotInMinute;
                }

                HospitalDoctorOPDSchedule hospitalDoctorOPDScheduleEvening = scheduleListByWeekDays.FirstOrDefault(x => x.OPDTimesOfDay == "evening");
                if (IsNotNull(hospitalDoctorOPDScheduleEvening))
                {
                    hospitalDoctorOPDScheduleModel.HospitalDoctorOPDScheduleEveningId = hospitalDoctorOPDScheduleEvening.HospitalDoctorOPDScheduleId;
                    hospitalDoctorOPDScheduleModel.FromTimeEvening = hospitalDoctorOPDScheduleEvening.FromTime;
                    hospitalDoctorOPDScheduleModel.UptoTimeEvening = hospitalDoctorOPDScheduleEvening.UptoTime;
                    hospitalDoctorOPDScheduleModel.TimeSlotInMinuteEvening = hospitalDoctorOPDScheduleEvening.TimeSlotInMinute;
                }
            }

            //Get the HospitalDoctors Details based on id.
            long? employeeId = _hospitalDoctorsRepository.Table.Where(x => x.HospitalDoctorId == hospitalDoctorId)?.Select(y => y.EmployeeId)?.FirstOrDefault();
            if (employeeId > 0)
            {
                GeneralPersonModel generalPersonModel = GetGeneralPersonDetailsByEntityType((long)employeeId, UserTypeEnum.Employee.ToString());
                if (IsNotNull(generalPersonModel))
                {
                    hospitalDoctorOPDScheduleModel.FirstName = generalPersonModel.FirstName;
                    hospitalDoctorOPDScheduleModel.LastName = generalPersonModel.LastName;
                    hospitalDoctorOPDScheduleModel.SelectedCentreCode = generalPersonModel.SelectedCentreCode;
                    hospitalDoctorOPDScheduleModel.SelectedDepartmentId = generalPersonModel.SelectedDepartmentId;
                }
            }
            return hospitalDoctorOPDScheduleModel;
        }

        //Insert Update Hospital Doctor OPD Schedule.
        public virtual bool UpdateHospitalDoctorOPDSchedule(HospitalDoctorOPDScheduleModel hospitalDoctorOPDScheduleModel)
        {
            if (IsNull(hospitalDoctorOPDScheduleModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (hospitalDoctorOPDScheduleModel.HospitalDoctorId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalDoctorId"));
            bool isHospitalDoctorOPDScheduleUpdated = false;

            HospitalDoctorOPDSchedule hospitalDoctorOPDScheduleMorning = new HospitalDoctorOPDSchedule()
            {
                HospitalDoctorOPDScheduleId = hospitalDoctorOPDScheduleModel.HospitalDoctorOPDScheduleMorningId,
                HospitalDoctorId = hospitalDoctorOPDScheduleModel.HospitalDoctorId,
                WeekDayEnumId = hospitalDoctorOPDScheduleModel.WeekDayEnumId,
                OPDTimesOfDay = "morning",
                FromTime = hospitalDoctorOPDScheduleModel.FromTimeMorning,
                UptoTime = hospitalDoctorOPDScheduleModel.UptoTimeMorning,
                TimeSlotInMinute = hospitalDoctorOPDScheduleModel.TimeSlotInMinuteMorning
            };
            if (hospitalDoctorOPDScheduleMorning.HospitalDoctorOPDScheduleId > 0)
            {
                //Update HospitalDoctorOPDSchedule
                isHospitalDoctorOPDScheduleUpdated = _hospitalDoctorOPDScheduleRepository.Update(hospitalDoctorOPDScheduleMorning);
                if (!isHospitalDoctorOPDScheduleUpdated)
                {
                    hospitalDoctorOPDScheduleModel.HasError = true;
                    hospitalDoctorOPDScheduleModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
                }
            }
            else
            {
                hospitalDoctorOPDScheduleMorning = _hospitalDoctorOPDScheduleRepository.Insert(hospitalDoctorOPDScheduleMorning);
                if (hospitalDoctorOPDScheduleMorning?.HospitalDoctorOPDScheduleId > 0)
                {
                    isHospitalDoctorOPDScheduleUpdated = true;
                }
                else
                {
                    hospitalDoctorOPDScheduleModel.HasError = true;
                    hospitalDoctorOPDScheduleModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
                }
            }

            HospitalDoctorOPDSchedule hospitalDoctorOPDScheduleEvening = new HospitalDoctorOPDSchedule()
            {
                HospitalDoctorOPDScheduleId = hospitalDoctorOPDScheduleModel.HospitalDoctorOPDScheduleEveningId,
                HospitalDoctorId = hospitalDoctorOPDScheduleModel.HospitalDoctorId,
                WeekDayEnumId = hospitalDoctorOPDScheduleModel.WeekDayEnumId,
                OPDTimesOfDay = "evening",
                FromTime = hospitalDoctorOPDScheduleModel.FromTimeEvening,
                UptoTime = hospitalDoctorOPDScheduleModel.UptoTimeEvening,
                TimeSlotInMinute = hospitalDoctorOPDScheduleModel.TimeSlotInMinuteEvening
            };

            if (hospitalDoctorOPDScheduleEvening.HospitalDoctorOPDScheduleId > 0)
            {
                //Update HospitalDoctorOPDSchedule
                isHospitalDoctorOPDScheduleUpdated = _hospitalDoctorOPDScheduleRepository.Update(hospitalDoctorOPDScheduleEvening);
                if (!isHospitalDoctorOPDScheduleUpdated)
                {
                    hospitalDoctorOPDScheduleModel.HasError = true;
                    hospitalDoctorOPDScheduleModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
                }
            }
            else
            {
                hospitalDoctorOPDScheduleEvening = _hospitalDoctorOPDScheduleRepository.Insert(hospitalDoctorOPDScheduleEvening);
                if (hospitalDoctorOPDScheduleEvening?.HospitalDoctorOPDScheduleId > 0)
                {
                    isHospitalDoctorOPDScheduleUpdated = true;
                }
                else
                {
                    hospitalDoctorOPDScheduleModel.HasError = true;
                    hospitalDoctorOPDScheduleModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
                }
            }
            return isHospitalDoctorOPDScheduleUpdated;
        }
    }
}
