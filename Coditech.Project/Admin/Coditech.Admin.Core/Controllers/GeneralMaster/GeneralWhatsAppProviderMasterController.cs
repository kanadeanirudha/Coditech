using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;
namespace Coditech.Admin.Controllers
{
    public class GeneralWhatsAppProviderMasterController : BaseController
    {
        private readonly IGeneralWhatsAppProviderAgent _generalWhatsAppProviderAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralWhatsAppProviderMaster/CreateEdit.cshtml";

        public GeneralWhatsAppProviderMasterController(IGeneralWhatsAppProviderAgent generalWhatsAppProviderAgent)
        {
            _generalWhatsAppProviderAgent = generalWhatsAppProviderAgent;

        }
        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralWhatsAppProviderListViewModel list = _generalWhatsAppProviderAgent.GetWhatsAppProviderList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralWhatsAppProviderMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralWhatsAppProviderMaster/List.cshtml", list);
        }
        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GeneralWhatsAppProviderViewModel());
        }
        [HttpPost]
        public virtual ActionResult Create(GeneralWhatsAppProviderViewModel generalWhatsAppProviderViewModel)
        {
            if (ModelState.IsValid)
            {
                generalWhatsAppProviderViewModel = _generalWhatsAppProviderAgent.CreateWhatsAppProvider(generalWhatsAppProviderViewModel);
                if (!generalWhatsAppProviderViewModel.HasError) 
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalWhatsAppProviderViewModel.ErrorMessage));
            return View(createEdit, generalWhatsAppProviderViewModel);

        }
        public virtual ActionResult Edit(short generalWhatsAppProviderId)
        {
            GeneralWhatsAppProviderViewModel generalWhatsAppProviderViewModel = _generalWhatsAppProviderAgent.GetWhatsAppProvider(generalWhatsAppProviderId);
            return ActionView(createEdit, generalWhatsAppProviderViewModel);
        }
        [HttpPost]
        public virtual ActionResult Edit(GeneralWhatsAppProviderViewModel generalWhatsAppProviderViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalWhatsAppProviderAgent.UpdateWhatsAppProvider(generalWhatsAppProviderViewModel).HasError
                 ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                 : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { generalWhatsAppProviderId = generalWhatsAppProviderViewModel.GeneralWhatsAppProviderId });
            }
            return View(createEdit, generalWhatsAppProviderViewModel);
        }
        public virtual ActionResult Delete(string generalWhatsAppProviderId)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(generalWhatsAppProviderId))
            {
                status = _generalWhatsAppProviderAgent.DeleteWhatsAppProvider(generalWhatsAppProviderId, out message);
                SetNotificationMessage(!status
                    ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                    : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralWhatsAppProviderMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralWhatsAppProviderMasterController>(x => x.List(null));
        }

        #region Protected
        #endregion
    }
}