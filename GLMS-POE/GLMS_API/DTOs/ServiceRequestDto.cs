namespace GLMS_API.DTOs
{
    public class ServiceRequestDto
    {
        public int Id { get; set; }

        public int ContractId { get; set; }

        public string ClientName { get; set; }

        public string Description { get; set; }

        public string CurrencyCode { get; set; }

        public decimal CostForeign { get; set; }

        public decimal CostZAR { get; set; }

        public string Status { get; set; }
    }
}