using System.ComponentModel.DataAnnotations;
namespace Coditech.Common.API.Model
{
    public partial class AccSetupGLModel : BaseModel
    {
        public int AccSetupGLId { get; set; }
        [Display(Name = "Name")]
        public string GLName { get; set; }
        public int? ParentAccSetupGLId { get; set; }
        public string CategoryCode { get; set; }
        [Display(Name = "Code")]
        public string GLCode { get; set; }
        public decimal OpeningBalance { get; set; }
        [Display(Name = "Is Group")]
        public bool IsGroup { get; set; }
        [Display(Name = "GL Type")]
        public short? AccSetupGLTypeId { get; set; }
        public int AccSetupBalancesheetId { get; set; }
        public int? AltSetupGLId { get; set; }
        public byte? AccSetupChartOfAccountTemplateId { get; set; }
        public byte AccSetupBalanceSheetTypeId { get; set; }
        public string SelectedCentreCode { get; set; }
        public short AccSetupCategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public List<AccSetupCategoryModel> AccSetupCategoryList { get; set; }
        public List<AccSetupGLModel> AccSetupGLList { get; set; }
        public List<AccSetupGLModel> SubAccounts { get; set; } = new List<AccSetupGLModel>();
    }
}
