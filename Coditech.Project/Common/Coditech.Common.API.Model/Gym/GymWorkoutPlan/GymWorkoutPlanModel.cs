﻿namespace Coditech.Common.API.Model
{
    public class GymWorkoutPlanModel : BaseModel
    {
        public long GymWorkoutPlanId { get; set; }
        public string CentreCode { get; set; }
        public string WorkoutPlanName { get; set; } 
        public string Target { get; set; }
        public byte NumberOfWeeks { get; set; } 
        public short NumberOfDaysPerWeek { get; set; } 
        public bool IsActive { get; set; }
        public long PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
