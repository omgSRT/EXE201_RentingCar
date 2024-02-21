using BusinessObjects.Models;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RentingCarAPI.Startup;
using RentingCarRepositories.Repository;
using RentingCarRepositories.RepositoryInterface;
using RentingCarServices.Service;
using RentingCarServices.ServiceInterface;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//add cloudinary
#region Cloudinary
CloudinaryDotNet.Account cloudinaryAccount = new CloudinaryDotNet.Account
{
    Cloud = builder.Configuration["Cloudinary:CloudName"],
    ApiKey = builder.Configuration["Cloudinary:ApiKey"],
    ApiSecret = builder.Configuration["Cloudinary:ApiSecret"],
};
Cloudinary cloudinary = new Cloudinary(cloudinaryAccount);
builder.Services.AddSingleton(cloudinary);
#endregion

builder.Services.CustomSwagger();


// Add services to the container.
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();

//builder.Services.AddScoped<IConfiguration>();
//builder.Services.AddSingleton<UserManager>();
//builder.Services.AddSingleton<SignInManager>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IVehicleTypeService, VehicleTypeService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IReviewService, ReviewService>();


// Add services to the container.

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        RoleClaimType = ClaimTypes.Role,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey =  new SymmetricSecurityKey(Encoding
        .UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
