using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GLMS_POE.Models
{
    public class Contract
    {
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        [ValidateNever]
        public Client? Client { get; set; }

        public string? ClientName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;

        [Required]
        public string ServiceLevel { get; set; } = string.Empty;

        [ValidateNever]
        public string FilePath { get; set; } = string.Empty;
    }
}
/////////////////////////////////////////// ----- End Of File ----- ///////////////////////////////////////////