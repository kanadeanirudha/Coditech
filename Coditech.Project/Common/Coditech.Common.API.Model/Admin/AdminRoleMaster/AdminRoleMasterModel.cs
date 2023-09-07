using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class AdminRoleMasterModel : BaseModel
    {
        public AdminRoleMasterModel()
        {

        }
        public Int16 AdminRoleMasterId { get; set; }
        public string AdminRoleCode { get; set; }
        public string SanctPostName { get; set; }
        public bool IsActive { get; set; }
        public string MonitoringLevel { get; set; }
        public bool IsLoginAllowFromOutside { get; set; }
        public bool IsAttendaceAllowFromOutside { get; set; }
        public List<string> SelectedRoleWiseCentres { get; set; }
        public string SelectedCentreCodeForSelf { get; set; }
        public List<UserAccessibleCentreModel> AllCentreList { get; set; }
    }
}
