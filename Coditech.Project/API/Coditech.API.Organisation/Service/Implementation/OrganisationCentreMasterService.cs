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
    public class OrganisationCentreMasterService : IOrganisationCentreMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<OrganisationCentreMaster> _organisationCentreMasterRepository;
        public OrganisationCentreMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _organisationCentreMasterRepository = new CoditechRepository<OrganisationCentreMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual OrganisationCentreListModel GetOrganisationCentreList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<OrganisationCentreMasterModel> objStoredProc = new CoditechViewRepository<OrganisationCentreMasterModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", null/*pageListModel?.SPWhereClause*/, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<OrganisationCentreMasterModel> organisationCentreList = objStoredProc.ExecuteStoredProcedureList("RARIndia_GetOrganisationCentreList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            OrganisationCentreListModel listModel = new OrganisationCentreListModel();

            listModel.OrganisationCentreList = organisationCentreList?.Count > 0 ? organisationCentreList : new List<OrganisationCentreMasterModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create Organisation Centre.
        public OrganisationCentreMasterModel CreateOrganisationCentre(OrganisationCentreMasterModel organisationCentreMasterModel)
        {
            if (IsNull(organisationCentreMasterModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);
            if (IsNameAlreadyExist(organisationCentreMasterModel.CentreCode))
            {
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Centre Code"));
            }
            OrganisationCentreMaster organisationCentreMaster = organisationCentreMasterModel.FromModelToEntity<OrganisationCentreMaster>();

            //Create new Organisation Centre and return it.
            OrganisationCentreMaster organisationData = _organisationCentreMasterRepository.Insert(organisationCentreMaster);
            if (organisationData?.OrganisationCentreMasterId > 0)
            {
                organisationCentreMasterModel.OrganisationCentreMasterId = organisationData.OrganisationCentreMasterId;
            }
            else
            {
                organisationCentreMasterModel.HasError = true;
                organisationCentreMasterModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return organisationCentreMasterModel;
        }

        //Get Organisation Centre by organisationCentreMasterId.
        public OrganisationCentreMasterModel GetOrganisationCentre(int organisationCentreId)
        {
            if (organisationCentreId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentreId"));

            //Get the  Organisation Details based on id.
            OrganisationCentreMaster organisationData = _organisationCentreMasterRepository.Table.FirstOrDefault(x => x.OrganisationCentreMasterId == organisationCentreId);
            OrganisationCentreMasterModel organisationCentreMasterModel = organisationData.FromEntityToModel<OrganisationCentreMasterModel>();
            return organisationCentreMasterModel;
        }

        //Update  Organisation Centre.
        public virtual bool UpdateOrganisationCentre(OrganisationCentreMasterModel organisationCentreMasterModel)
        {
            if (IsNull(organisationCentreMasterModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (organisationCentreMasterModel.OrganisationCentreMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentreId"));

            OrganisationCentreMaster organisationCentreMaster = organisationCentreMasterModel.FromModelToEntity<OrganisationCentreMaster>();

            //Update Organisation Centre
            bool isOrganisationCentreUpdated = _organisationCentreMasterRepository.Update(OrganisationCentreMaster);
            if (!isOrganisationCentreUpdated)
            {
                organisationCentreMasterModel.HasError = true;
                organisationCentreMasterModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isOrganisationCentreUpdated;
        }

        //Delete Organisation Centre.
        public virtual bool DeleteOrganisationCentre(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentreId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("OrganisationCentreId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("RARIndia_DeleteOrganisationCentre @organisationCentreId,  @Status OUT", 1, out status);
            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Centre code is already present or not.
        protected virtual bool IsNameAlreadyExist(string centreCode)
         => _organisationCentreMasterRepository.Table.Any(x => x.CentreCode == centreCode);
        #endregion
    }
}

