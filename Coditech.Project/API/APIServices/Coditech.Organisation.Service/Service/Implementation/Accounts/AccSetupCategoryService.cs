using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Coditech.API.Service
{
    public class AccSetupCategoryService : BaseService, IAccSetupCategoryService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<AccSetupCategory> _accSetupCategoryRepository;
        public AccSetupCategoryService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _accSetupCategoryRepository = new CoditechRepository<AccSetupCategory>(_serviceProvider.GetService<Coditech_Entities>());
        }
        
        public virtual AccSetupCategoryListModel GetAccSetupCategory()
        {
            AccSetupCategoryListModel listModel = new AccSetupCategoryListModel();

            listModel.AccSetupCategoryList = (from a in _accSetupCategoryRepository.Table
                                              select new AccSetupCategoryModel
                                              {
                                                  AccSetupCategoryId = a.AccSetupCategoryId,
                                                  CategoryName = a.CategoryName,
                                              })?.ToList();
            return listModel;
        }
    }
}


