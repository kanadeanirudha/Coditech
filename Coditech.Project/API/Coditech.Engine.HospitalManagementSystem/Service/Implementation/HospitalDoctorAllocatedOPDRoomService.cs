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
    public class HospitalDoctorAllocatedOPDRoomService : IHospitalDoctorAllocatedOPDRoomService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<HospitalDoctorAllocatedOPDRoom> _hospitalDoctorAllocatedOPDRoomRepository;
        public HospitalDoctorAllocatedOPDRoomService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _hospitalDoctorAllocatedOPDRoomRepository = new CoditechRepository<HospitalDoctorAllocatedOPDRoom>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual HospitalDoctorAllocatedOPDRoomListModel GetHospitalDoctorAllocatedOPDRoomList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<HospitalDoctorAllocatedOPDRoomModel> objStoredProc = new CoditechViewRepository<HospitalDoctorAllocatedOPDRoomModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<HospitalDoctorAllocatedOPDRoomModel> HospitalDoctorAllocatedOPDRoomList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetHospitalDoctorAllocatedOPDRoomList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            HospitalDoctorAllocatedOPDRoomListModel listModel = new HospitalDoctorAllocatedOPDRoomListModel();

            listModel.HospitalDoctorAllocatedOPDRoomList = HospitalDoctorAllocatedOPDRoomList?.Count > 0 ? HospitalDoctorAllocatedOPDRoomList : new List<HospitalDoctorAllocatedOPDRoomModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create HospitalDoctorAllocatedOPDRoom.
        public virtual HospitalDoctorAllocatedOPDRoomModel CreateHospitalDoctorAllocatedOPDRoom(HospitalDoctorAllocatedOPDRoomModel hospitalDoctorAllocatedOPDRoomModel)
        {
            if (IsNull(hospitalDoctorAllocatedOPDRoomModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            //if (IsRoomNameAlreadyExist(hospitalDoctorAllocatedOPDRoomModel.RoomName))
            //    throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Room Name"));

            HospitalDoctorAllocatedOPDRoom hospitalDoctorAllocatedOPDRoom = hospitalDoctorAllocatedOPDRoomModel.FromModelToEntity<HospitalDoctorAllocatedOPDRoom>();

            //Create new HospitalDoctorAllocatedOPDRoom and return it.
            HospitalDoctorAllocatedOPDRoom hospitalDoctorAllocatedOPDRoomData = _hospitalDoctorAllocatedOPDRoomRepository.Insert(hospitalDoctorAllocatedOPDRoom);
            if (hospitalDoctorAllocatedOPDRoomData?.HospitalDoctorAllocatedOPDRoomId > 0)
            {
                hospitalDoctorAllocatedOPDRoomModel.HospitalDoctorAllocatedOPDRoomId = hospitalDoctorAllocatedOPDRoomData.HospitalDoctorAllocatedOPDRoomId;
            }
            else
            {
                hospitalDoctorAllocatedOPDRoomModel.HasError = true;
                hospitalDoctorAllocatedOPDRoomModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return hospitalDoctorAllocatedOPDRoomModel;
        }

        //Get HospitalDoctorAllocatedOPDRoom by hospitalDoctorAllocatedOPDRoom Id.
        public virtual HospitalDoctorAllocatedOPDRoomModel GetHospitalDoctorAllocatedOPDRoom(int hospitalDoctorAllocatedOPDRoomId)
        {
            if (hospitalDoctorAllocatedOPDRoomId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalDoctorAllocatedOPDRoomID"));

            //Get the HospitalDoctorAllocatedOPDRoom Details based on id.
            HospitalDoctorAllocatedOPDRoom hospitalDoctorAllocatedOPDRoom = _hospitalDoctorAllocatedOPDRoomRepository.Table.FirstOrDefault(x => x.HospitalDoctorAllocatedOPDRoomId == hospitalDoctorAllocatedOPDRoomId);
            HospitalDoctorAllocatedOPDRoomModel hospitalDoctorAllocatedOPDRoomModel = hospitalDoctorAllocatedOPDRoom?.FromEntityToModel<HospitalDoctorAllocatedOPDRoomModel>();
            return hospitalDoctorAllocatedOPDRoomModel;
        }

        //Update HospitalDoctorAllocatedOPDRoom.
        public virtual bool UpdateHospitalDoctorAllocatedOPDRoom(HospitalDoctorAllocatedOPDRoomModel hospitalDoctorAllocatedOPDRoomModel)
        {
            if (IsNull(hospitalDoctorAllocatedOPDRoomModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (hospitalDoctorAllocatedOPDRoomModel.HospitalDoctorAllocatedOPDRoomId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalDoctorAllocatedOPDRoomID"));

            HospitalDoctorAllocatedOPDRoom hospitalDoctorAllocatedOPDRoom = hospitalDoctorAllocatedOPDRoomModel.FromModelToEntity<HospitalDoctorAllocatedOPDRoom>();

            //Update HospitalDoctorAllocatedOPDRoom
            bool isHospitalDoctorAllocatedOPDRoomUpdated = _hospitalDoctorAllocatedOPDRoomRepository.Update(hospitalDoctorAllocatedOPDRoom);
            if (!isHospitalDoctorAllocatedOPDRoomUpdated)
            {
                hospitalDoctorAllocatedOPDRoomModel.HasError = true;
                hospitalDoctorAllocatedOPDRoomModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isHospitalDoctorAllocatedOPDRoomUpdated;
        }

        //Delete HospitalDoctorAllocatedOPDRoom.
        public virtual bool DeleteHospitalDoctorAllocatedOPDRoom(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "HospitalDoctorAllocatedOPDRoomID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("HospitalDoctorAllocatedOPDRoomId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteHospitalDoctorAllocatedOPDRoom @HospitalDoctorAllocatedOPDRoomId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        //#region Protected Method
        ////Check if Room Name is already present or not.
        //protected virtual bool IsRoomNameAlreadyExist(string roomName, int hospitalDoctorAllocatedOPDRoomId = 0)
        // => _hospitalDoctorAllocatedOPDRoomRepository.Table.Any(x => x.RoomName == roomName && (x.HospitalDoctorAllocatedOPDRoomId != hospitalDoctorAllocatedOPDRoomId || hospitalDoctorAllocatedOPDRoomId == 0));
        //#endregion
    }
}
