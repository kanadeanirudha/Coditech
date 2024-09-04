using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GazetteChaptersPageDetailController : BaseController
    {
        private readonly IGazetteChaptersPageDetailAgent _gazetteChaptersPageDetailAgent;
        private const string createEdit = "~/Views/Gazette/GazetteChapterPageDetails/CreateEdit.cshtml";

        public GazetteChaptersPageDetailController(IGazetteChaptersPageDetailAgent gazetteChaptersPageDetailAgent)
        {
            _gazetteChaptersPageDetailAgent = gazetteChaptersPageDetailAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GazetteChaptersPageDetailListViewModel list = _gazetteChaptersPageDetailAgent.GetGazetteChaptersPageDetailList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Gazette/GazetteChapterPageDetails/_List.cshtml", list);
            }
            return View($"~/Views/Gazette/GazetteChapterPageDetails/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GazetteChaptersPageDetailViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GazetteChaptersPageDetailViewModel gazetteChaptersPageDetailViewModel)
        {
            if (ModelState.IsValid)
            {
                gazetteChaptersPageDetailViewModel = _gazetteChaptersPageDetailAgent.CreateGazetteChaptersPageDetail(gazetteChaptersPageDetailViewModel);
                if (!gazetteChaptersPageDetailViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(gazetteChaptersPageDetailViewModel.ErrorMessage));
            return View(createEdit, gazetteChaptersPageDetailViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int gazetteChaptersPageDetailId)
        {
            GazetteChaptersPageDetailViewModel gazetteChaptersPageDetailViewModel = _gazetteChaptersPageDetailAgent.GetGazetteChaptersPageDetail(gazetteChaptersPageDetailId);
            return ActionView(createEdit, gazetteChaptersPageDetailViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GazetteChaptersPageDetailViewModel gazetteChaptersPageDetailViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_gazetteChaptersPageDetailAgent.UpdateGazetteChaptersPageDetail(gazetteChaptersPageDetailViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { gazetteChaptersPageDetailId = gazetteChaptersPageDetailViewModel.GazetteChapterPageDetailId });
            }
            return View(createEdit, gazetteChaptersPageDetailViewModel);
        }

        public virtual ActionResult Delete(string gazetteChaptersPageDetailIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(gazetteChaptersPageDetailIds))
            {
                status = _gazetteChaptersPageDetailAgent.DeleteGazetteChaptersPageDetail(gazetteChaptersPageDetailIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GazetteChaptersPageDetailController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GazetteChaptersPageDetailController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}