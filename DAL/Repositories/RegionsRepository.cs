using chronos.DAL.Extensions;
using chronos.DAL.Interfaces;
using chronos.DAL.Models;
using Npgsql;
using NpgsqlTypes;

namespace chronos.DAL.Repositories;
using T = Region;

public class RegionsRepository: 
    IGetRepository<T>, IGetAllRepository<T>, ICreateRepository<T>, IUpdateRepository<T>, 
    IRemoveRepository<T>, IAsyncDisposable
{
    private readonly string _connectionString;
    private NpgsqlConnection? _connection;

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

    public async Task<long> Create(Region model, ITransientError error, CancellationToken ct = default)
    {
        try
        {
            if (_connection is null)
                await this.OpenConnection(ct);
            
            const string sql = "INSERT INTO regions(pretty_name, polygon) VALUES (@name, @polygon) RETURNING id";
            await using var command = new NpgsqlCommand(sql, _connection);
            command.Parameters.AddWithValue("name", model.Name);
            command.Parameters.AddWithValue("polygon", NpgsqlDbType.Polygon, model.Polygon.ToNpgsql());
            return (long)await command.ExecuteScalarAsync(ct);
        }
        catch (NpgsqlException ex)
        {
            if (ex.IsTransient)
            {
                error.Errno = TransientErrors.Timeout;
                return default;
            }
            else
                throw new DalException(ex.Message, ex);
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
    
    private async Task OpenConnection(CancellationToken ct = default)
    {
        _connection = new NpgsqlConnection(_connectionString);
        await _connection.OpenAsync(ct);
    }

    public async ValueTask DisposeAsync()
    {
        await _connection?.CloseAsync();
    }
}