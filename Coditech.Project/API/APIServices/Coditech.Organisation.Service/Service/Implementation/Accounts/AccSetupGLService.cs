using Coditech.API.Data;
using Coditech.API.Service;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.ServiceAccounts
{
    public class AccSetupGLService : BaseService, IAccSetupGLService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<AccSetupGL> _accSetupGLRepository;
        private readonly ICoditechRepository<AccSetupGLBalanceSheet> _accSetupGLBalanceSheetRepository;
        private readonly ICoditechRepository<AccSetupChartOfAccountTemplate> _accSetupChartOfAccountTemplateRepository;
        private readonly ICoditechRepository<AccSetupCategory> _accSetupCategoryRepository;
        public AccSetupGLService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _accSetupGLRepository = new CoditechRepository<AccSetupGL>(_serviceProvider.GetService<Coditech_Entities>());
            _accSetupGLBalanceSheetRepository = new CoditechRepository<AccSetupGLBalanceSheet>(_serviceProvider.GetService<Coditech_Entities>());
            _accSetupChartOfAccountTemplateRepository = new CoditechRepository<AccSetupChartOfAccountTemplate>(_serviceProvider.GetService<Coditech_Entities>());
            _accSetupCategoryRepository = new CoditechRepository<AccSetupCategory>(_serviceProvider.GetService<Coditech_Entities>());
        }

        // Get BalanceSheetAndCentreCode
        public virtual AccSetupGLModel GetAccSetupGLTree(string selectedcentreCode, byte accSetupBalanceSheetTypeId, int accSetupBalanceSheetId)
        {
            if (accSetupBalanceSheetId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, "AccSetupBalanceSheetId cannot be empty.");

            if (string.IsNullOrEmpty(selectedcentreCode))
                throw new CoditechException(ErrorCodes.IdLessThanOne, "CentreCode cannot be empty.");

            byte accSetupChartOfAccountTemplateId = _accSetupChartOfAccountTemplateRepository.Table.Where(x => x.TemplateName == AccSetupChartOfAccountTemplateEnum.IndianStandard.ToString()).Select(x => x.AccSetupChartOfAccountTemplateId).FirstOrDefault();

            if (accSetupChartOfAccountTemplateId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, "AccSetupChartOfAccountTemplateId cannot be empty.");

            AccSetupGLModel accSetupGLModel = new AccSetupGLModel()
            {
                AccSetupChartOfAccountTemplateId = accSetupChartOfAccountTemplateId,
            };
            List<AccSetupGLModel> accSetupGLRecords = new List<AccSetupGLModel>();
            if (!_accSetupGLBalanceSheetRepository.Table.Any(x => x.AccSetupBalanceSheetId == accSetupBalanceSheetId))
            {
                accSetupGLModel.ActionMode = ActionModeEnum.Create.ToString();
            }
            CoditechViewRepository<AccSetupGLModel> objStoredProc = new CoditechViewRepository<AccSetupGLModel>(_serviceProvider.GetService<Coditech_Entities>());
            PageListModel pageListModel = new PageListModel(null, null, 0, 0);
            objStoredProc.SetParameter("@AccSetupChartOfAccountTemplateId", accSetupChartOfAccountTemplateId, ParameterDirection.Input, DbType.Byte);
            objStoredProc.SetParameter("@AccSetupBalancesheetId", accSetupBalanceSheetId, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@ActionMode", accSetupGLModel.ActionMode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            accSetupGLRecords = objStoredProc.ExecuteStoredProcedureList("Coditech_GetAccSetupGLTree @AccSetupChartOfAccountTemplateId,@AccSetupBalancesheetId,@ActionMode, @RowsCount OUT", 3, out pageListModel.TotalRowCount)?.ToList();

            if (IsNotNull(accSetupGLRecords) && accSetupGLRecords.Any())
            {
                accSetupGLModel.AccSetupGLList = BuildAccountTree(accSetupGLRecords, null);
            }

            // Fetch active categories.
            List<AccSetupCategory> accSetupCategoryList = _accSetupCategoryRepository.Table.Where(x => x.IsActive).ToList();

            // Convert to AccSetupCategoryModel list.
            List<AccSetupCategoryModel> accSetupCategoryModelList = new List<AccSetupCategoryModel>();

            foreach (var category in accSetupCategoryList)
            {
                AccSetupCategoryModel categoryModel = new AccSetupCategoryModel
                {
                    AccSetupCategoryId = category.AccSetupCategoryId,
                    CategoryName = category.CategoryName,
                    CategoryCode = category.CategoryCode,
                    IsActive = category.IsActive
                };

                accSetupCategoryModelList.Add(categoryModel);
            }

            // Set the categories list to the model.
            accSetupGLModel.AccSetupCategoryList = accSetupCategoryModelList;

            return accSetupGLModel;
        }

        // Recursively build the hierarchical tree
        private List<AccSetupGLModel> BuildAccountTree(List<AccSetupGLModel> allAccounts, int? parentId)
        {
            List<AccSetupGLModel> allAccounts1 = allAccounts.Where(a => a.ParentAccSetupGLId == parentId)
                                    .Select(a => new AccSetupGLModel
                                    {
                                        AccSetupGLId = a.AccSetupGLId,
                                        GLName = a.GLName,
                                        CategoryCode = a.CategoryCode,
                                        GLCode = a.GLCode,
                                        ParentAccSetupGLId = a.ParentAccSetupGLId,
                                        SubAccounts = BuildAccountTree(allAccounts, a.AccSetupGLId) // Recursive call
                                    }).ToList();

            return allAccounts1;
        }
        //Create CreateAccountSetupGL.
        public virtual AccSetupGLModel CreateAccountSetupGL(AccSetupGLModel accSetupGLModel)
        {
            if (IsNull(accSetupGLModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            List<AccSetupGLBalanceSheet> accSetupGLBalanceSheets = (from a in _accSetupGLRepository.Table
                                                                    where a.AccSetupChartOfAccountTemplateId == accSetupGLModel.AccSetupChartOfAccountTemplateId && a.IsActive && a.IsSystemGenerated
                                                                    select new AccSetupGLBalanceSheet()
                                                                    {
                                                                        AccSetupBalanceSheetId = accSetupGLModel.AccSetupBalancesheetId,
                                                                        AccSetupGLId = a.AccSetupGLId,
                                                                        IsActive = true,
                                                                    }).ToList();

            //Create new AccountSetupGL and return it.
            _accSetupGLBalanceSheetRepository.Insert(accSetupGLBalanceSheets);
            return accSetupGLModel;
        }
        //Update AccountSetupGL  
        public virtual bool UpdateAccountSetupGL(AccSetupGLModel accSetupGLModel)
        {
            if (IsNull(accSetupGLModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (accSetupGLModel.AccSetupBalancesheetId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AccSetupBalancesheetId"));

            AccSetupGL accSetupGL = accSetupGLModel.FromModelToEntity<AccSetupGL>();

            //AccSetupGL Update 
            bool isAccSetupGLUpdated = _accSetupGLRepository.Update(accSetupGL);

            //if Update Successful then Insert
            if (isAccSetupGLUpdated)
            {
                //new record Insert 
                List<AccSetupGLBalanceSheet> accSetupGLBalanceSheets = new List<AccSetupGLBalanceSheet>()
        {
            new AccSetupGLBalanceSheet()
            {
                AccSetupBalanceSheetId = accSetupGLModel.AccSetupBalancesheetId,
                AccSetupGLId = accSetupGLModel.AccSetupGLId,
                IsActive = true,
            }
        };

                _accSetupGLBalanceSheetRepository.Insert(accSetupGLBalanceSheets);
            }
            else
            {
                accSetupGLModel.HasError = true;
                accSetupGLModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }

            return isAccSetupGLUpdated;
        }
    }
}