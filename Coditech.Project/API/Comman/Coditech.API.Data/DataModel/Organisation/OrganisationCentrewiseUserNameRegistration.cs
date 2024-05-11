namespace Coditech.API.Data
{
    public partial class OrganisationCentrewiseUserNameRegistration
    {
        public int OrganisationCentrewiseUserNameRegistrationId { get; set; }
        public string CentreCode { get; set; }
        public string UserType { get; set; }
        public string UserNameBasedOn { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set;}
    }
}
