using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GLMS_POE.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone] 
        public string PhoneNumber { get; set; }

        [Required]
        public string Region { get; set; }

        public List<Contract> Contracts { get; set; } = new List<Contract>();
    }
}
/////////////////////////////////////////// ----- End Of File ----- ///////////////////////////////////////////