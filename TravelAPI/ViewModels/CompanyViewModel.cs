using System;
using System.Collections.Generic;

namespace TravelAPI.ViewModels
{
    public class CompanyViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public UserViewModel Owner { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<UserViewModel> CompanyUsers { get; set; }
        public virtual ICollection<TravelViewModel> Travels { get; set; }
    }
}
