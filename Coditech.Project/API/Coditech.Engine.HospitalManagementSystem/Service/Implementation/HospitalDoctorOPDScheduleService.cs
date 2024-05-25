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
    public class HospitalDoctorOPDScheduleService : BaseService, IHospitalDoctorOPDScheduleService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<HospitalDoctorAllocatedRoom> _hospitalDoctorOPDScheduleRepository;
        private readonly ICoditechRepository<OrganisationCentrewiseBuildingRooms> _organisationCentrewiseBuildingRoomsRepository;
        private readonly ICoditechRepository<HospitalDoctors> _hospitalDoctorsRepository;

        public HospitalDoctorOPDScheduleService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _hospitalDoctorOPDScheduleRepository = new CoditechRepository<HospitalDoctorAllocatedRoom>(_serviceProvider.GetService<Coditech_Entities>());
            _hospitalDoctorsRepository = new CoditechRepository<HospitalDoctors>(_serviceProvider.GetService<Coditech_Entities>());
            _organisationCentrewiseBuildingRoomsRepository = new CoditechRepository<OrganisationCentrewiseBuildingRooms>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual HospitalDoctorOPDScheduleListModel GetHospitalDoctorOPDScheduleList(string selectedCentreCode, short selectedDepartmentId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<HospitalDoctorOPDScheduleModel> objStoredProc = new CoditechViewRepository<HospitalDoctorOPDScheduleModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@DepartmentId", selectedDepartmentId, ParameterDirection.Input, DbType.Int16);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<HospitalDoctorOPDScheduleModel> hospitalDoctorList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetHospitalDoctorOPDScheduleList @CentreCode,@DepartmentId,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();
            HospitalDoctorOPDScheduleListModel listModel = new HospitalDoctorOPDScheduleListModel();

            listModel.HospitalDoctorList = hospitalDoctorList?.Count > 0 ? hospitalDoctorList : new List<HospitalDoctorOPDScheduleModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Get HospitalDoctorOPDSchedule by hospitalDoctorOPDSchedule Id.
        public virtual HospitalDoctorOPDScheduleModel GetHospitalDoctorOPDSchedule(int hospitalDoctorId, long hospitalDoctorOPDScheduleId)
        {
            if (hospitalDoctorId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "hospitalDoctorId"));

            HospitalDoctorOPDScheduleModel hospitalDoctorOPDScheduleModel = new HospitalDoctorOPDScheduleModel();
            return hospitalDoctorOPDScheduleModel;
        }

        //Update HospitalDoctorOPDSchedule.
        public virtual bool UpdateHospitalDoctorOPDSchedule(HospitalDoctorOPDScheduleModel hospitalDoctorOPDScheduleModel)
        {
            if (IsNull(hospitalDoctorOPDScheduleModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (hospitalDoctorOPDScheduleModel.HospitalDoctorId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalDoctorId"));

            //HospitalDoctorOPDSchedule hospitalDoctorOPDSchedule = hospitalDoctorOPDScheduleModel.FromModelToEntity<HospitalDoctorOPDSchedule>();
            bool isHospitalDoctorOPDScheduleUpdated = false;
            //if (hospitalDoctorOPDScheduleModel.HospitalDoctorOPDScheduleId > 0)
            //{
            //    //Update HospitalDoctorOPDSchedule
            //    isHospitalDoctorOPDScheduleUpdated = _hospitalDoctorOPDScheduleRepository.Update(hospitalDoctorOPDSchedule);
            //    if (!isHospitalDoctorOPDScheduleUpdated)
            //    {
            //        hospitalDoctorOPDScheduleModel.HasError = true;
            //        hospitalDoctorOPDScheduleModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            //    }
            //}
            //else
            //{
            //    hospitalDoctorOPDSchedule = _hospitalDoctorOPDScheduleRepository.Insert(hospitalDoctorOPDSchedule);
            //    if (hospitalDoctorOPDSchedule?.HospitalDoctorId > 0)
            //    {
            //        hospitalDoctorOPDScheduleModel.HospitalDoctorOPDScheduleId = hospitalDoctorOPDSchedule.HospitalDoctorId;
            //    }
            //    else
            //    {
            //        hospitalDoctorOPDScheduleModel.HasError = true;
            //        hospitalDoctorOPDScheduleModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            //    }
            //}
            return isHospitalDoctorOPDScheduleUpdated;
        }
    }
}
