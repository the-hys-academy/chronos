using Npgsql;
using NpgsqlTypes;

namespace Draft
{
    public sealed class RegionRepository : IRegionRepository
    {
        private readonly string _connectionString;

        public RegionRepository(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public async Task<long> Create(Region region, Error error, CancellationToken cancellationToken = default)
        {
            try
            {
                await using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync(cancellationToken);

                await using (var command = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS Region (id bigserial primary key, name varchar(150), polygon polygon)", connection))
                {
                    await command.ExecuteNonQueryAsync(cancellationToken);
                }

                await using (var command = new NpgsqlCommand("INSERT INTO Region (name, polygon) VALUES (@name, @polygon) RETURNING ID", connection))
                {

                    command.Parameters.AddWithValue("name", region.Name);
                    command.Parameters.AddWithValue("polygon", RegionPolygonToNpgsqlPolygon(region.Polygon));

                    return (long)await command.ExecuteScalarAsync(cancellationToken);
                }
            }
            catch (NpgsqlException ex)
            {
                error.ErrorNumber = ex.IsTransient ? 1 : -1;
                error.IsTransient = ex.IsTransient;

                if (error.ErrorNumber == -1)
                    throw ex;

                return 0;
            }
        }

        public async Task<Region> GetRegion(long id, Error error, CancellationToken cancellationToken = default)
        {
            try
            {
                await using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync(cancellationToken);

                await using (var command = new NpgsqlCommand("SELECT * FROM Region WHERE id = @searchId;", connection))
                {
                    Region region = new Region();

                    command.Parameters.AddWithValue("searchId", id);
                    using (var dataReader = command.ExecuteReader())
                    {
                        if (dataReader.HasRows && dataReader.Read())
                        {
                            region.Id = Convert.ToInt32(dataReader["id"]);
                            region.Name = dataReader["name"].ToString();
                            region.Polygon = StringPolygonToRegionPolygon(dataReader["polygon"].ToString());

                        }
                    }
                    return region;
                }
            }
            catch (NpgsqlException ex)
            {
                error.ErrorNumber = ex.IsTransient ? 1 : -1;
                error.IsTransient = ex.IsTransient;

                if (error.ErrorNumber == -1)
                    throw new DALException();

                return null;
            }
        }

        public async Task Update(Region region, Error error, CancellationToken cancellationToken = default)
        {
            try
            {
                await using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync(cancellationToken);
                string stringCommand = default;

                if (region.Name != null)
                {
                    stringCommand += "UPDATE Region SET name = @name WHERE id = @id;";
                }
                if (region.Polygon != null)
                {
                    stringCommand += "UPDATE Region SET polygon = @polygon WHERE id = @id;";
                }

                await using (var command = new NpgsqlCommand(stringCommand, connection))
                {
                    command.Parameters.AddWithValue("id", region.Id);
                    if (region.Name != null)
                    {
                        command.Parameters.AddWithValue("name", region.Name);
                    }
                    if (region.Polygon != null)
                    {
                        command.Parameters.AddWithValue("polygon", RegionPolygonToNpgsqlPolygon(region.Polygon));
                    }

                    await command.ExecuteNonQueryAsync(cancellationToken);
                }
            }
            catch (NpgsqlException ex)
            {
                error.ErrorNumber = ex.IsTransient ? 1 : -1;
                error.IsTransient = ex.IsTransient;

                if (error.ErrorNumber == -1)
                    throw new DALException();
            }
        }

        public async Task<Region> Delete(long id, Error error, CancellationToken cancellationToken = default)
        {
            try
            {
                await using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync(cancellationToken);

                await using (var command = new NpgsqlCommand("DELETE FROM Region WHERE id = @searchId RETURNING id, name, polygon;", connection))
                {
                    Region region = new Region();

                    command.Parameters.AddWithValue("searchId", id);
                    using (var dataReader = command.ExecuteReader())
                    {
                        if (dataReader.HasRows && dataReader.Read())
                        {
                            region.Id = Convert.ToInt32(dataReader["id"]);
                            region.Name = dataReader["name"].ToString();
                            region.Polygon = StringPolygonToRegionPolygon(dataReader["polygon"].ToString());

                        }
                    }
                    return region;
                }
            }
            catch (NpgsqlException ex)
            {
                error.ErrorNumber = ex.IsTransient ? 1 : -1;
                error.IsTransient = ex.IsTransient;

                if (error.ErrorNumber == -1)
                    throw new DALException();

                return null;
            }
        }

        public async IAsyncEnumerable<Region> GetRegions(Error error, CancellationToken cancellationToken = default)
        {
            NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            try
            {
                await connection.OpenAsync(cancellationToken);
            }
            catch (NpgsqlException ex)
            {
                error.ErrorNumber = ex.IsTransient ? 1 : -1;
                error.IsTransient = ex.IsTransient;

                if (error.ErrorNumber == -1)
                    throw new DALException();
            }

            await using (var command = new NpgsqlCommand("SELECT * FROM Region;", connection))
            {
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.HasRows && dataReader.Read())
                    {
                        Region region = new Region();
                        region.Id = Convert.ToInt32(dataReader["id"]);
                        region.Name = dataReader["name"].ToString();
                        region.Polygon = StringPolygonToRegionPolygon(dataReader["polygon"].ToString());

                        yield return region;
                    }
                }
            }

        }

        private NpgsqlPolygon RegionPolygonToNpgsqlPolygon(IEnumerable<(double longitude, double latitude)> polygon)
        {
            var npgsqlPoints = new List<NpgsqlPoint>();
            foreach (var polygonItem in polygon)
            {
                npgsqlPoints.Add(new NpgsqlPoint(polygonItem.longitude, polygonItem.latitude));
            }

            return new NpgsqlPolygon(npgsqlPoints);
        }

        private IEnumerable<(double longitude, double latitude)> StringPolygonToRegionPolygon(string polygon)
        {
            List<double> pointsArray = polygon.Replace("(", String.Empty).Replace(")", String.Empty).Split('\u002C').Select(double.Parse).ToList();

            var regionPolygon = new List<(double longitude, double latitude)>();
            for (int i = 0; i < pointsArray.Count; i = i + 2)
            {
                regionPolygon.Add((pointsArray[i], pointsArray[i + 1]));
            }

            return regionPolygon;
        }
    }
}
