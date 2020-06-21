using System;
using System.ComponentModel.DataAnnotations;

namespace TravelAPI.ViewModels.RequestModels
{
    public class CreateCompanyRequest
    {
        [Required]
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        [Required]
        public string OwnerName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
