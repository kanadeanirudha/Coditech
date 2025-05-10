using System.ComponentModel.DataAnnotations;
using Coditech.Common.Helper;
using Coditech.Resources;
namespace Coditech.Admin.ViewModel
{
    public class AccPrequisiteListViewModel : BaseViewModel
    {
        public List<AccPrequisiteViewModel> AccPrequisiteList { get; set; }
        public AccPrequisiteListViewModel()
        {
            AccPrequisiteList = new List<AccPrequisiteViewModel>();
        }
    }
}
