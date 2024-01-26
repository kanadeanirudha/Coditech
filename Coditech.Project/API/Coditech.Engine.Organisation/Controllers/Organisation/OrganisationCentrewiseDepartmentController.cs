using Coditech.API.Organisation.Service.Interface.Organisation;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Coditech.API.Controllers
{
    public class OrganisationCentrewiseDepartmentController : BaseController
    {
        private readonly IOrganisationCentrewiseDepartmentService _organisationCentrewiseDepartmentService;
        protected readonly ICoditechLogging _coditechLogging;
        public OrganisationCentrewiseDepartmentController(ICoditechLogging coditechLogging, IOrganisationCentrewiseDepartmentService organisationCentrewiseDepartmentService)
        {
            _organisationCentrewiseDepartmentService = organisationCentrewiseDepartmentService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/OrganisationCentrewiseDepartment/GetOrganisationCentrewiseDepartmentList")]
        [Produces(typeof(OrganisationCentrewiseDepartmentListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetOrganisationCentrewiseDepartmentList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                OrganisationCentrewiseDepartmentListModel list = _organisationCentrewiseDepartmentService.GetOrganisationCentrewiseDepartmentList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<OrganisationCentrewiseDepartmentListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseDepartment.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseDepartmentListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseDepartment.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseDepartmentListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentrewiseDepartment/UpdateAssociateUnAssociateCentrewiseDepartment")]
        [HttpPut, ValidateModel]
        [Produces(typeof(OrganisationCentrewiseDepartmentResponse))]
        public virtual IActionResult UpdateAssociateUnAssociateCentrewiseDepartment([FromBody] OrganisationCentrewiseDepartmentModel model)
        {
            try
            {
                bool isUpdated = _organisationCentrewiseDepartmentService.UpdateAssociateUnAssociateCentrewiseDepartment(model);
                return isUpdated ? CreateOKResponse(new OrganisationCentrewiseDepartmentResponse { OrganisationCentrewiseDepartmentModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AssociateUnAssociateCentrewiseDepartment.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseDepartmentResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AssociateUnAssociateCentrewiseDepartment.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseDepartmentResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
