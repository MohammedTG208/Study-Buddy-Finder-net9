using System;
using System.Text;
using API.Data;
using API.Interfaces;
using API.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

//This Static class is used to extend the services in the application.
//It is used to add custom services to the application.
public static class MyAppServicesExtensions
{
    public static IServiceCollection AddMyAppServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();
        services.AddCors();
        // Add the DataContext to the services collection with a connection string from the configuration.
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });

        //Type of Dependency injection:
        // AddScoped: A new instance is created for each request.
        // AddSingleton: A single instance is created for the application's lifetime.
        // AddTransient: A new instance is created each time the service is requested.
        services.AddScoped<ITokenService, TokenServiceImp>();

        //Then return the services collection.
        // This method returns the IServiceCollection instance, allowing for method chaining.
        return services;
    }
}
