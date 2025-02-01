using Ecommerce.Data.Contexts;
using Ecommerce.Interfaces;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ecommerce.API.Middleware;
using Amazon.CognitoIdentityProvider;
using Amazon.Runtime;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("Connection") ?? builder.Configuration.GetConnectionString("Connection");
var awsRegion = Environment.GetEnvironmentVariable("Region") ?? builder.Configuration["AWS:Region"];
var awsUserPoolId = Environment.GetEnvironmentVariable("UserPoolId") ?? builder.Configuration["AWS:UserPoolId"];
var awsAccessKeyId = Environment.GetEnvironmentVariable("AccessKeyId") ?? builder.Configuration["AWS:AccessKeyId"];
builder.Configuration["AWS:AppClientId"] = Environment.GetEnvironmentVariable("AppClientId") ?? builder.Configuration["AWS:AppClientId"];
var awsBucketName = Environment.GetEnvironmentVariable("BucketName") ?? builder.Configuration["AWS:BucketName"];
builder.Configuration["AWS:ClientSecretId"] = Environment.GetEnvironmentVariable("ClientSecretId") ?? builder.Configuration["AWS:ClientSecretId"];
var awsSecretAccessKey = Environment.GetEnvironmentVariable("SecretAccessKey") ?? builder.Configuration["AWS:SecretAccessKey"];

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = $"https://cognito-idp.{awsRegion}.amazonaws.com/{awsUserPoolId}";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = $"https://cognito-idp.{awsRegion}.amazonaws.com/{awsUserPoolId}",
        ValidateLifetime = true,
        LifetimeValidator = (before, expires, token, param) => expires > DateTime.UtcNow,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["AWS:AppClientId"],
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Admin", policy => policy.RequireRole("Admin"))
    .AddPolicy("Cliente", policy => policy.RequireRole("Cliente"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<ILoteService, LoteService>();
builder.Services.AddScoped<IS3Service, S3Service>();

builder.Services.AddSingleton<IAmazonCognitoIdentityProvider>(
    new AmazonCognitoIdentityProviderClient
                (
                    new BasicAWSCredentials
                    (
                        awsAccessKeyId,
                        awsSecretAccessKey
                    ),
                    Amazon.RegionEndpoint.GetBySystemName(awsRegion)
                ));

builder.Services.AddDbContext<EcommerceContext>(options =>
    options.UseSqlServer(
        connectionString,
        b => b.MigrationsAssembly("Ecommerce.Data") // Ensamblado de migraciones
    )
);


var app = builder.Build();


app.UseAuthentication();
app.UseMiddleware<AuthMiddleware>();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var DbContext = scope.ServiceProvider.GetRequiredService<EcommerceContext>();

    if (DbContext.Database.CanConnect())
    {
        DbContext.Database.EnsureCreated();
    }
    else
    {
        Console.WriteLine("No se pudo conectar a la base de datos");
    }
};

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();
