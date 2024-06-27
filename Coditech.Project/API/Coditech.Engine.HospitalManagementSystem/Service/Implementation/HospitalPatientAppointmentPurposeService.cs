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
    public class HospitalPatientAppointmentPurposeService : IHospitalPatientAppointmentPurposeService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<HospitalPatientAppointmentPurpose> _hospitalPatientAppointmentPurposeRepository;
        public HospitalPatientAppointmentPurposeService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _hospitalPatientAppointmentPurposeRepository = new CoditechRepository<HospitalPatientAppointmentPurpose>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual HospitalPatientAppointmentPurposeListModel GetHospitalPatientAppointmentPurposeList( FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<HospitalPatientAppointmentPurposeModel> objStoredProc = new CoditechViewRepository<HospitalPatientAppointmentPurposeModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<HospitalPatientAppointmentPurposeModel> hospitalPatientAppointmentPurposeList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetHospitalPatientAppointmentPurposeList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();
            HospitalPatientAppointmentPurposeListModel listModel = new HospitalPatientAppointmentPurposeListModel();

            listModel.HospitalPatientAppointmentPurposeList = hospitalPatientAppointmentPurposeList?.Count > 0 ? hospitalPatientAppointmentPurposeList : new List<HospitalPatientAppointmentPurposeModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create HospitalPatientAppointmentPurpose.
        public virtual HospitalPatientAppointmentPurposeModel CreateHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeModel hospitalPatientAppointmentPurposeModel)
        {
            if (IsNull(hospitalPatientAppointmentPurposeModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            HospitalPatientAppointmentPurpose hospitalPatientAppointmentPurpose = hospitalPatientAppointmentPurposeModel.FromModelToEntity<HospitalPatientAppointmentPurpose>();

            //Create new HospitalPatientAppointmentPurpose and return it.
            HospitalPatientAppointmentPurpose hospitalPatientAppointmentPurposeData = _hospitalPatientAppointmentPurposeRepository.Insert(hospitalPatientAppointmentPurpose);
            if (hospitalPatientAppointmentPurposeData?.HospitalPatientAppointmentPurposeId > 0)
            {
                hospitalPatientAppointmentPurposeModel.HospitalPatientAppointmentPurposeId = hospitalPatientAppointmentPurposeData.HospitalPatientAppointmentPurposeId;
            }
            else
            {
                hospitalPatientAppointmentPurposeModel.HasError = true;
                hospitalPatientAppointmentPurposeModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return hospitalPatientAppointmentPurposeModel;
        }

        //Get HospitalPatientAppointmentPurpose by hospitalPatientAppointmentPurpose Id.
        public virtual HospitalPatientAppointmentPurposeModel GetHospitalPatientAppointmentPurpose(short hospitalPatientAppointmentPurposeId)
        {
            if (hospitalPatientAppointmentPurposeId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalPatientAppointmentPurposeId"));

            //Get the HospitalPatientAppointmentPurpose Details based on id.
            HospitalPatientAppointmentPurpose hospitalPatientAppointmentPurpose = _hospitalPatientAppointmentPurposeRepository.Table.FirstOrDefault(x => x.HospitalPatientAppointmentPurposeId == hospitalPatientAppointmentPurposeId);
            HospitalPatientAppointmentPurposeModel hospitalPatientAppointmentPurposeModel = hospitalPatientAppointmentPurpose?.FromEntityToModel<HospitalPatientAppointmentPurposeModel>();
            return hospitalPatientAppointmentPurposeModel;
        }

        //Update HospitalPatientAppointmentPurpose.
        public virtual bool UpdateHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeModel hospitalPatientAppointmentPurposeModel)
        {
            if (IsNull(hospitalPatientAppointmentPurposeModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (hospitalPatientAppointmentPurposeModel.HospitalPatientAppointmentPurposeId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalPatientAppointmentPurposeID"));

            HospitalPatientAppointmentPurpose hospitalPatientAppointmentPurpose = hospitalPatientAppointmentPurposeModel.FromModelToEntity<HospitalPatientAppointmentPurpose>();

            //Update HospitalPatientAppointmentPurpose
            bool isHospitalPatientAppointmentPurposeUpdated = _hospitalPatientAppointmentPurposeRepository.Update(hospitalPatientAppointmentPurpose);
            if (!isHospitalPatientAppointmentPurposeUpdated)
            {
                hospitalPatientAppointmentPurposeModel.HasError = true;
                hospitalPatientAppointmentPurposeModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isHospitalPatientAppointmentPurposeUpdated;
        }

        //Delete HospitalPatientAppointmentPurpose.
        public virtual bool DeleteHospitalPatientAppointmentPurpose(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalPatientAppointmentPurposeID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("HospitalPatientAppointmentPurposeId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteHospitalPatientAppointmentPurpose @HospitalPatientAppointmentPurposeId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }
    }
}