using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Server.Modelos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Agregar el servicio de DbContext y configurar SQL Server
builder.Services.AddDbContext<PruebaTecnicaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQL")));
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Muestra detalles de errores en desarrollo
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");  // Manejador global de errores en producción
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
