using FoodServiceAPI.DataModels;
using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using LinqToDB.Data;
using System;
using System.Runtime.CompilerServices;

namespace FoodServiceAPI
{
    public class Startup
    {
        public IConfiguration? Configuration { get; }

  
        public static void Main(string[] args) 
        { 
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddLinqToDBContext<PostgresDB>((provider, options)
                => options
                    .UsePostgreSQL("Server=localhost;Port=5432;Database=postgres;UserId=postgres;Password=admin;")
                    //default logging will log everything using the ILoggerFactory configured in the provider
                    .UseDefaultLogging(provider));

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
}
