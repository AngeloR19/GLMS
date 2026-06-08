using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GLMS_POE.Models
{
    public class ServiceRequest
    {
        public int Id { get; set; }

        [Required]
        public int ContractId { get; set; }

        [ValidateNever]
        public Contract? Contract { get; set; }

        public string? ClientName { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string CurrencyCode { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal CostForeign { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CostZAR { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
/////////////////////////////////////////// ----- End Of File ----- ///////////////////////////////////////////