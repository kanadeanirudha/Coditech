using Microsoft.AspNetCore.Mvc.Rendering;

namespace Coditech.Admin.ViewModel
{
    public class DropdownViewModel
    {
        public DropdownViewModel()
        {
            DropdownList = new List<SelectListItem>();
        }
        public List<SelectListItem> DropdownList { get; set; }
        public string DropdownName { get; set; }
        public string DropdownType { get; set; }
        public string DropdownSelectedValue { get; set; }
        public string ChangeEvent { get; set; }
        public string Parameter { get; set; }
        public string GroupCode { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsRequired { get; set; } = true;
        public bool IsTextValueSame { get; set; }
        public bool AddSelectItem { get; set; } = true;
        public string ClassName { get; set; } = string.Empty;
    }
}
