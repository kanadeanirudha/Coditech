﻿namespace Coditech.API.Data
{
    public partial class GeneralDesignationMaster
    {
        public Int16 EmployeeDesignationMasterId { get; set; }
        public string Description { get; set; }
        public int DesignationLevel { get; set; }
        public int Grade { get; set; }
        public string ShortCode { get; set; }
        public string EmpDesigType { get; set; }
        public string RelatedWith { get; set; }
        public bool IsActive { get; set; }
    }
}



