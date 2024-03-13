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
            CreateMap<GymMemberDetailsModel, GymMemberDetails>().ReverseMap();
            CreateMap<GymMemberFollowUpModel, GymMemberFollowUp>().ReverseMap();
            CreateMap<GymBodyMeasurementType, GymBodyMeasurementTypeModel>().ReverseMap();
            CreateMap<GymMembershipPlanModel, GymMembershipPlan>().ReverseMap();
            CreateMap<GymMemberBodyMeasurement, GymMemberBodyMeasurementModel>().ReverseMap();
            CreateMap<GymMemberMembershipPlanModel, GymMemberMembershipPlan>().ReverseMap();
            CreateMap<GymMemberSalesInvoiceModel, GymMemberSalesInvoice>().ReverseMap();
        }
    }
}
