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
    public class DBTMSubscriptionPlanService : BaseService, IDBTMSubscriptionPlanService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<DBTMSubscriptionPlan> _dBTMSubscriptionPlanRepository;
        public DBTMSubscriptionPlanService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _dBTMSubscriptionPlanRepository = new CoditechRepository<DBTMSubscriptionPlan>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual DBTMSubscriptionPlanListModel GetDBTMSubscriptionPlanList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<DBTMSubscriptionPlanModel> objStoredProc = new CoditechViewRepository<DBTMSubscriptionPlanModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<DBTMSubscriptionPlanModel> dBTMSubscriptionPlanList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetDBTMSubscriptionPlanList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            DBTMSubscriptionPlanListModel listModel = new DBTMSubscriptionPlanListModel();

            listModel.DBTMSubscriptionPlanList = dBTMSubscriptionPlanList?.Count > 0 ? dBTMSubscriptionPlanList : new List<DBTMSubscriptionPlanModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create DBTMSubscriptionPlan.
        public virtual DBTMSubscriptionPlanModel CreateDBTMSubscriptionPlan(DBTMSubscriptionPlanModel dBTMSubscriptionPlanModel)
        {
            if (IsNull(dBTMSubscriptionPlanModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            DBTMSubscriptionPlan dBTMSubscriptionPlan = dBTMSubscriptionPlanModel.FromModelToEntity<DBTMSubscriptionPlan>();

            //Create new DBTMSubscriptionPlan and return it.
            DBTMSubscriptionPlan dBTMSubscriptionPlanData = _dBTMSubscriptionPlanRepository.Insert(dBTMSubscriptionPlan);
            if (dBTMSubscriptionPlanData?.DBTMSubscriptionPlanId > 0)
            {
                dBTMSubscriptionPlanModel.DBTMSubscriptionPlanId = dBTMSubscriptionPlan.DBTMSubscriptionPlanId;
            }
            else
            {
                dBTMSubscriptionPlanModel.HasError = true;
                dBTMSubscriptionPlanModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return dBTMSubscriptionPlanModel;
        }

        //Get DBTMSubscriptionPlan by dBTMSubscriptionPlan id.
        public virtual DBTMSubscriptionPlanModel GetDBTMSubscriptionPlan(int dBTMSubscriptionPlanId)
        {
            if (dBTMSubscriptionPlanId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DBTMSubscriptionPlanId"));

            //Get the DBTMSubscriptionPlan Details based on id.
            DBTMSubscriptionPlan dBTMSubscriptionPlan = _dBTMSubscriptionPlanRepository.Table.Where(x => x.DBTMSubscriptionPlanId == dBTMSubscriptionPlanId)?.FirstOrDefault();
            DBTMSubscriptionPlanModel dBTMSubscriptionPlanModel = dBTMSubscriptionPlan?.FromEntityToModel<DBTMSubscriptionPlanModel>();
            return dBTMSubscriptionPlanModel;
        }

        //Update DBTMSubscriptionPlan.
        public virtual bool UpdateDBTMSubscriptionPlan(DBTMSubscriptionPlanModel dBTMSubscriptionPlanModel)
        {
            if (IsNull(dBTMSubscriptionPlanModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (dBTMSubscriptionPlanModel.DBTMSubscriptionPlanId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DBTMSubscriptionPlanId"));

            DBTMSubscriptionPlan dBTMSubscriptionPlan = dBTMSubscriptionPlanModel.FromModelToEntity<DBTMSubscriptionPlan>();

            //Update DBTMSubscriptionPlan
            bool isDBTMSubscriptionPlanUpdated = _dBTMSubscriptionPlanRepository.Update(dBTMSubscriptionPlan);
            if (!isDBTMSubscriptionPlanUpdated)
            {
                dBTMSubscriptionPlanModel.HasError = true;
                dBTMSubscriptionPlanModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isDBTMSubscriptionPlanUpdated;
        }

        //Delete DBTMSubscriptionPlan.
        public virtual bool DeleteDBTMSubscriptionPlan(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DBTMSubscriptionPlanId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("DBTMSubscriptionPlanId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteDBTMSubscriptionPlan @DBTMSubscriptionPlanId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

      
    }
}
