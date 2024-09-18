using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;

using ImageMagick;

using static Coditech.Common.Helper.HelperUtility;
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
        private readonly ICoditechRepository<MediaGlobalDisplaySetting> _mediaGlobalDisplaySettingRepository;
        private readonly ICoditechRepository<AdminRoleMediaFolders> _adminRoleMediaFolderRepository;
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
            _mediaGlobalDisplaySettingRepository = new CoditechRepository<MediaGlobalDisplaySetting>(_serviceProvider.GetService<Coditech_Entities>());
            _adminRoleMediaFolderRepository = new CoditechRepository<AdminRoleMediaFolders>(_serviceProvider.GetService<Coditech_Entities>());
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
            return model;
        }

        public async Task<MediaManagerResponse> UploadServerFiles(IEnumerable<IFormFile> files, HttpRequest request)
        {
            try
            {
                if (MultipartRequestHelper.IsMultipartContentType(request.ContentType))
                {
                    string projectPath = Directory.GetCurrentDirectory();
                    string uploadPath = Path.Combine(projectPath, "Data", "Media");
                    Directory.CreateDirectory(uploadPath);


                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                            string filePath = Path.Combine(uploadPath, uniqueFileName);

                            // Save the file to the server
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            var size = Convert.ToString(file.Length);
                            var type = file.ContentType;
                            var imagepath = filePath;

                            // Generate URL to access the file
                            var fileUrl = $"{GetMediaUrl()}{uniqueFileName}";
                            var height = string.Empty; var width = string.Empty;

                            if (file.ContentType.StartsWith("image"))
                            {
                                using (var image = System.Drawing.Image.FromStream(file.OpenReadStream()))
                                {
                                    width = Convert.ToString(image.Width);
                                    height = Convert.ToString(image.Height);
                                }
                            }

                            var result = _mediaDetailRepository.Insert(new MediaDetail()
                            {
                                MediaConfigurationId = 1,
                                MediaFolderMasterId = 1,
                                Path = uniqueFileName,
                                FileName = file.FileName,
                                Size = size,
                                Length = size,
                                Height = height,
                                Width = width,
                                Type = type
                            });

                            return new MediaManagerResponse()
                            {
                                UploadMediaModel = new UploadMediaModel()
                                {
                                    MediaId = result.MediaId,
                                    MediaPathUrl = fileUrl
                                }
                            };
                        }
                    }

                }
                else
                {
                    return null;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TrueFalseResponse> UploadFile(IFormFile formFile, int folderId, HttpRequest request)
        {
            try
            {
                if (MultipartRequestHelper.IsMultipartContentType(request.ContentType))
                {

                    TrueFalseResponse isFileValid = IsFileValid(formFile);

                    if (isFileValid.HasError)
                        return isFileValid;

                    //string path = $"{AppDomain.CurrentDomain.BaseDirectory}Data\\Media\\";
                    string projectPath = Directory.GetCurrentDirectory();
                    string uploadPath = Path.Combine(projectPath, "Data", "Media");
                    Directory.CreateDirectory(uploadPath);

                    if (formFile.Length > 0)
                    {
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(formFile.FileName);
                        string filePath = Path.Combine(uploadPath, uniqueFileName);

                        // Save the file to the server
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }

                        var size = Convert.ToString(formFile.Length);
                        var type = formFile.ContentType;
                        var imagepath = filePath;

                        // Generate URL to access the file
                        var fileUrl = $"{GetMediaUrl()}{uniqueFileName}";
                        var height = string.Empty; var width = string.Empty;

                        if (formFile.ContentType.StartsWith("image"))
                        {
                            using (var image = System.Drawing.Image.FromStream(formFile.OpenReadStream()))
                            {
                                width = Convert.ToString(image.Width);
                                height = Convert.ToString(image.Height);
                            }
                        }

                        var result = _mediaDetailRepository.Insert(new MediaDetail()
                        {
                            MediaConfigurationId = 1,
                            MediaFolderMasterId = folderId,
                            Path = uniqueFileName,
                            FileName = formFile.FileName,
                            Size = size,
                            Length = size,
                            Height = height,
                            Width = width,
                            Type = type
                        });

                        if (result.MediaId > 0)
                        {
                            return new TrueFalseResponse() { booleanModel = new BooleanModel() { SuccessMessage = "File successfully uploaded.", IsSuccess = true }, IsSuccess = true };
                        }
                        else
                        {
                            return new TrueFalseResponse() { booleanModel = new BooleanModel() { ErrorMessage = "Failed to upload a file.", IsSuccess = false, HasError = true }, IsSuccess = false };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new TrueFalseResponse() { booleanModel = new BooleanModel() { ErrorMessage = "Failed to upload a file.", IsSuccess = false, HasError = true }, IsSuccess = false };
            }
            return new TrueFalseResponse() { booleanModel = new BooleanModel() { ErrorMessage = "Failed to upload a file.", IsSuccess = false, HasError = true }, IsSuccess = false };
        }

        public async Task<MediaManagerFolderResponse> GetFolderStructure(int rootFolderId = 0, int adminRoleId = 0, bool isAdminUser = false)
        {
            try
            {
                MediaManagerFolderResponse managerFolderResponse = new();
                List<MediaFolderMaster> mediaFolderMasterList = [.. _mediaFolderMasterRepository.Table];

                MediaFolderMaster rootMediaFolder = mediaFolderMasterList.FirstOrDefault(x => x.MediaFolderParentId == 0);

                int activeFolderId = rootFolderId > 0 ? rootFolderId : rootMediaFolder.MediaFolderMasterId;

                List<int> adminRoleMediaFolders = new List<int>();
                if (!isAdminUser)
                {
                    adminRoleMediaFolders = [.._adminRoleMediaFolderRepository.Table.Where(x => x.AdminRoleMasterId == adminRoleId).Select(y => y.MediaFolderMasterId)];
                    if (!adminRoleMediaFolders.Contains(activeFolderId))
                    {
                        activeFolderId = 0;
                    }
                }

                managerFolderResponse.MediaManagerFolderModel = new MediaManagerFolderModel
                {                   
                    MediaRootFolder = new MediaFolderStructure()
                    {
                        SubFolders = GetSubFolders(rootMediaFolder.MediaFolderMasterId, mediaFolderMasterList, ref activeFolderId, adminRoleMediaFolders),
                        RootFolderId = rootMediaFolder.MediaFolderMasterId,
                        RootFolderName = rootMediaFolder.FolderName,
                        IsActiveFolder = activeFolderId == rootMediaFolder.MediaFolderMasterId,
                        adminRoleMediaFolders = adminRoleMediaFolders                       
                    },
                    ActiveFolderId = activeFolderId
                };

                List<int> folderIds = GetChildFolderIdsRecursive(mediaFolderMasterList, activeFolderId);

                folderIds.Add(activeFolderId);

                managerFolderResponse.MediaManagerFolderModel.MediaFiles = [.. (from media in _mediaDetailRepository.Table
                              where folderIds.Contains(media.MediaFolderMasterId)
                              select new Media()
                              {
                                  MediaId = media.MediaId,
                                  MediaName = media.FileName,
                                  MediaPath = media.Path,
                                  MediaSize = Convert.ToInt64(media.Size),
                                  ActiveFolderId = activeFolderId,
                                  ContentType = media.Type
                              })];

                long TotalFileSizeInByte = 0;

                string url = GetMediaUrl();
                foreach (Media media in managerFolderResponse.MediaManagerFolderModel.MediaFiles)
                {
                    TotalFileSizeInByte += media.MediaSize;

                    media.MediaPath = GetMediaPathUrl(media.ContentType, url, media.MediaPath); 
                }

                managerFolderResponse.MediaManagerFolderModel.TotalFileSize = TotalFileSizeInByte > 0 ? ConvertBytesToMegabytes(TotalFileSizeInByte) : 0;

                return await Task.FromResult(managerFolderResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FolderListResponse> GetAllFolders()
        {
            try
            {
                FolderListResponse folderListResponse = new();
                folderListResponse.FolderList.Folders = [.. (from folder in _mediaFolderMasterRepository.Table
                                                             select new Folder()
                                                             {
                                                                 FolderId = folder.MediaFolderMasterId,
                                                                 FolderName = folder.FolderName
                                                             })];
                return await Task.FromResult(folderListResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> MoveFolder(int folderId, int destinationFolderId)
        {
            try
            {
                MediaFolderMaster mediaFolderMaster = _mediaFolderMasterRepository.Table.Where(x => x.MediaFolderMasterId == folderId).FirstOrDefault();

                if (mediaFolderMaster != null)
                {
                    mediaFolderMaster.MediaFolderParentId = destinationFolderId;
                    mediaFolderMaster.ModifiedDate = DateTime.Now;

                    return await _mediaFolderMasterRepository.UpdateAsync(mediaFolderMaster);
                }
                return await Task.FromResult(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private double ConvertBytesToMegabytes(long bytes)
        {
            double megabytes = (double)bytes / 1048576;
            return Math.Round(megabytes, 2);
        }

        private async Task<List<MediaFolderMaster>> GetAllChildFoldersAsync(int folderId)
        {
            List<MediaFolderMaster> allFolders = new List<MediaFolderMaster>();
            await FetchChildFolders(folderId, allFolders);
            return allFolders;
        }

        private async Task FetchChildFolders(int parentId, List<MediaFolderMaster> allFolders)
        {
            var childFolders = _mediaFolderMasterRepository.Table
                                .Where(x => x.MediaFolderParentId == parentId)
                                .ToList();

            foreach (var folder in childFolders)
            {
                allFolders.Add(folder);
                await FetchChildFolders(folder.MediaFolderMasterId, allFolders);
            }
        }

        private async Task DeleteMediaFilesAsync(int folderId)
        {
            try
            {
                var mediaFiles = _mediaDetailRepository.Table
                                  .Where(x => x.MediaFolderMasterId == folderId)
                                  .ToList();

                foreach (var media in mediaFiles)
                {
                    string projectPath = Directory.GetCurrentDirectory();
                    string uploadedPath = Path.Combine(projectPath, "Data", "Media");
                    Directory.CreateDirectory(uploadedPath);

                    string filePath = Path.Combine(uploadedPath, media.Path);


                    // Delete the media file from the file system
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    _mediaDetailRepository.Delete(media);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public async Task<bool> DeleteFolder(int folderId)
        {
            try
            {
                // Get the folder to be deleted
                var mediaFolderMaster = _mediaFolderMasterRepository.Table
                                        .Where(x => x.MediaFolderMasterId == folderId)
                                        .FirstOrDefault();

                if (mediaFolderMaster != null)
                {
                    // Get all child folders
                    var allFolders = await GetAllChildFoldersAsync(folderId);

                    // Add the root folder to the list
                    allFolders.Add(mediaFolderMaster);

                    // Delete all media files and folders starting from the deepest child
                    foreach (var folder in allFolders.OrderByDescending(f => f.MediaFolderMasterId))
                    {
                        await DeleteMediaFilesAsync(folder.MediaFolderMasterId);
                        if (folder.MediaFolderMasterId != 1 && folder.FolderName != "Root")
                        {
                            _mediaFolderMasterRepository.Delete(folder);
                        }
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw;
            }
        }

        public async Task<bool> DeleteFile(int mediaId)
        {
            try
            {
                // Get the folder to be deleted
                var mediaFile = _mediaDetailRepository.Table
                                  .Where(x => x.MediaId == mediaId).FirstOrDefault();

                string projectPath = Directory.GetCurrentDirectory();
                string uploadedPath = Path.Combine(projectPath, "Data", "Media");
                Directory.CreateDirectory(uploadedPath);

                string filePath = Path.Combine(uploadedPath, mediaFile.Path);


                // Delete the media file from the file system
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                _mediaDetailRepository.Delete(mediaFile);

                return true;
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw;
            }
        }

        public async Task<bool> PostRenameFolder(int FolderId, string RenameFolderName)
        {
            if (FolderId > 0)
            {
                MediaFolderMaster mediaFolderMaster = _mediaFolderMasterRepository.Table.Where(x => x.MediaFolderMasterId == FolderId).FirstOrDefault();

                if (mediaFolderMaster != null)
                {
                    mediaFolderMaster.FolderName = RenameFolderName;
                    mediaFolderMaster.ModifiedDate = DateTime.Now;

                    return await _mediaFolderMasterRepository.UpdateAsync(mediaFolderMaster);
                }
            }
            return false;
        }

        public async Task<TrueFalseResponse> PostCreateFolder(int RootFolderId, string FolderName)
        {
            if (RootFolderId > 0)
            {
                bool isFolderExist = _mediaFolderMasterRepository.Table.Any(x => x.FolderName == FolderName && x.MediaFolderParentId == RootFolderId);

                if (isFolderExist)
                {
                    return new TrueFalseResponse() { booleanModel = new BooleanModel() { ErrorMessage = "Folder already exist.", IsSuccess = false, HasError = true }, IsSuccess = false };
                }

                MediaFolderMaster mediaFolderMaster = _mediaFolderMasterRepository.Table.Where(x => x.MediaFolderMasterId == RootFolderId).FirstOrDefault();

                if (mediaFolderMaster != null)
                {
                    MediaFolderMaster createFolder = new MediaFolderMaster();
                    createFolder.FolderName = FolderName;
                    createFolder.MediaFolderParentId = mediaFolderMaster.MediaFolderMasterId;
                    createFolder.IsActive = true;
                    createFolder.CreatedBy = 0;
                    createFolder.CreatedDate = DateTime.Now;
                    createFolder.ModifiedDate = DateTime.Now;
                    createFolder.ModifiedBy = 0;
                    MediaFolderMaster mediaFolder = await _mediaFolderMasterRepository.InsertAsync(createFolder);
                    if (mediaFolder.MediaFolderMasterId > 0)
                    {
                        return new TrueFalseResponse() { booleanModel = new BooleanModel() { SuccessMessage = "Folder successfully created.", IsSuccess = true }, IsSuccess = true };
                    }
                    else
                    {
                        return new TrueFalseResponse() { booleanModel = new BooleanModel() { ErrorMessage = "Failed to create a folder.", IsSuccess = false, HasError = true }, IsSuccess = false };
                    }
                }
            }
            return new TrueFalseResponse() { booleanModel = new BooleanModel() { ErrorMessage = "Folder Id not passed.", IsSuccess = false, HasError = true }, IsSuccess = false }; ;
        }

        private List<MediaFolderStructure> GetSubFolders(int parentId, List<MediaFolderMaster> allFolders, ref int activeFolderId, List<int> adminRoleMediaFolders)
        {
            if (activeFolderId == 0)
            {
                if (adminRoleMediaFolders.Contains(parentId))
                {
                    activeFolderId = parentId;
                }
            }

            var subFolders = allFolders.Where(x => x.MediaFolderParentId == parentId).ToList();
            var subFolderStructures = new List<MediaFolderStructure>();

            foreach (var subFolder in subFolders)
            {
                var subFolderStructure = new MediaFolderStructure
                {
                    SubFolders = GetSubFolders(subFolder.MediaFolderMasterId, allFolders, ref activeFolderId, adminRoleMediaFolders),
                    RootFolderId = subFolder.MediaFolderMasterId,
                    RootFolderName = subFolder.FolderName,
                    IsActiveFolder = activeFolderId == subFolder.MediaFolderMasterId,
                    adminRoleMediaFolders = adminRoleMediaFolders                   
                };
                subFolderStructures.Add(subFolderStructure);
            }

            return subFolderStructures;
        }

        private TrueFalseResponse IsFileValid(IFormFile formFile)
        {
            // Check if the file is null
            if (formFile == null)
            {
                return CreateErrorResponse("The file cannot be null.");
            }

            string contentType = formFile.ContentType;

            if (contentType.StartsWith("image"))
            {
                return ValidateFile(formFile, "Image");
            }
            else if (contentType.StartsWith("video"))
            {
                return ValidateFile(formFile, "Video");
            }
            else if (contentType.StartsWith("audio"))
            {
                return ValidateFile(formFile, "Audio");
            }
            else
            {
                return ValidateFile(formFile, "File");
            }
        }

        private string GetMediaPathUrl(string contentType, string url, string path)
        {
            if (contentType.Contains("image"))
            {
                return $"{url}{path}";
            }
            else if (contentType.Contains("pdf"))
            {
                return $"{url}ApplicationIcon/pdf.png";
            }
            else if (contentType.Contains("video"))
            {
                return $"{url}ApplicationIcon/video.png";
            }
            else if (contentType.Contains("audio"))
            {
                return $"{url}ApplicationIcon/playlist.png";
            }
            else
            {
                return $"{url}ApplicationIcon/doc.png";
            }
        }

        private List<int> GetChildFolderIdsRecursive(List<MediaFolderMaster> folders, int parentId)
        {
            var childFolders = folders
                .Where(f => f.MediaFolderParentId == parentId)
                .ToList();

            var allChildIds = new List<int>();

            foreach (var folder in childFolders)
            {
                allChildIds.Add(folder.MediaFolderMasterId);
                // Recursively find subfolders
                allChildIds.AddRange(GetChildFolderIdsRecursive(folders, folder.MediaFolderMasterId));
            }

            return allChildIds;
        }

        private TrueFalseResponse ValidateFile(IFormFile formFile, string mediaType)
        {
            int mediaTypeMasterId = GetMediaTypeMasterId(mediaType);
            if (mediaTypeMasterId == 0)
            {
                return CreateErrorResponse($"Media settings for {mediaType.ToLower()} type not found.");
            }

            MediaSettingMaster mediaSettingMaster = GetMediaSettingMaster(mediaTypeMasterId);
            if (mediaSettingMaster == null)
            {
                return CreateErrorResponse($"Media settings for {mediaType.ToLower()} type not found.");
            }

            List<int> allowedExtensionsIds = GetAllowedExtensionsIds(mediaSettingMaster.MediaTypeExtensionMasterIds);

            var extension = Path.GetExtension(formFile.FileName);

            if (!IsValidExtension(extension, allowedExtensionsIds))
            {
                return CreateErrorResponse($"Invalid {mediaType.ToLower()} extension.");
            }

            // Validate file size
            return ValidateFileSize(formFile, mediaSettingMaster.MaxSizeInMB);
        }

        private int GetMediaTypeMasterId(string mediaType)
        {
            return _mediaTypeMasterRepository.Table
                .Where(x => x.MediaType == mediaType)
                .Select(x => x.MediaTypeMasterId)
                .FirstOrDefault();
        }

        private MediaSettingMaster GetMediaSettingMaster(int mediaTypeMasterId)
        {
            return _mediaSettingMasterRepository.Table
                .Where(x => x.MediaTypeMasterId == mediaTypeMasterId)
                .FirstOrDefault();
        }

        private List<int> GetAllowedExtensionsIds(string mediaTypeExtensionMasterIds)
        {
            return !string.IsNullOrEmpty(mediaTypeExtensionMasterIds)
                ? mediaTypeExtensionMasterIds
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => int.TryParse(id, out var result) ? result : default)
                    .Where(result => result != default)
                    .ToList()
                : new List<int>();
        }

        private bool IsValidExtension(string extension, List<int> allowedExtensionsIds)
        {
            var mediaTypeExtensionMasterList = _mediaTypeExtensionMasterRepository.Table.ToList();
            return allowedExtensionsIds
                .Any(id => mediaTypeExtensionMasterList
                    .Any(x => x.MediaTypeExtensionMasterId == id && x.ExtensionName.Equals(extension, StringComparison.OrdinalIgnoreCase))
                );
        }

        private TrueFalseResponse ValidateFileSize(IFormFile formFile, double maxSizeInMB)
        {
            long fileSizeInBytes = formFile.Length;
            double fileSizeInMB = fileSizeInBytes / (1024.0 * 1024.0);

            if (fileSizeInMB <= maxSizeInMB)
            {
                return new TrueFalseResponse
                {
                    booleanModel = new BooleanModel
                    {
                        IsSuccess = true,
                        HasError = false
                    },
                    IsSuccess = true
                };
            }
            else
            {
                return CreateErrorResponse($"File size exceeds the maximum allowed limit of {maxSizeInMB} MB. Current file size is {fileSizeInMB:F2} MB.");
            }
        }

        private TrueFalseResponse CreateErrorResponse(string errorMessage)
        {
            return new TrueFalseResponse
            {
                booleanModel = new BooleanModel
                {
                    ErrorMessage = errorMessage,
                    IsSuccess = false,
                    HasError = true
                },
                IsSuccess = false,
                HasError = true
            };
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

        protected virtual MediaConfigurationModel ManageMediaUrl(MediaConfigurationModel mediaConfiguration)
        {
            string url = mediaConfiguration.CDNUrl;
            if (url.Contains("https") && mediaConfiguration.CDNUrl != null)
            {
                url = url.Substring(8, url.Length - 8);
                url = "https://" + url.Replace("//", "\\");
                mediaConfiguration.CDNUrl = url;
            }
            else if (mediaConfiguration.CDNUrl != null)
            {
                url = url.Substring(7, url.Length - 7);
                url = "http://" + url.Replace("//", "\\");
                mediaConfiguration.CDNUrl = url;
            }
            return mediaConfiguration;
        }

        //Get Data from cache if cache null then insert into Cache.
        protected virtual MediaConfigurationModel GetDefaultMediaConfiguration()
        {
            MediaConfigurationModel activeMediaConfiguration = _mediaConfigurationRepository.Table.FirstOrDefault(x => x.IsActive)?.FromEntityToModel<MediaConfigurationModel>();
            if (HelperUtility.IsNotNull(activeMediaConfiguration))
            {
                activeMediaConfiguration.GlobalMediaDisplaySetting = GetMediaGlobalDisplaySetting(activeMediaConfiguration);
            }
            else
            {
                activeMediaConfiguration = new MediaConfigurationModel { MediaServer = new MediaServerModel(), GlobalMediaDisplaySetting = GetMediaGlobalDisplaySetting(activeMediaConfiguration) };
            }
            return activeMediaConfiguration;
        }

        // Get global media display setting.
        public virtual MediaGlobalDisplaySettingModel GetMediaGlobalDisplaySetting(MediaConfigurationModel configurationModel)
        {

            MediaGlobalDisplaySettingModel globalMediaDisplaySetting = _mediaGlobalDisplaySettingRepository.Table.FirstOrDefault().FromEntityToModel<MediaGlobalDisplaySettingModel>();

            if (IsNotNull(globalMediaDisplaySetting))
            {
                if (globalMediaDisplaySetting?.MediaId > 0)
                {
                    string path = _mediaDetailRepository.Table.Where(x => x.MediaId == globalMediaDisplaySetting.MediaId).Select(x => x.Path)?.FirstOrDefault();

                    string mediaServerThumbnailPath = $"{GetMediaServerUrl(configurationModel)}{configurationModel?.ThumbnailFolderName}/{path}";

                    globalMediaDisplaySetting.DefaultImageName = string.IsNullOrEmpty(path) ? string.Empty : path;
                    globalMediaDisplaySetting.MediaPath = string.IsNullOrEmpty(mediaServerThumbnailPath) ? string.Empty : mediaServerThumbnailPath;
                }
            }
            else
            {
                globalMediaDisplaySetting = MediaGlobalDisplaySettingModel.GetGlobalMediaDisplaySetting();
            }
            return globalMediaDisplaySetting;
        }
        //Get the Media Server Url
        protected static string GetMediaServerUrl(MediaConfigurationModel configuration)
        {
            if (HelperUtility.IsNotNull(configuration))
            {
                return string.IsNullOrWhiteSpace(configuration.CDNUrl) ? configuration.URL
                           : configuration.CDNUrl.EndsWith("\\") ? configuration.CDNUrl : configuration.CDNUrl.EndsWith("/") ? configuration.CDNUrl : $"{configuration.CDNUrl}/";
            }
            return string.Empty;
        }

        private void GetStatus(List<FileUploadResponse> messages, FileInfo fi, int folderId, string fileType, bool isOverWrite, string fileName, bool isMediaReplace = false, int reqMediaId = 0, MediaConfigurationModel mediaConfiguration = null)
        {
            //List<FamilyExtensionModel> _allowExtensions = GetExtensions()?.FamilyExtensionListModel.FamilyExtensions ?? new List<FamilyExtensionModel>();
            //int familyId = 0;

            //int mediaId;
            //if (AllowExtension(_allowExtensions, fi.Extension, out familyId))
            //{
            //    if (AllowFileSize(_allowExtensions, fi.Length.ToString(), fi.Extension))
            //    {
            //        if (isMediaReplace && reqMediaId > 0)
            //            mediaId = reqMediaId;
            //        else
            //            mediaId = CheckExist(fileName, folderId);

            //        if (mediaId == 0)
            //        {
            //            mediaId = UploadFiles(0, fi, folderId, false, fi.FullName, familyId, fileType, fileName, false, mediaConfiguration);
            //            if (mediaId > 0)
            //                messages.Add(new FileUploadResponse { StatusCode = Convert.ToInt32(UploadStatusCode.Done), FileName = fileName, MediaId = Convert.ToString(mediaId) });
            //            else
            //                messages.Add(new FileUploadResponse { StatusCode = Convert.ToInt32(UploadStatusCode.Error), FileName = fileName });
            //        }
            //        else if (isOverWrite)
            //        {
            //            mediaId = UploadFiles(mediaId, fi, folderId, true, fi.FullName, familyId, fileType, fileName, isMediaReplace, mediaConfiguration);
            //            if (mediaId > 0)
            //                messages.Add(new FileUploadResponse { StatusCode = Convert.ToInt32(UploadStatusCode.Done), FileName = fileName, MediaId = Convert.ToString(mediaId) });
            //            else
            //                messages.Add(new FileUploadResponse { StatusCode = Convert.ToInt32(UploadStatusCode.Error), FileName = fileName });
            //        }
            //        else
            //            messages.Add(new FileUploadResponse { StatusCode = Convert.ToInt32(UploadStatusCode.FileAlreadyExist), FileName = fileName, MediaId = Convert.ToString(mediaId) });
            //    }
            //    else
            //        messages.Add(new FileUploadResponse { StatusCode = Convert.ToInt32(UploadStatusCode.MaxFileSize), FileName = fileName });
            //}
            //else
            //{
            //    messages.Add(new FileUploadResponse { StatusCode = Convert.ToInt32(UploadStatusCode.ExtensionNotAllow), FileName = fileName });
            //    fi.Delete();
            //}

        }

        //Check directory exist or not and create directory.
        protected virtual void CheckDirectoryExistOrCreate(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        protected virtual int UploadFiles(int mediaId, FileInfo files, int folderId, bool isOverrideFile, string actualFilePath, int? attributeFamilyId, string fileType, string fileName, bool isMediaReplace = false, MediaConfigurationModel mediaConfiguration = null)
        {
            MediaManagerModel mediaDetails = ToMediaManagerModel(fileType, files, actualFilePath, folderId, attributeFamilyId, fileName);

            //if (HelperUtility.IsNotNull(mediaDetails) && !string.IsNullOrEmpty(mediaDetails.Size))
            //{
            //    string className = string.Empty;
            //    int mediaConfigurationId = 0;
            //    ServerConnector _connectorobj = GetServerConnection(out className, out mediaConfigurationId);

            //    mediaDetails.MediaConfigurationId = mediaConfigurationId;

            //    if (isOverrideFile)
            //    {
            //        return UpdateExistingMedia(mediaId, mediaDetails, className, _connectorobj, isMediaReplace, mediaConfiguration);
            //    }

            //    //upload media to server
            //    UploadFilesMedia(className, _connectorobj, mediaDetails);
            //    MediaManagerModel mediamanagermodel = _service.SaveMedia(mediaDetails);

            //    try
            //    {
            //        if (mediaDetails.IsImage)
            //        {
            //            IImageMediaHelper? imageHelper = _serviceProvider.GetService<IImageMediaHelper>(); ;
            //            imageHelper.GenerateImageOnEdit(mediaDetails.Path, mediaConfiguration);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaManager.ToString(), TraceLevel.Error);
            //    }

            //    return mediamanagermodel.MediaId;
            //}
            return 0;
        }

        // Upload on server and save in to DB
        private MediaManagerModel ToMediaManagerModel(string fileType, FileInfo file, string actualFilePath, int folderId, int? attributeFamilyId, string fileName)
        {
            MediaManagerModel mediaManagerModel = new MediaManagerModel();
            mediaManagerModel.Path = file.Name;
            mediaManagerModel.Size = file.Length.ToString();
            mediaManagerModel.FileName = fileName;
            mediaManagerModel.Length = Convert.ToString(file.Length);
            mediaManagerModel.MediaType = file.Extension;

            if (string.Equals(fileType.Split('/')[0], "image"))
            {

                var Path = actualFilePath.Replace(@"\\\\", @"\\");
                try
                {
                    MagickFormat imageFormat = GetImageFormat(mediaManagerModel.MediaType);
                    using (MagickImage imageSize = new MagickImage(Path, imageFormat))
                    {
                        mediaManagerModel.Height = Convert.ToString(imageSize.Height);
                        mediaManagerModel.Width = Convert.ToString(imageSize.Width);
                    }
                }
                catch (Exception ex)
                {
                    //ToDo commented below throw exception due to facing issue while updating Image by using Magic Image.
                    //throw new ZnodeException(100, UploadStatusCode.UnSupportedFile.ToString());
                }
                mediaManagerModel.IsImage = true;
            }


            //assing value to model which is to be save in database
            mediaManagerModel.MediaPathId = folderId;
            return mediaManagerModel;
        }

        //get ImageFormat from string extentions
        private MagickFormat GetImageFormat(string extension)
        {
            switch (extension.ToLower())
            {
                case @".bmp":
                    return MagickFormat.Bmp;

                case @".gif":
                    return MagickFormat.Gif;

                case @".ico":
                    return MagickFormat.Icon;

                case @".jpg":
                case @".jpeg":
                    return MagickFormat.Jpeg;

                case @".png":
                    return MagickFormat.Png;

                case @".tif":
                case @".tiff":
                    return MagickFormat.Tiff;

                case @".wmf":
                    return MagickFormat.Wmf;

                case @".webp":
                    return MagickFormat.WebP;

                default:
                    return MagickFormat.Png;
            }
        }

        ////Gets the server connection
        //private ServerConnector GetServerConnection(out string className, out int mediaConfigurationId)
        //{
        //    ServerConnector? _connectorobj = null;
        //    //gets the default server configuration

        //    if (HelperUtility.IsNotNull(_mediaConfiguration.MediaConfiguration))
        //    {
        //        //Sets the server connection
        //        _connectorobj = new ServerConnector(new FileUploadPolicyModel(_mediaConfiguration.MediaConfiguration.AccessKey, _mediaConfiguration.MediaConfiguration.SecretKey, _mediaConfiguration.MediaConfiguration.BucketName, _mediaConfiguration.MediaConfiguration.ThumbnailFolderName, _mediaConfiguration.MediaConfiguration.URL, _mediaConfiguration.MediaConfiguration.NetworkUrl));
        //        className = _mediaConfiguration.MediaConfiguration.MediaServer.ClassName;
        //        mediaConfigurationId = _mediaConfiguration.MediaConfiguration.MediaConfigurationId;
        //    }
        //    else
        //    {
        //        _mediaConfiguration = _configurationCache.GetDefaultMediaConfiguration();
        //        _connectorobj = new ServerConnector(new FileUploadPolicyModel(string.Empty, string.Empty, APIConstant.DefaultMediaFolder, APIConstant.ThumbnailFolderName, _mediaConfiguration.MediaConfiguration.URL, _mediaConfiguration.MediaConfiguration.NetworkUrl));
        //        className = APIConstant.DefaultMediaClassName;
        //        mediaConfigurationId = _mediaConfiguration.MediaConfiguration.MediaConfigurationId;

        //    }
        //    return _connectorobj;
        //}
        #endregion
    }
}
