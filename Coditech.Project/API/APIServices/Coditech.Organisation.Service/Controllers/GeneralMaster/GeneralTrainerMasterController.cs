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
    public class GeneralTrainerMasterController : BaseController
    {
        private readonly IGeneralTrainerMasterService _generalTrainerMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralTrainerMasterController(ICoditechLogging coditechLogging, IGeneralTrainerMasterService generalTrainerMasterService)
        {
            _generalTrainerMasterService = generalTrainerMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralTrainerMaster/GetTrainerList")]
        [Produces(typeof(GeneralTrainerListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetTrainerList(string selectedCentreCode, short selectedDepartmentId, bool isAssociated, ExpandCollection expand, FilterCollection filter, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralTrainerListModel list = _generalTrainerMasterService.GetTrainerList(selectedCentreCode, selectedDepartmentId, isAssociated, filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralTrainerListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Trainer.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTrainerListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Trainer.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTrainerListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralTrainerMaster/CreateTrainer")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralTrainerResponse))]
        public virtual IActionResult CreateTrainer([FromBody] GeneralTrainerModel model)
        {
            try
            {
                GeneralTrainerModel trainer = _generalTrainerMasterService.CreateTrainer(model);
                return IsNotNull(trainer) ? CreateCreatedResponse(new GeneralTrainerResponse { GeneralTrainerModel = trainer }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Trainer.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralTrainerResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Trainer.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTrainerResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralTrainerMaster/GetTrainer")]
        [HttpGet]
        [Produces(typeof(GeneralTrainerResponse))]
        public virtual IActionResult GetTrainer(long generalTrainerId)
        {
            try
            {
                GeneralTrainerModel generalTrainerModel = _generalTrainerMasterService.GetTrainer(generalTrainerId);
                return IsNotNull(generalTrainerModel) ? CreateOKResponse(new GeneralTrainerResponse { GeneralTrainerModel = generalTrainerModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Trainer.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralTrainerResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Trainer.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTrainerResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralTrainerMaster/UpdateTrainer")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralTrainerResponse))]
        public virtual IActionResult UpdateTrainer([FromBody] GeneralTrainerModel model)
        {
            try
            {
                bool isUpdated = _generalTrainerMasterService.UpdateTrainer(model);
                return isUpdated ? CreateOKResponse(new GeneralTrainerResponse { GeneralTrainerModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Trainer.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralTrainerResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Trainer.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTrainerResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralTrainerMaster/DeleteTrainer")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteTrainer([FromBody] ParameterModel generalTrainerIds)
        {
            try
            {
                bool deleted = _generalTrainerMasterService.DeleteTrainer(generalTrainerIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Trainer.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Trainer.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("/GeneralTrainerMaster/GetAssociatedTrainerList")]
        [Produces(typeof(GeneralTraineeAssociatedToTrainerListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetAssociatedTrainerList(string selectedCentreCode, short selectedDepartmentId, bool isAssociated, long entityId, string userType, long personId, FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralTraineeAssociatedToTrainerListModel list = _generalTrainerMasterService.GetAssociatedTrainerList(selectedCentreCode, selectedDepartmentId, isAssociated, entityId, userType, personId, filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralTraineeAssociatedToTrainerListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AssociatedTrainer.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTraineeAssociatedToTrainerListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AssociatedTrainer.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTraineeAssociatedToTrainerListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralTrainerMaster/InsertAssociatedTrainer")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralTraineeAssociatedToTrainerResponse))]
        public virtual IActionResult InsertAssociatedTrainer([FromBody] GeneralTraineeAssociatedToTrainerModel model)
        {
            try
            {
                GeneralTraineeAssociatedToTrainerModel trainer = _generalTrainerMasterService.InsertAssociatedTrainer(model);
                return IsNotNull(trainer) ? CreateCreatedResponse(new GeneralTraineeAssociatedToTrainerResponse { GeneralTraineeAssociatedToTrainerModel = trainer }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AssociatedTrainer.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralTraineeAssociatedToTrainerResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AssociatedTrainer.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTraineeAssociatedToTrainerResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralTrainerMaster/GetAssociatedTrainer")]
        [HttpGet]
        [Produces(typeof(GeneralTraineeAssociatedToTrainerResponse))]
        public virtual IActionResult GetAssociatedTrainer(long generalTraineeAssociatedToTrainerId)
        {
            try
            {
                GeneralTraineeAssociatedToTrainerModel generalTraineeAssociatedToTrainerModel = _generalTrainerMasterService.GetAssociatedTrainer(generalTraineeAssociatedToTrainerId);
                return IsNotNull(generalTraineeAssociatedToTrainerModel) ? CreateOKResponse(new GeneralTraineeAssociatedToTrainerResponse { GeneralTraineeAssociatedToTrainerModel = generalTraineeAssociatedToTrainerModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AssociatedTrainer.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralTraineeAssociatedToTrainerResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AssociatedTrainer.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTraineeAssociatedToTrainerResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralTrainerMaster/UpdateAssociatedTrainer")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralTraineeAssociatedToTrainerResponse))]
        public virtual IActionResult UpdateAssociatedTrainer([FromBody] GeneralTraineeAssociatedToTrainerModel model)
        {
            try
            {
                bool isUpdated = _generalTrainerMasterService.UpdateAssociatedTrainer(model);
                return isUpdated ? CreateOKResponse(new GeneralTraineeAssociatedToTrainerResponse { GeneralTraineeAssociatedToTrainerModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AssociatedTrainer.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralTraineeAssociatedToTrainerResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AssociatedTrainer.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTraineeAssociatedToTrainerResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralTrainerMaster/DeleteAssociatedTrainer")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteAssociatedTrainer([FromBody] ParameterModel generalTraineeAssociatedToTrainerIds)
        {
            try
            {
                bool deleted = _generalTrainerMasterService.DeleteAssociatedTrainer(generalTraineeAssociatedToTrainerIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AssociatedTrainer.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AssociatedTrainer.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}