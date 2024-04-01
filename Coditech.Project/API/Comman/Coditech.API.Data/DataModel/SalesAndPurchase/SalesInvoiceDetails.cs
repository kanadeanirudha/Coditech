using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class SalesInvoiceDetails
    {
        [Key]
        public long SalesInvoiceDetailId { get; set; }
        public long SalesInvoiceMasterId { get; set; }
        public long InventoryGeneralItemLineId { get; set; }
        public decimal ItemQuantity { get; set; }
        public decimal ItemAmount { get; set; }
        public decimal ItemTaxAmount { get; set; }
        public decimal ItemShippingAmount { get; set; }
        public byte GeneralTaxGroupMasterId { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

