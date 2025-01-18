using Coditech.Common.API.Model;
using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class DashboardViewModel : BaseViewModel
    {
        public DashboardViewModel()
        {
        }
        public string DashboardFormEnumCode { get; set; }
        public Int16 NumberOfDaysRecord { get; set; }
    }
}