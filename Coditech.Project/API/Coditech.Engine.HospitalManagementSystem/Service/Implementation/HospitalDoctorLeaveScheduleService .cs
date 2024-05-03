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
    public class HospitalDoctorLeaveScheduleService : IHospitalDoctorLeaveScheduleService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<HospitalDoctorLeaveSchedule> _hospitalDoctorLeaveScheduleRepository;
        public HospitalDoctorLeaveScheduleService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _hospitalDoctorLeaveScheduleRepository = new CoditechRepository<HospitalDoctorLeaveSchedule>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual HospitalDoctorLeaveScheduleListModel GetHospitalDoctorLeaveScheduleList(string selectedCentreCode, short selectedDepartmentId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<HospitalDoctorLeaveScheduleModel> objStoredProc = new CoditechViewRepository<HospitalDoctorLeaveScheduleModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@DepartmentId", selectedDepartmentId, ParameterDirection.Input, DbType.Int16);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<HospitalDoctorLeaveScheduleModel> hospitalDoctorLeaveScheduleList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetHospitalDoctorLeaveScheduleList @CentreCode,@DepartmentId,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();
            HospitalDoctorLeaveScheduleListModel listModel = new HospitalDoctorLeaveScheduleListModel();

            listModel.HospitalDoctorLeaveScheduleList = hospitalDoctorLeaveScheduleList?.Count > 0 ? hospitalDoctorLeaveScheduleList : new List<HospitalDoctorLeaveScheduleModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create HospitalDoctorLeaveSchedule.
        public virtual HospitalDoctorLeaveScheduleModel CreateHospitalDoctorLeaveSchedule(HospitalDoctorLeaveScheduleModel hospitalDoctorLeaveScheduleModel)
        {
            if (IsNull(hospitalDoctorLeaveScheduleModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            //if (IsRoomNameAlreadyExist(hospitalDoctorLeaveScheduleModel.LeaveDate))
            //    throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Leave Date"));

            HospitalDoctorLeaveSchedule hospitalDoctorLeaveSchedule = hospitalDoctorLeaveScheduleModel.FromModelToEntity<HospitalDoctorLeaveSchedule>();

            //Create new HospitalDoctorLeaveSchedule and return it.
            HospitalDoctorLeaveSchedule hospitalDoctorLeaveScheduleData = _hospitalDoctorLeaveScheduleRepository.Insert(hospitalDoctorLeaveSchedule);
            if (hospitalDoctorLeaveScheduleData?.HospitalDoctorLeaveScheduleId > 0)
            {
                hospitalDoctorLeaveScheduleModel.HospitalDoctorLeaveScheduleId = hospitalDoctorLeaveScheduleData.HospitalDoctorLeaveScheduleId;
            }
            else
            {
                hospitalDoctorLeaveScheduleModel.HasError = true;
                hospitalDoctorLeaveScheduleModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return hospitalDoctorLeaveScheduleModel;
        }

        //Get HospitalDoctorLeaveSchedule by hospitalDoctorLeaveSchedule Id.
        public virtual HospitalDoctorLeaveScheduleModel GetHospitalDoctorLeaveSchedule(long hospitalDoctorLeaveScheduleId)
        {
            if (hospitalDoctorLeaveScheduleId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalDoctorLeaveScheduleId"));

            //Get the HospitalDoctorAllocatedOPDRoom Details based on id.
            HospitalDoctorLeaveSchedule hospitalDoctorLeaveSchedule = _hospitalDoctorLeaveScheduleRepository.Table.FirstOrDefault(x => x.HospitalDoctorLeaveScheduleId == hospitalDoctorLeaveScheduleId);
            HospitalDoctorLeaveScheduleModel hospitalDoctorLeaveScheduleModel = hospitalDoctorLeaveSchedule?.FromEntityToModel<HospitalDoctorLeaveScheduleModel>();
            return hospitalDoctorLeaveScheduleModel;
        }

        //Update HospitalDoctorLeaveSchedule.
        public virtual bool UpdateHospitalDoctorLeaveSchedule(HospitalDoctorLeaveScheduleModel hospitalDoctorLeaveScheduleModel)
        {
            if (IsNull(hospitalDoctorLeaveScheduleModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (hospitalDoctorLeaveScheduleModel.HospitalDoctorLeaveScheduleId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalDoctorLeaveScheduleID"));

            HospitalDoctorLeaveSchedule hospitalDoctorLeaveSchedule = hospitalDoctorLeaveScheduleModel.FromModelToEntity<HospitalDoctorLeaveSchedule>();

            //Update HospitalDoctorLeaveSchedule
            bool isHospitalDoctorLeaveScheduleUpdated = _hospitalDoctorLeaveScheduleRepository.Update(hospitalDoctorLeaveSchedule);
            if (!isHospitalDoctorLeaveScheduleUpdated)
            {
                hospitalDoctorLeaveScheduleModel.HasError = true;
                hospitalDoctorLeaveScheduleModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isHospitalDoctorLeaveScheduleUpdated;
        }

        //Delete HospitalDoctorLeaveSchedule.
        public virtual bool DeleteHospitalDoctorLeaveSchedule(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalDoctorLeaveScheduleID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("HospitalDoctorLeaveScheduleId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteHospitalDoctorLeaveSchedule @HospitalDoctorLeaveScheduleId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }
    }
}
