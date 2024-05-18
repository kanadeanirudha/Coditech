using AutoMapper;

using Coditech.API.Data;
using Coditech.API.Data.DataModel.Inventory;
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
            CreateMap<InventoryCategoryModel, InventoryCategory>().ReverseMap();
            CreateMap<InventoryItemModelGroupModel, InventoryItemModelGroup>().ReverseMap();
            CreateMap<InventoryProductDimensionGroupModel, InventoryProductDimensionGroup>().ReverseMap();
            CreateMap<InventoryItemStorageDimensionModel, InventoryItemStorageDimension>().ReverseMap();
            CreateMap<InventoryItemTrackingDimensionModel, InventoryItemTrackingDimension>().ReverseMap();
            CreateMap<InventoryProductDimensionModel, InventoryProductDimension>().ReverseMap();
            CreateMap<InventoryItemGroupModel, InventoryItemGroup>().ReverseMap();
            CreateMap<InventoryProductDimensionGroupMapperModel, InventoryProductDimensionGroupMapper>().ReverseMap();
            CreateMap<InventoryUoMMasterModel, InventoryUoMMaster>().ReverseMap();
            CreateMap<InventoryStorageDimensionGroupModel, InventoryStorageDimensionGroup>().ReverseMap();
            CreateMap<InventoryStorageDimensionGroupMapperModel, InventoryStorageDimensionGroupMapper>().ReverseMap();
        }
    }
}
