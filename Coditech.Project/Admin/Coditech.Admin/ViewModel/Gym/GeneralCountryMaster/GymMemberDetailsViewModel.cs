using Coditech.Common.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GymMemberDetailsViewModel : BaseViewModel
    {
        public GymMemberDetailsViewModel()
        {
            GeneralPersonViewModel = new GeneralPersonViewModel();
        }
        public GeneralPersonViewModel GeneralPersonViewModel { get; set; }
        public int GymMemberDetailId { get; set; }
        public long GeneralPersonId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
    }
}
