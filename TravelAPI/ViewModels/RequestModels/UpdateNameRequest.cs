using System.ComponentModel.DataAnnotations;

namespace TravelAPI.ViewModels.RequestModels
{
    public class UpdateNameRequest
    {
        [Required]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
