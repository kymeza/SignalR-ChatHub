namespace my_signalr_chathub_backend.Models
{
    public class AuthResponse
    {
        public List<TokenInfo> ListadoTokens { get; set; }
        // Include other properties as needed
    }

    public class TokenInfo
    {
        public string Token { get; set; }
        // Include other properties as needed
    }
}
