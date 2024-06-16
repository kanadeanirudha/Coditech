using Coditech.Admin.Agents;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralCommanDataController : BaseController
    {
        private readonly IGeneralCommanDataAgent _generalCommanDataAgent;
        public GeneralCommanDataController(IGeneralCommanDataAgent generalCommanDataAgent) 
        {
            _generalCommanDataAgent = generalCommanDataAgent;
        }
        //Get Departments By CentreCode
        public ActionResult GetDepartmentsByCentreCode(string centreCode)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.CentrewiseDepartment.ToString(),
                DropdownName = "SelectedDepartmentId",
                Parameter = centreCode,
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }

        public ActionResult GetRegionListByCountryId(string generalCountryMasterId)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.Region.ToString(),
                DropdownName = "GeneralRegionMasterId",
                Parameter = generalCountryMasterId,
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }

        public ActionResult GetCityListByRegionId(string generalRegionMasterId)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.City.ToString(),
                DropdownName = "GeneralCityMasterId",
                Parameter = generalRegionMasterId,
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }

        public virtual ActionResult GetHospitalDoctorsList(string selectedCentreCode, string selectedDepartmentId)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.HospitalDoctorsList.ToString(),
                DropdownName = "HospitalDoctorId",
                Parameter = $"{selectedCentreCode}~{selectedDepartmentId}",
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }

        [Route("/GeneralCommanData/UploadMedia")]
        public virtual ActionResult PostUploadImage()
        {
            IFormFileCollection filess = Request.Form.Files;
            if (filess.Count == 0)
            {
                return Json(new { success = false, message = "No file uploaded." });
            }

            IFormFile file = filess[0];

            if (file.Length == 0)
            {
                return Json(new { success = false, message = "Empty file uploaded." });
            }

            var response = _generalCommanDataAgent.UploadImage(file);

            return Json(new { imageUrl = response.UploadMediaModel.MediaPathUrl, photoMediaId = response.UploadMediaModel.MediaId });
        }
    }
}
