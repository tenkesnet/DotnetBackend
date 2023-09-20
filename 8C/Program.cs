
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Tanulok.Models;
using Tanulok.Repository;

namespace Tanulok;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddSingleton<DapperContext>();
        builder.Services.AddDbContext<IskolaContext>(
            options => options.UseNpgsql("name=ConnectionStrings:SqlConnection"));
        builder.Services.AddSingleton<ITanuloRepository, TanuloRepository>();
        builder.Services.AddSingleton<ITanarRepository, TanarRepository>();
        builder.Services.AddSingleton<ILakcimRepository, LakcimRepository>();
        builder.Services.AddScoped<IAlkalmazottRepository, AlkalmazottEFRepository>();
        builder.Services.AddScoped<IAutoEFRepository, AutoEFRepository>();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers().AddJsonOptions(x =>                       //Ez az utasítássor megakadályozza, hogy végtelen ciklusba kerüljön a json végpont
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        var app = builder.Build();

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
    }
}
