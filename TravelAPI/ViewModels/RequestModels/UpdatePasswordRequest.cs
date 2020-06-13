namespace TravelAPI.ViewModels.RequestModels
{
    public class UpdatePasswordRequest
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
