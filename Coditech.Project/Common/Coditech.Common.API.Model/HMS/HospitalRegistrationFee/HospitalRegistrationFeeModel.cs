using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.InteropServices;
using System;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class HospitalRegistrationFeeModel : BaseModel
    {
        public int HospitalRegistrationFeeId { get; set; }
        public string CentreCode { get; set; }
        public int InventoryGeneralItemLineId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? UptoDate { get; set; }  
        public decimal Charges { get; set; }
        public bool IsTaxExclusive { get; set; }
        public long PersonId { get; set; }
        public string UAHNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }      
    }
}
