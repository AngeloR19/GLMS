using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GLMS_API.Models
{
    public class ServiceRequest
    {
        public int Id { get; set; }

        [Required]
        public int ContractId { get; set; }

        [ValidateNever]
        public Contract Contract { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string CurrencyCode { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CostForeign { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CostZAR { get; set; }

        public string Status { get; set; }
    }
}
/////////////////////////////////////////// ----- End Of File ----- ///////////////////////////////////////////
