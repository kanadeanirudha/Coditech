namespace Coditech.API.Data
{
    public partial class UserType
    {
        public short UserTypeId { get; set; }
        public string UserTypeCode { get; set; }
        public string UserDescription { get; set; }
        public bool IsCommon { get; set; } = true;
        public bool IsLoginRequired { get; set; } = true;
        public string RegistrationFormat { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
