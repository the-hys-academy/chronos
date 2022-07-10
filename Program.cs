using chronos.DAL;
using chronos.DAL.Models;
using chronos.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await using var db = new RegionsRepository(app.Configuration.GetConnectionString("DefaultConnection"));

var errno = new TransientErrorWrapper();
var region = new Region
{
    Name = "region 1",
    Polygon = new []
    {
        new Region.GeoPoint{ Longitude = 1.5, Latitude = 3.0},
        new Region.GeoPoint{ Longitude = 3, Latitude = 5.9},
        new Region.GeoPoint{ Longitude = 5, Latitude = 9.7},
    }
};

var id = await db.Create(region, errno);
if (errno.Errno == TransientErrors.Timeout)
    Console.WriteLine("Error");
else
    Console.WriteLine($"id =  {id}");


app.MapGet("/", () =>
{
    return "CHRONOS: the God of Time.";
});

app.Run();