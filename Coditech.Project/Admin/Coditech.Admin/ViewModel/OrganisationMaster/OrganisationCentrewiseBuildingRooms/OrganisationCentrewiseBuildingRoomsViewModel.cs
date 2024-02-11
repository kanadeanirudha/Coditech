using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrewiseBuildingRoomsViewModel : BaseViewModel
    {
        public short OrganisationCentrewiseBuildingRoomId { get; set; }

        [Required]
        [Display(Name = "Building")]
        public short OrganisationCentrewiseBuildingMasterId { get; set; }

        [Required]
        [Display(Name = "Floor")]
        public int BuildFloorEnumId { get; set; }

        [MaxLength(100)]
        [Required]
        [Display(Name = "Room Name")]
        public string RoomName { get; set; }

        [Required]
        [Display(Name = "Area sq.ft")]
        public short Area { get; set; }

        [MaxLength(15)]
        [Required]
        [Display(Name = "Centre")]
        public string CentreCode { get; set; }
    }
}
