using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralWhatsAppTemplateController : BaseController
    {

        private readonly IGeneralEmailTemplateAgent _generalEmailTemplateAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralWhatsAppTemplate/CreateEdit.cshtml";

        public GeneralWhatsAppTemplateController(IGeneralEmailTemplateAgent generalEmailTemplateAgent)
        {
            _generalEmailTemplateAgent = generalEmailTemplateAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralEmailTemplateListViewModel list = _generalEmailTemplateAgent.GetWhatsAppTemplateList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralWhatsAppTemplate/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralWhatsAppTemplate/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GeneralEmailTemplateViewModel() { IsWhatsAppTemplate = true });
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralEmailTemplateViewModel generalEmailTemplateViewModel)
        {
            if (ModelState.IsValid)
            {
                generalEmailTemplateViewModel = _generalEmailTemplateAgent.CreateEmailTemplate(generalEmailTemplateViewModel);
                if (!generalEmailTemplateViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalEmailTemplateViewModel.ErrorMessage));
            return View(createEdit, generalEmailTemplateViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short generalEmailTemplateId)
        {
            GeneralEmailTemplateViewModel generalEmailTemplateViewModel = _generalEmailTemplateAgent.GetEmailTemplate(generalEmailTemplateId);
            return ActionView(createEdit, generalEmailTemplateViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralEmailTemplateViewModel generalEmailTemplateViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalEmailTemplateAgent.UpdateEmailTemplate(generalEmailTemplateViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { generalEmailTemplateId = generalEmailTemplateViewModel.GeneralEmailTemplateId });
            }
            return View(createEdit, generalEmailTemplateViewModel);
        }

        public virtual ActionResult Delete(string generalEmailTemplateIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(generalEmailTemplateIds))
            {
                status = _generalEmailTemplateAgent.DeleteEmailTemplate(generalEmailTemplateIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralWhatsAppTemplateController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralWhatsAppTemplateController>(x => x.List(null));
        }
    }
}
