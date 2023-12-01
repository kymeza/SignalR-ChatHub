using my_signalr_chathub_backend.Models;

namespace my_signalr_chathub_backend.Services.Login
{
    public interface ILoginService
    {
        Task<LoginResult> LoginAsync(string rut, string password, string codigoAplicacionOrigen);
    }
}
