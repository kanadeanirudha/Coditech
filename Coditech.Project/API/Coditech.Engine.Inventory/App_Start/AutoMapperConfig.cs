﻿using AutoMapper;

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
            CreateMap<InventoryGeneralItemLineModel, InventoryGeneralItemLine>().ReverseMap();
            CreateMap<InventoryGeneralItemMasterModel, InventoryGeneralItemMaster>().ReverseMap();
        }
    }
}
