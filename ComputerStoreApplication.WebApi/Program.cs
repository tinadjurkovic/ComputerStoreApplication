using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ComputerStoreApplication.Data;
using ComputerStoreApplication.Data.Interfaces;
using ComputerStoreApplication.Data.Repositories;
using ComputerStoreApplication.Service.Interfaces;
using ComputerStoreApplication.Service.Profiles;
using ComputerStoreApplication.Service.Services;
using UniversityApplication.WebApi.Infrastructure;

namespace ComputerStoreApplication.WebApi
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json")
                .Build();

            Configuration = configuration;

            builder.Services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            builder.Services.AddDbContextPool<ComputerStoreDataContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(Configuration.GetSection("ConnectionStrings")
                    .GetSection("DefaultConnection").Value,
                    x => x.MigrationsAssembly("ComputerStoreApplication.Data"));
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CategoryProfile());
                mc.AddProfile(new ProductProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
