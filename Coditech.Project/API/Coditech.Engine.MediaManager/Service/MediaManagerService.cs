using Coditech.API.Data;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

using System.Data;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Service
{
    public class MediaManagerService : BaseService, IMediaManagerService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        public static IWebHostEnvironment _environment;
        public MediaManagerService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider, IWebHostEnvironment environment) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _environment = environment;
        }

        #region Public
        public virtual UploadMediaModel UploadMedia(UploadMediaModel model)
        {
            if (model?.MediaFileList?.Count == 0)
            {
            }

            foreach (IFormFile file in model?.MediaFileList)
            {

                if (!Directory.Exists(_environment.WebRootPath + "\\Upload"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                }
                using (FileStream filestream = File.Create(_environment.WebRootPath + "\\Upload\\" + file.FileName))
                {
                    string ImgName = "\\Upload\\" + file.FileName;
                    file.CopyTo(filestream);
                    filestream.Flush();
                }
            }

            return model;
        }
        #endregion

        #region Protected Method

        #endregion
    }
}
