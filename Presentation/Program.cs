using Presentation;

var builder = WebApplication.CreateBuilder(args);
//Congiure Startup
builder.ConfigureServices();

var app = builder.Build();
//Congiure Startup
app.Configure();

app.Run();
