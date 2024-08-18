using AutoMapper;

using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<FilterTuple, FilterDataTuple>().ReverseMap();
            CreateMap<GeneralPerson, GeneralPersonModel>().ReverseMap();
            CreateMap<HospitalDoctors, HospitalDoctorsModel>().ReverseMap();
            CreateMap<HospitalDoctorOPDSchedule, HospitalDoctorOPDScheduleModel>().ReverseMap();
            CreateMap<HospitalDoctorVisitingCharges, HospitalDoctorVisitingChargesModel>().ReverseMap();
            CreateMap<HospitalDoctorAllocatedRoom, HospitalDoctorAllocatedOPDRoomModel>().ReverseMap();
            CreateMap<HospitalDoctorLeaveSchedule, HospitalDoctorLeaveScheduleModel>().ReverseMap();
            CreateMap<HospitalPatientRegistration, HospitalPatientRegistrationModel>().ReverseMap();
            CreateMap<HospitalPatientAppointmentPurpose, HospitalPatientAppointmentPurposeModel>().ReverseMap();
            CreateMap<HospitalPatientType, HospitalPatientTypeModel>().ReverseMap();
            CreateMap<HospitalPatientAppointment, HospitalPatientAppointmentModel>().ReverseMap();
            CreateMap<HospitalPathologyTestGroup, HospitalPathologyTestGroupModel>().ReverseMap();
            CreateMap<HospitalPathologyTest, HospitalPathologyTestModel>().ReverseMap();
        }
    }
}
