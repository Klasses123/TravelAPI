using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelAPI.Infrastructure.Interfaces;

namespace TravelAPI.Core.Models
{
    [Table("UserCompanyRoles")]
    public class UserCompanyRole : IDataModel
    {
        [Key, Required]
        public Guid Id { get; set; }
        public User User { get; set; }
        public CompanyRole CompanyRole { get; set; }
    }
}
