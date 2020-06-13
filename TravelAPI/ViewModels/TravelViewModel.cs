using System;

namespace TravelAPI.ViewModels
{
    public class TravelViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CompanyViewModel CompanyOrganizer { get; set; }
    }
}
