﻿using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralNationalityModel : BaseModel
    {
        public GeneralNationalityModel()
        {

        }
        public short GeneralNationalityMasterId { get; set; }
        public string Description { get; set; }
        public bool DefaultFlag { get; set; }
    }
}
