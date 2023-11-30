using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebHook_Notifications;
using WebHook_Notifications.Services;

IConfiguration configuration;

var builderConfig = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile("appsettings.Development.json", optional: true)
        .AddEnvironmentVariables();

configuration = builderConfig.Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson().
                AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Agregamos el DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("MyDB"));
});

//Agregamos el AutoMapper que nos va a ayudar a mapear lo que nos manden en las peticiones HTTP hacia la base de datos
builder.Services.AddAutoMapper(typeof(Program));

//Agregando el Cors para que me puedan hacer peticiones desde los navegadores, desde cualquier direcci�n
builder.Services.AddCors(options =>
{
    options.AddPolicy("Generic", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddScoped<IRepositoryNotification, RepositoryNotification>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("Generic");

app.UseAuthorization();

app.MapControllers();

app.Run();
