using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.API.Model.Responses.Inventory.InventoryGeneralItemMaster;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Controllers
{
    public class InventoryGeneralItemMasterController : BaseController
	{
		private readonly IInventoryGeneralItemMasterService _inventoryGeneralItemMasterService;
		protected readonly ICoditechLogging _coditechLogging;
		public InventoryGeneralItemMasterController(ICoditechLogging coditechLogging, IInventoryGeneralItemMasterService inventoryGeneralItemMasterService)
		{
			_inventoryGeneralItemMasterService = inventoryGeneralItemMasterService;
			_coditechLogging = coditechLogging;
		}

		[HttpGet]
		[Route("/InventoryGeneralItemMaster/GetInventoryGeneralItemMasterList")]
		[Produces(typeof(InventoryGeneralItemMasterListResponse))]
		[TypeFilter(typeof(BindQueryFilter))]
		public virtual IActionResult GetInventoryGeneralItemMasterList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
		{
			try
			{
				InventoryGeneralItemMasterListModel list = _inventoryGeneralItemMasterService.GetInventoryGeneralItemMasterList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
				string data = ApiHelper.ToJson(list);
				return !string.IsNullOrEmpty(data) ? CreateOKResponse<InventoryGeneralItemMasterListResponse>(data) : CreateNoContentResponse();
			}
			catch (CoditechException ex)
			{
				_coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Inventory.ToString(), TraceLevel.Error);
				return CreateInternalServerErrorResponse(new InventoryGeneralItemMasterListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
			}
			catch (Exception ex)
			{
				_coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Inventory.ToString(), TraceLevel.Error);
				return CreateInternalServerErrorResponse(new InventoryGeneralItemMasterListResponse { HasError = true, ErrorMessage = ex.Message });
			}
		}

		[Route("/InventoryGeneralItemMaster/CreateInventoryGeneralItemMaster")]
		[HttpPost, ValidateModel]
		[Produces(typeof(InventoryGeneralItemMasterResponse))]
		public virtual IActionResult CreateInventoryGeneralItemMaster([FromBody] InventoryGeneralItemMasterModel model)
		{
			try
			{
				InventoryGeneralItemMasterModel Inventory = _inventoryGeneralItemMasterService.CreateInventoryGeneralItemMaster(model);
				return IsNotNull(Inventory) ? CreateCreatedResponse(new InventoryGeneralItemMasterResponse { InventoryGeneralItemMasterModel = Inventory }) : CreateInternalServerErrorResponse();
			}
			catch (CoditechException ex)
			{
				_coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Inventory.ToString(), TraceLevel.Warning);
				return CreateInternalServerErrorResponse(new InventoryGeneralItemMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
			}
			catch (Exception ex)
			{
				_coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Inventory.ToString(), TraceLevel.Error);
				return CreateInternalServerErrorResponse(new InventoryGeneralItemMasterResponse { HasError = true, ErrorMessage = ex.Message });
			}
		}

		[Route("/InventoryGeneralItemMaster/GetInventoryGeneralItemMaster")]
		[HttpGet]
		[Produces(typeof(InventoryGeneralItemMasterResponse))]
		public virtual IActionResult GetInventoryGeneralItemMaster(short inventoryGeneralItemMasterId)
		{
			try
			{
				InventoryGeneralItemMasterModel inventoryGeneralItemMasterModel = _inventoryGeneralItemMasterService.GetInventoryGeneralItemMaster(inventoryGeneralItemMasterId);
				return IsNotNull(inventoryGeneralItemMasterModel) ? CreateOKResponse(new InventoryGeneralItemMasterResponse { InventoryGeneralItemMasterModel = inventoryGeneralItemMasterModel }) : CreateNoContentResponse();
			}
			catch (CoditechException ex)
			{
				_coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Inventory.ToString(), TraceLevel.Warning);
				return CreateInternalServerErrorResponse(new InventoryGeneralItemMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
			}
			catch (Exception ex)
			{
				_coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Inventory.ToString(), TraceLevel.Error);
				return CreateInternalServerErrorResponse(new InventoryGeneralItemMasterResponse { HasError = true, ErrorMessage = ex.Message });
			}
		}

		[Route("/InventoryGeneralItemMaster/UpdateInventoryGeneralItemMaster")]
		[HttpPut, ValidateModel]
		[Produces(typeof(InventoryGeneralItemMasterResponse))]
		public virtual IActionResult UpdateInventoryGeneralItemMaster([FromBody] InventoryGeneralItemMasterModel model)
		{
			try
			{
				bool isUpdated = _inventoryGeneralItemMasterService.UpdateInventoryGeneralItemMaster(model);
				return isUpdated ? CreateOKResponse(new InventoryGeneralItemMasterResponse { InventoryGeneralItemMasterModel = model }) : CreateInternalServerErrorResponse();
			}
			catch (CoditechException ex)
			{
				_coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Inventory.ToString(), TraceLevel.Warning);
				return CreateInternalServerErrorResponse(new InventoryGeneralItemMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
			}
			catch (Exception ex)
			{
				_coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Inventory.ToString(), TraceLevel.Error);
				return CreateInternalServerErrorResponse(new InventoryGeneralItemMasterResponse { HasError = true, ErrorMessage = ex.Message });
			}
		}

		[Route("/InventoryGeneralItemMaster/DeleteInventoryGeneralItemMaster")]
		[HttpPost, ValidateModel]
		[Produces(typeof(TrueFalseResponse))]
		public virtual IActionResult DeleteInventoryGeneralItemMaster([FromBody] ParameterModel inventoryGeneralItemMasterIds)
		{
			try
			{
				bool deleted = _inventoryGeneralItemMasterService.DeleteInventoryGeneralItemMaster(inventoryGeneralItemMasterIds);
				return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
			}
			catch (CoditechException ex)
			{
				_coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Inventory.ToString(), TraceLevel.Warning);
				return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
			}
			catch (Exception ex)
			{
				_coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Inventory.ToString(), TraceLevel.Error);
				return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
			}
		}

		[HttpGet]
		[Route("/InventoryGeneralItemMaster/GetGeneralServicesList")]
		[Produces(typeof(InventoryGeneralItemMasterListResponse))]
		public virtual IActionResult GetGeneralServicesList(string searchText = null)
		{
			try
			{
				InventoryGeneralItemMasterListModel list = _inventoryGeneralItemMasterService.GetGeneralServicesList(searchText);
				string data = ApiHelper.ToJson(list);
				return !string.IsNullOrEmpty(data) ? CreateOKResponse<InventoryGeneralItemMasterListResponse>(data) : CreateNoContentResponse();
			}
			catch (CoditechException ex)
			{
				_coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Inventory.ToString(), TraceLevel.Error);
				return CreateInternalServerErrorResponse(new InventoryGeneralItemMasterListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
			}
			catch (Exception ex)
			{
				_coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Inventory.ToString(), TraceLevel.Error);
				return CreateInternalServerErrorResponse(new InventoryGeneralItemMasterListResponse { HasError = true, ErrorMessage = ex.Message });
			}
		}
	}
}