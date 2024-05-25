using Coditech.Admin.Utilities;

namespace Coditech.Admin.ViewModel
{
    public class DataTableViewModel
    {
        public string SearchBy { get; set; }
        public string SortByColumn { get; set; }
        public string SortBy { get; set; } = AdminConstants.ASCKey;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SelectedCentreCode { get; set; } = string.Empty;
        public short SelectedDepartmentId { get; set; }
        public int HospitalDoctorId { get; set; }
        public string SelectedParameter1 { get; set; } = string.Empty;
        public string SelectedParameter2 { get; set; } = string.Empty;
        public string SelectedParameter3 { get; set; } = string.Empty;
        public string SelectedParameter4 { get; set; } = string.Empty;
        public string SelectedParameter5 { get; set; } = string.Empty;
    }
}
