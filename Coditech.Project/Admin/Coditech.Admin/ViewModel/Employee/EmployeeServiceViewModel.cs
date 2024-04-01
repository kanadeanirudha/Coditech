using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel 
{
    public class EmployeeServiceViewModel : BaseViewModel
    {
        public EmployeeServiceViewModel()
        {
        }
        public GeneralDesignationModel GeneralDesignation;
        public GeneralEnumaratorModel GeneralEnumarator;
        public long EmployeeServiceId { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public short EmployeeDesignationMasterId { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime PromotionDemotionDate { get; set; }
        public int EmployeeStageEnumId { get; set; }
        public DateTime? DateOfLeaving { get; set; }
        public bool IsCurrentPosition { get; set; }
        public string SalaryGradeCode { get; set; }
        public string PayScale { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public string Remark { get; set; }                
    }
}