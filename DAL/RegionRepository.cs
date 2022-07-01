using System;
using chronos.DAL;
using chronos.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Npgsql;
using NpgsqlTypes;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

public class RegionRepository : IRegionRepository
{
    private readonly string _connString;

    public RegionRepository(string connString)
    {
        _connString = connString ?? "Host=localhost;Username=postgres;Password=255855";
    }

    public async Task<long> Create(Region region, Errors error, CancellationToken ct = default)
    {
        try
        {
            await using var conn = new NpgsqlConnection(_connString);
            await conn.OpenAsync(ct);

            await using (var cmd = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS regions (id BIGSERIAL PRIMARY KEY, name VARCHAR(255) NOT NULL, polygon POLYGON)", conn))
                await cmd.ExecuteNonQueryAsync(ct);

            await using (var cmd = new NpgsqlCommand("INSERT INTO regions (name, polygon) VALUES (@name, @polygon) RETURNING id", conn))
            {
                cmd.Parameters.AddWithValue("name", region.Name);
                cmd.Parameters.AddWithValue("polygon", NpgsqlDbType.Polygon, region.Polygon);
                var id = await cmd.ExecuteScalarAsync(ct);
                error.ErrorNo = 0;
                return (long)id;
            }
        }
        catch (NpgsqlException exn)
        {
            error.ErrorNo = exn.IsTransient ? 1 : -1;

            if (error.ErrorNo == -1)
                throw exn;

            return 0;
        }
    }

    public async Task<Region> Delete(long id, Errors error, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Region> Get(long id, Errors error, CancellationToken ct = default)
    {
        try
        {
            await using var conn = new NpgsqlConnection(_connString);
            await conn.OpenAsync(ct);

            await using (var cmd = new NpgsqlCommand("SELECT * FROM regions WHERE id = @id", conn))
            {
                cmd.Parameters.AddWithValue("id", id);
                var reader = await cmd.ExecuteReaderAsync(ct);
                if (!reader.Read())
                {
                    error.ErrorNo = 1;
                    return null;
                }
                Region region = new Region();
                region.ID = reader.GetInt64(0);
                region.Name = reader.GetString(1);
                region.Polygon = reader.GetFieldValue<NpgsqlPolygon>(2);

                error.ErrorNo = 0;
                return region;
            }
        }
        catch (NpgsqlException exn)
        {
            error.ErrorNo = exn.IsTransient ? 1 : -1;

            if (error.ErrorNo == -1)
                throw exn;

            return null;
        }
    }

    public async Task<IEnumerable<Region>> Get(Errors error, CancellationToken ct = default)
    {
        try
        {
            await using var conn = new NpgsqlConnection(_connString);
            await conn.OpenAsync(ct);

            await using (var cmd = new NpgsqlCommand("SELECT * FROM regions", conn))
            {
                var reader = await cmd.ExecuteReaderAsync(ct);

                var regions = new List<Region>();
                while (await reader.ReadAsync(ct))
                {
                    var region = new Region
                    {
                        ID = reader.GetInt64(0),
                        Name = reader.GetString(1),
                        Polygon = reader.GetFieldValue<NpgsqlPolygon>(2)
                    };
                    regions.Add(region);
                }
                error.ErrorNo = 0;
                return regions;            }
        }
        catch (NpgsqlException exn)
        {
            error.ErrorNo = exn.IsTransient ? 1 : -1;

            if (error.ErrorNo == -1)
                throw exn;

            return null;
        }
    }

    public Task Update(Region region, Errors error, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}