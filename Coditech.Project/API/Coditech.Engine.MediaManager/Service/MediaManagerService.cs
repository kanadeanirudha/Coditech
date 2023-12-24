using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;

using System.Configuration;
using System.Linq;

using static Coditech.Common.Helper.HelperUtility;
using static System.Net.Mime.MediaTypeNames;
namespace Coditech.API.Service
{
    public class MediaManagerService : BaseService, IMediaManagerService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        public static IWebHostEnvironment _environment;
        private readonly ICoditechRepository<MediaTypeMaster> _mediaTypeMasterRepository;
        private readonly ICoditechRepository<MediaSettingMaster> _mediaSettingMasterRepository;
        private readonly ICoditechRepository<MediaTypeExtensionMaster> _mediaTypeExtensionMasterRepository;
        private readonly ICoditechRepository<MediaConfiguration> _mediaConfigurationRepository;
        private readonly ICoditechRepository<MediaFolderMaster> _mediaFolderMasterRepository;
        private readonly ICoditechRepository<MediaDetail> _mediaDetailRepository;
        public MediaManagerService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider, IWebHostEnvironment environment) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _environment = environment;
            _mediaTypeMasterRepository = new CoditechRepository<MediaTypeMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _mediaSettingMasterRepository = new CoditechRepository<MediaSettingMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _mediaTypeExtensionMasterRepository = new CoditechRepository<MediaTypeExtensionMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _mediaConfigurationRepository = new CoditechRepository<MediaConfiguration>(_serviceProvider.GetService<Coditech_Entities>());
            _mediaFolderMasterRepository = new CoditechRepository<MediaFolderMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _mediaDetailRepository = new CoditechRepository<MediaDetail>(_serviceProvider.GetService<Coditech_Entities>());
        }

        #region Public
        public virtual UploadMediaModel UploadMedia(UploadMediaModel model)
        {
            if (IsNull(model))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (string.IsNullOrEmpty(model.MediaFolderName) || model.MediaFolderMasterId == 0)
                throw new CoditechException(ErrorCodes.InvalidFolderPath, GeneralResources.ErrorInvalidFolderPath);


            if (ValidateMediaBeforeUpload(model))
            {
                MediaConfiguration mediaConfiguration = _mediaConfigurationRepository.Table.FirstOrDefault(x => x.IsActive);
                string imageUploadPath = mediaConfiguration.Server == "Network Drive" ? Path.Combine(mediaConfiguration?.URL + mediaConfiguration.BucketName) : $"{AppDomain.CurrentDomain.BaseDirectory}Data\\Media\\";
                int? mediaFolderMasterId = model.MediaFolderMasterId == 0 ? _mediaFolderMasterRepository.Table.FirstOrDefault(x => x.FolderName.Equals(model.MediaFolderName, StringComparison.InvariantCultureIgnoreCase))?.MediaFolderMasterId : model.MediaFolderMasterId;
                bool isFolderExixst = true;
                if (mediaFolderMasterId == 0)
                {
                    isFolderExixst = false;
                    MediaFolderMaster mediaFolderMaster = new MediaFolderMaster()
                    {
                        FolderName = model.MediaFolderName,
                        IsActive = true
                    };
                    mediaFolderMaster = _mediaFolderMasterRepository.Insert(mediaFolderMaster);
                    if (mediaFolderMaster.MediaFolderMasterId > 0)
                    {
                        mediaFolderMasterId = mediaFolderMaster.MediaFolderMasterId;
                    }
                }
                //Check media is already exixt
                MediaDetail mediaDetail = isFolderExixst ? _mediaDetailRepository.Table.FirstOrDefault(x => x.MediaFolderMasterId == mediaFolderMasterId && x.FileName.Equals(model.MediaFile.FileName)) : new MediaDetail();
                bool fileAlreadyExist = false;
                if (mediaDetail?.MediaId > 0)
                {
                  
                }
                else
                {
                    mediaDetail.MediaConfigurationId = mediaConfiguration.MediaConfigurationId;
                    mediaDetail.MediaFolderMasterId = Convert.ToInt32(mediaFolderMasterId);
                    mediaDetail.Path = "";
                    mediaDetail.FileName = model.MediaFile.FileName;
                    mediaDetail.Size = Convert.ToString(model.MediaFile.Length);
                    mediaDetail.Height = "0";
                    mediaDetail.Width = "0";
                    mediaDetail.Length = Convert.ToString(model.MediaFile.Length);
                    mediaDetail.Type = model.MediaFile.Name;
                    mediaDetail = _mediaDetailRepository.Insert(mediaDetail);
                    if (mediaDetail.ModifiedBy > 0)
                    {
                        using (FileStream filestream = File.Create($"{_environment.WebRootPath}\\{mediaConfiguration.BucketName}\\{model.MediaFile.FileName}"))
                        {
                            model.MediaFile.CopyTo(filestream);
                            filestream.Flush();
                        }
                    }
                }
            }
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
            bool status = true;
            byte? mediaTypeMasterId = _mediaTypeMasterRepository.Table.FirstOrDefault(x => x.MediaType == model.MediaType)?.MediaTypeMasterId;
            if (mediaTypeMasterId == 0)
                return status;

            MediaSettingMaster mediaSettingMaster = _mediaSettingMasterRepository.Table.FirstOrDefault(x => x.MediaTypeMasterId == mediaTypeMasterId);
            if (IsNull(mediaSettingMaster))
                return status;

            //Check Validate the file name.
            if (!IsValidFileName(model.MediaFile.FileName))
            {
                throw new CoditechException(ErrorCodes.InvalidFileName, GeneralResources.ErrorInvalidFileName);
            }


            //Check Validate the file size limit in MB.
            if ((model.MediaFile.Length / 1024000M) > mediaSettingMaster.MaxSizeInMB)
            {
                throw new CoditechException(ErrorCodes.FileSizeLimitExceed, string.Format(GeneralResources.ErrorFileSizeLimitExceed, mediaSettingMaster.MaxSizeInMB));
            }

            List<string> extensionNameList = _mediaTypeExtensionMasterRepository.Table.Where(x => mediaSettingMaster.MediaTypeExtensionMasterIds.Contains(Convert.ToString(x.MediaTypeExtensionMasterId)))?.Select(y => y.ExtensionName)?.ToList();

            //Check Validate the file extension.
            if (extensionNameList?.Count > 0 && !extensionNameList.Any(ext => model.MediaFile.Name.EndsWith(ext, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new CoditechException(ErrorCodes.InvalidFileExtension, GeneralResources.ErrorInvalidFileExtension);
            }
            return status;
        }

        protected virtual bool IsValidFileName(string fileName)
        {
            char[] invalidChars = Path.GetInvalidFileNameChars();

            // Check if the file name contains any invalid characters
            if (fileName.Any(c => invalidChars.Contains(c)))
            {
                return false;
            }

            // Additional custom checks for special characters like < > # % + { } | \ ^ ~ [ ]
            char[] specialChars = new char[] { '<', '>', '#', '%', '+', '{', '}', '|', '\\', '^', '~', '[', ']' };

            // Check if the file name contains any additional special characters
            if (fileName.Any(c => specialChars.Contains(c)))
            {
                return false;
            }

            // If no invalid or special characters were found, the file name is valid
            return true;
        }

        #endregion
    }
}
