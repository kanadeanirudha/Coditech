using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class DBTMTestMasterController : BaseController
    {
        private readonly IDBTMTestAgent _dBTMTestAgent;
        private const string createEdit = "~/Views/DBTM/DBTMTestMaster/CreateEdit.cshtml";

        public DBTMTestMasterController(IDBTMTestAgent dBTMTestAgent)
        {
            _dBTMTestAgent = dBTMTestAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            DBTMTestListViewModel list = _dBTMTestAgent.GetDBTMTestList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/DBTM/DBTMTestMaster/_List.cshtml", list);
            }
            return View($"~/Views/DBTM/DBTMTestMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new DBTMTestViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(DBTMTestViewModel dBTMTestViewModel)
        {
            if (ModelState.IsValid)
            {
                dBTMTestViewModel = _dBTMTestAgent.CreateDBTMTest(dBTMTestViewModel);
                if (!dBTMTestViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(dBTMTestViewModel.ErrorMessage));
            return View(createEdit, dBTMTestViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int dBTMTestMasterId)
        {
            DBTMTestViewModel dBTMTestViewModel = _dBTMTestAgent.GetDBTMTest(dBTMTestMasterId);
            return ActionView(createEdit, dBTMTestViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(DBTMTestViewModel dBTMTestViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_dBTMTestAgent.UpdateDBTMTest(dBTMTestViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { dBTMTestMasterId = dBTMTestViewModel.DBTMTestMasterId });
            }
            return View(createEdit, dBTMTestViewModel);
        }

        public virtual ActionResult Delete(string dBTMTestMasterIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(dBTMTestMasterIds))
            {
                status = _dBTMTestAgent.DeleteDBTMTest(dBTMTestMasterIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<DBTMTestMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<DBTMTestMasterController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}