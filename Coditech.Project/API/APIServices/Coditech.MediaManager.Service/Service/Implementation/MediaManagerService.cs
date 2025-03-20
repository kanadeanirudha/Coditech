using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
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
        public virtual MediaManagerResponse UploadMedia(int folderId, string folderName, long mediaId, IEnumerable<IFormFile> formFile, HttpRequest request)
        {
            if (MultipartRequestHelper.IsMultipartContentType(request.ContentType))
            {
                if (folderId == 0 && !string.IsNullOrEmpty(folderName))
                {
                    folderId = _mediaFolderMasterRepository.Table.Where(x => x.FolderName == folderName).FirstOrDefault().MediaFolderParentId;
                }
                if (folderId == 0)
                    return new MediaManagerResponse() { HasError = true };

                string uploadPath = FileUploadPath();
                foreach (var file in formFile)
                {
                    TrueFalseResponse isFileValid = IsFileValid(file, folderId);

                    if (isFileValid.HasError)
                        return new MediaManagerResponse() { HasError = true, ErrorMessage = isFileValid.ErrorMessage };
                    if (mediaId > 0)
                    {
                        if (_mediaDetailRepository.Table.Any(x => x.FileName == file.FileName && x.MediaFolderMasterId == folderId && x.MediaId != mediaId))
                        {
                            return new MediaManagerResponse() { HasError = true, ErrorMessage = $"{file.FileName} already exist." };
                        }
                    }
                    else
                    {
                        if (_mediaDetailRepository.Table.Any(x => x.FileName == file.FileName && x.MediaFolderMasterId == folderId))
                        {
                            return new MediaManagerResponse() { HasError = true, ErrorMessage = $"{file.FileName} already exist." };
                        }
                    }

                    if (file.Length > 0)
                    {
                        MediaModel result = mediaId == 0 ? SaveMedia(file, file.FileName, folderId, uploadPath) : ReplaceMedia(file, file.FileName, folderId, mediaId, uploadPath);
                        if (result != null && result.MediaId > 0)
                        {
                            return new MediaManagerResponse()
                            {
                                MediaModel = new MediaModel()
                                {
                                    MediaId = result.MediaId,
                                    Path = $"{result.Path}"
                                }
                            };
                        }
                        else
                        {
                            return new MediaManagerResponse() { HasError = result.HasError, ErrorMessage = !string.IsNullOrEmpty(result.ErrorMessage) ? result.ErrorMessage : "Failed to upload media." };
                        }
                    }
                }
            }
            return new MediaManagerResponse() { HasError = true };
        }

        public virtual MediaManagerFolderResponse GetMediaList(int rootFolderId, int adminRoleId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            MediaManagerFolderResponse managerFolderResponse = new MediaManagerFolderResponse();
            List<MediaFolderMaster> mediaFolderMasterList = _mediaFolderMasterRepository.Table.ToList();

            MediaFolderMaster rootMediaFolder = mediaFolderMasterList.FirstOrDefault(x => x.MediaFolderParentId == 0);

            int activeFolderId = rootFolderId > 0 ? rootFolderId : rootMediaFolder.MediaFolderMasterId;

            List<int> adminRoleMediaFolders = new List<int>();
            if (adminRoleId > 0)
            {
                adminRoleMediaFolders = _adminRoleMediaFolderRepository.Table
                                        .Where(x => x.AdminRoleMasterId == adminRoleId)
                                        .Select(y => y.MediaFolderMasterId)
                                        .ToList();
                if (!adminRoleMediaFolders.Contains(activeFolderId))
                {
                    rootFolderId = activeFolderId = adminRoleMediaFolders?.Count() > 0 ? adminRoleMediaFolders.FirstOrDefault() : 0;
                }
                else
                {
                    rootFolderId = rootFolderId == 0 ? activeFolderId : rootFolderId;
                }
            }
            else
            {
                rootFolderId = rootFolderId == 0 ? activeFolderId : rootFolderId;
            }

            managerFolderResponse.MediaManagerFolderModel = new MediaManagerFolderModel
            {
                MediaRootFolder = new MediaFolderStructureModel()
                {
                    SubFolders = GetSubFolders(rootMediaFolder.MediaFolderMasterId, mediaFolderMasterList, ref activeFolderId, adminRoleMediaFolders),
                    RootFolderId = rootMediaFolder.MediaFolderMasterId,
                    RootFolderName = rootMediaFolder.FolderName,
                    IsActiveFolder = activeFolderId == rootMediaFolder.MediaFolderMasterId,
                    adminRoleMediaFolders = adminRoleMediaFolders
                },
                ActiveFolderId = activeFolderId
            };

            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<MediaModel> objStoredProc = new CoditechViewRepository<MediaModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@MediaFolderMasterId", rootFolderId, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@AdminRoleMasterId", adminRoleId, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<MediaModel> mediaList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetMediaList @MediaFolderMasterId,@AdminRoleMasterId,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();

            managerFolderResponse.MediaManagerFolderModel.MediaFiles = mediaList?.Count > 0 ? mediaList : new List<MediaModel>();
            managerFolderResponse.MediaManagerFolderModel.BindPageListModel(pageListModel);
            double totalFileSizeInByte = 0;
            string url = GetMediaUrl();
            foreach (MediaModel media in managerFolderResponse?.MediaManagerFolderModel?.MediaFiles)
            {
                media.Size = !string.IsNullOrEmpty(media.Size) ? (Convert.ToDouble(media.Size) * 0.001).ToString("0.00") : "0";
                totalFileSizeInByte += !string.IsNullOrEmpty(media.Size) ? Convert.ToDouble(media.Size) : 0;
                media.Path = GetMediaPathUrl(media.Type, url, media.Path);
            }
            //managerFolderResponse.MediaManagerFolderModel.TotalFileSize = totalFileSizeInByte > 0 ? ConvertBytesToMegabytes(totalFileSizeInByte) : 0;
            managerFolderResponse.BindPageListResponseModel(pageListModel);
            return managerFolderResponse;
        }

        public MediaModel GetMediaDetails(long mediaId)
        {
            MediaDetail mediaDetail = _mediaDetailRepository.Table.Where(x => x.MediaId == mediaId)?.FirstOrDefault();

            if (mediaDetail == null)
            {
                return new MediaModel();
            }

            MediaModel mediaModel = mediaDetail.FromEntityToModel<MediaModel>();
            mediaModel.Path = $"{GetMediaUrl()}{mediaModel.Path}";
            mediaModel.Size = !string.IsNullOrEmpty(mediaModel.Size) ? (Convert.ToDouble(mediaModel.Size) * 0.001).ToString("0.00") : "0";
            mediaModel.Type = mediaModel.Type.Replace("application/", "").ToLower();
            return mediaModel;
        }
        public virtual FolderListResponse GetMoveFolders(int moveFolderId)
        {

            FolderListResponse folderListResponse = new();
            CoditechViewRepository<Folder> objStoredProc = new CoditechViewRepository<Folder>(_serviceProvider.GetService<Coditech_Entities>());

            objStoredProc.SetParameter("@MediaFolderMasterId", moveFolderId, ParameterDirection.Input, DbType.String);

            folderListResponse.FolderList.Folders = objStoredProc.ExecuteStoredProcedureList("Coditech_GetMoveFoldersList @MediaFolderMasterId")?.ToList();
          
            return folderListResponse;
        }

        public virtual bool MoveFolder(int folderId, int destinationFolderId)
        {
            MediaFolderMaster mediaFolderMaster = _mediaFolderMasterRepository.Table.Where(x => x.MediaFolderMasterId == folderId).FirstOrDefault();

            if (mediaFolderMaster != null)
            {
                mediaFolderMaster.MediaFolderParentId = destinationFolderId;
                mediaFolderMaster.ModifiedDate = DateTime.Now;

                return _mediaFolderMasterRepository.Update(mediaFolderMaster);
            }
            return false;
        }

        public virtual bool DeleteFolder(int folderId)
        {
            // Get the folder to be deleted
            var mediaFolderMaster = _mediaFolderMasterRepository.Table
                                    .Where(x => x.MediaFolderMasterId == folderId)
                                    .FirstOrDefault();

            if (mediaFolderMaster != null)
            {
                // Get all child folders
                var allFolders = GetAllChildFoldersAsync(folderId);

                // Add the root folder to the list
                allFolders.Add(mediaFolderMaster);

                // Delete all media files and folders starting from the deepest child
                foreach (var folder in allFolders.OrderByDescending(f => f.MediaFolderMasterId))
                {
                    DeleteMediaFilesAsync(folder.MediaFolderMasterId);
                    if (folder.MediaFolderMasterId != 1 && folder.FolderName != "Root")
                    {
                        _mediaFolderMasterRepository.Delete(folder);
                    }
                }

                return true;
            }

            return false;
        }

        public virtual bool DeleteFile(int mediaId)
        {
            // Get the folder to be deleted
            var mediaFile = _mediaDetailRepository.Table
                              .Where(x => x.MediaId == mediaId).FirstOrDefault();

            string projectPath = Directory.GetCurrentDirectory();
            string uploadedPath = Path.Combine(projectPath, "Data", "Media");
            Directory.CreateDirectory(uploadedPath);

            string filePath = Path.Combine(uploadedPath, mediaFile.Path);


            // Delete the media file from the file system
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            _mediaDetailRepository.Delete(mediaFile);

            return true;
        }

        public virtual bool PostRenameFolder(int FolderId, string RenameFolderName)
        {
            if (FolderId > 0)
            {
                MediaFolderMaster mediaFolderMaster = _mediaFolderMasterRepository.Table.Where(x => x.MediaFolderMasterId == FolderId).FirstOrDefault();

                if (mediaFolderMaster != null)
                {
                    mediaFolderMaster.FolderName = RenameFolderName;
                    mediaFolderMaster.ModifiedDate = DateTime.Now;

                    return _mediaFolderMasterRepository.Update(mediaFolderMaster);
                }
            }
            return false;
        }

        public virtual TrueFalseResponse PostCreateFolder(int RootFolderId, string FolderName, int adminRoleMasterId)
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
                    MediaFolderMaster createFolder = new MediaFolderMaster()
                    {
                        FolderName = FolderName,
                        MediaFolderParentId = mediaFolderMaster.MediaFolderMasterId,
                        IsActive = true
                    };

                    MediaFolderMaster mediaFolder = _mediaFolderMasterRepository.Insert(createFolder);

                    if (mediaFolder.MediaFolderMasterId > 0)
                    {
                        if (adminRoleMasterId > 0)
                            _adminRoleMediaFolderRepository.Insert(new AdminRoleMediaFolders() { AdminRoleMasterId = adminRoleMasterId, MediaFolderMasterId = mediaFolder.MediaFolderMasterId, IsActive = true });
                        return new TrueFalseResponse() { booleanModel = new BooleanModel() { SuccessMessage = "Folder successfully created.", IsSuccess = true, Custom1 = mediaFolder.MediaFolderMasterId.ToString()}};
                    }
                    else
                    {
                        return new TrueFalseResponse() { booleanModel = new BooleanModel() { ErrorMessage = "Failed to create a folder.", IsSuccess = false, HasError = true }, IsSuccess = false };
                    }
                }
            }
            return new TrueFalseResponse() { booleanModel = new BooleanModel() { ErrorMessage = "Folder Id not passed.", IsSuccess = false, HasError = true }, IsSuccess = false }; ;
        }

        #endregion

        #region Protected Method
        //protected virtual double ConvertBytesToMegabytes(double bytes)
        //{
        //    double megabytes = bytes / 1048576;
        //    return Math.Round(megabytes, 2);
        //}

        protected virtual List<MediaFolderMaster> GetAllChildFoldersAsync(int folderId)
        {
            List<MediaFolderMaster> allFolders = new List<MediaFolderMaster>();
            FetchChildFolders(folderId, allFolders);
            return allFolders;
        }

        protected virtual async Task FetchChildFolders(int parentId, List<MediaFolderMaster> allFolders)
        {
            var childFolders = _mediaFolderMasterRepository.Table
                                .Where(x => x.MediaFolderParentId == parentId)
                                .ToList();

            foreach (var folder in childFolders)
            {
                allFolders.Add(folder);
                FetchChildFolders(folder.MediaFolderMasterId, allFolders);
            }
        }

        protected virtual async Task DeleteMediaFilesAsync(int folderId)
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
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    _mediaDetailRepository.Delete(media);
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected virtual List<MediaFolderStructureModel> GetSubFolders(int parentId, List<MediaFolderMaster> allFolders, ref int activeFolderId, List<int> adminRoleMediaFolders)
        {
            if (activeFolderId == 0)
            {
                if (adminRoleMediaFolders.Contains(parentId))
                {
                    activeFolderId = parentId;
                }
            }

            var subFolders = allFolders.Where(x => x.MediaFolderParentId == parentId).ToList();
            var subFolderStructures = new List<MediaFolderStructureModel>();

            foreach (var subFolder in subFolders)
            {
                var subFolderStructure = new MediaFolderStructureModel
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

        protected virtual TrueFalseResponse IsFileValid(IFormFile formFile, int folderId)
        {
            // Check if the file is null
            if (formFile == null)
            {
                return CreateErrorResponse("The file cannot be null.");
            }

            if (formFile.FileName.Length > 250)
            {
                return CreateErrorResponse("the file name is too long.");
            }

            if (!IsValidFileName(formFile.FileName))
            {
                return CreateErrorResponse("The file name contains some special characters.");
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

        protected virtual string GetMediaPathUrl(string contentType, string url, string path)
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

        protected virtual List<int> GetChildFolderIdsRecursive(List<MediaFolderMaster> folders, int parentId)
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

        protected virtual TrueFalseResponse ValidateFile(IFormFile formFile, string mediaType)
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

        protected virtual int GetMediaTypeMasterId(string mediaType)
        {
            return _mediaTypeMasterRepository.Table
                .Where(x => x.MediaType == mediaType)
                .Select(x => x.MediaTypeMasterId)
                .FirstOrDefault();
        }

        protected virtual MediaSettingMaster GetMediaSettingMaster(int mediaTypeMasterId)
        {
            return _mediaSettingMasterRepository.Table
                .Where(x => x.MediaTypeMasterId == mediaTypeMasterId)
                .FirstOrDefault();
        }

        protected virtual List<int> GetAllowedExtensionsIds(string mediaTypeExtensionMasterIds)
        {
            return !string.IsNullOrEmpty(mediaTypeExtensionMasterIds)
                ? mediaTypeExtensionMasterIds
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => int.TryParse(id, out var result) ? result : default)
                    .Where(result => result != default)
                    .ToList()
                : new List<int>();
        }

        protected virtual bool IsValidExtension(string extension, List<int> allowedExtensionsIds)
        {
            var mediaTypeExtensionMasterList = _mediaTypeExtensionMasterRepository.Table.ToList();
            return allowedExtensionsIds
                .Any(id => mediaTypeExtensionMasterList
                    .Any(x => x.MediaTypeExtensionMasterId == id && x.ExtensionName.Equals(extension, StringComparison.OrdinalIgnoreCase))
                );
        }

        protected virtual TrueFalseResponse ValidateFileSize(IFormFile formFile, double maxSizeInMB)
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

        protected virtual TrueFalseResponse CreateErrorResponse(string errorMessage)
        {
            return new TrueFalseResponse
            {
                IsSuccess = false,
                HasError = true,
                ErrorMessage = errorMessage
            };
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

            MediaGlobalDisplaySetting mediaGlobalDisplaySetting = _mediaGlobalDisplaySettingRepository.Table.FirstOrDefault();
            MediaGlobalDisplaySettingModel globalMediaDisplaySetting = mediaGlobalDisplaySetting.FromEntityToModel<MediaGlobalDisplaySettingModel>();

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

        //Check directory exist or not and create directory.
        protected virtual void CheckDirectoryExistOrCreate(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        //get ImageFormat from string extentions
        protected virtual MagickFormat GetImageFormat(string extension)
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

        protected virtual MediaModel SaveMedia(IFormFile formFile, string fileName, int folderId, string uploadPath)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(fileName);
            string filePath = Path.Combine(uploadPath, uniqueFileName);

            // Save the file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }

            long size = formFile.Length;
            FileInfo fi = new FileInfo(filePath);
            if (fi.Length != size)
            {
                return new MediaModel() { HasError = true, ErrorMessage = "Failed to upload media." };
            }

            var type = formFile.ContentType;

            // Generate URL to access the file
            var height = string.Empty;
            var width = string.Empty;

            if (formFile.ContentType.StartsWith("image"))
            {
                using (var image = Image.FromStream(formFile.OpenReadStream()))
                {
                    width = Convert.ToString(image.Width);
                    height = Convert.ToString(image.Height);
                }
            }

            var result = _mediaDetailRepository.Insert(new MediaDetail()
            {
                MediaConfigurationId = GetDefaultMediaConfiguration().MediaConfigurationId,
                MediaFolderMasterId = folderId,
                Path = uniqueFileName,
                FileName = fileName,
                Size = Convert.ToString(size),
                Length = Convert.ToString(size),
                Height = height,
                Width = width,
                Type = type
            });

            if (result.MediaId > 0)
            {
                return new MediaModel()
                {
                    MediaId = result.MediaId,
                    Path = $"{GetMediaUrl()}{result.Path}"
                };
            }
            return new MediaModel() { HasError = true };
        }

        protected virtual MediaModel ReplaceMedia(IFormFile formFile, string fileName, int folderId, long mediaId, string uploadPath)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(fileName);
            string filePath = Path.Combine(uploadPath, uniqueFileName);

            // Save the file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }

            long size = formFile.Length;
            FileInfo fi = new FileInfo(filePath);
            if (fi.Length != size)
            {
                return new MediaModel() { HasError = true, ErrorMessage = "Failed to upload media." };
            }

            var type = formFile.ContentType;

            // Generate URL to access the file
            var height = string.Empty;
            var width = string.Empty;

            if (formFile.ContentType.StartsWith("image"))
            {
                using (var image = Image.FromStream(formFile.OpenReadStream()))
                {
                    width = Convert.ToString(image.Width);
                    height = Convert.ToString(image.Height);
                }
            }
            MediaDetail mediaDetail = _mediaDetailRepository.Table.Where(x => x.MediaId == mediaId)?.FirstOrDefault();
            if (mediaDetail != null)
            {
                mediaDetail.Path = uniqueFileName;
                mediaDetail.FileName = fileName;
                mediaDetail.Size = Convert.ToString(size);
                mediaDetail.Length = Convert.ToString(size);
                mediaDetail.Height = height;
                mediaDetail.Width = width;
                mediaDetail.Type = type;
                if (_mediaDetailRepository.Update(mediaDetail))
                {
                    return new MediaModel()
                    {
                        MediaId = mediaDetail.MediaId,
                        Path = $"{GetMediaUrl()}{mediaDetail.Path}"
                    };
                }
            }
            return new MediaModel() { HasError = true, ErrorMessage = "Failed to replace media." };
        }
        protected virtual string FileUploadPath()
        {
            string projectPath = Directory.GetCurrentDirectory();
            string uploadPath = Path.Combine(projectPath, "Data", "Media");
            Directory.CreateDirectory(uploadPath);
            return uploadPath;
        }
        #endregion
    }
}
