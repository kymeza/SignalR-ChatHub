using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using my_signalr_chathub_backend.Services.SessionManager;
using my_signalr_chathub_backend.Services.SessionStore;

namespace my_signalr_chathub_backend.Services.Session
{
    public class SessionManager : ISessionManager
    {
        private readonly ISessionStore _sessionStore;
        public SessionManager(ISessionStore sessionStore)
        {
            _sessionStore = sessionStore;
        }

        public string CreateSession(string jwtToken)
        {
            var sessionId = Guid.NewGuid().ToString(); // Unique session ID
            var tokenExpiry = GetJwtTokenExpiry(jwtToken); // Implement this to extract expiry from JWT
            _sessionStore.Store(sessionId, jwtToken, tokenExpiry);
            return sessionId;
        }

        public DateTimeOffset? GetJwtTokenExpiry(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(jwtToken))
            {
                throw new ArgumentException("The token doesn't seem to be in a proper JWT format.");
            }

            var token = handler.ReadJwtToken(jwtToken);
            var expiryUnixTimeSeconds = token.Payload.Expiration ?? throw new InvalidOperationException("The token doesn't have an 'exp' claim.");

            var epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
            return epoch.AddSeconds(expiryUnixTimeSeconds);
        }

        public DateTimeOffset? GetSessionExpiry(string sessionId)
        {
            var jwtToken = _sessionStore.Retrieve(sessionId); // This should return the JWT token or null if not found
            return GetJwtTokenExpiry(jwtToken);
        }

        public bool ValidateSession(string sessionId, out ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal = null;
            var jwtToken = _sessionStore.Retrieve(sessionId);

            if (string.IsNullOrWhiteSpace(jwtToken))
            {
                return false; // Session ID not found or no token associated with it
            }

            if (!IsTokenValid(jwtToken, out var claims))
            {
                return false; // Token is not valid (e.g., expired)
            }

            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Custom"));
            return true;
        }

        private bool IsTokenValid(string jwtToken, out IEnumerable<Claim> claims)
        {
            claims = null;
            try
            {
                var handler = new JwtSecurityTokenHandler();
                if (!handler.CanReadToken(jwtToken))
                {
                    return false;
                }

                var token = handler.ReadJwtToken(jwtToken);
                var expiry = GetJwtTokenExpiry(jwtToken);

                if (!expiry.HasValue || expiry.Value <= DateTimeOffset.UtcNow)
                {
                    return false; // Token is expired
                }

                claims = token.Claims;
                return true;
            }
            catch
            {
                // Log exception details if necessary
                return false;
            }
        }


        // Placeholder for refresh token logic
        public async Task<string?> RefreshJwtToken(string sessionId)
        {
            // Implement logic to refresh the token when it's about to expire
            throw new NotImplementedException();
        }


    }
}
