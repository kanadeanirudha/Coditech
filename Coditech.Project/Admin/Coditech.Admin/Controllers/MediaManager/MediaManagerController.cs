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

        public IActionResult Index(int rootFolderId = 0, DataTableViewModel dataTableViewModel = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (AjaxHelper.IsAjaxRequest)
                {
                    MediaManagerFolderListViewModel mediaViewModel = _mediaManagerFolderAgent.GetFolderStructure(rootFolderId, dataTableViewModel);
                    return PartialView($"~/Views/MediaManager/MediaManagerDetails/_MediaDetails.cshtml", mediaViewModel);
                }
                else
                {
                    MediaManagerFolderListViewModel mediaViewModel = _mediaManagerFolderAgent.GetFolderStructure(0, dataTableViewModel);
                    return View($"~/Views/MediaManager/MediaManagerDetails/MediaUpload.cshtml", mediaViewModel);
                }
            }
            else
            {
                return RedirectToAction<UserController>(x => x.Login(string.Empty));
            }
        }

        [Route("/MediaManager/UploadFile")]
        public virtual ActionResult PostUploadImage()
        {
            return RedirectToAction("Index", "MediaManager");
        }

        [Route("/MediaManager/CreateFolder")]
        public virtual ActionResult CreateFolder()
        {
            return RedirectToAction("Index", "MediaManager");
        }

        [Route("/MediaManager/DeleteFolder")]
        public virtual ActionResult DeleteFolder()
        {
            return RedirectToAction("Index", "MediaManager");
        }

        [Route("/MediaManager/DeleteFile")]
        public virtual ActionResult DeleteFile()
        {
            return RedirectToAction("Index", "MediaManager");
        }

        [Route("/MediaManager/RenameFolder")]
        public virtual ActionResult RenameFolder()
        {
            return RedirectToAction("Index", "MediaManager");
        }

        [Route("/MediaManager/MoveFolder")]
        public ActionResult MoveFolder()
        {
            return RedirectToAction("Index", "MediaManager");
        }

        [Route("/MediaManager/UploadFile")]
        [HttpPost]
        public virtual ActionResult PostUploadImage(int folderId)
        {
            if (User.Identity.IsAuthenticated)
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

                BooleanModel status = _mediaManagerFolderAgent.UploadFile(folderId, file);

                SetNotificationMessage(status.IsSuccess
                       ? GetSuccessNotificationMessage(status.SuccessMessage)
                       : GetErrorNotificationMessage(status.ErrorMessage));

                return RedirectToAction("Index", "MediaManager", new { rootFolderId = folderId });
            }
            else
            {
                return RedirectToAction<UserController>(x => x.Login(string.Empty));
            }
        }

        [Route("/MediaManager/CreateFolder")]
        [HttpPost]
        public virtual ActionResult CreateFolder(int rootFolderId, string folderName)
        {
            if (User.Identity.IsAuthenticated)
            {
                BooleanModel booleanModel = _mediaManagerFolderAgent.CreateFolder(rootFolderId, folderName);

                SetNotificationMessage(booleanModel.IsSuccess
                        ? GetSuccessNotificationMessage(booleanModel.SuccessMessage)
                        : GetErrorNotificationMessage(booleanModel.ErrorMessage));

                MediaManagerFolderListViewModel mediaViewModel = _mediaManagerFolderAgent.GetFolderStructure(rootFolderId);

                return View($"~/Views/MediaManager/MediaManagerDetails/MediaUpload.cshtml", mediaViewModel);
            }
            else
            {
                return RedirectToAction<UserController>(x => x.Login(string.Empty));
            }
        }

        [Route("/MediaManager/DeleteFolder")]
        [HttpPost]
        public virtual ActionResult DeleteFolder(int folderId)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    bool status = _mediaManagerFolderAgent.DeleteFolder(folderId);

                    SetNotificationMessage(status
                        ? GetSuccessNotificationMessage("Folders/Files are successfully deleted.")
                        : GetErrorNotificationMessage("Failed to delete."));

                    MediaManagerFolderListViewModel mediaViewModel = _mediaManagerFolderAgent.GetFolderStructure(0);

                    return View($"~/Views/MediaManager/MediaManagerDetails/MediaUpload.cshtml", mediaViewModel);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
            else
            {
                return RedirectToAction<UserController>(x => x.Login(string.Empty));
            }
        }

        [Route("/MediaManager/DeleteFile")]
        [HttpPost]
        public virtual ActionResult DeleteFile(int activeFolderId, int mediaId)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    bool status = _mediaManagerFolderAgent.DeleteFile(mediaId);

                    SetNotificationMessage(status
                        ? GetSuccessNotificationMessage("File is successfully deleted.")
                        : GetErrorNotificationMessage("Failed to delete."));

                    MediaManagerFolderListViewModel mediaViewModel = _mediaManagerFolderAgent.GetFolderStructure(activeFolderId);

                    return View($"~/Views/MediaManager/MediaManagerDetails/MediaUpload.cshtml", mediaViewModel);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
            else
            {
                return RedirectToAction<UserController>(x => x.Login(string.Empty));
            }
        }

        [Route("/MediaManager/RenameFolder")]
        [HttpPost]
        public virtual ActionResult RenameFolder(int folderId, string folderName)
        {
            if (User.Identity.IsAuthenticated)
            {
                bool status = _mediaManagerFolderAgent.RenameFolder(folderId, folderName);

                SetNotificationMessage(status
                        ? GetSuccessNotificationMessage("Renamed successfully.")
                        : GetErrorNotificationMessage("Failed to rename."));

                MediaManagerFolderListViewModel mediaViewModel = _mediaManagerFolderAgent.GetFolderStructure(folderId);

                return View($"~/Views/MediaManager/MediaManagerDetails/MediaUpload.cshtml", mediaViewModel);
            }
            else
            {
                return RedirectToAction<UserController>(x => x.Login(string.Empty));
            }
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
            if (User.Identity.IsAuthenticated)
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
            else
            {
                return RedirectToAction<UserController>(x => x.Login(string.Empty));
            }
        }

    }
}
