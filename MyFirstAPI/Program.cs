
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

			builder.Services.AddDbContext<ShopContext>(options =>
			{
				options.UseInMemoryDatabase("Shop");
			});


            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
					builder
						.WithOrigins("https://localhost:7109")
                        .WithHeaders("X-API-Version");
                });
            });


            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();
            app.UseCors();

            app.MapControllers();

			app.Run();
		}
	}
}