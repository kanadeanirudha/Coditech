using Coditech.API.Data;
using Coditech.API.Organisation.Service.Interface.Organisation;
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
    public class OrganisationCentrewiseDepartmentService : IOrganisationCentrewiseDepartmentService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<OrganisationCentrewiseDepartment> _organisationCentrewiseDepartmentRepository;

        public OrganisationCentrewiseDepartmentService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _organisationCentrewiseDepartmentRepository = new CoditechRepository<OrganisationCentrewiseDepartment>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual OrganisationCentrewiseDepartmentListModel GetOrganisationCentrewiseDepartmentList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            string selectedCentreCode = filters?.Find(x => string.Equals(x.FilterName, FilterKeys.SelectedCentreCode, StringComparison.CurrentCultureIgnoreCase))?.FilterValue;
            filters.RemoveAll(x => x.FilterName == FilterKeys.SelectedCentreCode);

            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<OrganisationCentrewiseDepartmentModel> objStoredProc = new CoditechViewRepository<OrganisationCentrewiseDepartmentModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<OrganisationCentrewiseDepartmentModel> organisationCentrewiseDepartmentList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetOrganisationCentrewiseDepartmentList @CentreCode,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();
            OrganisationCentrewiseDepartmentListModel listModel = new OrganisationCentrewiseDepartmentListModel();

            listModel.OrganisationCentrewiseDepartmentList = organisationCentrewiseDepartmentList?.Count > 0 ? organisationCentrewiseDepartmentList : new List<OrganisationCentrewiseDepartmentModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Update  Associate UnAssociate Centrewise Department.
        public virtual bool AssociateUnAssociateCentrewiseDepartment(OrganisationCentrewiseDepartmentModel organisationCentrewiseDepartmentModel)
        {
            if (IsNull(organisationCentrewiseDepartmentModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (organisationCentrewiseDepartmentModel.GeneralDepartmentMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "generalDepartmentMasterId"));

            if (string.IsNullOrEmpty(organisationCentrewiseDepartmentModel.CentreCode))
                throw new CoditechException(ErrorCodes.NotFound, string.Format(GeneralResources.ErrorCodeExists, "CentreCode"));

            bool isAssociateUnAssociateCentrewiseDepartment = false;
            OrganisationCentrewiseDepartment organisationCentrewiseDepartment = organisationCentrewiseDepartmentModel.FromModelToEntity<OrganisationCentrewiseDepartment>();
            if (organisationCentrewiseDepartmentModel.OrganisationCentrewiseDepartmentId > 0)
            {
                _organisationCentrewiseDepartmentRepository.Delete(organisationCentrewiseDepartment);
            }
            else
            {
                organisationCentrewiseDepartment.ActiveFlag = true;
                isAssociateUnAssociateCentrewiseDepartment = _organisationCentrewiseDepartmentRepository.Insert(organisationCentrewiseDepartment).OrganisationCentrewiseDepartmentId > 0;
            }

            if (!isAssociateUnAssociateCentrewiseDepartment)
            {
                organisationCentrewiseDepartmentModel.HasError = true;
                organisationCentrewiseDepartmentModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isAssociateUnAssociateCentrewiseDepartment;
        }

        #region Protected Method
        //Check if Centre code is already present or not.
        protected virtual bool IsCentreCodeAlreadyExist(string centreCode, short generalDepartmentMasterId = 0)
         => _organisationCentrewiseDepartmentRepository.Table.Any(x => x.CentreCode == centreCode && (x.GeneralDepartmentMasterId != generalDepartmentMasterId || generalDepartmentMasterId == 0));
        #endregion
    }
}

