using Coditech.Admin.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class InventoryItemTrackingDimensionController : BaseController
    {
        private const string createEdit = "~/Views/Inventory/InventoryItemTrackingDimension/CreateEdit.cshtml";

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new InventoryItemTrackingDimensionViewModel());
        }
    }
}
