using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAPI.Core.Models
{
    [Table("UserCompanyRoles")]
    public class UserCompanyRole
    {
        [Key, Required]
        public Guid Id { get; set; }
        public User User { get; set; }
        public CompanyRole CompanyRole { get; set; }
    }
}
