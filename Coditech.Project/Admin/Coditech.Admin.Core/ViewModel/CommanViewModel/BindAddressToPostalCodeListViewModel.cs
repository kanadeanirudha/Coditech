using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class BindAddressToPostalCodeListViewModel : BaseViewModel
    {
        public List<BindAddressToPostalCodeViewModel> BindAddressToPostalCodeList { get; set; }
        public BindAddressToPostalCodeListViewModel()
        {
            BindAddressToPostalCodeList = new List<BindAddressToPostalCodeViewModel>();
        }
    }
}
