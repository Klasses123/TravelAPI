namespace TravelAPI.ViewModels.ResponseModels
{
    public class SignInResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public UserViewModel User { get; set; }
    }
}
