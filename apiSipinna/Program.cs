using apiSipinna.Models;
using Microsoft.EntityFrameworkCore;
using apiSipinna.CRUD;


//XLS Reader Required
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? cadena = builder.Configuration.GetConnectionString("DefaultConnection") ?? "otracadena";
builder.Services.AddControllers();
//conexion a base de datos
builder.Services.AddDbContext<Conexiones>(opt =>
    opt.UseMySQL(cadena));
//builder.services.AddScoped<EstadisticaDAO>();    

builder.Services.AddTransient<IOperations, Operations>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var serviceProvider = serviceScope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<Conexiones>();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
