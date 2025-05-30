﻿using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrewiseBuildingRoomsViewModel : BaseViewModel
    {
        public int OrganisationCentrewiseBuildingRoomId { get; set; }

        [Required]
        [Display(Name = "Building Name")]
        public int OrganisationCentrewiseBuildingMasterId { get; set; }

        [Required]
        [Display(Name = "Building Floor")]
        public int BuildingFloorEnumId { get; set; }

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
        public string SelectedCentreCode { get; set; }
        public string BuildingName { get; set; }
        public string BuildingFloor { get; set; }
    }
}
