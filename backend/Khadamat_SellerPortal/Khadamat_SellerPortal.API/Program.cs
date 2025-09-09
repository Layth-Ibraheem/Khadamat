
using Khadamat_SellerPortal.API.Mappings;
using Khadamat_SellerPortal.Application;
using Khadamat_SellerPortal.Infrastructure;

namespace Khadamat_SellerPortal.API
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
            builder.Services
                .AddInfrastructureDependencies(builder.Configuration)
                .AddApplicationServices()
                .AddMappings();
                
            builder.Services.AddHttpContextAccessor();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseInfrastructureMiddleware();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
