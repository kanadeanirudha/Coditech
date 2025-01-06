using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Microsoft.AspNetCore.Http;
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

        public virtual ActionResult Index(DataTableViewModel dataTableViewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                MediaManagerFolderListViewModel mediaViewModel = _mediaManagerFolderAgent.GetFolderStructure(dataTableViewModel);
                if (AjaxHelper.IsAjaxRequest)
                {
                    return PartialView($"~/Views/MediaManager/MediaManagerDetails/_MediaDetails.cshtml", mediaViewModel);
                }
                else
                {
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
        public virtual ActionResult MoveFolder()
        {
            return RedirectToAction("Index", "MediaManager");
        }

        [Route("/MediaManager/UploadFile")]
        [HttpPost]
        public virtual ActionResult UploadFile(int folderId)
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

                MediaModel uploadMediaModel = _mediaManagerFolderAgent.UploadFile(folderId, 0, file);

                SetNotificationMessage(!uploadMediaModel.HasError
                       ? GetSuccessNotificationMessage("File successfully uploaded.")
                       : GetErrorNotificationMessage(uploadMediaModel.ErrorMessage));

                return RedirectToAction("Index", new DataTableViewModel { SelectedParameter1 = folderId.ToString() });
            }
            else
            {
                return RedirectToAction<UserController>(x => x.Login(string.Empty));
            }
        }

        [Route("/MediaManager/ReplaceFile")]
        [HttpPost]
        public virtual ActionResult ReplaceFile(int folderId, long mediaId)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (mediaId == 0) {
                    return Json(new { success = false, message = "Failed to replace media." });
                }
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

                MediaModel uploadMediaModel = _mediaManagerFolderAgent.UploadFile(folderId, mediaId, file);

                SetNotificationMessage(!uploadMediaModel.HasError
                       ? GetSuccessNotificationMessage("File successfully replaced.")
                       : GetErrorNotificationMessage(uploadMediaModel.ErrorMessage));

                return RedirectToAction<MediaManagerController>(x => x.GetMediaDetails(mediaId));
            }
            else
            {
                return RedirectToAction<UserController>(x => x.Login(string.Empty));
            }
        }

        [HttpGet]
        public virtual ActionResult GetMediaDetails(long mediaId)
        {
            if (User.Identity.IsAuthenticated)
            {
                MediaModel model = _mediaManagerFolderAgent.GetMediaDetails(mediaId);
                return ActionView("~/Views/MediaManager/MediaManagerDetails/ViewMediaDetails.cshtml", model);
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

                return RedirectToAction("Index", new DataTableViewModel { SelectedParameter1 = rootFolderId.ToString() });
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

                    return RedirectToAction<MediaManagerController>(x => x.CreateFolder());
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

                    return RedirectToAction("Index", new DataTableViewModel { SelectedParameter1 = activeFolderId.ToString() });
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

                return RedirectToAction("Index", new DataTableViewModel { SelectedParameter1 = folderId.ToString() });
            }
            else
            {
                return RedirectToAction<UserController>(x => x.Login(string.Empty));
            }
        }

        [Route("/MediaManager/GetFolderDropdown")]
        [HttpGet]
        public virtual JsonResult GetFolderDropdown(int excludeFolderId)
        {
            FolderListViewModel folders = _mediaManagerFolderAgent.GetAllFolders(excludeFolderId);

            return Json(folders.Folders);
        }

        [Route("/MediaManager/MoveFolder")]
        [HttpPost]
        public virtual ActionResult MoveFolder(int folderId, int destinationFolderId)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    bool status = _mediaManagerFolderAgent.MoveFolder(folderId, destinationFolderId);

                    SetNotificationMessage(status
                        ? GetSuccessNotificationMessage("Moved successfully.")
                        : GetErrorNotificationMessage("Failed to move."));

                    return RedirectToAction("Index", new DataTableViewModel { SelectedParameter1 = folderId.ToString() });
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
