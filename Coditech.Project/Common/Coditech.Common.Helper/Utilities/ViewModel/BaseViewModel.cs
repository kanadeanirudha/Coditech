using Microsoft.AspNetCore.Mvc.Rendering;

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
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }
        public string Custom4 { get; set; }
        public string Custom5 { get; set; }

        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public PageListViewModel PageListViewModel { get; set; }
        public List<string> CustomDropdownSelectedValue1 { get; set; }
        public List<SelectListItem> CustomDropdownList1 { get; set; }
        public List<string> CustomDropdownSelectedValue2 { get; set; }
        public List<SelectListItem> CustomDropdownList2 { get; set; }
        public List<string> CustomDropdownSelectedValue3 { get; set; }
        public List<SelectListItem> CustomDropdownList3 { get; set; }
        public List<string> CustomDropdownSelectedValue4 { get; set; }
        public List<SelectListItem> CustomDropdownList4 { get; set; }
        public List<string> CustomDropdownSelectedValue5 { get; set; }
        public List<SelectListItem> CustomDropdownList5 { get; set; }
    }
}
