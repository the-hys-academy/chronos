using System.Collections;
using System.Collections.Generic;

public class Region {
    public long id { get; set; }
    public string name { get; set; }
    public IEnumerable <(double lat, double lon)> polygon { get; set; }
    
}