﻿using AspNetCore.Reporting;

using Coditech.Admin.Helpers;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using System.Data;
using System.Linq.Expressions;
using System.Text;

namespace Coditech.Admin.Controllers
{
    public class BaseController : Controller
    {
        #region Notification
        /// <summary>
        /// Set notification message.
        /// </summary>
        /// <param name="notificationMessage">Message to set.</param>
        protected void SetNotificationMessage(string notificationMessage)
            => TempData[AdminConstants.Notifications] = notificationMessage;

        /// <summary>
        /// Get the success notification message.
        /// </summary>
        /// <param name="successMessage">success message.</param>
        /// <returns>Returns serialize MessageBoxModel with notification set to success.</returns>
        protected string GetSuccessNotificationMessage(string successMessage)
            => GenerateNotificationMessages(successMessage, NotificationType.success);

        /// <summary>
        /// Get the error notification message.
        /// </summary>
        /// <param name="errorMessage">error message.</param>
        /// <returns>Returns serialize MessageBoxModel with notification set to error.</returns>
        protected string GetErrorNotificationMessage(string errorMessage)
            => GenerateNotificationMessages(errorMessage, NotificationType.error);

        /// <summary>
        /// Get the information notification message.
        /// </summary>
        /// <param name="infoMessage">information message.</param>
        /// <returns>Returns serialize MessageBoxModel with notification set to info.</returns>
        protected string GetInfoNotificationMessage(string infoMessage)
            => GenerateNotificationMessages(infoMessage, NotificationType.info);

        /// <summary>
        /// To show Notification message 
        /// </summary>
        /// <param name="message">string message to show on page</param>
        /// <param name="type">enum type of message</param>
        /// <param name="isFadeOut">bool isFadeOut true/false</param>
        /// <param name="fadeOutMilliSeconds">int fadeOutMilliSeconds</param>
        /// <returns>string Json format of message box</returns>
        protected string GenerateNotificationMessages(string message, NotificationType type)
        {
            MessageBoxModel msgObj = new MessageBoxModel();
            msgObj.Message = message;
            msgObj.Type = type.ToString();
            msgObj.IsFadeOut = CheckIsFadeOut();
            return JsonConvert.SerializeObject(msgObj);
        }
        /// <summary>
        /// To get IsFadeOut status from web config file, 
        /// if NotificationMessagesIsFadeOut key not found in config then it will returns false 
        /// </summary>
        /// <returns>return true/false</returns>
        private bool CheckIsFadeOut()
        {
            bool isFadeOut = Convert.ToBoolean(CoditechAdminSettings.NotificationMessagesIsFadeOut);
            return isFadeOut;
        }
        #endregion

        /// <summary>
        /// Strongly Type Redirect To Action
        /// </summary>
        /// <typeparam name="TController">Controller Name</typeparam>
        /// <param name="action">Action Name</param>
        /// <returns>Strongly Type Action Result</returns>
        /// <example>
        /// If your controller name is "Dashboard" and Action Mehtod name is "Dashboard"
        /// Then we can redirect to action method using strongly type as
        /// <code>
        /// RedirectToAction<DashboardController>(o => o.Index())
        /// </code>
        /// </example>
        protected ActionResult RedirectToAction<TController>(
                Expression<Action<TController>> action)
                where TController : Controller
        {
            return ControllerExtensions.RedirectToAction(this, action);
        }

        public virtual ActionResult ActionView(string viewName, object model)
        {
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView(viewName, model);
            }
            return View(viewName, model);
        }

