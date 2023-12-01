using my_signalr_chathub_backend.Models.Config;
using my_signalr_chathub_backend.Services.Session;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using my_signalr_chathub_backend.Models;

namespace my_signalr_chathub_backend.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        private readonly AccessControlServerConfig _accessControlServerConfig;
        private readonly SessionManager _sessionManager;
        private readonly IHttpContextAccessor _httpContextAccessor; // You need to inject this to access the HttpContext

        public LoginService(HttpClient httpClient, AccessControlServerConfig accessControlServerConfig, SessionManager sessionManager, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _accessControlServerConfig = accessControlServerConfig;
            _sessionManager = sessionManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginResult> LoginAsync(string rut, string password, string codigoAplicacionOrigen)
        {
            // Prepare the request
            var request = new HttpRequestMessage(HttpMethod.Post, _accessControlServerConfig.Url);

            // Add headers from the configuration
            request.Headers.Add(_accessControlServerConfig.HeaderKey, _accessControlServerConfig.HeaderValue);

            // Add the request body
            request.Content = new StringContent(JsonSerializer.Serialize(new
            {
                rut,
                password,
                codigoAplicacionOrigen
            }), Encoding.UTF8, "application/json");

            try
            {
                // Send the request
                var response = await _httpClient.SendAsync(request);

                // Ensure the response is successful
                response.EnsureSuccessStatusCode();

                // Read the response content
                var content = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace(content))
                {
                    var jwtToken = ExtractTokenFromResponse(content);
                    var sessionId = _sessionManager.CreateSession(jwtToken);
                    SetSessionCookie(sessionId); // Pass sessionId instead of jwtToken

                    // Assume ExtractUserInfo is another method that extracts user info from the response

                    return new LoginResult
                    {
                        Success = true,
                        Message = "Login successful.",
                    };
                }

                return new LoginResult
                {
                    Success = false,
                    Message = "Login failed. Please check your credentials."
                };

            }
            catch (HttpRequestException e)
            {
                // Handle any errors here
                Console.WriteLine($"Request exception: {e.Message}");
                return new LoginResult
                {
                    Success = false,
                    Message = "Login failed. Something went wrong."
                };
            }

        }

        private void SetSessionCookie(string sessionId)
        {
            // Create a cookie with the session ID
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                // Set the cookie to expire at the same time as the JWT token
                Expires = _sessionManager.GetSessionExpiry(sessionId) // This method needs to be implemented in SessionManager
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("SessionCookie", sessionId, cookieOptions);
        }

        
        private string ExtractTokenFromResponse(string responseContent)
        {
            // Extracts the token from the ListadoTokens array in the response
            var authResponse = JsonSerializer.Deserialize<AuthResponse>(responseContent);
            if (authResponse?.ListadoTokens != null && authResponse.ListadoTokens.Any())
            {
                return authResponse.ListadoTokens[0].Token; // Assuming you want the first token
            }

            throw new InvalidOperationException("Token not found in response.");
        }
        
    }
}

