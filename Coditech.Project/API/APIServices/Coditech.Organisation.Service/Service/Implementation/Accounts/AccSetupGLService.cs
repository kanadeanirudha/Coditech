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
        private readonly ICoditechRepository<AccSetupGLBank> _accSetupGLBankRepository;

        public AccSetupGLService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _accSetupGLRepository = new CoditechRepository<AccSetupGL>(_serviceProvider.GetService<Coditech_Entities>());
            _accSetupGLBalanceSheetRepository = new CoditechRepository<AccSetupGLBalanceSheet>(_serviceProvider.GetService<Coditech_Entities>());
            _accSetupChartOfAccountTemplateRepository = new CoditechRepository<AccSetupChartOfAccountTemplate>(_serviceProvider.GetService<Coditech_Entities>());
            _accSetupCategoryRepository = new CoditechRepository<AccSetupCategory>(_serviceProvider.GetService<Coditech_Entities>());
            _accSetupGLBankRepository = new CoditechRepository<AccSetupGLBank>(_serviceProvider.GetService<Coditech_Entities>());
        }

        // Get GetAccSetupGLTree
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
                                        IsGroup = a.IsGroup,
                                        IsSystemGenerated = a.IsSystemGenerated,
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
        public virtual bool AddChild(AccSetupGLModel accSetupGLModel)
        {
            if (IsNull(accSetupGLModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsGLNameOrGLCodeAlreadyExist(accSetupGLModel.GLName, accSetupGLModel.GLCode, accSetupGLModel.AccSetupGLId))
            {
                throw new CoditechException(ErrorCodes.AlreadyExist,
                    string.Format(GeneralResources.ErrorCodeExists, "GL Name or GL Code"));
            }

            if (accSetupGLModel.ParentAccSetupGLId == null || accSetupGLModel.ParentAccSetupGLId < 1)
            {
                accSetupGLModel.ParentAccSetupGLId = (int?)null;
            }

            // Create a new model instance.
            AccSetupGLModel accSetupGLModel1 = new AccSetupGLModel
            {
                GLName = accSetupGLModel.GLName,
                ParentAccSetupGLId = accSetupGLModel.ParentAccSetupGLId,
                IsActive = true,
                AccSetupBalancesheetId = accSetupGLModel.AccSetupBalancesheetId,
                AccSetupChartOfAccountTemplateId = (byte?)null,
                AccSetupGLTypeId = accSetupGLModel.AccSetupGLTypeId,
                AccSetupBalanceSheetTypeId = accSetupGLModel.AccSetupBalanceSheetTypeId,
                AccSetupCategoryId = accSetupGLModel.AccSetupCategoryId,
                GLCode = accSetupGLModel.GLCode,
                AltSetupGLId = accSetupGLModel.AccSetupBalancesheetId,
                IsGroup = accSetupGLModel.IsGroup,
                SelectedCentreCode = accSetupGLModel.SelectedCentreCode,
                IsControlHeadEnum = accSetupGLModel.UserTypeId == 0 ? (short?)null : accSetupGLModel.UserTypeId
            };

            // Map the model to an entity.
            AccSetupGL accSetupGLEntity = accSetupGLModel1.FromModelToEntity<AccSetupGL>();

            // Insert the new  record.
            AccSetupGL accSetupGLData = _accSetupGLRepository.Insert(accSetupGLEntity);

            if (IsNull(accSetupGLData))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (accSetupGLData.AccSetupGLId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AccSetupGLId"));

            // Update the input model.
            accSetupGLModel.AccSetupGLId = accSetupGLData.AccSetupGLId;

            List<AccSetupGLBalanceSheet> balanceSheets = new List<AccSetupGLBalanceSheet>
            {
                new AccSetupGLBalanceSheet
                {
                    AccSetupBalanceSheetId = accSetupGLModel.AccSetupBalancesheetId,
                    AccSetupGLId = accSetupGLData.AccSetupGLId,
                    IsActive = true,
                }
            };

            if (accSetupGLModel.AccSetupGLTypeId == 5)
            {
                InsertBankDetails(accSetupGLModel);
            }

            _accSetupGLBalanceSheetRepository.Insert(balanceSheets);

            return true;
        }

        //Delete AccountSetupGL.
        public virtual bool DeleteAccountSetupGL(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AccSetupGLID"));

            if (!int.TryParse(parameterModel.Ids, out int accSetupGLId))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AccSetupGLID"));

            if (IsDataPresentInAccsetupGLBankByAccsetupGLId(accSetupGLId))
                throw new CoditechException(ErrorCodes.InvalidData, "Record is present in the bank , deletion not allowed.");

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("AccSetupGLId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);

            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteAccountSetupGL @AccSetupGLId, @Status OUT", 1, out status);

            return status == 1;
        }
        //Get AccSetupMaster by AccSetupMaster id.
        public virtual AccSetupGLModel GetAccountSetupGL(int accSetupGLId)
        {
            if (accSetupGLId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "accSetupGLId"));

            // Get the AccSetupMaster Details based on id.
            AccSetupGL accSetupGLMaster = _accSetupGLRepository.Table.FirstOrDefault(x => x.AccSetupGLId == accSetupGLId);
            AccSetupGLModel accSetupGLModel = accSetupGLMaster?.FromEntityToModel<AccSetupGLModel>();
            accSetupGLModel.UserTypeId = accSetupGLMaster.IsControlHeadEnum;

            if (accSetupGLModel != null && accSetupGLModel.AccSetupGLTypeId == 5)
            {
                // ✅ Get bank data if GL Type ID is 5
                var bankData = _accSetupGLBankRepository.Table.FirstOrDefault(b => b.AccSetupGLId == accSetupGLId);
                if (bankData != null)
                {
                    accSetupGLModel.BankAccountName = bankData.BankAccountName;
                    accSetupGLModel.BankAccountNumber = bankData.BankAccountNumber;
                    accSetupGLModel.BankBranchName = bankData.BankBranchName;
                    accSetupGLModel.IFSCCode = bankData.IFSCCode;
                }
            }

            return accSetupGLModel;
        }
        public virtual bool UpdateAccount(AccSetupGLModel accSetupGLModel)
        {
            if (accSetupGLModel == null || accSetupGLModel.AccSetupGLId <= 0)
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            // Fetch the existing entity
            AccSetupGL accSetupGLMaster = _accSetupGLRepository.Table
                .FirstOrDefault(x => x.AccSetupGLId == accSetupGLModel.AccSetupGLId);

            if (accSetupGLMaster == null)
                throw new CoditechException(ErrorCodes.NotFound, "Account setup record not found.");

            // Update entity properties
            accSetupGLMaster.GLName = accSetupGLModel.GLName;
            accSetupGLMaster.GLCode = accSetupGLModel.GLCode;
            accSetupGLMaster.IsActive = true;

            _accSetupGLRepository.Update(accSetupGLMaster);

            //Update AccsetupGLBank If AccsetupglTypeId = 5 //
            if (accSetupGLModel.AccSetupGLTypeId == 5)
            {
                var existingBankRecord = _accSetupGLBankRepository.Table
                    .FirstOrDefault(b => b.AccSetupGLId == accSetupGLModel.AccSetupGLId && b.BankAccountNumber == accSetupGLModel.BankAccountNumber);

                if (existingBankRecord != null)
                {
                    existingBankRecord.BankAccountName = accSetupGLModel.BankAccountName;
                    existingBankRecord.BankBranchName = accSetupGLModel.BankBranchName;
                    existingBankRecord.IFSCCode = accSetupGLModel.IFSCCode.ToUpper();
                    _accSetupGLBankRepository.Update(existingBankRecord);
                }
            }
            return true;
        }

        #region Protected Method
        // Check if GLName or GLCode already exists for a different AccSetupGLId
        protected virtual bool IsGLNameOrGLCodeAlreadyExist(string glName, string glCode, int accSetupGLId = 0)
        {
            return _accSetupGLRepository.Table.Any(x => (x.GLName == glName || x.GLCode == glCode) && (x.AccSetupGLId != accSetupGLId || accSetupGLId == 0));
        }
        protected virtual bool IsDataPresentInAccsetupGLBankByAccsetupGLId(int accSetupGLId)
        {
            return _accSetupGLBankRepository.Table.Any(x => x.AccSetupGLId == accSetupGLId);
        }

        // Check if GLName or GLCode already exists for a different AccSetupGLId
        protected virtual bool IsAccsetupGLBankAlreadyExist(string bankAccountNumber, int accSetupGLBankId = 0)
        {
            return _accSetupGLBankRepository.Table.Any(x =>
                x.BankAccountNumber == bankAccountNumber &&
                (x.AccSetupGLBankId != accSetupGLBankId || accSetupGLBankId == 0)
            );
        }
        #endregion
        protected virtual void InsertBankDetails(AccSetupGLModel accSetupGLModel)
        {
            if (accSetupGLModel.AccSetupGLTypeId == 5 && accSetupGLModel.AccSetupGLBankList != null)
            {
                foreach (var item in accSetupGLModel.AccSetupGLBankList)
                {
                    // Create a new bank model object
                    AccSetupGLBank accSetupGLBankData = new AccSetupGLBank
                    {
                        AccSetupBalanceSheetId = accSetupGLModel.AccSetupBalancesheetId,
                        AccSetupGLId = accSetupGLModel.AccSetupGLId,
                        BankAccountName = item.BankAccountName,
                        BankBranchName = item.BankBranchName,
                        BankLimitAmount = item.BankLimitAmount,
                        RateOfInterest = item.RateOfInterest,
                        InterestMode = item.InterestMode,
                        IFSCCode = item.IFSCCode.ToUpper(),
                        BankAccountNumber = item.BankAccountNumber,
                    };
                    if (IsAccsetupGLBankAlreadyExist(item.BankAccountNumber))
                    {
                        throw new InvalidOperationException("Bank account number already exists.");
                    }

                    _accSetupGLBankRepository.Insert(accSetupGLBankData);
                }
            }
        }
    }
}