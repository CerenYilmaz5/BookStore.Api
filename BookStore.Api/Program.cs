using BookStore.Api;

var builder = WebApplication.CreateBuilder(args);

// Use the Startup class to configure the application
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, builder.Environment);

app.Run();