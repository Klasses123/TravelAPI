using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI.ViewModels.ResponseModels
{
    public class SignInResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
