namespace Coditech.Common.API.Model
{
    public class SalesInvoiceItemLineDetailsModel : BaseModel
    {
        public long SalesInvoiceDetailId { get; set; }
        public long SalesInvoiceMasterId { get; set; }
        public long InventoryGeneralItemLineId { get; set; }
        public decimal ItemQuantity { get; set; }
        public decimal ItemAmount { get; set; }
        public decimal ItemTaxAmount { get; set; }
        public decimal ItemShippingAmount { get; set; }
        public short GeneralTaxGroupMasterId { get; set; }
    }
}

