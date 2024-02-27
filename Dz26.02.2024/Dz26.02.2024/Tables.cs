using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dz26._02._2024 {
    public class Continent {
        public int Id { get; set; }
        public string? Title { get; set; }
    }
    public class Countries {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Capital { get; set; }
        public long Population { get; set; }
        public double Area { get; set; }
        public Continent? Continent { get; set; }
        public int ContinentId { get; set; }
    }
}