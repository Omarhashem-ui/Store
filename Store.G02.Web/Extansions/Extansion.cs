using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Identity;
using Store.G02.Persistence;
using Store.G02.Persistence.Identity;
using Store.G02.Services;
using Store.G02.Shared.AuthDto;
using Store.G02.Shared.ErrorModels;
using Store.G02.Web.MiddleWares;
using System.Text;

namespace Store.G02.Web.Extansions
{
    public static class Extansion
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services ,IConfiguration configuration)
        {
            services.AddInfrastructureServices(configuration);
            services.AddApplicationServices(configuration);
            services.AddWebServices();
            services.AddIdentityServices();
            services.Configure<JwtOptions>(configuration.GetSection("JWTOption"));
           services.ConfigureApiBehaviorOption();
            services.AddAuthenticationService(configuration);


            return services;
        }
        private static IServiceCollection ConfigureApiBehaviorOption(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var error = actionContext.ModelState.Where(M => M.Value.Errors.Any())
                                                        .Select(M => new ValdiationError()
                                                        {
                                                            Field = M.Key,
                                                            Errors = M.Value.Errors.Select(E => E.ErrorMessage)
                                                        });
                    var response = new ValdiationErrorResponse()
                    {
                        Errors = error
                    };
                    return new BadRequestObjectResult(response);
                };
            });
            return services;
        }
        private static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            // Add services to the container.

             services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
             services.AddEndpointsApiExplorer();
             services.AddSwaggerGen();
            return services;
        }
        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentityCore<AppUser>(options =>
            {
                options.User.RequireUniqueEmail=true;
            }).AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityStoreDbContext>();
            return services;
        }
        private static IServiceCollection AddAuthenticationService(this IServiceCollection services ,IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JWTOption").Get<JwtOptions>();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
                };
            });
            return services;
        }
       


        public static async Task<WebApplication> ConfiguareMiddleWaresAsync (this WebApplication app)
        {
            await app.SeedData();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseGlobalErrorHandling();
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
           


            app.MapControllers();
            return app;
        }

        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }

        private static async Task<WebApplication> SeedData(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var dbinitilizer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbinitilizer.InitializeAsync();
            await dbinitilizer.IdentityInitializeAsync();
            return app;
        }

    }
}
