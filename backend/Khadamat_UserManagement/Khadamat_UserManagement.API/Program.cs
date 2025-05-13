using Khadamat_UserManagement.Application;
using Khadamat_UserManagement.Infrastructure;
using Serilog;
using Serilog.Events;
namespace Khadamat_UserManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddPresintationDependencies()
                .AddApplicationDependencies(builder.Configuration)
                .AddInfrastructureDependencies(builder.Configuration);


            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Information()
            //    .Enrich.FromLogContext()
            //    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Reduce Microsoft logs
            //    .MinimumLevel.Override("System", LogEventLevel.Warning)
            //    .WriteTo.Console()
            //    .WriteTo.File(
            //        path: "D:\\Khadamat\\Khadamat_UserManagement\\Khadamat_UserManagement.API\\Logs/app-.log",
            //        rollingInterval: RollingInterval.Day,
            //        retainedFileCountLimit: 7,
            //        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}{NewLine}")
            //    .CreateLogger();

            //builder.Services.AddLogging(lb =>
            //{
            //    lb.AddSerilog(dispose: true);
            //});

            builder.Host.UseSerilog();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll"); // Use the CORS policy

            app.AddInfrastructureMiddleware();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }

}
