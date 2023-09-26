using Microsoft.AspNetCore.Mvc.Rendering;

namespace Coditech.ViewModel
{
    public class DropdownViewModel
    {
        public List<SelectListItem> DropdownList { get; set; }
        public string DropdownName { get; set; }
        public string DropdownType { get; set; }
        public string DropdownSelectedValue { get; set; }
        public string ChangeEvent { get; set; }
        public string Parameter { get; set; }
    }
}

