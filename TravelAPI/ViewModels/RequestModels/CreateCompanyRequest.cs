using System;
using System.ComponentModel.DataAnnotations;

namespace TravelAPI.ViewModels.RequestModels
{
    public class CreateCompanyRequest
    {
        [Required]
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public UserViewModel Owner { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
