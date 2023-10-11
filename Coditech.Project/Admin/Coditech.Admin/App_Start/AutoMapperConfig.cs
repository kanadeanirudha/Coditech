using AutoMapper;

using Coditech.Admin.ViewModel;
using Coditech.API.Data;
using Coditech.Common.API.Model;

namespace Coditech.Admin
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<GeneralDepartmentModel, GeneralDepartmentViewModel>().ReverseMap();
            CreateMap<GeneralDepartmentListModel, GeneralDepartmentListViewModel>().ReverseMap();
            CreateMap<GeneralCountryModel, GeneralCountryViewModel>().ReverseMap();
            CreateMap<GeneralCountryListModel, GeneralCountryListViewModel>().ReverseMap();
            CreateMap<GeneralNationalityModel, GeneralNationalityViewModel>().ReverseMap();
            CreateMap<GeneralNationalityListModel, GeneralNationalityListViewModel>().ReverseMap();
            CreateMap<GeneralDesignationModel, GeneralDesignationViewModel>().ReverseMap();
            CreateMap<GeneralDesignationListModel, GeneralDesignationListViewModel>().ReverseMap();
            CreateMap<GeneralCityModel, GeneralCityViewModel>().ReverseMap();
            CreateMap<GeneralCityListModel, GeneralCityListViewModel>().ReverseMap();
            CreateMap<GeneralTaxMasterModel, GeneralTaxMasterViewModel>().ReverseMap();
            CreateMap<GeneralTaxMasterListModel, GeneralTaxMasterListViewModel>().ReverseMap();
            CreateMap<GeneralTaxGroupModel, GeneralTaxGroupMasterViewModel>().ReverseMap();
            CreateMap<GeneralTaxGroupMasterListModel, GeneralTaxGroupMasterListViewModel>().ReverseMap();
            #region Organisation
            CreateMap<OrganisationModel, OrganisationMasterViewModel>().ReverseMap();
           // CreateMap<OrganisationModel, OrganisationMaster>().ReverseMap();
            #endregion
        }
    }
}
