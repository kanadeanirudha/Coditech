namespace Coditech.Common.API.Model
{
    public class GeneralMeasurementUnitModel : BaseModel
    {
        public GeneralMeasurementUnitModel()
        {

        }
        public short GeneralMeasurementUnitMasterId { get; set; }
        public string MeasurementUnitDisplayName { get; set; }
        public string MeasurementUnitShortCode { get; set; }
    }
}
