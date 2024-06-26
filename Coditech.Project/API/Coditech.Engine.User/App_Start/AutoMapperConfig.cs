﻿using AutoMapper;

using Coditech.API.Data;
using Coditech.API.Data.DataModel.Gym;
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
            CreateMap<UserMaster, UserModel>().ReverseMap();
           CreateMap<UserModuleMaster, UserModuleModel>().ReverseMap();
            CreateMap<GeneralPerson, GeneralPersonModel>().ReverseMap();
            CreateMap<UserMaster, GeneralPersonModel>().ReverseMap();
            CreateMap<GeneralPersonAddress, GeneralPersonAddressModel>().ReverseMap();
            CreateMap<GeneralPersonFollowUp, GeneralPersonFollowUpModel>().ReverseMap();
            CreateMap<GeneralPersonAttendanceDetails, GeneralPersonAttendanceDetailsModel>().ReverseMap();
        }
    }
}
