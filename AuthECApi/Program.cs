using AuthECApi.Controllers;
using AuthECApi.Extensions;
using AuthECApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddSwaggerExplorer()
                .InjectDbContext(builder.Configuration)
                .AddAppConfig(builder.Configuration)
                .AddIdentityHandlersAndStores()
                .ConfigureIdentityOptions()
                .AddIdentityAuth(builder.Configuration);

// Services from Identity Core
//builder.Services
//    .AddIdentityApiEndpoints<AppUser>()
//    .AddEntityFrameworkStores<AppDbContext>();

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    options.Password.RequireDigit = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequireLowercase = false;
//    options.User.RequireUniqueEmail = true;
//});

// Correctly add CORS policy here
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAngularApp",
//        policy =>
//        {
//            policy.WithOrigins("http://localhost:4200")
//                  .AllowAnyHeader()
//                  .AllowAnyMethod()
//                  .AllowCredentials();
//        });
//});

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DevDB")));

//builder.Services.AddAuthentication(x =>
//{
//    x.DefaultAuthenticateScheme=
//    x.DefaultChallengeScheme =
//    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(y =>
//{
//    y.SaveToken=false;
//    y.TokenValidationParameters= new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
//            builder.Configuration["AppSettings:JWTSecret"]!))
//    };
//});


var app = builder.Build();
app.UseCors("AllowAngularApp");
app.ConfigureSwaggerExplorer()
    .ConfigureCORS(builder.Configuration)
    .AddIdentityAuthMiddleware();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

// Apply CORS policy before mapping controllers
//app.UseCors("AllowAngularApp");
    
//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();
app.MapGroup("/api")
   .MapIdentityApi<AppUser>();
app.MapGroup("/api")
   .MapIdentityUserEndpoints()
   .MapAccountEndpoints()
   .MapAuthorizationDemoEndpoints();



app.Run();


