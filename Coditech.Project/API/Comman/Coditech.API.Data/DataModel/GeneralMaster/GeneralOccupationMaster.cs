﻿using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GeneralOccupationMaster
    {
        [Key]
        public short GeneralOccupationMasterId { get; set; }
        public string OccupationName { get; set; }
        public short DisplayOrder { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

