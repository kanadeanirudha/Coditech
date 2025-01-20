using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Specialized;
using System.Data;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class GeneralNotificationService : IGeneralNotificationMasterService
    {
        protected readonly IServiceProvider serviceProvider;
        protected readonly ICoditechLogging coditechLogging;
        private readonly ICoditechRepository<GeneralNotification> _generalNotificationMasterRepository;
        private IServiceProvider _serviceProvider;
        private readonly ICoditechLogging _coditechLogging;

        public GeneralNotificationService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalNotificationMasterRepository = new CoditechRepository<GeneralNotification>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralNotificationListModel GetNotificationList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralNotificationModel> objStoredProc = new CoditechViewRepository<GeneralNotificationModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralNotificationModel> NotificationList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetNotificationList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralNotificationListModel listModel = new GeneralNotificationListModel();

            listModel.GeneralNotificationList = NotificationList?.Count > 0 ? NotificationList : new List<GeneralNotificationModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create Notification
        public virtual GeneralNotificationModel CreateNotification(GeneralNotificationModel generalNotificationModel)
        {
            if (IsNull(generalNotificationModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsNotificationCodeAlreadyExist(generalNotificationModel.NotificationDetails))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Notification"));

            GeneralNotification generalNotification = generalNotificationModel.FromModelToEntity<GeneralNotification>();

            //Create new Notification and return it.
            GeneralNotification NotificationData = _generalNotificationMasterRepository.Insert(generalNotification);
            if (NotificationData?.GeneralNotificationId > 0)
            {
                generalNotificationModel.GeneralNotificationId = NotificationData.GeneralNotificationId;
            }
            else
            {
                generalNotificationModel.HasError = true;
                generalNotificationModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalNotificationModel;
        }
        //Get Notification by Notification id.
        public virtual GeneralNotificationModel GetNotification(long NotificationId)
        {
            if (NotificationId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "NotificationId"));

            //Get the Notification Details based on id.
            GeneralNotification generalNotification = _generalNotificationMasterRepository.Table.FirstOrDefault(x => x.GeneralNotificationId == NotificationId);
            GeneralNotificationModel generalNotificationModel = generalNotification?.FromEntityToModel<GeneralNotificationModel>();
            return generalNotificationModel;
        }
        //Update Notification.
        public virtual bool UpdateNotification(GeneralNotificationModel generalNotificationModel)
        {
            if (IsNull(generalNotificationModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalNotificationModel.GeneralNotificationId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "NotificationId"));

            if (IsNotificationCodeAlreadyExist(generalNotificationModel.NotificationDetails, generalNotificationModel.GeneralNotificationId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Notification Details"));

            GeneralNotification generalNotification = generalNotificationModel.FromModelToEntity<GeneralNotification>();

            //Update Notification
            bool isSmsProviderUpdated = _generalNotificationMasterRepository.Update(generalNotification);
            if (!isSmsProviderUpdated)
            {
                generalNotificationModel.HasError = true;
                generalNotificationModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isSmsProviderUpdated;
        }
        //Delete Notification.
        public virtual bool DeleteNotification(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralNotoficationId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("GeneralNotoficationId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteGeneralNotification @GeneralNotoficationId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        public virtual GeneralNotificationListModel GetActiveNotificationList()
        {
            GeneralNotificationListModel listModel = new GeneralNotificationListModel();

            DateTime todaysDate = DateTime.Now.Date;
            listModel.GeneralNotificationList = (from a in _generalNotificationMasterRepository.Table
                                                 where a.IsActive && todaysDate >= a.FromDate && todaysDate <= a.UptoDate
                                                 select new GeneralNotificationModel
                                                 {
                                                     GeneralNotificationId = a.GeneralNotificationId,
                                                     NotificationDetails = a.NotificationDetails,
                                                     FromDate = a.FromDate,
                                                     UptoDate = a.UptoDate,
                                                     IsActive = a.IsActive
                                                 })?.ToList();

            return listModel;
        }
        #region Protected Method
        // Check if Notification code is already present or not.
        protected virtual bool IsNotificationCodeAlreadyExist(string NotificationDetails, long generalNotificationId = 0)
         => _generalNotificationMasterRepository.Table.Any(x => x.NotificationDetails == NotificationDetails && (x.GeneralNotificationId != generalNotificationId || generalNotificationId == 0));
        #endregion
    }
}