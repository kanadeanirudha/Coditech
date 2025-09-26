using System.ComponentModel.DataAnnotations;
using Coditech.Common.API.Model;
using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class BindAddressToPostalCodeViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string District { get; set; }
        public string Division { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
        public long? PersonId { get; set; }
        public long? EntityId { get; set; }
        public string ControllerName { get; set; }
        public string EntityType { get; set; }
        public string AddressTypeEnum { get; set; }
        public string AddressData { get; set; }
        public int SelectedCityId { get; set; }
        public short SelectedRegionId { get; set; }
        public short GeneralCountryMasterId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public List<BindAddressToPostalCodeViewModel> BindAddressToPostalCodeList { get; set; }

    }
}
