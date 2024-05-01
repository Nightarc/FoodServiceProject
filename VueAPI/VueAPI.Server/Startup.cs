using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using VueAPI.Server.DataModels;

namespace VueAPI.Server
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_MyAllowSpecificOrigins";
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
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173",
                                           "https://localhost:5173");
                    });
            });

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
