﻿using Coditech.Common.Helper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class EmployeeMasterViewModel : BaseViewModel
    {
        public EmployeeMasterViewModel()
        {
        }
        public long EmployeeId { get; set; }
        public long PersonId { get; set; }

        [MaxLength(50)]
        [Editable(false)]
        [Display(Name = "Employee Code")]
        public string PersonCode { get; set; }

        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date Of Birth")]
        public string DateOfBirth { get; set; }
                
        public string UserType { get; set; }

        [Display(Name = "Designation")]
        public short EmployeeDesignationMasterId { get; set; }

        [Display(Name = "Centre wise Department")]
        public short OrganisationCentrewiseDepartmentId { get; set; }

        [Display(Name = "Is Employee Smoker ?")]
        public bool IsEmployeeSmoker { get; set; }

        [Display(Name = "Reporting Employee")]
        public long? ReportingEmployeeId { get; set; }

        [MaxLength(10)]
        [Display(Name = "PAN Card Number")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string PANCardNumber { get; set; }

        [MaxLength(12)]
        [Display(Name = "UAN Number")]
        public string UANNumber { get; set; }

        [MaxLength(10)]
        [Display(Name = "Passport Number")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string PassportNumber { get; set; }

        [MaxLength(12, ErrorMessage = "The Aadhaar Card Number must be exactly 12 digits long.")]
        [Display(Name = "Aadhaar Card Number")]
        public string AdharCardNumber { get; set; }

        public string ImagePath { get; set; }

        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [MaxLength(200)]
        [EmailAddress]
        [Display(Name = "Email Id")]
        public string EmailId { get; set; }
        
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [MaxLength(50)]        
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [MaxLength(20)]
        [Display(Name = "Bank Account Number")]
        public string BankAccountNumber { get; set; }

        [MaxLength(20)]
        [Display(Name = "Bank IFSC Code")]
        public string BankIFSCCode { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public string CentreCode { get; set; }
        public short GeneralDepartmentMasterId { get; set; }
    }

}
