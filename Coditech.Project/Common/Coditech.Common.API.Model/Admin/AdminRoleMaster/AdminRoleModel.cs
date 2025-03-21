﻿namespace Coditech.Common.API.Model
{
    public class AdminRoleModel : BaseModel
    {
        public int AdminRoleMasterId { get; set; }
        public string AdminRoleCode { get; set; }
        public string SanctionPostName { get; set; }
        public bool IsActive { get; set; }
        public string MonitoringLevel { get; set; }
        public bool IsLoginAllowFromOutside { get; set; }
        public bool IsAttendaceAllowFromOutside { get; set; }
        public List<string> SelectedRoleWiseCentres { get; set; }
        public string SelectedCentreCodeForSelf { get; set; }
        public List<UserAccessibleCentreModel> AllCentreList { get; set; }
        public string SelectedCentreCode { get; set; }
        public string SelectedDepartmentId { get; set; }
		public int DashboardFormEnumId { get; set; }
		public int LimitedDataAccessEnumId { get; set; }
	}
}
