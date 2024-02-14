﻿using Coditech.Common.Helper;

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
        public string PersonCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string UserType { get; set; }
        public int EmployeeDesignationMasterId { get; set; }
        public int OrganisationCentrewiseDepartmentId { get; set; }
        public bool IsEmployeeSmoker { get; set; }
        public long ReportingEmployeeId { get; set; }
        public string PANCardNumber { get; set; }
        public string UANNumber { get; set; }
        public string PassportNumber { get; set; }
        [Required]
        public string AdharCardNumber { get; set; }
        public string ImagePath { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string Gender { get; set; }
        public string BankName { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankIFSCCode { get; set; }
        public bool IsActive { get; set; }
    }

}
