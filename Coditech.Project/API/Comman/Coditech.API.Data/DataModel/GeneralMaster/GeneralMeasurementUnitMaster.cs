namespace Coditech.API.Data
{
    public partial class GeneralMeasurementUnitMaster
    {
        public short GeneralMeasurementUnitMasterId { get; set; }
        public string MeasurementUnitDisplayName { get; set; }
        public string MeasurementUnitShortCode { get; set; }
        
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

