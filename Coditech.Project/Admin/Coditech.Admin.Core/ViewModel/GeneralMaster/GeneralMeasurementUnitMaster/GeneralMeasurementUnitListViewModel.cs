using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralMeasurementUnitListViewModel : BaseViewModel
    {
        public List<GeneralMeasurementUnitViewModel> GeneralMeasurementUnitList { get; set; }
        public GeneralMeasurementUnitListViewModel()
        {
            GeneralMeasurementUnitList = new List<GeneralMeasurementUnitViewModel>();
        }
    }
}
