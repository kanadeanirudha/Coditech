using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Data;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Coditech.Admin.Controllers
{
    public class OrganisationCentreMasterController : BaseController
    {
        private readonly IOrganisationCentreAgent _organisationCentreAgent;
        private readonly IUserAgent _userTypeAgent;
        private const string createEdit = "~/Views/Organisation/OrganisationCentre/CreateEdit.cshtml";
        private const string OrganisationCentrePrintingFormat = "~/Views/Organisation/OrganisationCentre/OrganisationCentrePrintingFormat.cshtml";
        private const string OrganisationCentrewiseGSTCredential = "~/Views/Organisation/OrganisationCentre/OrganisationCentrewiseGSTCredential.cshtml";
        private const string OrganisationCentrewiseSmtpSetting = "~/Views/Organisation/OrganisationCentre/OrganisationCentrewiseSmtpSetting.cshtml";
        private const string OrganisationCentrewiseSmsSetting = "~/Views/Organisation/OrganisationCentre/OrganisationCentrewiseSmsSetting.cshtml";
        private const string OrganisationCentrewiseEmailTemplate = "~/Views/Organisation/OrganisationCentre/OrganisationCentrewiseEmailTemplate.cshtml";
        private const string OrganisationCentrewiseWhatsAppTemplate = "~/Views/Organisation/OrganisationCentre/OrganisationCentrewiseWhatsAppTemplate.cshtml";
        private const string OrganisationCentrewiseSMSTemplate = "~/Views/Organisation/OrganisationCentre/OrganisationCentrewiseSMSTemplate.cshtml";
        private const string OrganisationCentrewiseUserNameRegistration = "~/Views/Organisation/OrganisationCentre/OrganisationCentrewiseUserNameRegistration.cshtml";
        private const string OrganisationCentrewiseWhatsAppSetting = "~/Views/Organisation/OrganisationCentre/OrganisationCentrewiseWhatsAppSetting.cshtml";
        public OrganisationCentreMasterController(IOrganisationCentreAgent organisationCentreAgent, IUserAgent userTypeAgent)
        {
            _organisationCentreAgent = organisationCentreAgent;
            _userTypeAgent = userTypeAgent;
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
                    if (string.Equals(organisationCentreViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { organisationCentreId = organisationCentreViewModel.OrganisationCentreMasterId });
                    }
                    else if (string.Equals(organisationCentreViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToList);
                    }
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(organisationCentreViewModel.ErrorMessage));
            return View(createEdit, organisationCentreViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int organisationCentreId)
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
                if (string.Equals(organisationCentreViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { organisationCentreId = organisationCentreViewModel.OrganisationCentreMasterId });
                }
                else if (string.Equals(organisationCentreViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
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
        public virtual ActionResult PrintingFormat(int organisationCentreId)
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
                if (string.Equals(organisationCentrePrintingFormatViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("PrintingFormat", new { organisationCentreId = organisationCentrePrintingFormatViewModel.OrganisationCentreMasterId });
                }
                else if (string.Equals(organisationCentrePrintingFormatViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            return View(OrganisationCentrePrintingFormat, organisationCentrePrintingFormatViewModel);
        }
        #endregion

        #region CentrewiseGSTSetup
        [HttpGet]
        public virtual ActionResult CentrewiseGSTSetup(int organisationCentreId)
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
                if (string.Equals(organisationCentrewiseGSTCredentialViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("CentrewiseGSTSetup", new { organisationCentreId = organisationCentrewiseGSTCredentialViewModel.OrganisationCentreMasterId });
                }
                else if (string.Equals(organisationCentrewiseGSTCredentialViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            return View(OrganisationCentrewiseGSTCredential, organisationCentrewiseGSTCredentialViewModel);
        }
        #endregion

        #region CentrewiseSMTPSetting
        [HttpGet]
        public virtual ActionResult CentrewiseSmtpSetup(int organisationCentreId)
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
                if (string.Equals(organisationCentrewiseSmtpSettingViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("CentrewiseSmtpSetup", new { organisationCentreId = organisationCentrewiseSmtpSettingViewModel.OrganisationCentreMasterId });
                }
                else if (string.Equals(organisationCentrewiseSmtpSettingViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            return View(OrganisationCentrewiseSmtpSetting, organisationCentrewiseSmtpSettingViewModel);
        }
        [HttpGet]
        public virtual ActionResult SendTestEmail(string centreCode, int organisationCentreMasterId)
        {
            OrganisationCentrewiseSmtpSettingSendTestEmailViewModel organisationCentrewiseSmtpSettingSendTestEmailViewModel = new OrganisationCentrewiseSmtpSettingSendTestEmailViewModel()
            {
                CentreCode = centreCode,
                OrganisationCentreMasterId = organisationCentreMasterId
            };
            return PartialView("~/Views/Organisation/OrganisationCentre/_OrganisationCentrewiseSmtpSettingSendTestEmailPopUp.cshtml", organisationCentrewiseSmtpSettingSendTestEmailViewModel);
        }

        [HttpPost]
        public ActionResult SendTestEmail(OrganisationCentrewiseSmtpSettingSendTestEmailViewModel organisationCentrewiseSmtpSettingSendTestEmailViewModel)
        {
            int OrganisationCentreMasterId = organisationCentrewiseSmtpSettingSendTestEmailViewModel.OrganisationCentreMasterId;
            ModelState.Remove("Message");
            ModelState.Remove("MobileNumber");
            if (ModelState.IsValid)
            {
                organisationCentrewiseSmtpSettingSendTestEmailViewModel = _organisationCentreAgent.SendTestSetting(organisationCentrewiseSmtpSettingSendTestEmailViewModel);
                if (!organisationCentrewiseSmtpSettingSendTestEmailViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage("Test email sent successfully."));

                    return RedirectToAction("CentrewiseSmtpSetup", new { organisationCentreId = OrganisationCentreMasterId });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(organisationCentrewiseSmtpSettingSendTestEmailViewModel.ErrorMessage));
            return View(createEdit, organisationCentrewiseSmtpSettingSendTestEmailViewModel);
        }
        #endregion

        #region CentrewiseSMSSetting
        [HttpGet]
        public virtual ActionResult CentrewiseSmsSetup(int organisationCentreId, byte generalSmsProviderId = 0)
        {
            OrganisationCentrewiseSmsSettingViewModel organisationCentrewiseSmsSettingViewModel = _organisationCentreAgent.GetCentrewiseSmsSetup(organisationCentreId, generalSmsProviderId);
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
                if (string.Equals(organisationCentrewiseSmsSettingViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("CentrewiseSMSSetup", new { organisationCentreId = organisationCentrewiseSmsSettingViewModel.OrganisationCentreMasterId, generalSmsProviderId = organisationCentrewiseSmsSettingViewModel.GeneralSmsProviderId });
                }
                else if (string.Equals(organisationCentrewiseSmsSettingViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            return View(OrganisationCentrewiseSmsSetting, organisationCentrewiseSmsSettingViewModel);
        }
        [HttpGet]
        public virtual ActionResult SendSmsTestModel(string centreCode, int organisationCentreMasterId)
        {
            OrganisationCentrewiseSmtpSettingSendTestEmailViewModel organisationCentrewiseSmtpSettingSendTestEmailViewModel = new OrganisationCentrewiseSmtpSettingSendTestEmailViewModel()
            {
                CentreCode = centreCode,
                OrganisationCentreMasterId = organisationCentreMasterId
            };
            return PartialView("~/Views/Organisation/OrganisationCentre/_OrganisationCentrewiseSmsTestPopUp.cshtml", organisationCentrewiseSmtpSettingSendTestEmailViewModel);
        }

        [HttpPost]
        public ActionResult SendSmsTestModel(OrganisationCentrewiseSmtpSettingSendTestEmailViewModel organisationCentrewiseSmtpSettingSendTestEmailViewModel)
        {
            int OrganisationCentreMasterId = organisationCentrewiseSmtpSettingSendTestEmailViewModel.OrganisationCentreMasterId;

            ModelState.Remove("TO");
            ModelState.Remove("Subject");
            ModelState.Remove("Body");
            if (ModelState.IsValid)
            {
                organisationCentrewiseSmtpSettingSendTestEmailViewModel = _organisationCentreAgent.SendTestSetting(organisationCentrewiseSmtpSettingSendTestEmailViewModel);
                if (!organisationCentrewiseSmtpSettingSendTestEmailViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage("Test SMS sent successfully."));

                    return RedirectToAction("CentrewiseSMSSetup", new { organisationCentreId = OrganisationCentreMasterId });


                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(organisationCentrewiseSmtpSettingSendTestEmailViewModel.ErrorMessage));
            return View(createEdit, organisationCentrewiseSmtpSettingSendTestEmailViewModel);
        }
        #endregion

        #region CentrewiseWhatsAppSetting
        [HttpGet]
        public virtual ActionResult CentrewiseWhatsAppSetup(int organisationCentreId, byte generalWhatsAppProviderId = 0)
        {
            OrganisationCentrewiseWhatsAppSettingViewModel organisationCentrewiseWhatsAppSettingViewModel = _organisationCentreAgent.GetCentrewiseWhatsAppSetup(organisationCentreId, generalWhatsAppProviderId);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Organisation/OrganisationCentre/_OrganisationCentrewiseWhatsAppSetting.cshtml", organisationCentrewiseWhatsAppSettingViewModel);
            }
            return ActionView(OrganisationCentrewiseWhatsAppSetting, organisationCentrewiseWhatsAppSettingViewModel);
        }

        [HttpPost]
        public virtual ActionResult CentrewiseWhatsAppSetup(OrganisationCentrewiseWhatsAppSettingViewModel organisationCentrewiseWhatsAppSettingViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_organisationCentreAgent.UpdateCentrewiseWhatsAppSetup(organisationCentrewiseWhatsAppSettingViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                if (string.Equals(organisationCentrewiseWhatsAppSettingViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("CentrewiseWhatsAppSetup", new { organisationCentreId = organisationCentrewiseWhatsAppSettingViewModel.OrganisationCentreMasterId, generalWhatsAppProviderId = organisationCentrewiseWhatsAppSettingViewModel.GeneralWhatsAppProviderId });
                }
                else if (string.Equals(organisationCentrewiseWhatsAppSettingViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            return View(OrganisationCentrewiseWhatsAppSetting, organisationCentrewiseWhatsAppSettingViewModel);
        }

        [HttpGet]
        public virtual ActionResult SendWhatsAppTestModel(string centreCode, int organisationCentreMasterId)
        {
            OrganisationCentrewiseSmtpSettingSendTestEmailViewModel organisationCentrewiseSmtpSettingSendTestEmailViewModel = new OrganisationCentrewiseSmtpSettingSendTestEmailViewModel()
            {
                CentreCode = centreCode,
                OrganisationCentreMasterId = organisationCentreMasterId
            };
            return PartialView("~/Views/Organisation/OrganisationCentre/_OrganisationCentrewiseWhatsAppTestPopUp.cshtml", organisationCentrewiseSmtpSettingSendTestEmailViewModel);
        }

        [HttpPost]
        public ActionResult SendWhatsAppTestModel(OrganisationCentrewiseSmtpSettingSendTestEmailViewModel organisationCentrewiseSmtpSettingSendTestEmailViewModel)
        {
            int OrganisationCentreMasterId = organisationCentrewiseSmtpSettingSendTestEmailViewModel.OrganisationCentreMasterId;

            ModelState.Remove("TO");
            ModelState.Remove("Subject");
            ModelState.Remove("Body");
            if (ModelState.IsValid)
            {
                organisationCentrewiseSmtpSettingSendTestEmailViewModel = _organisationCentreAgent.SendTestSetting(organisationCentrewiseSmtpSettingSendTestEmailViewModel);
                if (!organisationCentrewiseSmtpSettingSendTestEmailViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage("Test whatsApp mesage sent successfully."));

                    return RedirectToAction("CentrewiseWhatsAppSetup", new { organisationCentreId = OrganisationCentreMasterId });


                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(organisationCentrewiseSmtpSettingSendTestEmailViewModel.ErrorMessage));
            return View(createEdit, organisationCentrewiseSmtpSettingSendTestEmailViewModel);
        }
        #endregion

        #region CentrewiseEmailTemplate
        [HttpGet]
        public virtual ActionResult CentrewiseEmailTemplateSetup(int organisationCentreId, string emailTemplateCode)
        {
            OrganisationCentrewiseEmailTemplateViewModel organisationCentrewiseEmailTemplateViewModel = _organisationCentreAgent.GetCentrewiseEmailTemplateSetup(organisationCentreId, emailTemplateCode, "email");
            return ActionView(OrganisationCentrewiseEmailTemplate, organisationCentrewiseEmailTemplateViewModel);
        }

        [HttpGet]
        public virtual ActionResult GetEmailTemplateByCentreCode(int organisationCentreId, string emailTemplateCode, string templateType)
        {
            OrganisationCentrewiseEmailTemplateViewModel organisationCentrewiseEmailTemplateViewModel = _organisationCentreAgent.GetCentrewiseEmailTemplateSetup(organisationCentreId, emailTemplateCode, templateType);
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
                if (string.Equals(organisationCentrewiseEmailTemplateViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("CentrewiseEmailTemplateSetup", new { organisationCentreId = organisationCentrewiseEmailTemplateViewModel.OrganisationCentreMasterId, emailTemplateCode = organisationCentrewiseEmailTemplateViewModel.EmailTemplateCode });
                }
                else if (string.Equals(organisationCentrewiseEmailTemplateViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            return View(OrganisationCentrewiseEmailTemplate, organisationCentrewiseEmailTemplateViewModel);
        }
        #endregion

        #region CentrewiseSMSTemplate
        [HttpGet]
        public virtual ActionResult CentrewiseSMSTemplateSetup(int organisationCentreId, string emailTemplateCode)
        {
            OrganisationCentrewiseEmailTemplateViewModel organisationCentrewiseEmailTemplateViewModel = _organisationCentreAgent.GetCentrewiseSMSTemplateSetup(organisationCentreId, emailTemplateCode);
            return ActionView(OrganisationCentrewiseSMSTemplate, organisationCentrewiseEmailTemplateViewModel);
        }

        [HttpPost]
        public virtual ActionResult CentrewiseSMSTemplateSetup(OrganisationCentrewiseEmailTemplateViewModel organisationCentrewiseEmailTemplateViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_organisationCentreAgent.UpdateCentrewiseSMSTemplateSetup(organisationCentrewiseEmailTemplateViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                if (string.Equals(organisationCentrewiseEmailTemplateViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("CentrewiseSMSTemplateSetup", new { organisationCentreId = organisationCentrewiseEmailTemplateViewModel.OrganisationCentreMasterId, emailTemplateCode = organisationCentrewiseEmailTemplateViewModel.EmailTemplateCode });
                }
                else if (string.Equals(organisationCentrewiseEmailTemplateViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            return View(OrganisationCentrewiseSMSTemplate, organisationCentrewiseEmailTemplateViewModel);
        }
        #endregion

        #region CentrewiseWhatsAppTemplate
        [HttpGet]
        public virtual ActionResult CentrewiseWhatsAppTemplateSetup(int organisationCentreId, string emailTemplateCode)
        {
            OrganisationCentrewiseEmailTemplateViewModel organisationCentrewiseEmailTemplateViewModel = _organisationCentreAgent.GetCentrewiseWhatsAppTemplateSetup(organisationCentreId, emailTemplateCode);
            return ActionView(OrganisationCentrewiseWhatsAppTemplate, organisationCentrewiseEmailTemplateViewModel);
        }

        [HttpPost]
        public virtual ActionResult CentrewiseWhatsAppTemplateSetup(OrganisationCentrewiseEmailTemplateViewModel organisationCentrewiseEmailTemplateViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_organisationCentreAgent.UpdateCentrewiseWhatsAppTemplateSetup(organisationCentrewiseEmailTemplateViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                if (string.Equals(organisationCentrewiseEmailTemplateViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("CentrewiseWhatsAppTemplateSetup", new { organisationCentreId = organisationCentrewiseEmailTemplateViewModel.OrganisationCentreMasterId, emailTemplateCode = organisationCentrewiseEmailTemplateViewModel.EmailTemplateCode });

                }
                else if (string.Equals(organisationCentrewiseEmailTemplateViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            return View(OrganisationCentrewiseWhatsAppTemplate, organisationCentrewiseEmailTemplateViewModel);
        }
        #endregion

        #region CentrewiseUserNameRegistration
        [HttpGet]
        public virtual ActionResult CentrewiseUserNameRegistrationList(int organisationCentreId)
        {
            OrganisationCentrewiseUserNameRegistrationViewModel organisationCentrewiseUserNameRegistrationViewModel = _organisationCentreAgent.GetCentrewiseUserName(organisationCentreId);
            return ActionView("~/Views/Organisation/OrganisationCentre/OrganisationCentrewiseUserNameRegistrationList.cshtml", organisationCentrewiseUserNameRegistrationViewModel);
        }

        [HttpGet]
        public virtual ActionResult CentrewiseUserNameRegistration(int organisationCentreId, int organisationCentrewiseUserNameRegistrationId)
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
                if (string.Equals(organisationCentrewiseUserNameRegistrationViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("CentrewiseUserNameRegistrationList", new { organisationCentreId = organisationCentrewiseUserNameRegistrationViewModel.OrganisationCentreMasterId });
                }
                else if (string.Equals(organisationCentrewiseUserNameRegistrationViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("CentrewiseUserNameRegistrationList", new { organisationCentreId = organisationCentrewiseUserNameRegistrationViewModel.OrganisationCentreMasterId });
                }
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
            var userTypeList = _userTypeAgent.GetUserTypeList().TypeList.Where(x => x.UserTypeCode != "Admin" && x.UserTypeCode != "Branch");

            foreach (var item in userTypeList)
            {
                if (organisationCentrewiseUserNameRegistrationViewModel.OrganisationCentrewiseUserNameRegistrationId == 0 &&
                    organisationCentrewiseUserNameRegistrationViewModel?.CentrewiseUserNameRegistrationList?.Count > 0 &&
                    organisationCentrewiseUserNameRegistrationViewModel.CentrewiseUserNameRegistrationList.Any(x => x.UserType == item.ToString()))
                {
                    continue;
                }
                UserTypeList.Add(new SelectListItem { Text = item.UserDescription, Value = item.UserTypeCode, Selected = item.UserTypeCode == organisationCentrewiseUserNameRegistrationViewModel.UserType });
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
