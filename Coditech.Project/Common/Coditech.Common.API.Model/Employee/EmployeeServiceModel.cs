﻿
using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class EmployeeServiceModel : BaseModel
    {
        [Required]
        public long EmployeeServiceId { get; set; }
        public long CurrentEmployeeServiceId { get; set; }

        [Required]
        public long EmployeeId { get; set; }
        public long PersonId { get; set; }

        public string EmployeeCode { get; set; }

        [Required]
        public short EmployeeDesignationMasterId { get; set; }

        public string CurrentEmployeeDesignation { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        public DateTime? PromotionDemotionDate { get; set; }

        [Required]
        public int EmployeeStageEnumId { get; set; }
        public string EmployeeStage { get; set; }
        public DateTime? DateOfLeaving { get; set; }
        public bool IsCurrentPosition { get; set; }
        public string SalaryGradeCode { get; set; }
        public string PayScale { get; set; }
        public DateTime? OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public string Remark { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
