using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;
using System.Collections.Specialized;
using System.Data;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class TicketMasterService : BaseService, ITicketMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<TicketMaster> _ticketMasterRepository;
        private readonly ICoditechRepository<TicketDetails> _ticketDetailsRepository;
        private readonly ICoditechRepository<UserMaster> _userMasterRepository;
        public TicketMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _ticketMasterRepository = new CoditechRepository<TicketMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _ticketDetailsRepository = new CoditechRepository<TicketDetails>(_serviceProvider.GetService<Coditech_Entities>());
            _userMasterRepository = new CoditechRepository<UserMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual TicketMasterListModel GetTicketMasterList(long userMasterId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<TicketMasterModel> objStoredProc = new CoditechViewRepository<TicketMasterModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@UserId", userMasterId, ParameterDirection.Input, DbType.Int64);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<TicketMasterModel> ticketMasterList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetTicketListByUserId @UserId,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
            TicketMasterListModel listModel = new TicketMasterListModel();

            listModel.TicketMasterList = ticketMasterList?.Count > 0 ? ticketMasterList : new List<TicketMasterModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create TicketMaster.
        public virtual TicketMasterModel CreateTicket(TicketMasterModel ticketMasterModel)
        {
            if (IsNull(ticketMasterModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);
            ticketMasterModel.TicketNumber = $"{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}";
            TicketMaster ticketMasterData = ticketMasterModel.FromModelToEntity<TicketMaster>();
            ticketMasterData.TicketStatusEnumId = GetEnumIdByEnumCode("Submitted", GeneralEnumaratorGroupCodeEnum.TicketStatus.ToString());
            //Create new TicketMaster and return it.
            ticketMasterData = _ticketMasterRepository.Insert(ticketMasterData);
            if (ticketMasterData?.TicketMasterId > 0)
            {
                ticketMasterModel.TicketMasterId = ticketMasterData.TicketMasterId;
                TicketDetails ticketDetailsData = new TicketDetails()
                {
                    TicketMasterId = ticketMasterData.TicketMasterId,
                    Details = ticketMasterModel.Details
                };
                ticketDetailsData = _ticketDetailsRepository.Insert(ticketDetailsData);
            }
            else
            {
                ticketMasterModel.HasError = true;
                ticketMasterModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return ticketMasterModel;
        }

        //Get TicketMaster by TicketMaster id.
        public virtual TicketMasterModel GetTicket(long ticketMasterId, long userMasterId)
        {
            if (ticketMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "ticketMasterId"));

            //Get the TicketMaster Details based on id.
            TicketMaster ticketMaster = _ticketMasterRepository.Table.FirstOrDefault(x => x.TicketMasterId == ticketMasterId);
            TicketMasterModel ticketMasterModel = ticketMaster?.FromEntityToModel<TicketMasterModel>();
            ticketMasterModel.TicketDetailsList = new List<TicketDetailsModel>();
            ticketMasterModel.TicketDetailsList = (from a in _ticketDetailsRepository.Table
                                                   join b in _userMasterRepository.Table on a.CreatedBy equals b.UserMasterId
                                                   where a.TicketMasterId == ticketMasterId
                                                   orderby a.CreatedDate descending
                                                   select new TicketDetailsModel()
                                                   {
                                                       Details = a.Details,
                                                       CreatedDate = Convert.ToDateTime(a.CreatedDate),
                                                       FirstName = b.FirstName,
                                                       LastName = b.LastName
                                                   })?.ToList();

            return ticketMasterModel;
        }

        //Update TicketMaster.
        public virtual bool UpdateTicket(TicketMasterModel ticketMasterModel)
        {
            if (IsNull(ticketMasterModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (ticketMasterModel.TicketMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TicketMasterID"));

            TicketMaster ticketMaster = _ticketMasterRepository.Table.Where(x => x.TicketMasterId == ticketMasterModel.TicketMasterId)?.FirstOrDefault();
            if (IsNull(ticketMaster))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);
            
            if (ticketMasterModel.IsTicketReplied)
            {
                ticketMaster.TicketStatusEnumId = ticketMasterModel.TicketStatusEnumId;
            }
            ticketMaster.TicketPriorityEnumId = ticketMasterModel.TicketPriorityEnumId;
            //Update TicketMaster
            bool isTicketUpdated = _ticketMasterRepository.Update(ticketMaster);
            if (!isTicketUpdated)
            {
                ticketMasterModel.HasError = true;
                ticketMasterModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            InsertTicketDetails(ticketMasterModel);
            return isTicketUpdated;
        }

        //Delete TicketMaster.
        public virtual bool DeleteTicket(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TicketMasterId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("TicketMasterId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteTicketMaster @TicketMasterId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        protected virtual void InsertTicketDetails(TicketMasterModel ticketMasterModel)
        {
            if (!string.IsNullOrEmpty(ticketMasterModel.Details))
            {
                TicketDetails ticketDetails = new TicketDetails
                {
                    TicketMasterId = ticketMasterModel.TicketMasterId,
                    Details = ticketMasterModel.Details,
                    CreatedBy = ticketMasterModel.UserId
                };

                _ticketDetailsRepository.Insert(ticketDetails);
            }
        }
        #endregion
    }
}
