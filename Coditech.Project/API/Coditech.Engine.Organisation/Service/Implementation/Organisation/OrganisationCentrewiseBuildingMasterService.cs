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
    public class OrganisationCentrewiseBuildingMasterService : IOrganisationCentrewiseBuildingMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<OrganisationCentrewiseBuildingMaster> _organisationCentrewiseBuildingMasterRepository;

        public OrganisationCentrewiseBuildingMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _organisationCentrewiseBuildingMasterRepository = new CoditechRepository<OrganisationCentrewiseBuildingMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual OrganisationCentrewiseBuildingListModel GetOrganisationCentrewiseBuildingList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            string selectedCentreCode = filters?.Find(x => string.Equals(x.FilterName, FilterKeys.SelectedCentreCode, StringComparison.CurrentCultureIgnoreCase))?.FilterValue;

            filters.RemoveAll(x => x.FilterName == FilterKeys.SelectedCentreCode);
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<OrganisationCentrewiseBuildingModel> objStoredProc = new CoditechViewRepository<OrganisationCentrewiseBuildingModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<OrganisationCentrewiseBuildingModel> organisationCentrewiseBuildingList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetOrganisationCentrewiseBuildingMasterList @CentreCode,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            OrganisationCentrewiseBuildingListModel listModel = new OrganisationCentrewiseBuildingListModel();

            listModel.OrganisationCentrewiseBuildingList = organisationCentrewiseBuildingList?.Count > 0 ? organisationCentrewiseBuildingList : new List<OrganisationCentrewiseBuildingModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create Organisation Centrewise Building Master.
        public virtual OrganisationCentrewiseBuildingModel CreateOrganisationCentrewiseBuilding(OrganisationCentrewiseBuildingModel organisationCentrewiseBuildingModel)
        {
            if (IsNull(organisationCentrewiseBuildingModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsCentreCodeAlreadyExist(organisationCentrewiseBuildingModel.CentreCode, organisationCentrewiseBuildingModel.BuildingName))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Centre Code"));

            OrganisationCentrewiseBuildingMaster organisationCentrewiseBuildingMaster = organisationCentrewiseBuildingModel.FromModelToEntity<OrganisationCentrewiseBuildingMaster>();

            //Create new Organisation Centrewise Building Master and return it.
            OrganisationCentrewiseBuildingMaster organisationCentrewiseBuildingMasterData = _organisationCentrewiseBuildingMasterRepository.Insert(organisationCentrewiseBuildingMaster);
            if (organisationCentrewiseBuildingMasterData?.OrganisationCentrewiseBuildingMasterId > 0)
            {
                organisationCentrewiseBuildingModel.OrganisationCentrewiseBuildingMasterId = organisationCentrewiseBuildingMasterData.OrganisationCentrewiseBuildingMasterId;
            }
            else
            {
                organisationCentrewiseBuildingModel.HasError = true;
                organisationCentrewiseBuildingModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return organisationCentrewiseBuildingModel;
        }

        //Get Organisation Centrewise Building Master by organisationCentrewiseBuildingId.
        public virtual OrganisationCentrewiseBuildingModel GetOrganisationCentrewiseBuilding(short organisationCentrewiseBuildingId)
        {
            if (organisationCentrewiseBuildingId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentrewiseBuildingId"));

            //Get the  Organisation Details based on id.
            OrganisationCentrewiseBuildingMaster organisationData = _organisationCentrewiseBuildingMasterRepository.Table.FirstOrDefault(x => x.OrganisationCentrewiseBuildingMasterId == organisationCentrewiseBuildingId);
            OrganisationCentrewiseBuildingModel organisationCentrewiseBuildingModel = organisationData.FromEntityToModel<OrganisationCentrewiseBuildingModel>();
            return organisationCentrewiseBuildingModel;
        }

        //Update  Organisation Centrewise Building Master.
        public virtual bool UpdateOrganisationCentrewiseBuilding(OrganisationCentrewiseBuildingModel organisationCentrewiseBuildingModel)
        {
            if (IsNull(organisationCentrewiseBuildingModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (organisationCentrewiseBuildingModel.OrganisationCentrewiseBuildingMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentrewiseBuildingId"));

            if (IsCentreCodeAlreadyExist(organisationCentrewiseBuildingModel.CentreCode, organisationCentrewiseBuildingModel.BuildingName, organisationCentrewiseBuildingModel.OrganisationCentrewiseBuildingMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Centre Code"));

            OrganisationCentrewiseBuildingMaster organisationCentrewiseBuildingMaster = organisationCentrewiseBuildingModel.FromModelToEntity<OrganisationCentrewiseBuildingMaster>();

            //Update Organisation Centrewise Building Master
            bool isOrganisationCentrewiseBuildingUpdated = _organisationCentrewiseBuildingMasterRepository.Update(organisationCentrewiseBuildingMaster);
            if (!isOrganisationCentrewiseBuildingUpdated)
            {
                organisationCentrewiseBuildingModel.HasError = true;
                organisationCentrewiseBuildingModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isOrganisationCentrewiseBuildingUpdated;
        }

        //Delete Organisation Centrewise Building Master.
        public virtual bool DeleteOrganisationCentrewiseBuilding(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentrewiseBuildingId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("OrganisationCentrewiseBuildingMasterId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteOrganisationCentrewiseBuildingMaster @OrganisationCentrewiseBuildingMasterId,  @Status OUT", 1, out status);
            return status == 1 ? true : false;
        }


        #region Protected Method
        //Check if Centre code is already present or not.
        protected virtual bool IsCentreCodeAlreadyExist(string centreCode, string buildingName, int organisationCentrewiseBuildingMasterId = 0)
         => _organisationCentrewiseBuildingMasterRepository.Table.Any(x => x.CentreCode == centreCode && x.BuildingName == buildingName && (x.OrganisationCentrewiseBuildingMasterId != organisationCentrewiseBuildingMasterId || organisationCentrewiseBuildingMasterId == 0));
        #endregion
    }
}

