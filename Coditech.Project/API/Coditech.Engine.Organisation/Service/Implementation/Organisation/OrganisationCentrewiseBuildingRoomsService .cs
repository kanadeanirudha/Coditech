
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
    public class OrganisationCentrewiseBuildingRoomsService : IOrganisationCentrewiseBuildingRoomsService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<OrganisationCentrewiseBuildingRooms> _organisationCentrewiseBuildingRoomsRepository;
        public OrganisationCentrewiseBuildingRoomsService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _organisationCentrewiseBuildingRoomsRepository = new CoditechRepository<OrganisationCentrewiseBuildingRooms>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual OrganisationCentrewiseBuildingRoomsListModel GetOrganisationCentrewiseBuildingRoomsList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            string selectedCentreCode = filters?.Find(x => string.Equals(x.FilterName, FilterKeys.SelectedCentreCode, StringComparison.CurrentCultureIgnoreCase))?.FilterValue;
            filters.RemoveAll(x => x.FilterName == FilterKeys.SelectedCentreCode);

            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<OrganisationCentrewiseBuildingRoomsModel> objStoredProc = new CoditechViewRepository<OrganisationCentrewiseBuildingRoomsModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<OrganisationCentrewiseBuildingRoomsModel> organisationCentrewiseBuildingRoomsList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetOrganisationCentrewiseBuildingRoomsList @CentreCode,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
            OrganisationCentrewiseBuildingRoomsListModel listModel = new OrganisationCentrewiseBuildingRoomsListModel();

            listModel.OrganisationCentrewiseBuildingRoomsList = organisationCentrewiseBuildingRoomsList?.Count > 0 ? organisationCentrewiseBuildingRoomsList : new List<OrganisationCentrewiseBuildingRoomsModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create OrganisationCentrewiseBuildingRooms.
        public virtual OrganisationCentrewiseBuildingRoomsModel CreateOrganisationCentrewiseBuildingRooms(OrganisationCentrewiseBuildingRoomsModel organisationCentrewiseBuildingRoomsModel)
        {
            if (IsNull(organisationCentrewiseBuildingRoomsModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsRoomNameAlreadyExist(organisationCentrewiseBuildingRoomsModel.RoomName))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Room Name"));

            OrganisationCentrewiseBuildingRooms organisationCentrewiseBuildingRooms = organisationCentrewiseBuildingRoomsModel.FromModelToEntity<OrganisationCentrewiseBuildingRooms>();

            //Create new OrganisationCentrewiseBuildingRooms and return it.
            OrganisationCentrewiseBuildingRooms organisationCentrewiseBuildingRoomsData = _organisationCentrewiseBuildingRoomsRepository.Insert(organisationCentrewiseBuildingRooms);
            if (organisationCentrewiseBuildingRoomsData?.OrganisationCentrewiseBuildingRoomId > 0)
            {
                organisationCentrewiseBuildingRoomsModel.OrganisationCentrewiseBuildingRoomId = organisationCentrewiseBuildingRoomsData.OrganisationCentrewiseBuildingRoomId;
            }
            else
            {
                organisationCentrewiseBuildingRoomsModel.HasError = true;
                organisationCentrewiseBuildingRoomsModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return organisationCentrewiseBuildingRoomsModel;
        }

        //Get OrganisationCentrewiseBuildingRooms by organisationCentrewiseBuildingRoom id.
        public virtual OrganisationCentrewiseBuildingRoomsModel GetOrganisationCentrewiseBuildingRooms(short organisationCentrewiseBuildingRoomId)
        {
            if (organisationCentrewiseBuildingRoomId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "OrganisationCentrewiseBuildingRoomId"));

            //Get the OrganisationCentrewiseBuildingRooms Details based on id.
            OrganisationCentrewiseBuildingRooms organisationCentrewiseBuildingRooms = _organisationCentrewiseBuildingRoomsRepository.Table.FirstOrDefault(x => x.OrganisationCentrewiseBuildingRoomId == organisationCentrewiseBuildingRoomId);
            OrganisationCentrewiseBuildingRoomsModel organisationCentrewiseBuildingRoomsModel = organisationCentrewiseBuildingRooms?.FromEntityToModel<OrganisationCentrewiseBuildingRoomsModel>();
            return organisationCentrewiseBuildingRoomsModel;
        }

        //Update OrganisationCentrewiseBuildingRooms.
        public virtual bool UpdateOrganisationCentrewiseBuildingRooms(OrganisationCentrewiseBuildingRoomsModel organisationCentrewiseBuildingRoomsModel)
        {
            if (IsNull(organisationCentrewiseBuildingRoomsModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (organisationCentrewiseBuildingRoomsModel.OrganisationCentrewiseBuildingRoomId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "OrganisationCentrewiseBuildingRoomId"));

            if (IsRoomNameAlreadyExist(organisationCentrewiseBuildingRoomsModel.RoomName, organisationCentrewiseBuildingRoomsModel.OrganisationCentrewiseBuildingRoomId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Room Name"));

            OrganisationCentrewiseBuildingRooms organisationCentrewiseBuildingRooms = organisationCentrewiseBuildingRoomsModel.FromModelToEntity<OrganisationCentrewiseBuildingRooms>();

            //Update OrganisationCentrewiseBuildingRooms.
            bool isOrganisationCentrewiseBuildingRoomsUpdated = _organisationCentrewiseBuildingRoomsRepository.Update(organisationCentrewiseBuildingRooms);
            if (!isOrganisationCentrewiseBuildingRoomsUpdated)
            {
                organisationCentrewiseBuildingRoomsModel.HasError = true;
                organisationCentrewiseBuildingRoomsModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isOrganisationCentrewiseBuildingRoomsUpdated;
        }

        //Delete OrganisationCentrewiseBuildingRooms.
        public virtual bool DeleteOrganisationCentrewiseBuildingRooms(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "OrganisationCentrewiseBuildingRoomId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("OrganisationCentrewiseBuildingRoomId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteOrganisationCentrewiseBuildingRooms @OrganisationCentrewiseBuildingRoomId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Room Name is already present or not.
        protected virtual bool IsRoomNameAlreadyExist(string roomName, short organisationCentrewiseBuildingRoomId = 0)
         => _organisationCentrewiseBuildingRoomsRepository.Table.Any(x => x.RoomName == roomName && (x.OrganisationCentrewiseBuildingRoomId != organisationCentrewiseBuildingRoomId || organisationCentrewiseBuildingRoomId == 0));
        #endregion
    }
}
