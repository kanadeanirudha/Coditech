using Coditech.Admin.Agents;
using Coditech.Admin.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Coditech.Admin.Controllers
{
    public class MediaManagerController : BaseController
    {
        private readonly IMediaManagerFolderAgent _mediaManagerFolderAgent;
        public MediaManagerController(IMediaManagerFolderAgent mediaManagerFolderAgent)
        {
            _mediaManagerFolderAgent = mediaManagerFolderAgent;
        }

        public IActionResult Index()
        {
            MediaManagerFolderListViewModel mediaViewModel = _mediaManagerFolderAgent.GetFolderStructure(0);
            return View($"~/Views/MediaManager/MediaManagerDetails/MediaUpload.cshtml", mediaViewModel);
        }

        public IActionResult GetFolderStructureById(int rootFolderId)
        {
            MediaManagerFolderListViewModel mediaViewModel = _mediaManagerFolderAgent.GetFolderStructure(rootFolderId);
            return PartialView($"~/Views/MediaManager/MediaManagerDetails/_MediaDetails.cshtml", mediaViewModel);
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

            bool response = _mediaManagerFolderAgent.UploadFile(folderId, file);

            MediaManagerFolderListViewModel mediaViewModel = _mediaManagerFolderAgent.GetFolderStructure(folderId);

            return View($"~/Views/MediaManager/MediaManagerDetails/MediaUpload.cshtml", mediaViewModel);
        }

        [Route("/MediaManager/CreateFolder")]
        [HttpPost]
        public virtual ActionResult CreateFolder(int rootFolderId, string folderName)
        {
            bool response = _mediaManagerFolderAgent.CreateFolder(rootFolderId, folderName);

            MediaManagerFolderListViewModel mediaViewModel = _mediaManagerFolderAgent.GetFolderStructure(rootFolderId);

            return View($"~/Views/MediaManager/MediaManagerDetails/MediaUpload.cshtml", mediaViewModel);
        }
    }
}
