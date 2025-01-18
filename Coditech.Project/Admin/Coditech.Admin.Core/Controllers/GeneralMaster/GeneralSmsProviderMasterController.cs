using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;
namespace Coditech.Admin.Controllers
{
    public class GeneralSmsProviderMasterController : BaseController
    {
        private readonly IGeneralSmsProviderAgent _generalSmsProviderAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralSmsProviderMaster/CreateEdit.cshtml";
        public GeneralSmsProviderMasterController(IGeneralSmsProviderAgent generalSmsProviderAgent)
        {
            _generalSmsProviderAgent = generalSmsProviderAgent;
        }
        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralSmsProviderListViewModel list = _generalSmsProviderAgent.GetSmsProviderList(dataTableModel);
  
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralSmsProviderMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralSmsProviderMaster/List.cshtml", list);
        }
        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GeneralSmsProviderViewModel());
        }
        [HttpPost]
        public virtual ActionResult Create(GeneralSmsProviderViewModel generalSmsProviderViewModel)
        {
            if (ModelState.IsValid)
            {
                generalSmsProviderViewModel = _generalSmsProviderAgent.CreateSmsProvider(generalSmsProviderViewModel);
                if (!generalSmsProviderViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalSmsProviderViewModel.ErrorMessage));
            return View(createEdit, generalSmsProviderViewModel);
        }
        [HttpGet]
        public virtual ActionResult Edit(short generalSmsProviderId)
        {
            GeneralSmsProviderViewModel generalSmsProviderViewModel = _generalSmsProviderAgent.GetSmsProvider(generalSmsProviderId);
            return ActionView(createEdit, generalSmsProviderViewModel);
        }
        [HttpPost]
        public virtual ActionResult Edit(GeneralSmsProviderViewModel generalSmsProviderViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalSmsProviderAgent.UpdateSmsProvider(generalSmsProviderViewModel).HasError
                 ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                 : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { generalSmsProviderId = generalSmsProviderViewModel.GeneralSmsProviderId });
            }
            return View(createEdit, generalSmsProviderViewModel);


        }
        public virtual ActionResult Delete(string generalSmsProviderId)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(generalSmsProviderId))
            {
                status = _generalSmsProviderAgent.DeleteSmsProvider(generalSmsProviderId, out message);
                SetNotificationMessage(!status
                    ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                    : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralSmsProviderMasterController>(x => x.List(null));
            }
            
            SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralSmsProviderMasterController>(x => x.List(null)); 
        }

        #region Protected
        #endregion
    }
}

