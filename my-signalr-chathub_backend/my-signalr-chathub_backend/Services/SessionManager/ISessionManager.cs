using System.Security.Claims;

namespace my_signalr_chathub_backend.Services.SessionManager
{
    public interface ISessionManager
    {
        string CreateSession(string jwtToken);
        public DateTimeOffset? GetSessionExpiry(string sessionId);
        public Task<string?> RefreshJwtToken(string sessionId);
        public DateTimeOffset? GetJwtTokenExpiry(string jwtToken);
        bool ValidateSession(string? sessionId, out ClaimsPrincipal o);
    }
}
