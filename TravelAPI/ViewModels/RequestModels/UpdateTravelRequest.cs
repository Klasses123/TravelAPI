using System;
using System.ComponentModel.DataAnnotations;

namespace TravelAPI.ViewModels.RequestModels
{
    public class UpdateTravelRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
