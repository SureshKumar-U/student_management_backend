// using CrudAPi.Services;
using Microsoft.EntityFrameworkCore;

using CrudAPi.Data;
using CrudAPi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CrudAPi.Services;
using CrudAPi.Models;
using System.Text.Json.Serialization;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register AppDbContext with PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("LocalConnection")));

//allow enum conversion
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IstudentRepository, StudentRepository>();
builder.Services.AddScoped<IDepartmentService,DepartmentService>();
builder.Services.AddScoped<IDepartmentRepository,DeparmentRepository>();
builder.Services.AddScoped<ICourseService,CourseService>();
builder.Services.AddScoped<ICourseRepository,CourseRepository>();
builder.Services.AddScoped<IAdminDashboardRepository, AdminDashboardRepository>();
builder.Services.AddScoped<IAdminDashboardService, AdminDashboardService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

// Register your custom exception handling service
builder.Services.AddExceptionHandler<CrudAPi.Exceptions.CustomExceptionHandler>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();

//cors config

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

//jwt
//by default, cookie Authentication is the default authentication scheme
builder.Services.AddAuthentication(options =>
{
    
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
  {
  options.TokenValidationParameters = new TokenValidationParameters()
  {
      ValidateAudience = false,
      ValidateIssuer   = false,
      ValidateLifetime = true,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)),
  };
     // ─── ADDING JWT EVENTS HERE ───
   options.Events = new JwtBearerEvents
   {
               // Catches 401 Unauthorized errors (Expired, invalid, or missing token)
        OnChallenge = async context =>
        {
            // Skip the default behavior so it doesn't overwrite your custom response
            context.HandleResponse();

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var response = new { 
                error = "Unauthorized", 
                message = context.ErrorDescription ?? "You are not authorized to access this resource because your token is missing or invalid." 
            };
            
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        },
         // Catches 403 Forbidden errors (Token is valid, but the user lacks specific Roles/Claims)
        OnForbidden = async context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";

            var response = new { 
                error = "Forbidden", 
                message = "You do not have the required permissions or roles to view this resource." 
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

   };
  }
);


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    Console.WriteLine("developement............");
    app.UseSwagger();//create endpoint for swagger.json
    app.UseSwaggerUI(); //Generate Swagger UI
}
app.UseExceptionHandler(); 
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
