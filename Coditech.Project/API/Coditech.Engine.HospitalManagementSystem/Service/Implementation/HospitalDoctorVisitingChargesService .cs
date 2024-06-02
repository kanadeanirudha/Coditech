
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
    public class HospitalDoctorVisitingChargesService : IHospitalDoctorVisitingChargesService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<HospitalDoctorVisitingCharges> _hospitalDoctorVisitingChargesMasterRepository;
        public HospitalDoctorVisitingChargesService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _hospitalDoctorVisitingChargesMasterRepository = new CoditechRepository<HospitalDoctorVisitingCharges>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual HospitalDoctorVisitingChargesListModel GetHospitalDoctorVisitingChargesList(string selectedCentreCode, short selectedDepartmentId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<HospitalDoctorVisitingChargesModel> objStoredProc = new CoditechViewRepository<HospitalDoctorVisitingChargesModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@DepartmentId", selectedDepartmentId, ParameterDirection.Input, DbType.Int16);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<HospitalDoctorVisitingChargesModel> HospitalDoctorVisitingChargesList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetHospitalDoctorVisitingChargesList @CentreCode,@DepartmentId,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();
            HospitalDoctorVisitingChargesListModel listModel = new HospitalDoctorVisitingChargesListModel();

            listModel.HospitalDoctorVisitingChargesList = HospitalDoctorVisitingChargesList?.Count > 0 ? HospitalDoctorVisitingChargesList : new List<HospitalDoctorVisitingChargesModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        public virtual HospitalDoctorVisitingChargesListModel GetHospitalDoctorVisitingChargesByDoctorIdList(int hospitalDoctorId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<HospitalDoctorVisitingChargesModel> objStoredProc = new CoditechViewRepository<HospitalDoctorVisitingChargesModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@HospitalDoctorId", hospitalDoctorId, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<HospitalDoctorVisitingChargesModel> HospitalDoctorVisitingChargesList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetHospitalDoctorVisitingChargesListByDoctorId @HospitalDoctorId,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
            HospitalDoctorVisitingChargesListModel listModel = new HospitalDoctorVisitingChargesListModel();

            listModel.HospitalDoctorVisitingChargesList = HospitalDoctorVisitingChargesList?.Count > 0 ? HospitalDoctorVisitingChargesList : new List<HospitalDoctorVisitingChargesModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create HospitalDoctorVisitingCharges.
        public virtual HospitalDoctorVisitingChargesModel CreateHospitalDoctorVisitingCharges(HospitalDoctorVisitingChargesModel hospitalDoctorVisitingChargesModel)
        {
            if (IsNull(hospitalDoctorVisitingChargesModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            //if (IsHospitalDoctorVisitingChargesCodeAlreadyExist(hospitalDoctorVisitingChargesModel.HospitalDoctorId))
            //    throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Hospital Doctor"));

           HospitalDoctorVisitingCharges hospitalDoctorVisitingCharges = hospitalDoctorVisitingChargesModel.FromModelToEntity<HospitalDoctorVisitingCharges>();

            //Create new HospitalDoctorVisitingCharges and return it.
           HospitalDoctorVisitingCharges hospitaldoctorvisitingchargesData = _hospitalDoctorVisitingChargesMasterRepository.Insert(hospitalDoctorVisitingCharges);
            if (hospitaldoctorvisitingchargesData?.HospitalDoctorVisitingChargesId > 0)
            {
                hospitalDoctorVisitingChargesModel.HospitalDoctorVisitingChargesId = hospitaldoctorvisitingchargesData.HospitalDoctorVisitingChargesId;
            }
            else
            {
                hospitalDoctorVisitingChargesModel.HasError = true;
                hospitalDoctorVisitingChargesModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return hospitalDoctorVisitingChargesModel;
        }

        //Get HospitalDoctorVisitingCharges by HospitalDoctorVisitingCharges id.
        public virtual HospitalDoctorVisitingChargesModel GetHospitalDoctorVisitingCharges(long hospitaldoctorvisitingchargesId,int hospitalDoctorId)
        {
            if (hospitaldoctorvisitingchargesId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalDoctorVisitingChargesID"));

            //Get the HospitalDoctorVisitingCharges Details based on id.
           HospitalDoctorVisitingCharges hospitalDoctorVisitingCharges = _hospitalDoctorVisitingChargesMasterRepository.Table.FirstOrDefault(x => x.HospitalDoctorVisitingChargesId == hospitaldoctorvisitingchargesId);
            HospitalDoctorVisitingChargesModel hospitalDoctorVisitingChargesModel = hospitalDoctorVisitingCharges?.FromEntityToModel<HospitalDoctorVisitingChargesModel>();
            return hospitalDoctorVisitingChargesModel;
        }

        //Update HospitalDoctorVisitingCharges.
        public virtual bool UpdateHospitalDoctorVisitingCharges(HospitalDoctorVisitingChargesModel hospitalDoctorVisitingChargesModel)
        {
            if (IsNull(hospitalDoctorVisitingChargesModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (hospitalDoctorVisitingChargesModel.HospitalDoctorVisitingChargesId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalDoctorVisitingChargesID"));

            //if (IsHospitalDoctorVisitingChargesCodeAlreadyExist(hospitalDoctorVisitingChargesModel.HospitalDoctorId, hospitalDoctorVisitingChargesModel.HospitalDoctorVisitingChargesId))
            //    throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "HospitalDoctorVisitingCharges Code"));

           HospitalDoctorVisitingCharges hospitalDoctorVisitingCharges = hospitalDoctorVisitingChargesModel.FromModelToEntity<HospitalDoctorVisitingCharges>();

            //Update HospitalDoctorVisitingCharges
            bool isHospitalDoctorVisitingChargesUpdated = _hospitalDoctorVisitingChargesMasterRepository.Update(hospitalDoctorVisitingCharges);
            if (!isHospitalDoctorVisitingChargesUpdated)
            {
                hospitalDoctorVisitingChargesModel.HasError = true;
                hospitalDoctorVisitingChargesModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isHospitalDoctorVisitingChargesUpdated;
        }

        //Delete HospitalDoctorVisitingCharges.
        public virtual bool DeleteHospitalDoctorVisitingCharges(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalDoctorVisitingChargesID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("HospitalDoctorVisitingChargesId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteHospitalDoctorVisitingCharges @HospitalDoctorVisitingChargesId,  @Status OUT", 1, out status);

			return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if HospitalDoctorVisitingCharges code is already present or not.
        //protected virtual bool IsHospitalDoctorVisitingChargesCodeAlreadyExist(string hospitalDoctorId, short HospitalDoctorVisitingChargesId = 0)
        // => _hospitalDoctorVisitingChargesMasterRepository.Table.Any(x => x.HospitalDoctorId == hospitalDoctorId && (x.HospitalDoctorVisitingChargesId !=hospitalDoctorVisitingChargesId ||hospitalDoctorVisitingChargesId == 0));
        #endregion
    }
}
