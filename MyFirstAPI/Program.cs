
using Microsoft.EntityFrameworkCore;
using MyFirstAPI.Models;

namespace MyFirstAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			//builder.Services.AddControllers()
			//								.ConfigureApiBehaviorOptions(options =>
			//								{
			//									options.SuppressModelStateInvalidFilter = true;
			//								});

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();


            builder.Services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;//I set this to true because then the HTTP response also contains the version number
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;

                //instead of using https://localhost:7233/products?api-version=2.0
                //	options.ApiVersionReader = new QueryStringApiVersionReader("hps-api-version");//https://localhost:7233/products?hps-api-version=2.0

                //options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");// for header versioning
            });




            builder.Services.AddDbContext<ShopContext>(options =>
			{
				options.UseInMemoryDatabase("Shop");
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			//app.UseHttpsRedirection();
			app.UseHsts();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}