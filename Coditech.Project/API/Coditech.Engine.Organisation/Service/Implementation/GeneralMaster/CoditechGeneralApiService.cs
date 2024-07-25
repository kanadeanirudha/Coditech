using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;

using System.Data;
namespace Coditech.API.Service
{
    public class CoditechGeneralApiService : ICoditechGeneralApiService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<CoditechApplicationSetting> _coditechApplicationSettingRepository;
        public CoditechGeneralApiService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _coditechApplicationSettingRepository = new CoditechRepository<CoditechApplicationSetting>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual CoditechApplicationSettingListModel GetCoditechApplicationSettingList(string applicationCodes)
        {
            CoditechApplicationSettingListModel listModel = new CoditechApplicationSettingListModel();
            listModel.CoditechApplicationSettingList = new List<CoditechApplicationSettingModel>();
            if (!string.IsNullOrEmpty(applicationCodes))
            {
                List<string> applicationCodeList = applicationCodes.Split(",").ToList();
                List<CoditechApplicationSetting> coditechApplicationSettingList = _coditechApplicationSettingRepository.Table.Where(x=> applicationCodeList.Contains(x.ApplicationCode))?.ToList();
                foreach (CoditechApplicationSetting item in coditechApplicationSettingList)
                {
                    listModel.CoditechApplicationSettingList.Add(item.FromEntityToModel<CoditechApplicationSettingModel>());
                }
            }
            return listModel;
        }
        #region Protected Method
        #endregion
    }
}
