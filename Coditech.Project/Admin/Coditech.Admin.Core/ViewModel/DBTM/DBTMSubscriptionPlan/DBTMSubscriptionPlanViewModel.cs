using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using Coditech.Resources;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class DBTMSubscriptionPlanViewModel : BaseViewModel
    {
        public int DBTMSubscriptionPlanId { get; set; }
        [Required]
        [Display(Name = "Plan Name")]
        public string PlanName { get; set; }
        [Required]
        [Display(Name = "Duration In Days")]
        public short DurationInDays { get; set; }
        [Required]
        [Display(Name = "Plan Cost")]
        public Nullable<decimal> PlanCost { get; set; }
        [Required]
        [Display(Name = "Plan Discount")]
        public Nullable<decimal> PlanDiscount { get; set; }
        [Required]
        [Display(Name = "Subscription Plan Type")]
        public int SubscriptionPlanTypeEnumId { get; set; }
        [Display(Name = "Subscription Plan Type")]
        public string SubscriptionPlanType { get; set; }
    }
}
