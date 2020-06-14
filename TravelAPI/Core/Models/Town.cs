using System;
using System.ComponentModel.DataAnnotations.Schema;
using TravelAPI.Infrastructure.Interfaces;

namespace TravelAPI.Core.Models
{
    [Table("Towns")]
    public class Town : IDataModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Region Region { get; set; }
    }
}
