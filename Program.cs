using chronos.DAL;
using chronos.DAL.Models;
using chronos.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var db = new RegionsRepository(app.Configuration.GetConnectionString("DefaultConnection"));
var errno = new TransientErrorWrapper();
var region = new Region
{
    Name = "region 1",
    Polygon = new []
    {
        new Region.GeoPoint{ Latitude = 1, Longitude = 1},
        new Region.GeoPoint{ Latitude = 2, Longitude = 2},
        new Region.GeoPoint{ Latitude = 1, Longitude = 1},
    }
};
await db.Create(region, errno);


app.MapGet("/", () =>
{
    return "CHRONOS: the God of Time.";
});

app.Run();