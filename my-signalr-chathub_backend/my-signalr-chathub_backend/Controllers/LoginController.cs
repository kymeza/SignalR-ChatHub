using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_signalr_chathub_backend.Services.Login;

namespace my_signalr_chathub_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService; // You need to inject this
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        // POST: api/Login
        [HttpPost]
        public async Task<ActionResult<string>> Post(string rut, string password, string codigoAplicacionOrigen)
        {
            // validate the parameters
            if (string.IsNullOrWhiteSpace(rut) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(codigoAplicacionOrigen))
            {
                return BadRequest("Invalid parameters");
            }

            // Call the login service
            var result = await _loginService.LoginAsync(rut, password, codigoAplicacionOrigen);

            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }

        }
    }
}
