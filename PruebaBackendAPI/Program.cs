using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PruebaBackendAPI.BLL.Interfaces;
using PruebaBackendAPI.BLL.Services;
using PruebaBackendAPI.DLL.Models;
using PruebaBackendAPI.DLL.Repositories;
using PruebaBackendAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AlumnosDbContext>(options =>
options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
new MySqlServerVersion(new Version(9, 6, 0)
)
                                                                                                                                            ));
builder.Services.AddScoped<IAlumnoService, AlumnoService>();
builder.Services.AddScoped<IAutorizacionService, AutorizacionService>();

builder.Services.AddScoped<CalificacionRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var key = builder.Configuration.GetValue<string>("JwtSettings:key");
var keyBytes = Encoding.ASCII.GetBytes(key);

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.Run();
