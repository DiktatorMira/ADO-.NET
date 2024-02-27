using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;

namespace Dz26._02._2024 {
    public class Countries {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Capital { get; set; }
        public long Population { get; set; }
        public double Area { get; set; }
        public virtual Continent? Continent { get; set; }
    }
    public class Continent {
        public int Id { get; set; }
        public string? Title { get; set; }
        public virtual ICollection<Countries>? Contries { get; set; }
    }
}