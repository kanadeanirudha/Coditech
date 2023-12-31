﻿using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class UserModuleModel : BaseModel
    {
        [Required]
        public short UserModuleMasterId { get; set; }

        [MaxLength(50)]
        [Required]
        public string ModuleCode { get; set; }

        [MaxLength(60)]
        [Required]
        public string ModuleName { get; set; }

        public int? ModuleSeqNumber { get; set; }

        [MaxLength(50)]
        public string ModuleTooltip { get; set; }

        [MaxLength(50)]
        public string ModuleIconName { get; set; }

        [MaxLength(50)]
        public string ModuleColorClass { get; set; }

        public string DefaultMenuLink { get; set; }
    }
}
