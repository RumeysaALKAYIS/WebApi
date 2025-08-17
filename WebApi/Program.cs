using Business.Abstracts;
using Business.Concretes;
using DataAccess.Concretes;
using DataAccess.Concretes.EntityFramework;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connStr = builder.Configuration.GetConnectionString("ConfigDb");
Console.WriteLine(connStr);
builder.Services.AddDbContext<ConfigDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("ConfigDb"),
        new MySqlServerVersion(new Version(8, 0, 31))
    ));
// ConfigurationReader service injection
builder.Services.AddScoped<Configuration>(sp =>
{
    var context = sp.GetRequiredService<ConfigDbContext>();
    var appName = builder.Configuration.GetValue<string>("ApplicationName");
    return new Configuration(context, appName);
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
