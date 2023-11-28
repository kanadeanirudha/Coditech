namespace Coditech.API.Data
{
    public partial class OrganisationCentrePrintingFormat
    {
        public short OrganisationCentrePrintingFormatId { get; set; }
        public short OrganisationCentreMasterId { get; set; }
        public string PrintingLine1 { get; set; }
        public string PrintingLine2 { get; set; }
        public string PrintingLine3 { get; set; }
        public string PrintingLine4 { get; set; }
        public byte[] Logo { get; set; }
        public string LogoType { get; set; }
        public string LogoFilename { get; set; }
        public string LogoFileWidth { get; set; }
        public string LogoFileHeight { get; set; }
        public string LogoFileSize { get; set; }
        public string PrintingLinebelowLogo { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
