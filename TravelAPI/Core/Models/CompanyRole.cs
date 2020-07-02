using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelAPI.Infrastructure.Interfaces;

namespace TravelAPI.Core.Models
{
    [Table("CompanyRoles")]
    public class CompanyRole : IDataModel
    {
        public const string OwnerRoleName = "Owner";
        public const string AdminRoleName = "Admin";

        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool CanAddPeoples { get; set; }
        public bool CanBeDeleted { get; set; }
        public bool CanCreateTravel { get; set; }
        public Company Company { get; set; }
        public virtual ICollection<UserCompanyRole> Users { get; set; }

        public static CompanyRole GetDefaultCompanyOwnerRole()
        {
            return new CompanyRole
            {
                Name = OwnerRoleName,
                CanAddPeoples = true,
                CanBeDeleted = false,
                CanCreateTravel = true
            };
        }

        public static CompanyRole GetDefaultCompanyAdminRole()
        {
            return new CompanyRole
            {
                Name = AdminRoleName,
                CanAddPeoples = true,
                CanBeDeleted = false,
                CanCreateTravel = true
            };
        }
    }
}
