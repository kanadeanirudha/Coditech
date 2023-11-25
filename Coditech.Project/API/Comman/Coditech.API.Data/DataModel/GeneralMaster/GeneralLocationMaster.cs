namespace Coditech.API.Data
{
    public partial class GeneralLocationMaster
    {
        public int GeneralLocationMasterId { get; set; }
        public string LocationAddress { get; set; }
        public bool DefaultFlag { get; set; }
        public int RegionId { get; set; }
        public string PostCode { get; set; }
        public int CityId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool IsUserDefined { get; set; }
        public bool IsProviance { get; set; }
        public bool IsTahsil { get; set; }
        public bool Accuracy { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

