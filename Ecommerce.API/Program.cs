using Ecommerce.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Interfaces;
using Ecommerce.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EcommerceContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Connection"),
        b => b.MigrationsAssembly("Ecommerce.Data") // Ensamblado de migraciones
    )
);
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<ILoteService, LoteService>();
builder.Services.AddScoped<IS3Service, S3Service>();


var app = builder.Build();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
