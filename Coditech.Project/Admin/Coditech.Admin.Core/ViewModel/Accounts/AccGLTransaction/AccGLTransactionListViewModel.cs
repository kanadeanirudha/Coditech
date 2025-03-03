using System.ComponentModel.DataAnnotations;
using Coditech.Common.Helper;
using Coditech.Resources;
namespace Coditech.Admin.ViewModel
{
    public class AccGLTransactionListViewModel : BaseViewModel
    {
        public List<AccGLTransactionViewModel> AccGLTransactionList { get; set; }
        public AccGLTransactionListViewModel()
        {
            AccGLTransactionList = new List<AccGLTransactionViewModel>();
        }        
        public string SelectedCentreCode { get; set; }
        public string SelectedParameter1 { get; set; }
        public string SelectedParameter2 { get; set; }
        public string SelectedParameter3 { get; set; }
        public string SelectedParameter4 { get; set; }
    }
}
