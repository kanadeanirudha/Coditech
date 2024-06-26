using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Coditech.Admin.Controllers
{
    public class OrganisationCentreMasterController : BaseController
    {
        private readonly IOrganisationCentreAgent _organisationCentreAgent;
        private const string createEdit = "~/Views/Organisation/OrganisationCentre/CreateEdit.cshtml";
        private const string OrganisationCentrePrintingFormat = "~/Views/Organisation/OrganisationCentre/OrganisationCentrePrintingFormat.cshtml";
        private const string OrganisationCentrewiseGSTCredential = "~/Views/Organisation/OrganisationCentre/OrganisationCentrewiseGSTCredential.cshtml";
        private const string OrganisationCentrewiseSmtpSetting = "~/Views/Organisation/OrganisationCentre/OrganisationCentrewiseSmtpSetting.cshtml";
        private const string OrganisationCentrewiseSmsSetting = "~/Views/Organisation/OrganisationCentre/OrganisationCentrewiseSmsSetting.cshtml";
        private const string OrganisationCentrewiseEmailTemplate = "~/Views/Organisation/OrganisationCentre/OrganisationCentrewiseEmailTemplate.cshtml";
        private const string OrganisationCentrewiseUserNameRegistration = "~/Views/Organisation/OrganisationCentre/OrganisationCentrewiseUserNameRegistration.cshtml";

        public OrganisationCentreMasterController(IOrganisationCentreAgent organisationCentreAgent)
        {
            _organisationCentreAgent = organisationCentreAgent;
        }

        #region OrganisationCentre 
        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            OrganisationCentreListViewModel list = _organisationCentreAgent.GetOrganisationCentreList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Organisation/OrganisationCentre/_List.cshtml", list);
            }
            return View($"~/Views/Organisation/OrganisationCentre/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new OrganisationCentreViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(OrganisationCentreViewModel organisationCentreViewModel)
        {
            if (ModelState.IsValid)
            {
                organisationCentreViewModel = _organisationCentreAgent.CreateOrganisationCentre(organisationCentreViewModel);
                if (!organisationCentreViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<OrganisationCentreMasterController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(organisationCentreViewModel.ErrorMessage));
            return View(createEdit, organisationCentreViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short organisationCentreId)
        {
            OrganisationCentreViewModel organisationCentreViewModel = _organisationCentreAgent.GetOrganisationCentre(organisationCentreId);
            return ActionView(createEdit, organisationCentreViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(OrganisationCentreViewModel organisationCentreViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_organisationCentreAgent.UpdateOrganisationCentre(organisationCentreViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { organisationCentreId = organisationCentreViewModel.OrganisationCentreMasterId });
            }
            return View(createEdit, organisationCentreViewModel);
        }

        public virtual ActionResult Delete(string organisationCentreIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(organisationCentreIds))
            {
                status = _organisationCentreAgent.DeleteOrganisationCentre(organisationCentreIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<OrganisationCentreMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<OrganisationCentreMasterController>(x => x.List(null));
        }

        #endregion

        #region Centre Printing Format
        [HttpGet]
        public virtual ActionResult PrintingFormat(short organisationCentreId)
        {
            OrganisationCentrePrintingFormatViewModel organisationCentrePrintingFormatViewModel = _organisationCentreAgent.GetPrintingFormat(organisationCentreId);
            return ActionView(OrganisationCentrePrintingFormat, organisationCentrePrintingFormatViewModel);
        }

        [HttpPost]
        public virtual ActionResult PrintingFormat(OrganisationCentrePrintingFormatViewModel organisationCentrePrintingFormatViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_organisationCentreAgent.UpdatePrintingFormat(organisationCentrePrintingFormatViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("PrintingFormat", new { organisationCentreId = organisationCentrePrintingFormatViewModel.OrganisationCentreMasterId });
            }
            return View(OrganisationCentrePrintingFormat, organisationCentrePrintingFormatViewModel);
        }
        #endregion

        #region CentrewiseGSTSetup
        [HttpGet]
        public virtual ActionResult CentrewiseGSTSetup(short organisationCentreId)
        {
            OrganisationCentrewiseGSTCredentialViewModel organisationCentrewiseGSTCredentialViewModel = _organisationCentreAgent.GetCentrewiseGSTSetup(organisationCentreId);
            return ActionView(OrganisationCentrewiseGSTCredential, organisationCentrewiseGSTCredentialViewModel);
        }

        [HttpPost]
        public virtual ActionResult CentrewiseGSTSetup(OrganisationCentrewiseGSTCredentialViewModel organisationCentrewiseGSTCredentialViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_organisationCentreAgent.UpdateCentrewiseGSTSetup(organisationCentrewiseGSTCredentialViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("CentrewiseGSTSetup", new { organisationCentreId = organisationCentrewiseGSTCredentialViewModel.OrganisationCentreMasterId });
            }
            return View(OrganisationCentrewiseGSTCredential, organisationCentrewiseGSTCredentialViewModel);
        }
        #endregion

        #region CentrewiseSMTPSetting
        [HttpGet]
        public virtual ActionResult CentrewiseSmtpSetup(short organisationCentreId)
        {
            OrganisationCentrewiseSmtpSettingViewModel organisationCentrewiseSmtpSettingViewModel = _organisationCentreAgent.GetCentrewiseSmtpSetup(organisationCentreId);
            return ActionView(OrganisationCentrewiseSmtpSetting, organisationCentrewiseSmtpSettingViewModel);

        }

        [HttpPost]
        public virtual ActionResult CentrewiseSmtpSetup(OrganisationCentrewiseSmtpSettingViewModel organisationCentrewiseSmtpSettingViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_organisationCentreAgent.UpdateCentrewiseSmtpSetup(organisationCentrewiseSmtpSettingViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("CentrewiseSMTPSetup", new { organisationCentreId = organisationCentrewiseSmtpSettingViewModel.OrganisationCentrewiseSmtpSettingId });
            }
            return View(OrganisationCentrewiseSmtpSetting, organisationCentrewiseSmtpSettingViewModel);
        }
        #endregion

        #region CentrewiseSMSSetting
        [HttpGet]
        public virtual ActionResult CentrewiseSmsSetup(short organisationCentreId, byte generalSmsProviderId = 0)
        {
            OrganisationCentrewiseSmsSettingViewModel organisationCentrewiseSmsSettingViewModel = _organisationCentreAgent.GetCentrewiseSmsSetup(organisationCentreId);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Organisation/OrganisationCentre/_OrganisationCentrewiseSmsSetting.cshtml", organisationCentrewiseSmsSettingViewModel);
            }
            return ActionView(OrganisationCentrewiseSmsSetting, organisationCentrewiseSmsSettingViewModel);
        }

        [HttpPost]
        public virtual ActionResult CentrewiseSmsSetup(OrganisationCentrewiseSmsSettingViewModel organisationCentrewiseSmsSettingViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_organisationCentreAgent.UpdateCentrewiseSmsSetup(organisationCentrewiseSmsSettingViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("CentrewiseSMSSetup", new { organisationCentreId = organisationCentrewiseSmsSettingViewModel.OrganisationCentrewiseSmsSettingId });
            }
            return View(OrganisationCentrewiseSmsSetting, organisationCentrewiseSmsSettingViewModel);
        }
        #endregion

        #region CentrewiseEmailTemplate
        [HttpGet]
        public virtual ActionResult CentrewiseEmailTemplateSetup(short organisationCentreId)
        {
            OrganisationCentrewiseEmailTemplateViewModel organisationCentrewiseEmailTemplateViewModel = _organisationCentreAgent.GetCentrewiseEmailTemplateSetup(organisationCentreId, string.Empty);
            return ActionView(OrganisationCentrewiseEmailTemplate, organisationCentrewiseEmailTemplateViewModel);
        }

        [HttpGet]
        public virtual ActionResult GetEmailTemplateByCentreCode(short organisationCentreId, string emailTemplateCode)
        {
            OrganisationCentrewiseEmailTemplateViewModel organisationCentrewiseEmailTemplateViewModel = _organisationCentreAgent.GetCentrewiseEmailTemplateSetup(organisationCentreId, emailTemplateCode);
            return PartialView("~/Views/Organisation/OrganisationCentre/_OrganisationCentrewiseEmailTemplate.cshtml", organisationCentrewiseEmailTemplateViewModel);
        }

        [HttpPost]
        public virtual ActionResult CentrewiseEmailTemplateSetup(OrganisationCentrewiseEmailTemplateViewModel organisationCentrewiseEmailTemplateViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_organisationCentreAgent.UpdateCentrewiseEmailTemplateSetup(organisationCentrewiseEmailTemplateViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("CentrewiseEmailTemplateSetup", new { organisationCentreId = organisationCentrewiseEmailTemplateViewModel.OrganisationCentreMasterId });
            }
            return View(OrganisationCentrewiseEmailTemplate, organisationCentrewiseEmailTemplateViewModel);
        }
        #endregion

        #region CentrewiseUserNameRegistration
        [HttpGet]
        public virtual ActionResult CentrewiseUserNameRegistrationList(short organisationCentreId)
        {
            OrganisationCentrewiseUserNameRegistrationViewModel organisationCentrewiseUserNameRegistrationViewModel = _organisationCentreAgent.GetCentrewiseUserName(organisationCentreId);
            return ActionView("~/Views/Organisation/OrganisationCentre/OrganisationCentrewiseUserNameRegistrationList.cshtml", organisationCentrewiseUserNameRegistrationViewModel);
        }

        [HttpGet]
        public virtual ActionResult CentrewiseUserNameRegistration(short organisationCentreId, short organisationCentrewiseUserNameRegistrationId)
        {
            OrganisationCentrewiseUserNameRegistrationViewModel organisationCentrewiseUserNameRegistrationViewModel = _organisationCentreAgent.GetCentrewiseUserName(organisationCentreId, organisationCentrewiseUserNameRegistrationId);
            BindDropdown(organisationCentrewiseUserNameRegistrationViewModel);
            return ActionView(OrganisationCentrewiseUserNameRegistration, organisationCentrewiseUserNameRegistrationViewModel);
        }

        [HttpPost]
        public virtual ActionResult CentrewiseUserNameRegistration(OrganisationCentrewiseUserNameRegistrationViewModel organisationCentrewiseUserNameRegistrationViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_organisationCentreAgent.UpdateCentrewiseUserName(organisationCentrewiseUserNameRegistrationViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("CentrewiseUserNameRegistrationList", new { organisationCentreId = organisationCentrewiseUserNameRegistrationViewModel.OrganisationCentreMasterId });
            }
            BindDropdown(organisationCentrewiseUserNameRegistrationViewModel);
            return View(OrganisationCentrewiseUserNameRegistration, organisationCentrewiseUserNameRegistrationViewModel);
        }
        #endregion

        #region Protected
        protected virtual void BindDropdown(OrganisationCentrewiseUserNameRegistrationViewModel organisationCentrewiseUserNameRegistrationViewModel)
        {
            List<SelectListItem> UserTypeList = new List<SelectListItem>();
            UserTypeList.Add(new SelectListItem { Text = GeneralResources.SelectLabel, Value = "" });
            var userTypeList = Enum.GetValues(typeof(UserTypeEnum)).Cast<UserTypeEnum>().ToList();
            foreach (var item in userTypeList)
            {
                if (organisationCentrewiseUserNameRegistrationViewModel.OrganisationCentrewiseUserNameRegistrationId == 0 &&
                    organisationCentrewiseUserNameRegistrationViewModel?.CentrewiseUserNameRegistrationList?.Count > 0 &&
                    organisationCentrewiseUserNameRegistrationViewModel.CentrewiseUserNameRegistrationList.Any(x => x.UserType == item.ToString()))
                {
                    continue;
                }
                UserTypeList.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString(), Selected = item.ToString() == organisationCentrewiseUserNameRegistrationViewModel.UserType });
            }
            ViewData["UserType"] = UserTypeList;

            List<SelectListItem> UserNameBasedOnList = new List<SelectListItem>();
            UserNameBasedOnList.Add(new SelectListItem { Text = GeneralResources.SelectLabel, Value = "" });
            var userNameBasedOnList = Enum.GetValues(typeof(UserNameRegistrationTypeEnum)).Cast<UserNameRegistrationTypeEnum>().ToList();
            foreach (var item in userNameBasedOnList)
            {
                UserNameBasedOnList.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString(), Selected = item.ToString() == organisationCentrewiseUserNameRegistrationViewModel.UserNameBasedOn });
            }
            ViewData["UserNameBasedOn"] = UserNameBasedOnList;
        }
        #endregion
    }
}
