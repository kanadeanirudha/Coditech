﻿using Coditech.Common.Helper;
using Coditech.Resources;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class DBTMTraineeAssignmentViewModel : BaseViewModel
    {
        public long DBTMTraineeAssignmentId { get; set; }

        [Required]
        [Display(Name = "Trainer")]
        public long GeneralTrainerMasterId { get; set; }

        [Required]
        [Display(Name = "DBTM Trainee Details")]
        public long DBTMTraineeDetailId { get; set; }

        [Required]
        [Display(Name = "DBTM Test")]
        public int DBTMTestMasterId { get; set; }

        [Required]
        [Display(Name = "Assignment Date")]
        public DateTime AssignmentDate { get; set; }

        [Display(Name = "Assignment Time")]
        public TimeSpan? AssignmentTime { get; set; }

        [Required]
        public int DBTMTestStatusEnumId { get; set; }

        [Display(Name = "DBTM Test Status")]
        public string DBTMTestStatus { get; set; }

        [Required]
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string SelectedCentreCode { get; set; }
    }
}
