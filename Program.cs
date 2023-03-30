var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);
var services = builder.Services;

// add services to DI container
startup.ConfigureServices(services); // calling ConfigureServices method

var app = builder.Build();

// add Middlewares
startup.Configure(app, builder.Environment); // calling Configure method