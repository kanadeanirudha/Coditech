using System.Collections.Generic;
using Coditech.Admin.Agents;
using Coditech.Admin.Helpers;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Coditech.Admin.Controllers
{
    public class AccGLTransactionController : BaseController
    {
        private readonly IAccGLTransactionAgent _accGLTransactionAgent;
        private readonly IGeneralCommonAgent _generalCommonAgent;
        private const string createEdit = "~/Views/Accounts/AccGLTransaction/CreateEdit.cshtml";

        public AccGLTransactionController(IAccGLTransactionAgent accGLTransactionAgent, IGeneralCommonAgent generalCommonAgent)
        {
            _accGLTransactionAgent = accGLTransactionAgent;
            _generalCommonAgent = generalCommonAgent;

        }
        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            AccGLTransactionListViewModel list = new AccGLTransactionListViewModel();
            GetListOnlyIfSingleCentre(dataTableModel);
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode) && !string.IsNullOrEmpty(dataTableModel.SelectedParameter1) && !string.IsNullOrEmpty(dataTableModel.SelectedParameter2) && !string.IsNullOrEmpty(dataTableModel.SelectedParameter3) && !string.IsNullOrEmpty(dataTableModel.SelectedParameter4))
            {
                list = _accGLTransactionAgent.GetGLTransactionList(dataTableModel, Convert.ToString(dataTableModel.SelectedCentreCode), Convert.ToInt32(dataTableModel.SelectedParameter1), Convert.ToInt16(dataTableModel.SelectedParameter2), Convert.ToInt16(dataTableModel.SelectedParameter3), Convert.ToByte(dataTableModel.SelectedParameter4));
            }
            list.SelectedCentreCode = dataTableModel.SelectedCentreCode;
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
        public virtual ActionResult GetFinancialYearListByCentreCode(string selectedCentreCode)
        {
            DropdownViewModel financialYearDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.FinancialYear.ToString(),
                DropdownName = "GeneralFinancialYearId",
                Parameter = $"{selectedCentreCode}",
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", financialYearDropdown);
        }
        [HttpGet]
        public virtual ActionResult Create()
        {
            if (!_generalCommonAgent.GetAccountPrequisite())
                return IscheckAccPrequisiteStatified();
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
                    return RedirectToAction<AccGLTransactionController>(x => x.Create(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(accGLTransactionViewModel.ErrorMessage));
            return View(createEdit, accGLTransactionViewModel);
        }


        public virtual ActionResult Cancel()
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { };
            return RedirectToAction("Create", dataTableViewModel);
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
        public virtual ActionResult Cancel(string SelectedParameter1, string SelectedParameter2, string SelectParameter3)
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { SelectedParameter1 = SelectedParameter1, SelectedParameter2 = SelectedParameter2, SelectedParameter3 = SelectParameter3 };
            return RedirectToAction("List", dataTableViewModel);
        }
        #region AutoSearch
        public JsonResult GetAccounts(string term, int accountId, string personType, string transactionTypeCode)
        {
            var data = GetAccSetupGLAccountList(term, accountId, personType, transactionTypeCode);
            return Json(data);
        }

        [HttpPost]
        public IActionResult GetAccSetupGLAccountList(string SearchKeyWord, int accountId, string personType, string transactionTypeCode)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid request." });
            }

            var transactions = _accGLTransactionAgent.GetAccSetupGLAccountList(SearchKeyWord, accountId, personType, transactionTypeCode);

            // Extract only AccSetupGLList and store it in SuggestionsAccSetupGLList
            var suggestionsAccSetupGLList = transactions
                .SelectMany(t => t.AccSetupGLList ?? new List<AccSetupGLModel>()) // Flatten the list
                .Select(gl => new AccSetupGLModel
                {
                    AccSetupGLId = gl.AccSetupGLId,
                    GLName = gl.GLName?.Trim(), // Clean up extra spaces/tabs
                    GLCode = gl.GLCode?.Trim(),
                    AccSetupGLTypeId=gl.AccSetupGLTypeId,
                    ParentAccSetupGLId = gl.ParentAccSetupGLId,
                    CategoryCode = gl.CategoryCode
                })
                .ToList();

            return Json(suggestionsAccSetupGLList); // Instead of wrapping it inside another object

        }

        public void SetNotificationMessage(List<AccGLTransactionViewModel> accGLTransactionViewModels)
        {
            if (accGLTransactionViewModels == null || !accGLTransactionViewModels.Any())
            {
                //var result = _accGLTransactionAgent.GetAccSetupGLAccountList(SearchKeyWord, accountId, personType, transactionTypeCode);
            }
            else
            {

            }
        }

        //private void SetNotificationMessage(List<AccGLTransactionViewModel> accGLTransactionViewModels)
        //{
        //    throw new NotImplementedException();
        //}

        //protected AccGLTransaction GetAccSetupGLAccountLists(string SearchKeyWord, int accountId, string personType, string transactionTypeCode)
        //{
        //    AccGLTransaction searchRequest = new AccGLTransaction();
        //    searchRequest.SearchWord = SearchKeyWord;
        //    searchRequest.AccountId = accountId;
        //    searchRequest.PersonType = personType;
        //    searchRequest.TransactionTypeCode = transactionTypeCode;
        //    List<AccGLTransaction> listAccountTransactionMaster = new List<AccGLTransaction>();
        //    IBaseEntityCollectionResponse<AccGLTransaction> baseEntityCollectionResponse = _accGLTransaction.GetAccountList(searchRequest);
        //    if (baseEntityCollectionResponse != null)
        //    {
        //        if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
        //        {
        //            listAccountTransactionMaster = baseEntityCollectionResponse.CollectionResponse.ToList();
        //        }
        //    }
        //    return listAccountTransactionMaster;
        //}
        ////protected async Task<List<AccGLTransaction>> GetAccSetupGLAccountList(string searchKeyword, int accountId, string personType, string transactionTypeCode)
        //{
        //    //var searchRequest = new AccTransactionSearchRequest
        //    //{
        //    //    SearchWord = searchKeyword,
        //    //    AccountId = accountId,
        //    //    PersonType = personType,
        //    //    TransactionTypeCode = transactionTypeCode
        //    //};

        //    //var baseEntityCollectionResponse = await Task.Run(() => _accountTransactionMasterBA.GetAccountList(searchRequest));

        //    //return baseEntityCollectionResponse?.CollectionResponse?.ToList() ?? new List<AccountTransactionMaster>();
        //}



        //[NonAction]
        //protected List<AccountTransactionMaster> GetAccountList(string SearchKeyWord, int accountId, string personType, string transactionTypeCode)
        //{
        //    AccGLTransactionSearchRequest searchRequest = new AccGLTransactionSearchRequest();
        //    searchRequest.SearchWord = SearchKeyWord;
        //    searchRequest.AccountId = accountId;
        //    searchRequest.PersonType = personType;
        //    searchRequest.TransactionTypeCode = transactionTypeCode;
        //    List<AccountTransactionMaster> listAccountTransactionMaster = new List<AccountTransactionMaster>();
        //    IBaseEntityCollectionResponse<AccountTransactionMaster> baseEntityCollectionResponse = _accountTransactionMasterBA.GetAccountList(searchRequest);
        //    if (baseEntityCollectionResponse != null)
        //    {
        //        if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
        //        {
        //            listAccountTransactionMaster = baseEntityCollectionResponse.CollectionResponse.ToList();
        //        }
        //    }
        //    return listAccountTransactionMaster;
        //}



        #endregion


        #region Protected

        #endregion
    }
}