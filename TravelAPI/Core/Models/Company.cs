using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelAPI.Infrastructure.Interfaces;

namespace TravelAPI.Core.Models
{
    [Table("Companies")]
    public class Company : IDataModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public User Owner { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<User> CompanyUsers { get; set; }
        public virtual ICollection<Travel> Travels { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
