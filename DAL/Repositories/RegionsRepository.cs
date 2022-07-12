using System.Data.Common;
using chronos.DAL.Extensions;
using chronos.DAL.Interfaces;
using chronos.DAL.Models;
using Npgsql;
using NpgsqlTypes;

namespace chronos.DAL.Repositories;
using T = Region;

public class RegionsRepository:
    IGetRepository<T>, 
    IGetAllRepository<T>, 
    IAddRepository<T>, 
    IUpdateRepository<T>, 
    IRemoveRepository<T>,
    IAsyncDisposable
{
    private readonly string _connectionString;
    private NpgsqlConnection? _connection;

    public RegionsRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<Region?> GetAsync(long id, ITransientError error, CancellationToken ct = default)
    {
        const string sql = "SELECT * FROM regions WHERE id = @id";

        try
        {
            if (_connection is null)
                await this.OpenConnectionAsync(ct);
            
            await using var command = new NpgsqlCommand(sql, _connection);
            command.Parameters.AddWithValue("id", NpgsqlDbType.Bigint, id);

            await using var reader = await command.ExecuteReaderAsync(ct);
            if (!reader.HasRows || !await reader.ReadAsync(ct))
                return null;

            error.Errno = 0;
            return new Region
            {
                Id = reader.GetInt64(0),
                Name = reader.GetString(1),
                Polygon = ((NpgsqlPolygon)reader.GetValue(2)).ToModel()
            };
        }
        catch (NpgsqlException ex)
        {
            if (ex.IsTransient)
            {
                error.Errno = 1;
                return default;
            }
            else
                throw new DalException(ex.Message, ex);
        }
    }

    public async IAsyncEnumerable<Region> GetAllAsync(ITransientError error, CancellationToken ct = default)
    {
        const string sql = "SELECT * FROM regions";

        NpgsqlDataReader? reader = null;
        try
        {
            if (_connection is null)
                await this.OpenConnectionAsync(ct);
            
            await using var command = new NpgsqlCommand(sql, _connection);
            reader = await command.ExecuteReaderAsync(ct);
            if (!reader.HasRows)
                yield break;
        }
        catch (NpgsqlException ex)
        {
            if (ex.IsTransient)
            {
                error.Errno = 1;
                yield break;
            }
            else
                throw new DalException(ex.Message, ex);
        }

        while (await reader.ReadAsync(ct))
        {
            yield return new Region
            {
                Id = reader.GetInt64(0),
                Name = reader.GetString(1),
                Polygon = ((NpgsqlPolygon)reader.GetValue(2)).ToModel()
            };
        }
        await reader.CloseAsync();
    }

    public async Task<long> AddAsync(Region model, ITransientError error, CancellationToken ct = default)
    {
        const string sql = "INSERT INTO regions(pretty_name, polygon) VALUES (@name, @polygon) RETURNING id";

        try
        {
            if (_connection is null)
                await this.OpenConnectionAsync(ct);
            
            await using var command = new NpgsqlCommand(sql, _connection);
            command.Parameters.AddWithValue("name", NpgsqlDbType.Text, model.Name);
            command.Parameters.AddWithValue("polygon", NpgsqlDbType.Polygon, model.Polygon.ToNpgsql());

            error.Errno = 0;
            return (long) await command.ExecuteScalarAsync(ct);
        }
        catch (NpgsqlException ex)
        {
            if (ex.IsTransient)
            {
                error.Errno = 1;
                return default;
            }
            else
                throw new DalException(ex.Message, ex);
        }
    }

    public async Task UpdateAsync(Region model, ITransientError error, CancellationToken ct = default)
    {
        const string sql = "UPDATE regions SET pretty_name = @name, polygon = @polygon WHERE id = @id";

        try
        {
            if (_connection is null)
                await this.OpenConnectionAsync(ct);
            
            await using var command = new NpgsqlCommand(sql, _connection);
            command.Parameters.AddWithValue("id", NpgsqlDbType.Bigint, model.Id);
            command.Parameters.AddWithValue("name", NpgsqlDbType.Text, model.Name);
            command.Parameters.AddWithValue("polygon", NpgsqlDbType.Polygon, model.Polygon.ToNpgsql());

            await command.ExecuteNonQueryAsync(ct);
        }
        catch (NpgsqlException ex)
        {
            if (ex.IsTransient)
                error.Errno = 1;
            else
                throw new DalException(ex.Message, ex);
        }
    }

    public async Task<Region?> RemoveAsync(long id, ITransientError error, CancellationToken ct = default)
    {
        const string sql = "DELETE FROM regions WHERE id = @id RETURNING *";

        try
        {
            if (_connection is null)
                await this.OpenConnectionAsync(ct);
            
            await using var command = new NpgsqlCommand(sql, _connection);
            command.Parameters.AddWithValue("id", NpgsqlDbType.Bigint, id);
            
            await using var reader = await command.ExecuteReaderAsync(ct);
            if (!reader.HasRows || !await reader.ReadAsync(ct))
                return null;

            error.Errno = 0;
            return new Region
            {
                Id = reader.GetInt64(0),
                Name = reader.GetString(1),
                Polygon = ((NpgsqlPolygon)reader.GetValue(2)).ToModel()
            };
        }
        catch (NpgsqlException ex)
        {
            if (ex.IsTransient)
            {
                error.Errno = 1;
                return default;
            }
            else
                throw new DalException(ex.Message, ex);
        }
    }

    public async Task OpenConnectionAsync(CancellationToken ct = default)
    {
        _connection = new NpgsqlConnection(_connectionString);
        await _connection.OpenAsync(ct);
    }
    
    public async Task CloseConnectionAsync()
    {
        if (_connection is not null)
        {
            await _connection.CloseAsync();
            _connection = null;
        }
    }

    public async ValueTask DisposeAsync()
    {
        await CloseConnectionAsync();
    }
}