using System;

namespace TravelAPI.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public DateTime RegisterOn { get; set; }
        public CompanyViewModel Company { get;set; }
    }
}
