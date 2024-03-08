using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class SalesInvoiceMaster
    {
        [Key]
        public long SalesInvoiceMasterId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string InvoiceNumber { get; set; }
        public long EntityId { get; set; }
        public string UserType { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ShippingAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal BillAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal RoundUpAmount { get; set; }
        public short AccountBalanceSheetId { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

