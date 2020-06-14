using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TravelAPI.Infrastructure.Interfaces;

namespace TravelAPI.Core.Models
{
    [Table("Regions")]
    public class Region : IDataModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Town> Towns { get; set; }
        public ICollection<Travel> Travels { get; set; }
    }
}
