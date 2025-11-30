using Application.User;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Database;
using Infrastructure.User;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<VocyDbContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration["ConnectionStrings:DatabaseConnection"]);
});

builder.Services.AddScoped<IUserRepository, PgUserRepository>();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapGet("/", () => "Hello world!")
    .WithName("GetWeatherForecast");

app.MapControllers();
app.Run();