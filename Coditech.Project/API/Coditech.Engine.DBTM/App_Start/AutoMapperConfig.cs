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
            CreateMap<DBTMDeviceMaster, DBTMDeviceModel>().ReverseMap();
            CreateMap<DBTMTraineeDetails, DBTMTraineeDetailsModel>().ReverseMap();
            CreateMap<GeneralPerson, GeneralPersonModel>().ReverseMap();
            CreateMap<DBTMActivityCategory, DBTMActivityCategoryModel>().ReverseMap();
            CreateMap<DBTMTestMaster, DBTMTestModel>().ReverseMap();
            CreateMap<DBTMDeviceRegistrationDetails, DBTMDeviceRegistrationDetailsModel>().ReverseMap();
            CreateMap<DBTMTraineeAssignment, DBTMTraineeAssignmentModel>().ReverseMap();
            CreateMap<DBTMBatchActivity, DBTMBatchActivityModel>().ReverseMap();
            CreateMap<DBTMSubscriptionPlan, DBTMSubscriptionPlanModel>().ReverseMap();
        }
    }
}
