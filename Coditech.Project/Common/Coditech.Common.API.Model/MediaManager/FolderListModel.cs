using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coditech.Common.API.Model
{
    public class FolderListModel : BaseModel
    {
        public List<Folder> Folders { get; set; } = new List<Folder>();
    }

    public class Folder 
    {
        public int FolderId { get; set; }
        public string FolderName { get; set; }
    }
}
