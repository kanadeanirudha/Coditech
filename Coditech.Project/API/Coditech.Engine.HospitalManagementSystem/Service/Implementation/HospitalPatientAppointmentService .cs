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
    public class HospitalPatientAppointmentService : IHospitalPatientAppointmentService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<HospitalPatientAppointment> _hospitalPatientAppointmentRepository;
        public HospitalPatientAppointmentService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _hospitalPatientAppointmentRepository = new CoditechRepository<HospitalPatientAppointment>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual HospitalPatientAppointmentListModel GetHospitalPatientAppointmentList(string selectedCentreCode, short selectedDepartmentId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<HospitalPatientAppointmentModel> objStoredProc = new CoditechViewRepository<HospitalPatientAppointmentModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@DepartmentId", selectedDepartmentId, ParameterDirection.Input, DbType.Int16);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<HospitalPatientAppointmentModel> hospitalPatientAppointmentList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetHospitalPatientAppointmentList @CentreCode,@DepartmentId,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();
            HospitalPatientAppointmentListModel listModel = new HospitalPatientAppointmentListModel();

            listModel.HospitalPatientAppointmentList = hospitalPatientAppointmentList?.Count > 0 ? hospitalPatientAppointmentList : new List<HospitalPatientAppointmentModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create HospitalPatientAppointment.
        public virtual HospitalPatientAppointmentModel CreateHospitalPatientAppointment(HospitalPatientAppointmentModel hospitalPatientAppointmentModel)
        {
            if (IsNull(hospitalPatientAppointmentModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            HospitalPatientAppointment hospitalPatientAppointment = hospitalPatientAppointmentModel.FromModelToEntity<HospitalPatientAppointment>();

            //Create new HospitalPatientAppointment and return it.
            HospitalPatientAppointment hospitalPatientAppointmentData = _hospitalPatientAppointmentRepository.Insert(hospitalPatientAppointment);
            if (hospitalPatientAppointmentData?.HospitalPatientAppointmentId > 0)
            {
                hospitalPatientAppointmentModel.HospitalPatientAppointmentId = hospitalPatientAppointmentData.HospitalPatientAppointmentId;
            }
            else
            {
                hospitalPatientAppointmentModel.HasError = true;
                hospitalPatientAppointmentModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return hospitalPatientAppointmentModel;
        }

        //Get HospitalPatientAppointment by hospitalPatientAppointment Id.
        public virtual HospitalPatientAppointmentModel GetHospitalPatientAppointment(long hospitalPatientAppointmentId)
        {
            if (hospitalPatientAppointmentId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalPatientAppointmentId"));

            //Get the HospitalPatientAppointment Details based on id.
            HospitalPatientAppointment hospitalPatientAppointment = _hospitalPatientAppointmentRepository.Table.FirstOrDefault(x => x.HospitalPatientAppointmentId == hospitalPatientAppointmentId);
            HospitalPatientAppointmentModel hospitalPatientAppointmentModel = hospitalPatientAppointment?.FromEntityToModel<HospitalPatientAppointmentModel>();
            return hospitalPatientAppointmentModel;
        }

        //Update HospitalPatientAppointment.
        public virtual bool UpdateHospitalPatientAppointment(HospitalPatientAppointmentModel hospitalPatientAppointmentModel)
        {
            if (IsNull(hospitalPatientAppointmentModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (hospitalPatientAppointmentModel.HospitalPatientAppointmentId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalPatientAppointmentID"));

            HospitalPatientAppointment hospitalPatientAppointment = hospitalPatientAppointmentModel.FromModelToEntity<HospitalPatientAppointment>();

            //Update HospitalPatientAppointment
            bool isHospitalPatientAppointmentUpdated = _hospitalPatientAppointmentRepository.Update(hospitalPatientAppointment);
            if (!isHospitalPatientAppointmentUpdated)
            {
                hospitalPatientAppointmentModel.HasError = true;
                hospitalPatientAppointmentModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isHospitalPatientAppointmentUpdated;
        }

        //Delete HospitalPatientAppointment.
        public virtual bool DeleteHospitalPatientAppointment(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalPatientAppointmentID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("HospitalPatientAppointmentId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteHospitalPatientAppointment @HospitalPatientAppointmentId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }
    }
}
