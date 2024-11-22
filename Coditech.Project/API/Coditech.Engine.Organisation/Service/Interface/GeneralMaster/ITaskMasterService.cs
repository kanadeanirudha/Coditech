using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface ITaskMasterService
    {
        TaskMasterListModel GetTaskMasterList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        TaskMasterModel CreateTaskMaster(TaskMasterModel model);
        TaskMasterModel GetTaskMaster(short taskMasterId);
        bool UpdateTaskMaster(TaskMasterModel model);
        bool DeleteTaskMaster(ParameterModel parameterModel);
    }
}
