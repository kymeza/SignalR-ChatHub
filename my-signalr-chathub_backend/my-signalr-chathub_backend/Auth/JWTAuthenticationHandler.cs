using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using my_signalr_chathub_backend.Services.SessionManager;
using System.Text.Encodings.Web;

namespace my_signalr_chathub_backend.Auth
{
    public class JWTAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ISessionManager _sessionManager;

        public JWTAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ISessionManager sessionManager)
            : base(options, logger, encoder, clock)
        {
            _sessionManager = sessionManager;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Cookies.TryGetValue("SessionCookie", out var sessionId))
            {
                return AuthenticateResult.NoResult();
            }

            if (!_sessionManager.ValidateSession(sessionId, out var claimsPrincipal))
            {
                return AuthenticateResult.Fail("Invalid session.");
            }

            var ticket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }

}
