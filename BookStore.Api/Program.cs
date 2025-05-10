using BookStore.Api;

var builder = WebApplication.CreateBuilder(args);

// Create and configure Startup
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app, builder.Environment);

app.Run();
