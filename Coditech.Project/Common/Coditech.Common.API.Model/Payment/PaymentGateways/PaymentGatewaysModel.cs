namespace Coditech.Common.API.Model
{
    public class PaymentGatewaysModel : BaseModel
    {
        public byte PaymentGatewayId { get; set; } 
        public string PaymentName { get; set; }  
        public string PaymentCode { get; set; }
        public bool IsActive { get; set; }   
    }
}
