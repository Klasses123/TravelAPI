using TravelAPI.Core.Models;

namespace TravelAPI.ViewModels.RequestModels
{
    public class CreateTravelRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Company CompanyOrganizer { get; set; }
        public Region Region { get; set; }
    }
}
