﻿namespace Coditech.Common.API.Model
{
    public class DBTMSubscriptionPlanModel : BaseModel
    {
        public int DBTMSubscriptionPlanId { get; set; }
        public string PlanName { get; set; }
        public short DurationInDays { get; set; }
        public decimal PlanCost { get; set; }       
        public decimal PlanDiscount { get; set; }
     
    }
}
