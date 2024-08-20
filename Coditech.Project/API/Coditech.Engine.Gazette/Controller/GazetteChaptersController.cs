using Coditech.API.Data;
using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Controllers
{
    public class GazetteChaptersController : BaseController
    {
        private readonly IGazetteChaptersService _gazetteChaptersService;
        protected readonly ICoditechLogging _coditechLogging;
        public GazetteChaptersController(ICoditechLogging coditechLogging, IGazetteChaptersService gazetteChaptersService)
        {
            _gazetteChaptersService = gazetteChaptersService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GazetteChapters/GetGazetteChaptersList")]
        [Produces(typeof(GazetteChaptersListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetGazetteChaptersList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GazetteChaptersListModel list = _gazetteChaptersService.GetGazetteChaptersList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GazetteChaptersListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapter.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GazetteChaptersListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapter.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GazetteChaptersListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GazetteChapters/CreateGazetteChapters")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GazetteChaptersResponse))]
        public virtual IActionResult CreateGazetteChapters([FromBody] GazetteChaptersModel model)
        {
            try
            {
                GazetteChaptersModel gazetteChapters = _gazetteChaptersService.CreateGazetteChapters(model);
                return IsNotNull(gazetteChapters) ? CreateCreatedResponse(new GazetteChaptersResponse { GazetteChaptersModel = gazetteChapters }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapter.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GazetteChaptersResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapter.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GazetteChaptersResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GazetteChapters/GetGazetteChapters")]
        [HttpGet]
        [Produces(typeof(GazetteChaptersResponse))]
        public virtual IActionResult GetGazetteChapters(int gazetteChaptersId)
        {
            try
            {
                GazetteChaptersModel gazetteChaptersModel = _gazetteChaptersService.GetGazetteChapters(gazetteChaptersId);
                return IsNotNull(gazetteChaptersModel) ? CreateOKResponse(new GazetteChaptersResponse { GazetteChaptersModel = gazetteChaptersModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapter.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GazetteChaptersResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapter.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GazetteChaptersResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GazetteChapters/UpdateGazetteChapters")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GazetteChaptersResponse))]
        public virtual IActionResult UpdateGazetteChapters([FromBody] GazetteChaptersModel model)
        {
            try
            {
                bool isUpdated = _gazetteChaptersService.UpdateGazetteChapters(model);
                return isUpdated ? CreateOKResponse(new GazetteChaptersResponse { GazetteChaptersModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapter.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GazetteChaptersResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapter.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GazetteChaptersResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GazetteChapters/DeleteGazetteChapters")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteGazetteChapters([FromBody] ParameterModel gazetteChaptersIds)
        {
            try
            {
                bool deleted = _gazetteChaptersService.DeleteGazetteChapters(gazetteChaptersIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapter.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapter.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("/GazetteChapters/GetGazetteChaptersByDistrictWise")]
        [HttpGet]
        [Produces(typeof(GazetteChaptersListResponse))]
        public virtual IActionResult GetGazetteChaptersByDistrictWise(int generalDistrictMasterId)
        {
            try
            {
                GazetteChaptersListModel list = _gazetteChaptersService.GetGazetteChaptersByDistrictWise(generalDistrictMasterId);
                return IsNotNull(list) ? CreateOKResponse(new GazetteChaptersListResponse { GazetteChaptersList = list.GazetteChaptersList }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapter.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GazetteChaptersListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapter.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GazetteChaptersListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}