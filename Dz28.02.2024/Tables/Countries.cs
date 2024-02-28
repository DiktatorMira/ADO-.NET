namespace Tables {
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
