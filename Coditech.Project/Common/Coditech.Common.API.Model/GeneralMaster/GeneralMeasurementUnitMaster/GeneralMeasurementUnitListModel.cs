namespace Coditech.Common.API.Model
{
    public class GeneralMeasurementUnitListModel : BaseListModel
    {
        public List<GeneralMeasurementUnitModel> GeneralMeasurementUnitList { get; set; }
        public GeneralMeasurementUnitListModel()
        {
            GeneralMeasurementUnitList = new List<GeneralMeasurementUnitModel>();
        }

    }
}
