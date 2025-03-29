using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

//It like inject all services I have in the application from MyAppServicesExtensions.cs and AuthServiceExtensions.cs
// Add services to the container.
builder.Services.AddMyAppServices(builder.Configuration);
builder.Services.AddAuthServiceExtensions(builder.Configuration);



var app = builder.Build();

// Add CORS middleware (order is important - put this before UseAuthorization)
app.UseCors(
    policy=>policy
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:4200","https://localhost:4200")
);

// the order of middleware is important here Authentication must be before Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
