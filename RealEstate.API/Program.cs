using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RealEstate.Application.Services;
using RealEstate.Application.Services.Implementations;
using RealEstate.DataAccess.Dapper;
using RealEstate.DataAccess.Dapper.Implementations;
using System.Text;
using static System.Net.WebRequestMethods;

string[] allowedOrigins =
{"https://realestate.planetearthsolutions.com.np",
                        "http://localhost:3000",
                        "https://localhost:433"
};

var builder = WebApplication.CreateBuilder(args);

string UploadFolder = Path.Combine(Directory.GetCurrentDirectory(), builder.Configuration["FileStorage:UploadPath"]!);
if (!Directory.Exists(UploadFolder))
{
    Directory.CreateDirectory(UploadFolder);
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opts =>
{
    var title = "Real Estate API";
    var description = "API for managing real estate operations";

    opts.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = title,
        Version = "v1",
        Description = description
    });
});

builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IAccountingService, AccountingService>();
builder.Services.AddScoped<ICommonService, CommonService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddTransient<FileService>();


builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"]!)
            )
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (string.IsNullOrWhiteSpace(context.Token) &&
                    context.Request.Cookies.TryGetValue("AccessToken", out var accessToken))
                {
                    context.Token = accessToken;
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddCors(options =>
{

    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(opts =>
{
    opts.RoutePrefix = "";
    opts.SwaggerEndpoint("/swagger/v1/swagger.json", "Real Estate API v1");
});

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(UploadFolder),
    RequestPath = "/api/uploads"
});


app.UseRouting();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();