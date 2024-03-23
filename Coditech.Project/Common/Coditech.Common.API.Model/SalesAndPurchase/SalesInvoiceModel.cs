namespace Coditech.Common.API.Model
{
    public class SalesInvoiceModel : BaseModel
    {
       public OrganisationCentrePrintingFormatModel OrganisationCentrePrintingFormatModel { get; set; }
       public GeneralPersonModel GeneralPersonModel { get; set; }
       public List<SalesInvoiceItemLineDetailsModel> SalesInvoiceItemLineDetailsModel { get; set; }
    }
}
