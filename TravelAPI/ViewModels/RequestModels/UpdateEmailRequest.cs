using System.ComponentModel.DataAnnotations;

namespace TravelAPI.ViewModels.RequestModels
{
    public class UpdateEmailRequest
    {
        [Required]
        public string Id { get; set; } 
        [Required]
        public string NewEmail { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
