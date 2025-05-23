﻿using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class OrganisationCentrewiseBuildingRoomsModel : BaseModel
    {
        public int OrganisationCentrewiseBuildingRoomId { get; set; }

        [Required]
        public int OrganisationCentrewiseBuildingMasterId { get; set; }

        [Required]
        public int BuildingFloorEnumId { get; set; }

        [MaxLength(100)]
        [Required]
        public string RoomName { get; set; }

        [Required]
        public short Area { get; set; }
        public string BuildingName { get; set; }
        public string BuildingFloor { get; set; }
        public string SelectedCentreCode { get; set; }
    }
}
