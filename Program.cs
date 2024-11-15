using CarolineProyect.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Añadir los servicios necesarios para los controladores de la API
builder.Services.AddControllers(); // Agrega soporte para controladores API
builder.Services.AddRazorPages();

builder.Services.AddSingleton(sp => new ExcelService("Data/data.xlsx"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()  // Permite cualquier origen
               .AllowAnyMethod()  // Permite cualquier método HTTP (GET, POST, PUT, DELETE)
               .AllowAnyHeader(); // Permite cualquier encabezado (headers)
    });
});

var app = builder.Build();
// Configuración del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseStaticFiles();

app.UseRouting();

// Mapear los controladores de la API
app.MapControllers(); // Esto mapea las rutas de los controladores de API

app.MapRazorPages();

app.Run();
