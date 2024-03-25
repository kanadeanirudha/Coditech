namespace Coditech.Common.API.Model
{
    public class SalesInvoicePrintModel
    {
        public long SalesInvoiceMasterId { get; set; }
        public long EntityId { get; set; }
        public string UserType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal NetAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public string TaxGoupName { get; set; }
        public decimal ShippingAmount { get; set; }
        public decimal BillAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string CentreCode { get; set; }
        public string PaymentType { get; set; }
        public string TransactionReference { get; set; }
        public List<SalesInvoiceTaxModel> SalesInvoiceTaxList { get; set; }
        public List<SalesInvoiceDetailsPrintModel> SalesInvoiceDetailsList { get; set; }
        public SalesInvoiceCustomerInformationModel CustomerInformation { get; set; }
        public OrganisationCentreInvoicePrintingFormat OrganisationCentreInvoicePrintingFormat { get; set; }
        public OrganisationCentreModel OrganisationCentreModel { get; set; }

    }

    public class OrganisationCentreInvoicePrintingFormat
    {
        public string PrintingLine1 { get; set; }
        public string PrintingLine2 { get; set; }
        public string PrintingLine3 { get; set; }
        public string PrintingLine4 { get; set; }
        public string LogoPath { get; set; }
    }

    public class SalesInvoiceCustomerInformationModel
    {
        public long EntityId { get; set; }
        public string PersonTitle { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
    }

    public class SalesInvoiceTaxModel
    {
        public string TaxName { get; set; }
        public string TaxInPercentage { get; set; }
        public string TaxAmount { get; set; }
    }
    public class SalesInvoiceDetailsPrintModel
    {
        public long SalesInvoiceMasterId { get; set; }
        public long InventoryGeneralItemLineId { get; set; }
        public string HSNSACCode { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public decimal ItemQuantity { get; set; }
        public decimal ItemAmount { get; set; }
        public decimal ItemTaxAmount { get; set; }
        public short GeneralTaxGroupMasterId { get; set; }
        public List<SalesInvoiceTaxModel> SalesInvoiceTaxList { get; set; }
    }
}
