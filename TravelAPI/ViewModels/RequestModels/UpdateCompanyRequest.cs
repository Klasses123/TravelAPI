using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI.ViewModels.RequestModels
{
    public class UpdateCompanyRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public UserViewModel Owner { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
