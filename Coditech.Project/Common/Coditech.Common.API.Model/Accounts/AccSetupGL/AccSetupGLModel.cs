﻿using System.ComponentModel.DataAnnotations;
namespace Coditech.Common.API.Model
{
    public partial class AccSetupGLModel : BaseModel
    {
        public int AccSetupGLId { get; set; }
        [Display(Name = "Ledger Name")]
        public string GLName { get; set; }
        public int? ParentAccSetupGLId { get; set; }
        public string CategoryCode { get; set; }
        [Display(Name = "Code")]
        public string GLCode { get; set; }
        public decimal ClosingBalance { get; set; }
        [Display(Name = "Is Group")]
        public bool IsGroup { get; set; }
        [Display(Name = "GL Type")]
        public short? AccSetupGLTypeId { get; set; }
        public int AccSetupBalancesheetId { get; set; }
        public int? AltSetupGLId { get; set; }
        public short? UserTypeEnum { get; set; }
        public byte? AccSetupChartOfAccountTemplateId { get; set; }
        public byte AccSetupBalanceSheetTypeId { get; set; }
        public string SelectedCentreCode { get; set; }
        public short AccSetupCategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystemGenerated { get; set; }
        [Display(Name = "User Type")]
        public short? UserTypeId { get; set; }
        public string BankModelData { get; set; }
        public string dataModel { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public string BankBranchName { get; set; }
        public string IFSCCode { get; set; }
        public short GeneralFinancialYearId { get; set; }
        public List<AccSetupCategoryModel> AccSetupCategoryList { get; set; }
        public List<AccSetupGLModel> AccSetupGLList { get; set; }
        public List<AccSetupGLModel> SubAccounts { get; set; } = new List<AccSetupGLModel>();
        public List<AccSetupGLBankModel> AccSetupGLBankList { get; set; }
        public GeneralFinancialYearModel GeneralFinancialYearModel { get; set; }
        public string CurrencySymbol { get; set; }
    }
}
