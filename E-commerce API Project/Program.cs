using Ecommerce.BLL;
using Ecommerce.BLL.Settings;
using Ecommerce.DAL;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Microsoft.Extensions.FileProviders;

namespace E_commerce_API_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            /*----------------------------------------------------------*/
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            /*----------------------------------------------------------*/
            //managers
            builder.Services.AddScoped<IAuthManager, AuthManager>();
            builder.Services.AddScoped<ICategoryManager, CategoryManager>();
            builder.Services.AddScoped<IProductManager, ProductManager>();
            builder.Services.AddScoped<ICartManager, CartManager>();
            builder.Services.AddScoped<IOrderManager, OrderManager>();
            builder.Services.AddScoped<IImageManager, ImageManager>();


            //repos
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
            builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            /*----------------------------------------------------------*/

            //Validators    
            builder.Services.AddValidatorsFromAssembly(typeof(ProductCreateValidator).Assembly);
            /*----------------------------------------------------------*/

            //register the jwt settings DI container
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

            /*----------------------------------------------------------*/
            //resolve user manager and sign in manager
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<AppDbContext>();
            /*----------------------------------------------------------*/


            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });
            /*----------------------------------------------------------*/

            var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>() ?? new JwtSettings();

            builder.Services
                .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwtSettings.SecretKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });
            /*----------------------------------------------------------*/
            //policy based Authorization
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));

            });
            /*----------------------------------------------------------*/
            //static files configuration
            var rootPath = builder.Environment.ContentRootPath;//return the root path of the project
            var staticFilesPath = Path.Combine(rootPath, "Files");//combine the root path with the folder name to get the full path
            //Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            if (!Directory.Exists(staticFilesPath))
            {
                Directory.CreateDirectory(staticFilesPath);
            }
            builder.Services.Configure<StaticFileOptions>(options =>
            {
                options.FileProvider = new PhysicalFileProvider(staticFilesPath);
                options.RequestPath = "/Files";
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }
            app.UseStaticFiles(); //Enable serving static files
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
