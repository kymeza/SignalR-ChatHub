using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace my_signalr_chathub_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAuthController : ControllerBase
    {
        // Inject any required services here (e.g., ISessionManager)

        public TestAuthController()
        {
            // Constructor for any initialization
        }

        [HttpGet("public")]
        public IActionResult PublicTest()
        {
            // This endpoint is accessible to everyone, even without authentication
            return Ok("This is a public endpoint.");
        }

        [HttpGet("private")]
        [Authorize]
        public IActionResult PrivateTest()
        {
            // This endpoint is only accessible to authenticated users
            return Ok("This is a private, authenticated endpoint.");
        }

        [HttpGet("userinfo")]
        [Authorize]
        public IActionResult GetUserInfo()
        {
            // Here you can access the user's ClaimsPrincipal via User property
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return Ok(claims);
        }
    }
}
