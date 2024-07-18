using Application.AuthServices;
using Core.Repository.Contract;
using Core.Services.Contract;
using Infrustructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Route.Talabat.APIs.Errors;
using RouteTechSummit.API.Helpers;
using System.Text;

namespace RouteTechSummit.API.Extentions
{
    public static class SystemServicesExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddAutoMapper(typeof(MappingProfiles));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                            .SelectMany(P => P.Value.Errors)
                                                            .Select(E => E.ErrorMessage)
                                                            .ToList();

                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);

                };
            });

            return services;

        }

        public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication().AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:ValidIssure"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudiance"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:AuthKey"] ?? string.Empty)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });


            services.AddScoped(typeof(IAuthServices), typeof(AuthServices));

            return services;
        }

    }
}
