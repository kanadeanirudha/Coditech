﻿using Coditech.API.Data;
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
                                        IsGroup = a.IsGroup,
                                        IsSystemGenerated= a.IsSystemGenerated,
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
            // Validate required fields.
            if (string.IsNullOrWhiteSpace(accSetupGLModel.GLName))
                throw new CoditechException(ErrorCodes.InvalidData, "GLName must be provided.");

            if (IsGLNameOrGLCodeAlreadyExist(accSetupGLModel.GLName, accSetupGLModel.GLCode, accSetupGLModel.AccSetupGLId))
            {
                throw new CoditechException(ErrorCodes.AlreadyExist,
                    string.Format(GeneralResources.ErrorCodeExists, "GL Name or GL Code"));
            }

            if (accSetupGLModel.ParentAccSetupGLId == null || accSetupGLModel.ParentAccSetupGLId < 1)
            {
                accSetupGLModel.ParentAccSetupGLId = accSetupGLModel.ParentAccSetupGLId = (int?)null;
            }

            // Create a new child model instance.
            AccSetupGLModel childModel = new AccSetupGLModel
            {
                GLName = accSetupGLModel.GLName,
                ParentAccSetupGLId = accSetupGLModel.ParentAccSetupGLId,
                IsActive = true,
                AccSetupBalancesheetId = accSetupGLModel.AccSetupBalancesheetId,
                AccSetupChartOfAccountTemplateId = (byte?)null,  // set to null if not valid.
                AccSetupGLTypeId = accSetupGLModel.AccSetupGLTypeId,
                AccSetupBalanceSheetTypeId = accSetupGLModel.AccSetupBalanceSheetTypeId,
                AccSetupCategoryId = accSetupGLModel.AccSetupCategoryId,
                GLCode = accSetupGLModel.GLCode,
                AltSetupGLId = accSetupGLModel.AccSetupBalancesheetId,
                IsGroup = accSetupGLModel.IsGroup,
                SelectedCentreCode = accSetupGLModel.SelectedCentreCode
            };

            // Map the view model to an entity.
            AccSetupGL childEntity = childModel.FromModelToEntity<AccSetupGL>();

            // Insert the new child record.
            AccSetupGL insertedEntity = _accSetupGLRepository.Insert(childEntity);
            if (insertedEntity == null || insertedEntity.AccSetupGLId < 1)
            {
                accSetupGLModel.HasError = true;
                accSetupGLModel.ErrorMessage = "Error occurred while creating the record.";
                return false;
            }

            // Update the input model with the new child's ID.
            accSetupGLModel.AccSetupGLId = insertedEntity.AccSetupGLId;

            // Optionally, insert a record into the BalanceSheet repository.
            List<AccSetupGLBalanceSheet> balanceSheets = new List<AccSetupGLBalanceSheet>
            {
                 new AccSetupGLBalanceSheet
                 {
                    AccSetupBalanceSheetId = accSetupGLModel.AccSetupBalancesheetId,
                    AccSetupGLId = insertedEntity.AccSetupGLId,
                    IsActive = true,
                 }
            };
            _accSetupGLBalanceSheetRepository.Insert(balanceSheets);

            return true;
        }
        //Delete AccountSetupGL.
        public virtual bool DeleteAccountSetupGL(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AccSetupGLID"));

            //// Convert parameterModel.Ids to a valid integer
            //if (!long.TryParse(parameterModel.Ids, out long accSetupGLId))
            //    throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AccSetupGLID"));

            //// Fetch the record and check if IsSystemGenerated is true in a single query
            //var isSystemGenerated = _accSetupGLRepository.Table.Where(x => x.AccSetupGLId == accSetupGLId).Select(x => x.IsSystemGenerated).FirstOrDefault();

            //// If the record is system-generated, prevent deletion
            //if (isSystemGenerated == true)
            //    throw new CoditechException(ErrorCodes.AlreadyExist, "Failed to delete: The record is system-generated and cannot be deleted.");

            // Proceed with deletion
            CoditechViewRepository<View_ReturnBoolean> objStoredProc =
                new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());

            objStoredProc.SetParameter("AccSetupGLId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);

            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteAccountSetupGL @AccSetupGLId, @Status OUT", 1, out status);

            return status == 1;
        }


        #region Protected Method
        // Check if GLName or GLCode already exists for a different AccSetupGLId
        protected virtual bool IsGLNameOrGLCodeAlreadyExist(string glName, string glCode, int accSetupGLId = 0)
        {
            return _accSetupGLRepository.Table.Any(x =>
                (x.GLName == glName || x.GLCode == glCode) &&
                (x.AccSetupGLId != accSetupGLId || accSetupGLId == 0));
        }
        #endregion
    }
}