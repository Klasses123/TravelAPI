using System;
using TravelAPI.ViewModels.RequestModels;

namespace TravelAPI.ViewModels
{
    public class TownViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RegionViewModel Region { get; set; }
        public CompanyViewModel CompanyOrganizer { get; set; }
    }
}
