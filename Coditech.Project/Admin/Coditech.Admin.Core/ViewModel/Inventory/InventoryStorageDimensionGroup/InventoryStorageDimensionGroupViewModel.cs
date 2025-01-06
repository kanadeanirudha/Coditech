using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Coditech.Admin.ViewModel
{
    public class InventoryStorageDimensionGroupViewModel : BaseViewModel
    {
        public InventoryStorageDimensionGroupViewModel()
        {
            InventoryStorageDimensionGroupMapperList = new List<InventoryStorageDimensionGroupMapperModel>();
        }

        public List<InventoryStorageDimensionGroupMapperModel> InventoryStorageDimensionGroupMapperList { get; set; }

        public int InventoryStorageDimensionGroupId { get; set; }

        [Required]
        [Display(Name = "Storage Dimension Group Name")]
        public string StorageDimensionGroupName { get; set; }

        [Required]
        [Display(Name = "Storage Dimension Group Code")]
        public string StorageDimensionGroupCode { get; set; }

        public string StorageDimensionGroupMapperData { get; set; }

        [Display(Name = "Warehouse Management Processes")]
        public bool WarehouseManagementProcesses { get; set; }

        [Display(Name = "Mandatory")]
        public bool Mandatory { get; set; }

        [Display(Name = "Primary Stocking")]
        public bool PrimaryStocking { get; set; }
    }
}
