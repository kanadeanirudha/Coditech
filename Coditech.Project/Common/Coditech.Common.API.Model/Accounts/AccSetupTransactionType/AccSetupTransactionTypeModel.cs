namespace Coditech.Common.API.Model
{
    public partial class AccSetupTransactionTypeModel : BaseModel
    {
        public short AccSetupTransactionTypeId { get; set; }
        public string TransactionTypeCode { get; set; }
        public string TransactionTypeName { get; set; }
        public bool IsActive { get; set; }
    }
}
