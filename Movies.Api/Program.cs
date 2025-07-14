using Microsoft.EntityFrameworkCore;
using Movies.Api.Extensions;
using Movies.Core.DomainContracts;
using Movies.Data.Data;
using Movies.Data.Repositories;
using System.Reflection.Metadata;

namespace Movies.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                     
            builder.Services.AddDbContext<MovieApiContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MovieApiContext") ?? 
                    throw new InvalidOperationException("Connection string 'MovieApiContext' not found.")));
            
            builder.Services.AddControllers()
                            .AddApplicationPart(typeof(AssemblyReference).Assembly);

            builder.Services.AddRepositories();
            builder.Services.AddServiceLayer();
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MapperProfile>());
            builder.Services.AddSwaggerGen(opt => opt.EnableAnnotations());
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
                await app.SeedDataAsync();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();

        }
    }
}
