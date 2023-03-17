using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Imagekit.Sdk;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Shared.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Shared.Interfaces;
using Shared.Services;

namespace WebAPI
{
    public class Program
    {
        public static IConfiguration Config { get; private set; } = null!;

        public static ImagekitClient Imagekit { get; private set; }


        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddControllers().AddJsonOptions(x =>
                            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            builder.Services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "issuer",
                        ValidAudience = "audience",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("webAPIProject"))
                    };
                });
            WebApplication app = builder.Build();
            Config = app.Configuration;

            Imagekit = new(Config["Imagekit:PublicKey"], Config["Imagekit:PrivateKey"], Config["Imagekit:Url"]);


            // Add services to the container            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDbContext<AppDbContext>();
            builder.Services.AddScoped<ICaregoryServices, CategoryServices>();
            builder.Services.AddScoped<IProductServices, ProductServices>();


            AppDbContext db = new();
            if (!db.Database.CanConnect())
            {
                db.Database.EnsureCreated();
            }

            Config = app.Configuration;

            Imagekit = new(Config["Imagekit:PublicKey"], Config["Imagekit:PrivateKey"], Config["Imagekit:Url"]);

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}