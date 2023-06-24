﻿using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class UserLoginViewModel : BaseViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}