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
    public class DBTMTestMasterService : IDBTMTestMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<DBTMTestMaster> _dBTMTestMasterRepository;
        public DBTMTestMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _dBTMTestMasterRepository = new CoditechRepository<DBTMTestMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual DBTMTestListModel GetDBTMTestList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<DBTMTestModel> objStoredProc = new CoditechViewRepository<DBTMTestModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<DBTMTestModel> dBTMTestList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetDBTMTestList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            DBTMTestListModel listModel = new DBTMTestListModel();

            listModel.DBTMTestList = dBTMTestList?.Count > 0 ? dBTMTestList : new List<DBTMTestModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create DBTMTest.
        public virtual DBTMTestModel CreateDBTMTest(DBTMTestModel dBTMTestModel)
        {
            if (IsNull(dBTMTestModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            DBTMTestMaster dBTMTestMaster = dBTMTestModel.FromModelToEntity<DBTMTestMaster>();

            //Create new DBTMTest and return it.
            DBTMTestMaster dBTMTestData = _dBTMTestMasterRepository.Insert(dBTMTestMaster);
            if (dBTMTestData?.DBTMTestMasterId > 0)
            {
                dBTMTestModel.DBTMTestMasterId = dBTMTestData.DBTMTestMasterId;
            }
            else
            {
                dBTMTestModel.HasError = true;
                dBTMTestModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return dBTMTestModel;
        }

        //Get DBTMTest by dBTMTestMasterId.
        public virtual DBTMTestModel GetDBTMTest(int dBTMTestMasterId)
        {
            if (dBTMTestMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DBTMTestMasterId"));

            //Get the DBTMTest Details based on id.
            DBTMTestMaster dBTMTestMaster = _dBTMTestMasterRepository.Table.Where(x => x.DBTMTestMasterId == dBTMTestMasterId)?.FirstOrDefault();
            DBTMTestModel dBTMTestModel = dBTMTestMaster?.FromEntityToModel<DBTMTestModel>();
            return dBTMTestModel;
        }

        //Update DBTMTest.
        public virtual bool UpdateDBTMTest(DBTMTestModel dBTMTestModel)
        {
            if (IsNull(dBTMTestModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (dBTMTestModel.DBTMTestMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DBTMTestMasterID"));

            DBTMTestMaster dBTMTestMaster = dBTMTestModel.FromModelToEntity<DBTMTestMaster>();

            //Update DBTMTest
            bool isdBTMTestUpdated = _dBTMTestMasterRepository.Update(dBTMTestMaster);
            if (!isdBTMTestUpdated)
            {
                dBTMTestModel.HasError = true;
                dBTMTestModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isdBTMTestUpdated;
        }

        //Delete DBTMTest.
        public virtual bool DeleteDBTMTest(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DBTMTestMasterId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("DBTMTestMasterId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteDBTMTest @DBTMTestMasterId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        
        #endregion
    }
}
