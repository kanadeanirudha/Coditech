using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GazetteChaptersController : BaseController
    {
        private readonly IGazetteChaptersAgent _gazetteChaptersAgent;
        private const string createEdit = "~/Views/Gazette/GazetteChapters/CreateEdit.cshtml";

        public GazetteChaptersController(IGazetteChaptersAgent gazetteChaptersAgent)
        {
            _gazetteChaptersAgent = gazetteChaptersAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GazetteChaptersListViewModel list = _gazetteChaptersAgent.GetGazetteChaptersList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Gazette/GazetteChapters/_List.cshtml", list);
            }
            return View($"~/Views/Gazette/GazetteChapters/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GazetteChaptersViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GazetteChaptersViewModel gazetteChaptersViewModel)
        {
            if (ModelState.IsValid)
            {
                gazetteChaptersViewModel = _gazetteChaptersAgent.CreateGazetteChapters(gazetteChaptersViewModel);
                if (!gazetteChaptersViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(gazetteChaptersViewModel.ErrorMessage));
            return View(createEdit, gazetteChaptersViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int gazetteChaptersId)
        {
            GazetteChaptersViewModel gazetteChaptersViewModel = _gazetteChaptersAgent.GetGazetteChapters(gazetteChaptersId);
            return ActionView(createEdit, gazetteChaptersViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GazetteChaptersViewModel gazetteChaptersViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_gazetteChaptersAgent.UpdateGazetteChapters(gazetteChaptersViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { gazetteChaptersId = gazetteChaptersViewModel.GazetteChapterId });
            }
            return View(createEdit, gazetteChaptersViewModel);
        }

        public virtual ActionResult Delete(string gazetteChaptersIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(gazetteChaptersIds))
            {
                status = _gazetteChaptersAgent.DeleteGazetteChapters(gazetteChaptersIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GazetteChaptersController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GazetteChaptersController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}