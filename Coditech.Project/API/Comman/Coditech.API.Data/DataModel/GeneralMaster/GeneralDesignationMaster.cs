namespace Coditech.API.Data
{
    public partial class EmployeeDesignationMaster : BaseDataModel
    {
        public short EmployeeDesignationMasterId { get; set; }
        public string Description { get; set; }
        public Nullable<int> DesignationLevel { get; set; }
        public Nullable<int> Grade { get; set; }
        public string ShortCode { get; set; }
        public Nullable<int> CollegeID { get; set; }
        public string EmpDesigType { get; set; }
        public string RelatedWith { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}




