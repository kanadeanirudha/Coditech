using Coditech.Admin.Agents;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.Admin.Controllers
{
    public class GeneralCommonController : BaseController
    {
        private readonly IGeneralCommonAgent _generalCommonAgent;
        private readonly IUserAgent _userAgent;
        public GeneralCommonController(IGeneralCommonAgent generalCommonAgent, IUserAgent userAgent)
        {
            _generalCommonAgent = generalCommonAgent;
            _userAgent = userAgent;
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


            return Json(new { imageUrl = response?.MediaModel?.Path, photoMediaId = response?.MediaModel?.MediaId, status = !response.HasError, message = response.ErrorMessage });
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

        [HttpGet]
        public ActionResult ApplicationVersionDetails()
        {
            CoditechApplicationSettingViewModel coditechApplicationSettingViewModel = new CoditechApplicationSettingViewModel();
            CoditechApplicationSettingListViewModel coditechApplicationSettingListViewModel = _generalCommonAgent.GetCoditechApplicationSettingList("ApplicationVersionDetails");
            if (IsNotNull(coditechApplicationSettingListViewModel) && coditechApplicationSettingListViewModel.CoditechApplicationSettingList?.Count > 0)
            {
                coditechApplicationSettingViewModel = coditechApplicationSettingListViewModel.CoditechApplicationSettingList.FirstOrDefault();
            }
            return View("~/Views/Shared/PageTemplates/ApplicationVersionDetails.cshtml", coditechApplicationSettingViewModel);
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
        [HttpPost]
        [HttpGet]
        public virtual ActionResult FetchPostalCode(string postalCode, long? personId, long? entityId, string addressTypeEnum)
        {
            BindAddressToPostalCodeListViewModel listModel = _generalCommonAgent.FetchPostalCode(postalCode);
            BindAddressToPostalCodeViewModel bindAddressToPostalCodeViewModel = new BindAddressToPostalCodeViewModel
            {
                BindAddressToPostalCodeList = listModel.BindAddressToPostalCodeList,
                EntityId = entityId,
                PersonId = personId,
                AddressTypeEnum = addressTypeEnum
            };
            return PartialView("~/Views/GeneralMaster/BindAddressToPostalCode/_List.cshtml", bindAddressToPostalCodeViewModel);
        }
        [HttpPost]
        public virtual ActionResult ValidateAddress(string addressData)
        {
            BindAddressToPostalCodeViewModel bindAddressToPostalCodeViewModel = JsonConvert.DeserializeObject<BindAddressToPostalCodeViewModel>(addressData);
            BindAddressToPostalCodeViewModel validatedAddress = _generalCommonAgent.ValidateAddress(bindAddressToPostalCodeViewModel);
            GeneralPersonAddressListViewModel generalPersonAddressListViewModel = _userAgent.GetGeneralPersonAddresses(bindAddressToPostalCodeViewModel.PersonId ?? 0);
            generalPersonAddressListViewModel.GeneralPersonAddressList ??= new List<GeneralPersonAddressViewModel>();
            GeneralPersonAddressViewModel generalPersonAddressViewModel = generalPersonAddressListViewModel.GeneralPersonAddressList.FirstOrDefault(x => x.AddressTypeEnum == bindAddressToPostalCodeViewModel.AddressTypeEnum);
            if (IsNotNull(generalPersonAddressViewModel))
            {
                generalPersonAddressViewModel.GeneralCityMasterId = validatedAddress.SelectedCityId;
                generalPersonAddressViewModel.GeneralRegionMasterId = validatedAddress.SelectedRegionId;
                generalPersonAddressViewModel.GeneralCountryMasterId = validatedAddress.GeneralCountryMasterId;
                generalPersonAddressViewModel.Postalcode = validatedAddress.Pincode;
                generalPersonAddressViewModel.PersonId = bindAddressToPostalCodeViewModel.PersonId ?? 0;
                generalPersonAddressViewModel.FirstName = bindAddressToPostalCodeViewModel.FirstName;
                generalPersonAddressViewModel.LastName = bindAddressToPostalCodeViewModel.LastName;
                generalPersonAddressViewModel.MiddleName = bindAddressToPostalCodeViewModel.MiddleName;
                generalPersonAddressViewModel.AddressLine1 = bindAddressToPostalCodeViewModel.AddressLine1;
                generalPersonAddressViewModel.AddressLine2 = bindAddressToPostalCodeViewModel.AddressLine2;
                generalPersonAddressViewModel.MobileNumber = bindAddressToPostalCodeViewModel.MobileNumber;
                generalPersonAddressViewModel.PhoneNumber = bindAddressToPostalCodeViewModel.PhoneNumber;
                generalPersonAddressViewModel.EmailAddress = bindAddressToPostalCodeViewModel.EmailAddress;
            }
            GeneralPersonAddressListViewModel updatedModel = _userAgent.GetGeneralPersonAddresses(bindAddressToPostalCodeViewModel.PersonId ?? 0);
            if (string.IsNullOrEmpty(generalPersonAddressViewModel.ControllerName))
            {
                updatedModel.ControllerName = bindAddressToPostalCodeViewModel.ControllerName;
                updatedModel.PersonId = validatedAddress.PersonId ?? 0;
                updatedModel.EntityId = validatedAddress.EntityId ?? 0;
            }
           
            SetNotificationMessage(_userAgent.InsertUpdateGeneralPersonAddress(generalPersonAddressViewModel).HasError ?
            GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage) : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
            return PartialView("~/Views/Shared/GeneralPerson/_GeneralPersonAddress.cshtml", updatedModel);
        }


    }
}
