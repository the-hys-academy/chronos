using Npgsql;
using Npgsql.Util;
using NpgsqlTypes;
using System.Collections;
using System.Collections.Generic;

public class Region{
    public long id{get;set;}
    public string name{get;set;}
    public NpgsqlPolygon polygon;

    Region(string _name, NpgsqlPolygon _polygon)
    {
        name = _name;
        polygon = _polygon;

    }

}