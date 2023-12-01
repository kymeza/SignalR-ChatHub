using Microsoft.AspNetCore.Session;
using System.IdentityModel.Tokens.Jwt;

namespace my_signalr_chathub_backend.Services.Session
{
    // Located in Services/SessionManager.cs
    public class SessionManager
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

        private DateTimeOffset? GetJwtTokenExpiry(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(jwtToken))
            {
                throw new ArgumentException("The token doesn't seem to be in a proper JWT format.");
            }

            var token = handler.ReadJwtToken(jwtToken);
            var expiryUnixTimeSeconds = token.Payload.Exp ?? throw new InvalidOperationException("The token doesn't have an 'exp' claim.");

            var epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
            return epoch.AddSeconds(expiryUnixTimeSeconds);
        }

        public DateTimeOffset? GetSessionExpiry(string sessionId)
        {
            var jwtToken = _sessionStore.Retrieve(sessionId); // This should return the JWT token or null if not found
            return jwtToken != null ? GetJwtTokenExpiry(jwtToken) : null;
        }


        // Placeholder for refresh token logic
        public async Task<string?> RefreshJwtToken(string sessionId)
        {
            // Implement logic to refresh the token when it's about to expire
            throw new NotImplementedException();
        }

   
       
    }


}
