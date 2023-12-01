using my_signalr_chathub_backend.Hubs;
using my_signalr_chathub_backend.Models.Config;
using my_signalr_chathub_backend.Services.Login;
using my_signalr_chathub_backend.Services.Session;


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
                    builder => builder.WithOrigins("http://localhost:4200") // Specify the exact origin
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()); // Allow credentials is important for SignalR
            });

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



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            

            app.MapControllers();

            app.MapHub<ChatHub>("/chatHub");

            app.UseCors("AllowSpecificOrigin");

            app.Run();
        }
    }
}
