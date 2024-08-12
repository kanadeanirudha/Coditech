using System;

namespace Coditech.Common.API.Model
{
    public class GeneralDistrictModel : BaseModel
    {
        public short GeneralDistrictMasterId { get; set; }
        public string DistrictName { get; set; }
        public short GeneralRegionMasterId { get; set; }         
    }
}
