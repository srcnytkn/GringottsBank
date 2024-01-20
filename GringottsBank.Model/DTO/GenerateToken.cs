namespace GringottsBank.Model.DTO
{
    public class GenerateTokenRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class GenerateTokenResponse
    {
        public string Token { get; set; }
    }
}
