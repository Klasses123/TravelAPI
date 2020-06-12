using System;
using System.ComponentModel.DataAnnotations;

namespace TravelAPI.ViewModels.RequestModels
{
    public class CreateUserRequest
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime RegisteredOn { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
