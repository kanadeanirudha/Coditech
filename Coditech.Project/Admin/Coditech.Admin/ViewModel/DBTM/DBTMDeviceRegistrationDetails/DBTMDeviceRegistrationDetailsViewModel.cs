﻿using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class DBTMDeviceRegistrationDetailsViewModel : BaseViewModel
    {
        public long DBTMDeviceRegistrationDetailId { get; set; }
        [Display(Name = "Device")]
        public long DBTMDeviceMasterId { get; set; }
        [Required]
        public string DeviceSerialCode { get; set; }
        [Required]
        [MaxLength(200)]
        public string DeviceName { get; set; }
        public string UserType { get; set; }
        public long EntityId { get; set; }
        public DateTime PurchaseDate { get; set; }
        [Display(Name = "Warranty Expiration Date")]
        public DateTime WarrantyExpirationDate { get; set; }
        public bool IsMasterDevice { get; set; }
            
    }
}
