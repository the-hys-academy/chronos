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

var errno = new TransientError();
// ADD
// var region = new Region
// {
//     Name = "region 1",
//     Polygon = new []
//     {
//         new Region.GeoPoint{ Longitude = 1.5, Latitude = 3.0},
//         new Region.GeoPoint{ Longitude = 3, Latitude = 5.9},
//         new Region.GeoPoint{ Longitude = 5, Latitude = 9.7},
//     }
// };
//
// var id = await db.CreateAsync(region, errno);
// if (errno.Error == TransientErrors.Timeout)
//     Console.WriteLine("Error");
// else
//     Console.WriteLine($"id = {id}");

// UPDATE
// var region = new Region
// {
//     Id = 14,
//     Name = "region 14 new",
//     Polygon = new []
//     {
//         new Region.GeoPoint{ Longitude = 1.5, Latitude = 3.0},
//         new Region.GeoPoint{ Longitude = 3, Latitude = 5.9},
//     }
// };
// await db.UpdateAsync(region, errno);


// GET
// var region = await db.GetAsync(15, errno);
// if (region is null)
//     Console.WriteLine("NOT FOUND");
// else
// {
//     Console.WriteLine($"{region.Id} - {region.Name}");
//     foreach (var point in region.Polygon)
//     {
//         Console.WriteLine($"{point.Latitude} - {point.Longitude}");
//     }
// }

// GET ALL
await foreach (var region in db.GetAllAsync(errno))
{
    if (errno.Error == TransientErrors.None)
    {
        Console.WriteLine($"{region.Id} - {region.Name}");
        foreach (var point in region.Polygon)
        {
            Console.WriteLine($"{point.Latitude} - {point.Longitude}");
        }
    }
    Console.WriteLine();
}

// DELETE
// var region = await db.RemoveAsync(13, errno);
// if (region is null)
//     Console.WriteLine("NOT FOUND");
// else
// {
//     Console.WriteLine($"{region.Id} - {region.Name}");
//     foreach (var point in region.Polygon)
//     {
//         Console.WriteLine($"{point.Latitude} - {point.Longitude}");
//     }
// }


app.MapGet("/", () =>
{
    return "CHRONOS: the God of Time.";
});

app.Run();