using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data;
using my_signalr_chathub_backend.Models.SuperTienda;
using System.Diagnostics;
using System.Text;
using my_signalr_chathub_backend.Utils;


namespace my_signalr_chathub_backend.Controllers.Business
{
    [Route("api/[controller]")]
    [ApiController]
    public class VulnerableController : ControllerBase
    {
        private readonly IDbConnection _connection;

        public VulnerableController(IDbConnection connection)
        {
            _connection = connection;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            var query = @"SELECT TOP 10
                            [ID Articulo] AS IdArticulo, 
                            [ID Sub-Categoria] AS IdSubCategoria, 
                            [Producto] AS Producto, 
                            [PrecioUnitario] AS PrecioUnitario, 
                            [CostoUnitario] AS CostoUnitario 
                        FROM 
                            [Products]
                        ";
            var products = await _connection.QueryAsync<ProductDto>(query);
            return Ok(products);
        }

        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetProducts(string id)
        {
            var query = $@"SELECT TOP 10
                    [ID Articulo] AS IdArticulo, 
                    [ID Sub-Categoria] AS IdSubCategoria, 
                    [Producto] AS Producto, 
                    [PrecioUnitario] AS PrecioUnitario, 
                    [CostoUnitario] AS CostoUnitario 
                FROM 
                    [Products]
                WHERE
                    [ID Articulo] = '{id}'"; // This is insecure!
            var product = await _connection.QueryAsync<ProductDto>(query);
            return Ok(product);

        }

        [HttpPost("upfile")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, [FromForm] string fileName)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file provided.");
            }

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");

            var extension = Path.GetExtension(file.FileName);

            fileName = fileName + extension;

            var fullPath = Path.Combine(uploadPath, fileName);

            try
            {
                // Save file to disk
                using (var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, such as access denied, path not found, etc.
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }

            return Ok("File uploaded successfully.");
        }

        [HttpPost("run")]
        public IActionResult RunCommand([FromBody] CommandDto commandDto)
        {
            var output = new StringBuilder();
            var error = new StringBuilder();

            // WARNING: This code is insecure and is for demonstration purposes only.
            using (var process = new Process())
            {
                process.StartInfo.FileName = "powershell.exe";
                process.StartInfo.Arguments = commandDto.Command; // Insecure
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.OutputDataReceived += (sender, args) => output.AppendLine(args.Data);
                process.ErrorDataReceived += (sender, args) => error.AppendLine(args.Data);

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
            }

            

            return Ok(output.ToString());
        }

        [HttpPost("checkUser")]
        public async Task<IActionResult> CheckUser([FromBody] CredentialsDto credentials)
        {
            var query = $"SELECT * FROM [Users] WHERE username = '{credentials.username}'";
            var user = await _connection.QueryAsync<DemoUserDto>(query);
            
            if (user.FirstOrDefault() == null)
            {
                return NotFound("User notfound");
            }

            if (Argon2PasswordHasher.VerifyHashedPassword(user.FirstOrDefault().password, credentials.password))
            {
                return Ok("User found and validated");
            }

            return Unauthorized("User found but not validated");
        }

    }

    public class DemoUserDto
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class CommandDto
    {
        public string Command { get; set; }
    }
    public class CredentialsDto
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
