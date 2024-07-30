using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
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
                   ? GetErrorNotificationMessage("Image uploaded successfully.")
                   : GetSuccessNotificationMessage("Failed to upload a image."));

            MediaManagerFolderListViewModel mediaViewModel = _mediaManagerFolderAgent.GetFolderStructure(folderId);

            return View($"~/Views/MediaManager/MediaManagerDetails/MediaUpload.cshtml", mediaViewModel);
        }

        [Route("/MediaManager/CreateFolder")]
        [HttpPost]
        public virtual ActionResult CreateFolder(int rootFolderId, string folderName)
        {
            bool status = _mediaManagerFolderAgent.CreateFolder(rootFolderId, folderName);

            SetNotificationMessage(status
                    ? GetErrorNotificationMessage("Folder created successfully.")
                    : GetSuccessNotificationMessage("Failed to create a folder."));

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
                    ? GetErrorNotificationMessage("Folders/Files are successfully deleted.")
                    : GetSuccessNotificationMessage("Failed to delete."));

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
                    ? GetErrorNotificationMessage("Renamed successfully.")
                    : GetSuccessNotificationMessage("Failed to rename."));

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
                    ? GetErrorNotificationMessage("Moved successfully.")
                    : GetSuccessNotificationMessage("Failed to move."));

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
