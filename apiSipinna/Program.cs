using apiSipinna.Models;
using Microsoft.EntityFrameworkCore;
using apiSipinna.CRUD;
using apiSipinna.Services.ArchivosEstadisticos;

//XLS Reader Required
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

//CORS
var allowLocalHost = "localhostOrigin";
//


var builder = WebApplication.CreateBuilder(args);

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowLocalHost,
    policy =>
    {
        policy.WithOrigins("http://localhost");
    });
});
//

// Add services to the container.
string? cadena = builder.Configuration.GetConnectionString("DefaultConnection") ?? "otracadena";
builder.Services.AddControllers();
//conexion a base de datos
builder.Services.AddDbContext<Conexiones>(opt =>
    opt.UseMySQL(cadena));
//builder.services.AddScoped<EstadisticaDAO>();    

builder.Services.AddTransient<IOperations, Operations>();
builder.Services.AddTransient<ArchivosEstadisticos>();
builder.Services.AddTransient<EstadisticaDAO>();

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

//CORS
app.UseCors(allowLocalHost);
//

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
