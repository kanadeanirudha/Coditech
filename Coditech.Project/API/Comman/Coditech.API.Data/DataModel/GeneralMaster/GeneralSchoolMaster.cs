namespace Coditech.API.Data
{
    public partial class GeneralSchoolMaster
    {
        public short GeneralSchoolMasterId { get; set; }
        public string SchoolName { get; set; }
        public string SchoolCode { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public short GeneralCountryMasterId { get; set; }
        public short GeneralRegionMasterId { get; set; }
        public int GeneralCityMasterId { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

