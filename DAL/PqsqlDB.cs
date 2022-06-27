using Npgsql;
using Npgsql.Util;
using NpgsqlTypes;
using System.Collections;
using System.Collections.Generic;

public sealed class PqsqlDB : IRegionRepository{

    private readonly string _connStr;

    public PqsqlDB(string? connStr) => _connStr = connStr ?? "Host=localhost;Username=hys;Password=123;Database=mydatabase";
    public async Task<int?> Create(Region reg, Error err, CancellationToken ct = default){
    try
    {
    err.ErrorNo = 12;
    await using var conn = new NpgsqlConnection(_connStr);
    await conn.OpenAsync(ct);

// Create table and Insert some data
    await using (var cmd = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS Region (id serial primary key, name varchar(150), region polygon )", conn))
    {
    await cmd.ExecuteNonQueryAsync(ct);
    }
    await using (var cmd = new NpgsqlCommand("INSERT INTO Region(name, region) VALUES (@name, @region) RETURNING Id", conn)){
    cmd.Parameters.AddWithValue("name", reg.name);
    
    cmd.Parameters.AddWithValue("region",reg.polygon);
    err.ErrorNo = 0;

                    int? v = (int?) await cmd.ExecuteScalarAsync(ct);
     
                    return v;
    }
    }
    catch (NpgsqlException exn)
    {
    err.ErrorNo=exn.IsTransient ? 1 :-1;

    if (err.ErrorNo==-1)//error codn't correct via retry
    {
        throw new DALException();
    }
    return 0;
    }
   } 
   }