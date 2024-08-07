﻿using Coditech.Common.API.Model;
using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class MediaManagerFolderListViewModel : BaseViewModel
    {
        public MediaFolderStructure MediaRootFolder { get; set; }
        public int ActiveFolderId { get; set; }
        public List<Media> MediaFiles { get; set; }
        public double TotalFileSize {  get; set; }
    }
}
