using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class MediaManagerController : BaseController
    {
        private readonly IMediaManagerFolderAgent _mediaManagerFolderAgent;
        public MediaManagerController(IMediaManagerFolderAgent mediaManagerFolderAgent)
        {
            _mediaManagerFolderAgent = mediaManagerFolderAgent;
        }

        public IActionResult Index(int rootFolderId = 0)
        {
            if (AjaxHelper.IsAjaxRequest)
            {
                MediaManagerFolderListViewModel mediaViewModel = _mediaManagerFolderAgent.GetFolderStructure(rootFolderId);
                return PartialView($"~/Views/MediaManager/MediaManagerDetails/_MediaDetails.cshtml", mediaViewModel);
            }
            else
            {
                MediaManagerFolderListViewModel mediaViewModel = _mediaManagerFolderAgent.GetFolderStructure(0);
                return View($"~/Views/MediaManager/MediaManagerDetails/MediaUpload.cshtml", mediaViewModel);
            }
        }

        [Route("/MediaManager/UploadFile")]
        [HttpPost]
        public virtual ActionResult PostUploadImage(int folderId)
        {
            IFormFileCollection filess = Request.Form.Files;
            if (filess.Count == 0)
            {
                return Json(new { success = false, message = "No file uploaded." });
            }

            IFormFile file = filess[0];

            if (file.Length == 0)
            {
                return Json(new { success = false, message = "Empty file uploaded." });
            }

            bool status = _mediaManagerFolderAgent.UploadFile(folderId, file);

            SetNotificationMessage(status
                   ? GetSuccessNotificationMessage("Image uploaded successfully.")
                   : GetErrorNotificationMessage("Failed to upload a image."));

            MediaManagerFolderListViewModel mediaViewModel = _mediaManagerFolderAgent.GetFolderStructure(folderId);

            return View($"~/Views/MediaManager/MediaManagerDetails/MediaUpload.cshtml", mediaViewModel);
        }

        [Route("/MediaManager/CreateFolder")]
        [HttpPost]
        public virtual ActionResult CreateFolder(int rootFolderId, string folderName)
        {
            BooleanModel booleanModel = _mediaManagerFolderAgent.CreateFolder(rootFolderId, folderName);

            SetNotificationMessage(booleanModel.IsSuccess
                    ? GetSuccessNotificationMessage(booleanModel.SuccessMessage)
                    : GetErrorNotificationMessage(booleanModel.ErrorMessage));

            MediaManagerFolderListViewModel mediaViewModel = _mediaManagerFolderAgent.GetFolderStructure(rootFolderId);

            return View($"~/Views/MediaManager/MediaManagerDetails/MediaUpload.cshtml", mediaViewModel);
        }

        [Route("/MediaManager/DeleteFolder")]
        [HttpPost]
        public virtual ActionResult DeleteFolder(int folderId)
        {
            try
            {
                bool status = _mediaManagerFolderAgent.DeleteFolder(folderId);

                SetNotificationMessage(status
                    ? GetSuccessNotificationMessage("Folders/Files are successfully deleted.")
                    : GetErrorNotificationMessage("Failed to delete."));

                MediaManagerFolderListViewModel mediaViewModel = _mediaManagerFolderAgent.GetFolderStructure(0);

                return PartialView($"~/Views/MediaManager/MediaManagerDetails/_MediaDetails.cshtml", mediaViewModel);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [Route("/MediaManager/RenameFolder")]
        [HttpPost]
        public virtual ActionResult RenameFolder(int folderId, string folderName) 
        {
            bool status = _mediaManagerFolderAgent.RenameFolder(folderId, folderName);

            SetNotificationMessage(status
                    ? GetSuccessNotificationMessage("Renamed successfully.")
                    : GetErrorNotificationMessage("Failed to rename."));

            MediaManagerFolderListViewModel mediaViewModel = _mediaManagerFolderAgent.GetFolderStructure(folderId);

            return View($"~/Views/MediaManager/MediaManagerDetails/MediaUpload.cshtml", mediaViewModel);
        }

        [Route("/MediaManager/GetFolderDropdown")]
        [HttpGet]
        public JsonResult GetFolderDropdown(int excludeFolderId)
        {
            FolderListViewModel folders = _mediaManagerFolderAgent.GetAllFolders(excludeFolderId);
            
            return Json(folders.Folders);
        }

        [Route("/MediaManager/MoveFolder")]
        [HttpPost]
        public ActionResult MoveFolder(int folderId, int destinationFolderId)
        {
            try
            {
                bool status = _mediaManagerFolderAgent.MoveFolder(folderId, destinationFolderId);

                SetNotificationMessage(status
                    ? GetSuccessNotificationMessage("Moved successfully.")
                    : GetErrorNotificationMessage("Failed to move."));

                MediaManagerFolderListViewModel mediaViewModel = _mediaManagerFolderAgent.GetFolderStructure(folderId);

                return View($"~/Views/MediaManager/MediaManagerDetails/MediaUpload.cshtml", mediaViewModel);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}
