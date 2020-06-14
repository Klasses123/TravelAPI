using System;
using System.Collections.Generic;

namespace TravelAPI.ViewModels.RequestModels
{
    public class RegionViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<TownViewModel> Towns { get; set; }
        public ICollection<TravelViewModel> Travels { get; set; }
    }
}
