using System.Text.Json.Serialization;

using WebApi.Helpers;
using WebApi.Services;
using WebApi.Interfaces;

public class Startup {
    public IConfiguration configRoot {
        get;
    }
    public Startup(IConfiguration configuration) {
        configRoot = configuration;
    }
    public void ConfigureServices(IServiceCollection services) {
        services.AddDbContext<DataContext>();
        services.AddCors();
        services.AddControllers().AddJsonOptions(x =>
        {
            // serialize enums as strings in api responses (e.g. Role)
          x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

          // ignore omitted parameters on models to enable optional params (e.g. User update)
          x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // configure DI for application services
        services.AddScoped<IUserService, UserService>();

        // add swagger
        // builder.Services.AddEndpointsApiExplorer();
        services.AddSwaggerDocument();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env) {
        // global cors policy
        app.UseCors(x => x
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

        // global error handler
        app.UseMiddleware<ErrorHandlerMiddleware>();

        app.MapControllers();
        Console.WriteLine($"-----{app.Environment}");

        if (app.Environment.IsDevelopment())
        {
          app.UseOpenApi();
          app.UseSwaggerUi3();
        }

        // app.Run("http://localhost:4000");
        app.Run();
    }
}