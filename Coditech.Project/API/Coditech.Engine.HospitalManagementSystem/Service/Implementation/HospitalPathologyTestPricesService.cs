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
    public class HospitalPathologyTestPricesService : IHospitalPathologyTestPricesService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<HospitalPathologyTestPrices> _hospitalPathologyTestPricesRepository;
        public HospitalPathologyTestPricesService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) 
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _hospitalPathologyTestPricesRepository = new CoditechRepository<HospitalPathologyTestPrices>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual HospitalPathologyTestPricesListModel GetHospitalPathologyTestPricesList(int hospitalPathologyPriceCategoryEnumId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<HospitalPathologyTestPricesModel> objStoredProc = new CoditechViewRepository<HospitalPathologyTestPricesModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@HospitalPathologyPriceCategoryEnumId", hospitalPathologyPriceCategoryEnumId, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<HospitalPathologyTestPricesModel> hospitalPathologyTestPricesList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetHospitalPathologyTestPricesList @HospitalPathologyPriceCategoryEnumId, @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            HospitalPathologyTestPricesListModel listModel = new HospitalPathologyTestPricesListModel();

            listModel.HospitalPathologyTestPricesList = hospitalPathologyTestPricesList?.Count > 0 ? hospitalPathologyTestPricesList : new List<HospitalPathologyTestPricesModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create HospitalPathologyTestPrices.
        public virtual HospitalPathologyTestPricesModel CreateHospitalPathologyTestPrices(HospitalPathologyTestPricesModel hospitalPathologyTestPricesModel)
        {
            if (IsNull(hospitalPathologyTestPricesModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            HospitalPathologyTestPrices hospitalPathologyTestPrices = hospitalPathologyTestPricesModel.FromModelToEntity<HospitalPathologyTestPrices>();

            //Create new HospitalPathologyTestPrices and return it.
            HospitalPathologyTestPrices hospitalPathologyTestPricesData = _hospitalPathologyTestPricesRepository.Insert(hospitalPathologyTestPrices);
            if (hospitalPathologyTestPricesData?.HospitalPathologyTestPricesId > 0)
            {
                hospitalPathologyTestPricesModel.HospitalPathologyTestPricesId = hospitalPathologyTestPricesData.HospitalPathologyTestPricesId;
            }
            else
            {
                hospitalPathologyTestPricesModel.HasError = true;
                hospitalPathologyTestPricesModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return hospitalPathologyTestPricesModel;
        }

        //Get HospitalPathologyTestPrices by HospitalPathologyTestPrices id.
        public virtual HospitalPathologyTestPricesModel GetHospitalPathologyTestPrices(long hospitalPathologyTestPricesId)
        {
            if (hospitalPathologyTestPricesId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalPathologyTestPricesID"));

            //Get the HospitalPathologyTestPrices Details based on id.
            HospitalPathologyTestPrices hospitalPathologyTestPrices = _hospitalPathologyTestPricesRepository.Table.FirstOrDefault(x => x.HospitalPathologyTestPricesId == hospitalPathologyTestPricesId);
            HospitalPathologyTestPricesModel hospitalPathologyTestPricesModel = hospitalPathologyTestPrices?.FromEntityToModel<HospitalPathologyTestPricesModel>();
            return hospitalPathologyTestPricesModel;
        }

        //Update HospitalPathologyTestPrices.
        public virtual bool UpdateHospitalPathologyTestPrices(HospitalPathologyTestPricesModel hospitalPathologyTestPricesModel)
        {
            if (IsNull(hospitalPathologyTestPricesModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (hospitalPathologyTestPricesModel.HospitalPathologyTestPricesId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalPathologyTestPricesID"));

            HospitalPathologyTestPrices hospitalPathologyTestPrices = hospitalPathologyTestPricesModel.FromModelToEntity<HospitalPathologyTestPrices>();

            //Update HospitalPathologyTestPrices
            bool isHospitalPathologyTestPricesUpdated = _hospitalPathologyTestPricesRepository.Update(hospitalPathologyTestPrices);
            if (!isHospitalPathologyTestPricesUpdated)
            {
                hospitalPathologyTestPricesModel.HasError = true;
                hospitalPathologyTestPricesModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isHospitalPathologyTestPricesUpdated;
        }

        //Delete HospitalPathologyTestPrices.
        public virtual bool DeleteHospitalPathologyTestPrices(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalPathologyTestPricesID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("HospitalPathologyTestPricesId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteHospitalPathologyTestPrices @HospitalPathologyTestPricesId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        #endregion
    }
}
