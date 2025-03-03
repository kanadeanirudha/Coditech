using System.Collections.Generic;
using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Data;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class AccGLTransactionController : BaseController
    {
        private readonly IAccGLTransactionAgent _accGLTransactionAgent;
        private const string createEdit = "~/Views/Accounts/AccGLTransaction/CreateEdit.cshtml";

        public AccGLTransactionController(IAccGLTransactionAgent accGLTransactionAgent)
        {
            _accGLTransactionAgent = accGLTransactionAgent;
        }
        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            AccGLTransactionListViewModel list = new AccGLTransactionListViewModel();
            GetListOnlyIfSingleCentre(dataTableModel);
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode) && !string.IsNullOrEmpty(dataTableModel.SelectedParameter1) && !string.IsNullOrEmpty(dataTableModel.SelectedParameter2) && !string.IsNullOrEmpty(dataTableModel.SelectedParameter3) && !string.IsNullOrEmpty(dataTableModel.SelectedParameter4))
            {
                list = _accGLTransactionAgent.GetGLTransactionList(dataTableModel, Convert.ToString(dataTableModel.SelectedCentreCode), Convert.ToInt32(dataTableModel.SelectedParameter1), Convert.ToInt16(dataTableModel.SelectedParameter2), Convert.ToInt16(dataTableModel.SelectedParameter3),Convert.ToByte(dataTableModel.SelectedParameter4));
            }
            list.SelectedCentreCode= dataTableModel.SelectedCentreCode;
            list.SelectedParameter1 = dataTableModel.SelectedParameter1;
            list.SelectedParameter2 = dataTableModel.SelectedParameter2;
            list.SelectedParameter3 = dataTableModel.SelectedParameter3;
            list.SelectedParameter4 = dataTableModel.SelectedParameter4;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Accounts/AccGLTransaction/_List.cshtml", list);
            }
            return View($"~/Views/Accounts/AccGLTransaction/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new AccGLTransactionViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(AccGLTransactionViewModel accGLTransactionViewModel)
        {
            if (ModelState.IsValid)
            {
                accGLTransactionViewModel = _accGLTransactionAgent.CreateGLTransaction(accGLTransactionViewModel);
                if (!accGLTransactionViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<AccGLTransactionController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(accGLTransactionViewModel.ErrorMessage));
            return View(createEdit, accGLTransactionViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(long accGLTransactionId)
        {
            AccGLTransactionViewModel accGLTransactionViewModel = _accGLTransactionAgent.GetGLTransaction(accGLTransactionId);
            return ActionView(createEdit, accGLTransactionViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(AccGLTransactionViewModel accGLTransactionViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_accGLTransactionAgent.UpdateGLTransaction(accGLTransactionViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { accGLTransactionId = accGLTransactionViewModel.AccGLTransactionId });
            }
            return View(createEdit, accGLTransactionViewModel);
        }
        //public virtual ActionResult Delete(string accGLTransactionIds)
        //{
        //    string message = string.Empty;
        //    bool status = false;
        //    if (!string.IsNullOrEmpty(accGLTransactionIds))
        //    {
        //        status = _accGLTransactionAgent.DeleteGLTransaction(accGLTransactionIds, out message);
        //        SetNotificationMessage(!status
        //        ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
        //        : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
        //        return RedirectToAction<AccGLTransactionController>(x => x.List(null));
        //    }

        //    SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
        //    return RedirectToAction<AccGLTransactionController>(x => x.List(null));
        //}
        public virtual ActionResult Cancel( string SelectedParameter1 , string SelectedParameter2 , string SelectParameter3)
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { SelectedParameter1= SelectedParameter1 ,SelectedParameter2=SelectedParameter2, SelectedParameter3=SelectParameter3  };
            return RedirectToAction("List", dataTableViewModel);
        }
        #region Protected

        #endregion
    }
}