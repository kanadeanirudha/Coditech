using Coditech.Admin.Agents;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.Admin.Controllers
{
    public class GeneralCommonController : BaseController
    {
        private readonly IGeneralCommonAgent _generalCommonAgent;
        public GeneralCommonController(IGeneralCommonAgent generalCommonAgent)
        {
            _generalCommonAgent = generalCommonAgent;
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

        [AllowAnonymous]
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

        [AllowAnonymous]
        public ActionResult GetDistrictListByRegionId(string generalRegionMasterId)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.District.ToString(),
                DropdownName = "GeneralDistrictMasterId",
                Parameter = generalRegionMasterId,
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }

        [AllowAnonymous]
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

        [Route("/GeneralCommon/UploadMedia")]
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

            var response = _generalCommonAgent.UploadImage(file);

            return Json(new { imageUrl = response.UploadMediaModel.MediaPathUrl, photoMediaId = response.UploadMediaModel.MediaId });
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetTermsAndCondition()
        {
            List<TermsAndConditionsViewModel> termsAndConditionList = new List<TermsAndConditionsViewModel>();
            CoditechApplicationSettingListViewModel coditechApplicationSettingListViewModel = _generalCommonAgent.GetCoditechApplicationSettingList("TermsAndCondition");
            if (IsNotNull(coditechApplicationSettingListViewModel) && coditechApplicationSettingListViewModel.CoditechApplicationSettingList?.Count > 0)
            {
                string termsAndCondition = coditechApplicationSettingListViewModel.CoditechApplicationSettingList.FirstOrDefault().ApplicationValue1;
                if (!string.IsNullOrEmpty(termsAndCondition))
                {
                    termsAndConditionList = JsonConvert.DeserializeObject<List<TermsAndConditionsViewModel>>(termsAndCondition);
                }
            }
            return PartialView("~/Views/Shared/PageTemplates/_TermsAndCondition.cshtml", termsAndConditionList);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult SendOTP(string sendOTPOn, string mobileNumber = null, string callingCode = null, string emailId = null)
        {
            if (string.IsNullOrEmpty(sendOTPOn))
            {
                return Json(new { success = false, message = "Invalid data send." });
            }
            
            GeneralMessagesViewModel generalMessagesViewModel = new()
            {
                IsSendOnEmail = sendOTPOn.ToLower() == "email",
                IsSendOnMobile = sendOTPOn.ToLower() == "mobile",
                IsSendOnWhatsapp = sendOTPOn.ToLower() == "whatsapp",
                MobileNumber = sendOTPOn.ToLower() == "mobile" ? mobileNumber : null,
                EmailAddress = sendOTPOn.ToLower() == "email" ? emailId : null
            };
            generalMessagesViewModel.MobileNumber = sendOTPOn.ToLower() == "whatsapp" ? $"{callingCode}{mobileNumber}" : mobileNumber;
            generalMessagesViewModel = _generalCommonAgent.SendOTP(generalMessagesViewModel);
            if (!string.IsNullOrEmpty(generalMessagesViewModel?.OTP))
            {
                TempData.Remove(sendOTPOn.ToLower() + "otp");
                TempData[sendOTPOn.ToLower() + "otp"] = generalMessagesViewModel?.OTP;
                return Json(new { success = true, message = "OTP Send." });
            }
            return Json(new { success = false, message = "Failed to Send OTP." });
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult VerifySendOTP(string sendOTPOn, string otp)
        {
            if (string.IsNullOrEmpty(sendOTPOn) || string.IsNullOrEmpty(otp))
            {
                return Json(new { success = false, message = "Please enter OTP" });
            }
            if (Convert.ToString(TempData[sendOTPOn.ToLower() + "otp"]) == otp)
            {
                TempData.Remove(sendOTPOn.ToLower() + "otp");
                return Json(new { success = true, message = "OTP Verified." });
            }
            else
                return Json(new { success = false, message = "Invalid OTP." });
        }
    }
}
