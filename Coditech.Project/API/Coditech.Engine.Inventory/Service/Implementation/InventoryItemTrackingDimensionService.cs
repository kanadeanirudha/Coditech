using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class InventoryItemTrackingDimensionService : IInventoryItemTrackingDimensionService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<InventoryItemTrackingDimension> _inventoryItemTrackingDimensionRepository;
        public InventoryItemTrackingDimensionService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _inventoryItemTrackingDimensionRepository = new CoditechRepository
                <InventoryItemTrackingDimension>(_serviceProvider.GetService<Coditech_Entities>());
        }

        //Create InventoryItemTrackingDimension.
        public virtual InventoryItemTrackingDimensionModel CreateInventoryItemTrackingDimension(InventoryItemTrackingDimensionModel inventoryItemTrackingDimensionModel)
        {
            if (IsNull(inventoryItemTrackingDimensionModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsInventoryItemTrackingDimensionCodeAlreadyExist(inventoryItemTrackingDimensionModel.TrackingDimensionCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "InventoryItemTrackingDimension Code"));

            InventoryItemTrackingDimension inventoryCategory = inventoryItemTrackingDimensionModel.FromModelToEntity<InventoryItemTrackingDimension>();

            //Create new InventoryItemTrackingDimension and return it.
            InventoryItemTrackingDimension inventoryCategoryData = _inventoryItemTrackingDimensionRepository.Insert(inventoryCategory);
            if (inventoryCategoryData?.InventoryItemTrackingDimensionId > 0)
            {
                inventoryItemTrackingDimensionModel.InventoryItemTrackingDimensionId = inventoryCategoryData.InventoryItemTrackingDimensionId;
            }
            else
            {
                inventoryItemTrackingDimensionModel.HasError = true;
                inventoryItemTrackingDimensionModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return inventoryItemTrackingDimensionModel;
        }

        #region Protected Method
        //Check if InventoryItemTrackingDimension code is already present or not.
        protected virtual bool IsInventoryItemTrackingDimensionCodeAlreadyExist(string trackingDimensionCode, short inventoryItemTrackingDimensionId = 0)
         => _inventoryItemTrackingDimensionRepository.Table.Any(x => x.TrackingDimensionCode == trackingDimensionCode && (x.InventoryItemTrackingDimensionId != inventoryItemTrackingDimensionId || inventoryItemTrackingDimensionId == 0));
        #endregion
    }
}
