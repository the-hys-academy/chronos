using Npgsql;
using Npgsql.Util;
using NpgsqlTypes;
using System.Collections;
using System.Collections.Generic;

public class Region{
    
    public long id { get; set; }
    public string name { get;set; }
    public IEnumerable <(double lat, double lon)> polygon { get; set; }
     
    public NpgsqlPolygon MapingToNpgsqlPolygon() 
    {
        var npgsqlPoints= new List<NpgsqlPoint>();
        foreach (var polygonItem in this.polygon)
        {
            npgsqlPoints.Add(new NpgsqlPoint(polygonItem.lon, polygonItem.lat));
        }
        return new NpgsqlPolygon(npgsqlPoints);
    }

    public IEnumerable <(double lat, double lon)> MapingToRegionPolygon(NpgsqlPolygon npgsqlPoints)
    {  
        var points = new List<(double lat, double lon)>();
        foreach(var point in npgsqlPoints)
        {
             points.Add((point.X, point.Y));
        }
        points.ToString();

        return points;
    }

}