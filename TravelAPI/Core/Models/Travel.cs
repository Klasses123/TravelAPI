using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelAPI.Infrastructure.Interfaces;

namespace TravelAPI.Core.Models
{
    [Table("Travels")]
    public class Travel : IDataModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public Company CompanyOrganizer { get; set; }
        public Region Region { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
