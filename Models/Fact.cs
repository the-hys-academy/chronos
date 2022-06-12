using System;
using System.Text.Json;

namespace chronos.Models{
    public class Fact{
                
        public long ID { get; set; } 
        
        public string Name { get; set; }

        public string? Descr { get; set; }

        public string? DateFrom { get; set; }

        public string? DateTo { get; set; }

        //public json Props { get; set; }
        // or
        public string? JsonProps { get; set; }  
    }
}