
using FineData.Domain;
using FineData.Persistence;
using FineData.Persistence.Repositories;
using FineDataApi.Repository;

namespace FineDataApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<FineDataContext>();
            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<IPersonRepository, PersonRepository>();

            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowEverything", // This is the open house we talked about!
                    builder =>
                    {
                        builder.AllowAnyOrigin() // Any origin is welcome...
                            .AllowAnyHeader() // With any type of headers...
                            .AllowAnyMethod(); // And any HTTP methods. Such a jolly party indeed!
                    });
            });

            //builder.Services.AddScoped(typeof(IRepository<Person>), typeof(Person<>));

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