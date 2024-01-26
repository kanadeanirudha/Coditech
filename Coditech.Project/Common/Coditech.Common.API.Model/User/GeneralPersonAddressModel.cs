namespace Coditech.Common.API.Model
{
    public class GeneralPersonAddressModel : BaseModel
    {
        public long GeneralPersonAddressId { get; set; }
        public string AddressTypeEnum { get; set; }
        public long PersonId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string CompanyName { get; set; }
        public short GeneralRegionMasterId { get; set; }
        public int GeneralCityMasterId { get; set; }
        public string Postalcode { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }        
    }
}
