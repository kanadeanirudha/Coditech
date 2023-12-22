using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Logger;
using Coditech.Common.Service;

namespace Coditech.API.Service
{
    public class MediaManagerService : BaseService, IMediaManagerService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        public static IWebHostEnvironment _environment;
        private readonly ICoditechRepository<MediaTypeMaster> _mediaTypeMasterRepository;
        private readonly ICoditechRepository<MediaSettingMaster> _mediaSettingMasterRepository;
        public MediaManagerService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider, IWebHostEnvironment environment) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _environment = environment;
            _mediaTypeMasterRepository = new CoditechRepository<MediaTypeMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _mediaSettingMasterRepository = new CoditechRepository<MediaSettingMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        #region Public
        public virtual UploadMediaModel UploadMedia(UploadMediaModel model)
        {
           
            //foreach (IFormFile file in model?.MediaFileList)
            //{
            //    if (!Directory.Exists(_environment.WebRootPath + "\\Upload"))
            //    {
            //        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
            //    }
            //    using (FileStream filestream = File.Create(_environment.WebRootPath + "\\Upload\\" + file.FileName))
            //    {
            //        string ImgName = "\\Upload\\" + file.FileName;
            //        file.CopyTo(filestream);
            //        filestream.Flush();
            //    }
            //}

            return model;
        }

        #endregion

        #region Protected Method
        protected virtual bool ValidateMediaBeforeUpload(UploadMediaModel model)
        {
            bool status = false;
            byte? mediaTypeMasterId = _mediaTypeMasterRepository.Table.FirstOrDefault(x => x.MediaType == model.MediaType)?.MediaTypeMasterId;
            MediaSettingMaster mediaSettingMaster = _mediaSettingMasterRepository.Table.FirstOrDefault(x => x.MediaTypeMasterId == mediaTypeMasterId);
      
            return true;
        }
        #endregion
    }
}
