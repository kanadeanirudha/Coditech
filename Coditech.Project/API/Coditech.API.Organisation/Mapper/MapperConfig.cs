using AutoMapper;

using Coditech.API.Data;
using Coditech.Common.API.Model;

namespace Coditech.API.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<GeneralDepartmentMaster,GeneralDepartmentMasterModel>().ReverseMap();
        }
    }
}
