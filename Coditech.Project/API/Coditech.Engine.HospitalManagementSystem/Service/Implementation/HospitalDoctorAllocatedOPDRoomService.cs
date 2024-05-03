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
    public class HospitalDoctorAllocatedOPDRoomService : BaseService, IHospitalDoctorAllocatedOPDRoomService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<HospitalDoctorAllocatedRoom> _hospitalDoctorAllocatedOPDRoomRepository;
        private readonly ICoditechRepository<OrganisationCentrewiseBuildingRooms> _organisationCentrewiseBuildingRoomsRepository;
        private readonly ICoditechRepository<HospitalDoctors> _hospitalDoctorsRepository;

        public HospitalDoctorAllocatedOPDRoomService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _hospitalDoctorAllocatedOPDRoomRepository = new CoditechRepository<HospitalDoctorAllocatedRoom>(_serviceProvider.GetService<Coditech_Entities>());
            _hospitalDoctorsRepository = new CoditechRepository<HospitalDoctors>(_serviceProvider.GetService<Coditech_Entities>());
            _organisationCentrewiseBuildingRoomsRepository = new CoditechRepository<OrganisationCentrewiseBuildingRooms>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual HospitalDoctorAllocatedOPDRoomListModel GetHospitalDoctorAllocatedOPDRoomList(string selectedCentreCode, short selectedDepartmentId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<HospitalDoctorAllocatedOPDRoomModel> objStoredProc = new CoditechViewRepository<HospitalDoctorAllocatedOPDRoomModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@DepartmentId", selectedDepartmentId, ParameterDirection.Input, DbType.Int16);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<HospitalDoctorAllocatedOPDRoomModel> hospitalDoctorAllocatedOPDRoomList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetHospitalDoctorAllocatedOPDRoomList @CentreCode,@DepartmentId,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();
            HospitalDoctorAllocatedOPDRoomListModel listModel = new HospitalDoctorAllocatedOPDRoomListModel();

            listModel.HospitalDoctorAllocatedOPDRoomList = hospitalDoctorAllocatedOPDRoomList?.Count > 0 ? hospitalDoctorAllocatedOPDRoomList : new List<HospitalDoctorAllocatedOPDRoomModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Get HospitalDoctorAllocatedOPDRoom by hospitalDoctorAllocatedOPDRoom Id.
        public virtual HospitalDoctorAllocatedOPDRoomModel GetHospitalDoctorAllocatedOPDRoom(int hospitalDoctorId, int hospitalDoctorAllocatedOPDRoomId)
        {
            if (hospitalDoctorId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "hospitalDoctorId"));

            HospitalDoctorAllocatedOPDRoomModel hospitalDoctorAllocatedOPDRoomModel = new HospitalDoctorAllocatedOPDRoomModel();
            if (hospitalDoctorAllocatedOPDRoomId > 0)
            {
                //Get the HospitalDoctorAllocatedOPDRoom Details based on id.
                HospitalDoctorAllocatedRoom hospitalDoctorAllocatedOPDRoom = _hospitalDoctorAllocatedOPDRoomRepository.Table.FirstOrDefault(x => x.HospitalDoctorAllocatedOPDRoomId == hospitalDoctorAllocatedOPDRoomId);
                if (IsNotNull(hospitalDoctorAllocatedOPDRoom))
                {
                    hospitalDoctorAllocatedOPDRoomModel = hospitalDoctorAllocatedOPDRoom?.FromEntityToModel<HospitalDoctorAllocatedOPDRoomModel>();
                    hospitalDoctorAllocatedOPDRoomModel.OrganisationCentrewiseBuildingMasterId = _organisationCentrewiseBuildingRoomsRepository.Table.Where(x => x.OrganisationCentrewiseBuildingRoomId == hospitalDoctorAllocatedOPDRoom.OrganisationCentrewiseBuildingRoomId)?.Select(y => y.OrganisationCentrewiseBuildingMasterId)?.FirstOrDefault();
                }
            }

            HospitalDoctors hospitalDoctors = _hospitalDoctorsRepository.Table.Where(x => x.HospitalDoctorId == hospitalDoctorId)?.FirstOrDefault();
            if (hospitalDoctors?.EmployeeId > 0)
            {
                GeneralPersonModel generalPersonModel = GetGeneralPersonDetailsByEntityType(hospitalDoctors.EmployeeId, UserTypeEnum.Employee.ToString());
                if (IsNotNull(generalPersonModel))
                {
                    hospitalDoctorAllocatedOPDRoomModel.FirstName = generalPersonModel.FirstName;
                    hospitalDoctorAllocatedOPDRoomModel.LastName = generalPersonModel.LastName;
                    hospitalDoctorAllocatedOPDRoomModel.SelectedCentreCode = generalPersonModel.SelectedCentreCode;
                    hospitalDoctorAllocatedOPDRoomModel.SelectedDepartmentId = generalPersonModel.SelectedDepartmentId;
                }
            }
            return hospitalDoctorAllocatedOPDRoomModel;
        }

        //Update HospitalDoctorAllocatedOPDRoom.
        public virtual bool UpdateHospitalDoctorAllocatedOPDRoom(HospitalDoctorAllocatedOPDRoomModel hospitalDoctorAllocatedOPDRoomModel)
        {
            if (IsNull(hospitalDoctorAllocatedOPDRoomModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (hospitalDoctorAllocatedOPDRoomModel.HospitalDoctorId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalDoctorId"));

            HospitalDoctorAllocatedRoom hospitalDoctorAllocatedOPDRoom = hospitalDoctorAllocatedOPDRoomModel.FromModelToEntity<HospitalDoctorAllocatedRoom>();
            bool isHospitalDoctorAllocatedOPDRoomUpdated = false;
            if (hospitalDoctorAllocatedOPDRoomModel.HospitalDoctorAllocatedOPDRoomId > 0)
            {
                //Update HospitalDoctorAllocatedOPDRoom
                isHospitalDoctorAllocatedOPDRoomUpdated = _hospitalDoctorAllocatedOPDRoomRepository.Update(hospitalDoctorAllocatedOPDRoom);
                if (!isHospitalDoctorAllocatedOPDRoomUpdated)
                {
                    hospitalDoctorAllocatedOPDRoomModel.HasError = true;
                    hospitalDoctorAllocatedOPDRoomModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
                }
            }
            else
            {
                hospitalDoctorAllocatedOPDRoom = _hospitalDoctorAllocatedOPDRoomRepository.Insert(hospitalDoctorAllocatedOPDRoom);
                if (hospitalDoctorAllocatedOPDRoom?.HospitalDoctorAllocatedOPDRoomId > 0)
                {
                    hospitalDoctorAllocatedOPDRoomModel.HospitalDoctorAllocatedOPDRoomId = hospitalDoctorAllocatedOPDRoom.HospitalDoctorAllocatedOPDRoomId;
                }
                else
                {
                    hospitalDoctorAllocatedOPDRoomModel.HasError = true;
                    hospitalDoctorAllocatedOPDRoomModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
                }
            }
            return isHospitalDoctorAllocatedOPDRoomUpdated;
        }
    }
}
