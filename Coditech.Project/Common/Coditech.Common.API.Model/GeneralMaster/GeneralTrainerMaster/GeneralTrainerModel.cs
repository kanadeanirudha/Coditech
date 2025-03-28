﻿using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralTrainerModel : BaseModel
    {
        public long GeneralTrainerMasterId { get; set; }
        [Required]
        public long PersonId { get; set; }
        public long EmployeeId { get; set; }
        [Required]
        public int TrainerSpecializationEnumId { get; set; }
        public string SelectedCentreCode { get; set; }
        public string SelectedDepartmentId { get; set; }
        public string ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string UniqueCode { get; set; }
        public bool IsAssociated { get; set; }
        public string TrainerSpecialization { get; set; }
        public string PersonCode { get; set; }
        public int NumberOfTraineeAssociated { get; set; }
    }
}
