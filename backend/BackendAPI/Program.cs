using BackendAPI.Data;
using BackendAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BackendAPI",
        Version = "1.0.0",  // ✅ Keep version as a string, not numeric
        Description = "API documentation",
        Contact = new OpenApiContact
        {
            Name = "Support",
            Email = "support@example.com",
        }
    });

    c.EnableAnnotations();
});


// ✅ Database context (ensure correct connection string for Docker/SQL Server)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Register SMS Service
builder.Services.AddSingleton<SmsService>();

// ✅ CORS policy
const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// ✅ Global Exception Logging Middleware for debugging
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception: {ex.Message}");
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync($"{{\"error\": \"{ex.Message}\"}}");
    }
});

// ✅ Enable Swagger with correct version
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackendAPI v1");  // ✅ Fixed version mismatch
    c.RoutePrefix = string.Empty; // Serve Swagger at root
});

// ✅ Apply CORS before Authorization
app.UseCors(MyAllowSpecificOrigins);

// app.UseHttpsRedirection(); // Uncomment if HTTPS is configured
app.UseAuthorization();

// ✅ Map controllers
app.MapControllers();

app.Run();
