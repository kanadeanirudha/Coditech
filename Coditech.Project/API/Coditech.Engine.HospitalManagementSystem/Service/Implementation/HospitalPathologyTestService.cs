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
    public class HospitalPathologyTestService : IHospitalPathologyTestService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<HospitalPathologyTest> _hospitalPathologyTestRepository;
        public HospitalPathologyTestService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _hospitalPathologyTestRepository = new CoditechRepository<HospitalPathologyTest>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual HospitalPathologyTestListModel GetHospitalPathologyTestList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<HospitalPathologyTestModel> objStoredProc = new CoditechViewRepository<HospitalPathologyTestModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<HospitalPathologyTestModel> hospitalPathologyTestList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetHospitalPathologyTestList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            HospitalPathologyTestListModel listModel = new HospitalPathologyTestListModel();

            listModel.HospitalPathologyTestList = hospitalPathologyTestList?.Count > 0 ? hospitalPathologyTestList : new List<HospitalPathologyTestModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create HospitalPathologyTest.
        public virtual HospitalPathologyTestModel CreateHospitalPathologyTest(HospitalPathologyTestModel hospitalPathologyTestModel)
        {
            if (IsNull(hospitalPathologyTestModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            HospitalPathologyTest hospitalPathologyTest = hospitalPathologyTestModel.FromModelToEntity<HospitalPathologyTest>();

            //Create new HospitalPathologyTest and return it.
            HospitalPathologyTest hospitalPathologyTestData = _hospitalPathologyTestRepository.Insert(hospitalPathologyTest);
            if (hospitalPathologyTestData?.HospitalPathologyTestId > 0)
            {
                hospitalPathologyTestModel.HospitalPathologyTestId = hospitalPathologyTestData.HospitalPathologyTestId;
            }
            else
            {
                hospitalPathologyTestModel.HasError = true;
                hospitalPathologyTestModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return hospitalPathologyTestModel;
        }

        //Get HospitalPathologyTest by HospitalPathologyTest id.
        public virtual HospitalPathologyTestModel GetHospitalPathologyTest(long hospitalPathologyTestId)
        {
            if (hospitalPathologyTestId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalPathologyTestID"));

            //Get the HospitalPathologyTest Details based on id.
            HospitalPathologyTest hospitalPathologyTest = _hospitalPathologyTestRepository.Table.FirstOrDefault(x => x.HospitalPathologyTestId == hospitalPathologyTestId);
            HospitalPathologyTestModel hospitalPathologyTestModel = hospitalPathologyTest?.FromEntityToModel<HospitalPathologyTestModel>();
            return hospitalPathologyTestModel;
        }

        //Update HospitalPathologyTest.
        public virtual bool UpdateHospitalPathologyTest(HospitalPathologyTestModel hospitalPathologyTestModel)
        {
            if (IsNull(hospitalPathologyTestModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (hospitalPathologyTestModel.HospitalPathologyTestId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalPathologyTestID"));

            HospitalPathologyTest hospitalPathologyTest = hospitalPathologyTestModel.FromModelToEntity<HospitalPathologyTest>();

            //Update HospitalPathologyTest
            bool isHospitalPathologyTestUpdated = _hospitalPathologyTestRepository.Update(hospitalPathologyTest);
            if (!isHospitalPathologyTestUpdated)
            {
                hospitalPathologyTestModel.HasError = true;
                hospitalPathologyTestModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isHospitalPathologyTestUpdated;
        }

        //Delete HospitalPathologyTest.
        public virtual bool DeleteHospitalPathologyTest(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalPathologyTestID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("HospitalPathologyTestId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteHospitalPathologyTest @HospitalPathologyTestId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        #endregion
    }
}
