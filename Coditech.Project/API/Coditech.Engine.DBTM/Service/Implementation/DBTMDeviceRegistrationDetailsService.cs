using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;

using System.Collections.Specialized;
using System.Data;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class DBTMDeviceRegistrationDetailsService : IDBTMDeviceRegistrationDetailsService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<DBTMDeviceRegistrationDetails> _dBTMDeviceRegistrationDetailsRepository;
        public DBTMDeviceRegistrationDetailsService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _dBTMDeviceRegistrationDetailsRepository = new CoditechRepository<DBTMDeviceRegistrationDetails>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual DBTMDeviceRegistrationDetailsListModel GetDBTMDeviceRegistrationDetailsList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<DBTMDeviceRegistrationDetailsModel> objStoredProc = new CoditechViewRepository<DBTMDeviceRegistrationDetailsModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<DBTMDeviceRegistrationDetailsModel> dBTMDeviceRegistrationDetailsList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetDBTMDeviceRegistrationDetailsList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            DBTMDeviceRegistrationDetailsListModel listModel = new DBTMDeviceRegistrationDetailsListModel();

            listModel.RegistrationDetailsList = dBTMDeviceRegistrationDetailsList?.Count > 0 ? dBTMDeviceRegistrationDetailsList : new List<DBTMDeviceRegistrationDetailsModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create DBTMDeviceRegistrationDetails.
        public virtual DBTMDeviceRegistrationDetailsModel CreateRegistrationDetails(DBTMDeviceRegistrationDetailsModel dBTMDeviceRegistrationDetailsModel)
        {
            if (IsNull(dBTMDeviceRegistrationDetailsModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            DBTMDeviceRegistrationDetails dBTMDeviceRegistrationDetails = dBTMDeviceRegistrationDetailsModel.FromModelToEntity<DBTMDeviceRegistrationDetails>();

            //Create new DBTMDeviceRegistrationDetails and return it.
            DBTMDeviceRegistrationDetails dBTMDeviceRegistrationDetailsData = _dBTMDeviceRegistrationDetailsRepository.Insert(dBTMDeviceRegistrationDetails);
            if (dBTMDeviceRegistrationDetailsData?.DBTMDeviceRegistrationDetailId > 0)
            {
                dBTMDeviceRegistrationDetailsModel.DBTMDeviceRegistrationDetailId = dBTMDeviceRegistrationDetailsData.DBTMDeviceRegistrationDetailId;
            }
            else
            {
                dBTMDeviceRegistrationDetailsModel.HasError = true;
                dBTMDeviceRegistrationDetailsModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return dBTMDeviceRegistrationDetailsModel;
        }

        //Get DBTMDeviceRegistrationDetails by dBTMDeviceRegistrationDetailId.
        public virtual DBTMDeviceRegistrationDetailsModel GetRegistrationDetails(long dBTMDeviceRegistrationDetailId)
        {
            if (dBTMDeviceRegistrationDetailId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DBTMDeviceRegistrationDetailId"));

            //Get the DBTMDeviceRegistrationDetails Details based on id.
            DBTMDeviceRegistrationDetails dBTMDeviceRegistrationDetails = _dBTMDeviceRegistrationDetailsRepository.Table.Where(x => x.DBTMDeviceRegistrationDetailId == dBTMDeviceRegistrationDetailId)?.FirstOrDefault();
            DBTMDeviceRegistrationDetailsModel dBTMDeviceRegistrationDetailsModel = dBTMDeviceRegistrationDetails?.FromEntityToModel<DBTMDeviceRegistrationDetailsModel>();
            return dBTMDeviceRegistrationDetailsModel;
        }

        //Update DBTMDeviceRegistrationDetails.
        public virtual bool UpdateRegistrationDetails(DBTMDeviceRegistrationDetailsModel dBTMDeviceRegistrationDetailsModel)
        {
            if (IsNull(dBTMDeviceRegistrationDetailsModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (dBTMDeviceRegistrationDetailsModel.DBTMDeviceRegistrationDetailId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DBTMDeviceRegistrationDetailID"));

            DBTMDeviceRegistrationDetails dBTMDeviceRegistrationDetails = dBTMDeviceRegistrationDetailsModel.FromModelToEntity<DBTMDeviceRegistrationDetails>();

            //Update DBTMDeviceRegistrationDetails
            bool isdBTMDeviceRegistrationDetailsUpdated = _dBTMDeviceRegistrationDetailsRepository.Update(dBTMDeviceRegistrationDetails);
            if (!isdBTMDeviceRegistrationDetailsUpdated)
            {
                dBTMDeviceRegistrationDetailsModel.HasError = true;
                dBTMDeviceRegistrationDetailsModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isdBTMDeviceRegistrationDetailsUpdated;
        }

        //Delete DBTMDeviceRegistrationDetails.
        public virtual bool DeleteRegistrationDetails(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DBTMDeviceRegistrationDetailId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("DBTMDeviceRegistrationDetailId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteDBTMDeviceRegistrationDetails @DBTMDeviceRegistrationDetailId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        
        #endregion
    }
}
