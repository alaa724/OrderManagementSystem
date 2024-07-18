
using Core.Entities.Identity;
using Infrustructure._identity;
using Infrustructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Route.Talabat.APIs.Middlewares;
using RouteTechSummit.API.Extentions;

namespace RouteTechSummit.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.

            webApplicationBuilder.Services.AddControllers();
            webApplicationBuilder.Services.AddSwaggerServices();
            webApplicationBuilder.Services.AddApplicationServices();
            webApplicationBuilder.Services.AddEndpointsApiExplorer();

            webApplicationBuilder.Services.AddDbContext<OrderManagementDbContext>(options =>
            {
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
            });

            webApplicationBuilder.Services.AddDbContext<SystemIdentityDbContext>(options =>
            {
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("IdentityConnection"));
            });

            webApplicationBuilder.Services.AddAuthServices(webApplicationBuilder.Configuration);

            webApplicationBuilder.Services.AddIdentity<SystemUser, IdentityRole>().AddEntityFrameworkStores<OrderManagementDbContext>();


            #endregion

            var app = webApplicationBuilder.Build();
            // To Update Database 
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var _dbContext = services.GetRequiredService<OrderManagementDbContext>(); // Ask CLR For Creating Object From DbContext Explicity

            var _IdentitydbContext = services.GetRequiredService<SystemIdentityDbContext>(); // Ask CLR For Creating Object From DbContext Explicity

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbContext.Database.MigrateAsync(); // Update-Database

                await _IdentitydbContext.Database.MigrateAsync(); // Update-Database

            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, " An Error Occured While Migration ");
            }

            #region Configure Kestrel Middelwares

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
