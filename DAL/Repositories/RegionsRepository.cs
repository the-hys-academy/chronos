using AutoMapper;
using chronos.DAL.Interfaces;
using chronos.DAL.Models;
using Npgsql;
using NpgsqlTypes;

namespace chronos.DAL.Repositories;
using T = Region;

public class RegionsRepository: 
    IGetRepository<T>, IGetAllRepository<T>, ICreateRepository<T>, IUpdateRepository<T>, IRemoveRepository<T>
{
    private readonly string _connectionString;
    // private readonly IMapper _mapper;
    
    public RegionsRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task<Region> Get(long id, ITransientError error, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<Region> GetAllAsync(ITransientError error, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Region> Create(Region model, ITransientError error, CancellationToken ct = default)
    {
        var polygon = new NpgsqlPolygon(model.Polygon
            .Select(x => new NpgsqlPoint(x.Latitude, x.Longitude)));
        
        try
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(ct);
            
            const string sql = "INSERT INTO regions(pretty_name, polygon) VALUES (@name, @polygon)";
            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("name",  model.Name);
            command.Parameters.AddWithValue("polygon", polygon);
            await command.ExecuteNonQueryAsync(ct);
            return default;
        }
        catch (NpgsqlException ex)
        {
            throw ex;
        }
    }

    public async Task Update(Region model, ITransientError error, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Region> Remove(long id, ITransientError error, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}