namespace Coditech.Common.Helper
{
    public abstract class BaseViewModel
    {
        public BaseViewModel()
        {
            PageListViewModel = new PageListViewModel();
        }
        public long CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public long ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }

        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public PageListViewModel PageListViewModel { get; set; }
    }
}
