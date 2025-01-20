using System.ComponentModel.DataAnnotations;
using Coditech.Common.Helper;
using Coditech.Resources;

namespace Coditech.Admin.ViewModel
{
    public partial class AccSetupBalanceSheetListViewModel : BaseViewModel
    {
        public List<AccSetupBalanceSheetViewModel> AccSetupBalanceSheetList { get; set; }

        public AccSetupBalanceSheetListViewModel()
        {
            AccSetupBalanceSheetList = new List<AccSetupBalanceSheetViewModel>();
        }
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string SelectedCentreCode { get; set; } = null;
        public string SelectedParameter1 { get; set; } = null;
    }
}
