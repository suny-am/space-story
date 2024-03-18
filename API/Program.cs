using API.Repositories;
using API.Utilities;
using Microsoft.EntityFrameworkCore;

var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env.api");
DotEnv.Load(dotenv);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<GameDbContext>(opt => {
    opt.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
