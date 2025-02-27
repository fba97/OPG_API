using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using OPG_API;
using Primitives;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDefaultDependencies();
// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins("https://localhost:7087"));
    options.AddPolicy("AllowAngularDevServer",
       builder =>
       {
           builder
               .WithOrigins("http://localhost:4200") // URL del tuo frontend Angular
               .AllowAnyMethod()
               .AllowAnyHeader();
       });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Use CORS middleware
app.UseCors("AllowOrigin");
app.UseCors("AllowAngularDevServer");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
