using GDi_API_IK.Model.DContext;
using GDi_API_IK.Model.Repositories;
using GDi_API_IK.Model.Repositories.Drivers;
using GDi_API_IK.Model.Services;
using GDi_API_IK.Model.Services.Drivers;
using Microsoft.EntityFrameworkCore;

namespace GDi_API_IK {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<IDriverService, DriverService>();
            builder.Services.AddScoped<IDriverRespository, DriverRepository>();
            //builder.Services.AddScoped<IDriverService, DriverService>();
            //builder.Services.AddScoped<IDriverRepository, DriverRepository>();

            builder.Services.AddCors(options => {
                options.AddDefaultPolicy(builder => {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
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