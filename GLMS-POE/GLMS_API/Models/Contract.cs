using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GLMS_API.Models
{
    public class Contract
    {
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        [ValidateNever]
        public Client Client { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string ServiceLevel { get; set; }

        [ValidateNever]
        public string FilePath { get; set; }
    }
}
/////////////////////////////////////////// ----- End Of File ----- ///////////////////////////////////////////
