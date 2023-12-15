using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

using my_signalr_chathub_backend.Auth;
using my_signalr_chathub_backend.Hubs;
using my_signalr_chathub_backend.Models.Config;
using my_signalr_chathub_backend.Models.SuperTienda;
using my_signalr_chathub_backend.Services.Login;
using my_signalr_chathub_backend.Services.Session;
using my_signalr_chathub_backend.Services.SessionManager;
using my_signalr_chathub_backend.Services.SessionStore;


namespace my_signalr_chathub_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("https://localhost:4200") // Specify the exact origin
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()); // Allow credentials is important for SignalR
            });

            builder.Services.AddDbContext<SuperTiendaContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SuperTienda")));

            builder.Services.AddAutoMapper(typeof(Program));


            builder.Services.AddHttpContextAccessor();

            // Add services to the container.
            builder.Services.AddSignalR();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            AccessControlServerConfig accessControlServerConfig = builder.Configuration.GetSection("AccessControlServerConfig").Get<AccessControlServerConfig>();

            // Service Registration
            
            builder.Services.AddSingleton(accessControlServerConfig);
            builder.Services.AddHttpClient<ILoginService, LoginService>();
            builder.Services.AddSingleton<ISessionStore, InMemorySessionStore>();
            builder.Services.AddScoped<ISessionManager, SessionManager>();

            builder.Services.AddAuthentication("JWTAuthScheme")
                .AddScheme<AuthenticationSchemeOptions, JWTAuthenticationHandler>("JWTAuthScheme", null);

            builder.Services.AddAuthorization();

           

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.MapHub<ChatHub>("/chatHub");

            app.UseCors("AllowSpecificOrigin");

            app.Run();
        }
    }
}