        protected virtual DataTableViewModel CreateActionDataTable(string centreCode = null, short selectedDepartmentId = 0, DataTableViewModel dataTableModel = null)
        {
            if (dataTableModel == null)
            {
                dataTableModel = new DataTableViewModel()
                {
                    SortByColumn = SortKeys.CreatedDate,
                    SortBy = AdminConstants.DESCKey,
                    SelectedCentreCode = centreCode,
                    SelectedDepartmentId = selectedDepartmentId
                };
            }
            else
            {
                dataTableModel.SortByColumn = SortKeys.ModifiedDate;
                dataTableModel.SortBy = AdminConstants.DESCKey;
                dataTableModel.SelectedCentreCode = centreCode;
                dataTableModel.SelectedDepartmentId = selectedDepartmentId;
            }
            return dataTableModel;
        }

        protected virtual DataTableViewModel UpdateActionDataTable(string centreCode = null, short selectedDepartmentId = 0, DataTableViewModel dataTableModel = null)
        {
            if (dataTableModel == null)
            {
                dataTableModel = new DataTableViewModel()
                {
                    SortByColumn = SortKeys.ModifiedDate,
                    SortBy = AdminConstants.DESCKey,
                    SelectedCentreCode = centreCode,
                    SelectedDepartmentId = selectedDepartmentId
                };
            }
            else
            {
                dataTableModel.SortByColumn = SortKeys.ModifiedDate;
                dataTableModel.SortBy = AdminConstants.DESCKey;
                dataTableModel.SelectedCentreCode = centreCode;
                dataTableModel.SelectedDepartmentId = selectedDepartmentId;
            }
            return dataTableModel;
        }

        public virtual Stream GetReport(IWebHostEnvironment _environment, string reportFolder, string rdlcReportName, DataTable dataTable, string dataSet, Dictionary<string, string> reportParameters, RenderType renderType)
        {
            string mimeType = "";
            int pageIndex = 1;
            var _reportPath = $"{_environment.ContentRootPath}\\Reports\\{reportFolder}\\{rdlcReportName}.rdlc";
            LocalReport localReport = new LocalReport(_reportPath);

            localReport.AddDataSource(dataSet, dataTable);

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var result = localReport.Execute(renderType, pageIndex, reportParameters, mimeType);
            byte[] file = result.MainStream;

            Stream stream = new MemoryStream(file);
            return stream;
        }

        protected virtual void GetListOnlyIfSingleCentre(DataTableViewModel dataTableModel)
        {
            if (string.IsNullOrEmpty(dataTableModel.SelectedCentreCode))
            {
                List<UserAccessibleCentreModel> accessibleCentreList = SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession)?.AccessibleCentreList;
                if (accessibleCentreList != null && accessibleCentreList.Count == 1)
                {
                    dataTableModel.SelectedCentreCode = accessibleCentreList[0].CentreCode;
                }
            }
        }
        public ActionResult IscheckAccPrequisiteStatified()
        {
            AccPrequisiteModel accPrequisitesModel = SessionHelper.GetDataFromSession<AccPrequisiteModel>(AdminConstants.AccountPrerequisiteSession);

            List<AccPrequisiteModel> accPrequisiteLists = new List<AccPrequisiteModel>();

            if (accPrequisitesModel == null)
            {
                accPrequisiteLists.Add(new AccPrequisiteModel { Name = "Balance Sheet", Field = "BalanceSheet", IsAssociated = false });
                return View("~/Views/Shared/BalanceSheetAssociated.cshtml", accPrequisiteLists);
            }
            accPrequisiteLists.Add(new AccPrequisiteModel { Name = "Currency", Field = "Currency", IsAssociated = accPrequisitesModel.IsCurrencyAssociated });
            accPrequisiteLists.Add(new AccPrequisiteModel { Name = "Financial Year", Field = "FinancialYear", IsAssociated = accPrequisitesModel.IsFinacialYearAssociated });
            accPrequisiteLists.Add(new AccPrequisiteModel { Name = "Account GL Balance Sheet", Field = "AccGLBalanceSheet", IsAssociated = accPrequisitesModel.IsAccGLBalanceSheetAssociated });
            return View("~/Views/Shared/BalanceSheetAssociated.cshtml", accPrequisiteLists);
        }



    }
}
