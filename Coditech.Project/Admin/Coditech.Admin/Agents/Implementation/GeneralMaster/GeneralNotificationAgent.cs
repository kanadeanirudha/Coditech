using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using System.Diagnostics;
using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.Admin.Agents
{
    public class GeneralNotificationAgent : BaseAgent, IGeneralNotificationAgent
    {
        #region Private Variable 
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralNotificationClient _generalNotificationClient;
        #endregion

        #region public constructor 
        public GeneralNotificationAgent(ICoditechLogging coditechLogging, IGeneralNotificationClient generalNotificationClient)
        {
            _coditechLogging = coditechLogging;
            _generalNotificationClient = GetClient<IGeneralNotificationClient>(generalNotificationClient);
        }
        #endregion
        #region public Methods 
        public virtual GeneralNotificationListViewModel GetNotificationList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();

            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("NotificationDetails", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("FromDate", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("UptoDate", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "NotificationDetails" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralNotificationListResponse response = _generalNotificationClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralNotificationListModel NotificationList = new GeneralNotificationListModel { GeneralNotificationList = response?.GeneralNotificationList };
            GeneralNotificationListViewModel listViewModel = new GeneralNotificationListViewModel();
            listViewModel.GeneralNotificationList = NotificationList?.GeneralNotificationList?.ToViewModel<GeneralNotificationViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralNotificationList.Count, BindColumns());
            return listViewModel;
        }
        public virtual GeneralNotificationViewModel CreateNotification(GeneralNotificationViewModel generalNotificationViewModel)
        {
            try
            {
                GeneralNotificationResponse response = _generalNotificationClient.CreateNotification(generalNotificationViewModel.ToModel<GeneralNotificationModel>());
                GeneralNotificationModel generalNotificationModel = response?.GeneralNotificationModel;
                return IsNotNull(generalNotificationModel) ? generalNotificationModel.ToViewModel<GeneralNotificationViewModel>() : new GeneralNotificationViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.NotificationMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralNotificationViewModel)GetViewModelWithErrorMessage(generalNotificationViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralNotificationViewModel)GetViewModelWithErrorMessage(generalNotificationViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.NotificationMaster.ToString(), TraceLevel.Error);
                return (GeneralNotificationViewModel)GetViewModelWithErrorMessage(generalNotificationViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }
        //Get general Notification by general Notification id.
        public virtual GeneralNotificationViewModel GetNotification(long generalNotificationId)
        {
            GeneralNotificationResponse response = _generalNotificationClient.GetNotification(generalNotificationId);
            return response?.GeneralNotificationModel.ToViewModel<GeneralNotificationViewModel>();
        }

        //Update generalNotification.
        public virtual GeneralNotificationViewModel UpdateNotification(GeneralNotificationViewModel generalNotificationViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.NotificationMaster.ToString(), TraceLevel.Info);
                GeneralNotificationResponse response = _generalNotificationClient.UpdateNotification(generalNotificationViewModel.ToModel<GeneralNotificationModel>());
                GeneralNotificationModel generalNotificationModel = response?.GeneralNotificationModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.NotificationMaster.ToString(), TraceLevel.Info);
                return IsNotNull(generalNotificationModel) ? generalNotificationModel.ToViewModel<GeneralNotificationViewModel>() : (GeneralNotificationViewModel)GetViewModelWithErrorMessage(new GeneralNotificationViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.NotificationMaster.ToString(), TraceLevel.Error);
                return (GeneralNotificationViewModel)GetViewModelWithErrorMessage(generalNotificationViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete Notification.
        public virtual bool DeleteNotification(string GeneralNotificationId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.NotificationMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalNotificationClient.DeleteNotification(new ParameterModel { Ids = GeneralNotificationId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.NotificationMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralNotificationMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.NotificationMaster.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }
        #endregion

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Notification Details",
                ColumnCode = "NotificationDetails",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "From Date",
                ColumnCode = "FromDate",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Upto Date",
                ColumnCode = "UptoDate",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "IsActive",
                ColumnCode = "DefaultFlag",
            });
            return datatableColumnList;
        }
        #endregion
        #region
         //it will return all Notification list from database 
       public virtual GeneralNotificationListResponse GetNotificationList()
        {
            GeneralNotificationListResponse NotificationList = _generalNotificationClient.List(null, null, null, 1, int.MaxValue);
            return NotificationList?.GeneralNotificationList?.Count > 0 ? NotificationList : new GeneralNotificationListResponse();
        }
        #endregion
    }
}