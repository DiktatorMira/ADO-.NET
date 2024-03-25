using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask {
    public class Country {
        public int Id { get; set; }
        public string? Title { get; set; }
    }
    public class City {
        public int Id { get; set; }
        public string? Title { get; set; }
    }
    public class Chapter {
        public int Id { get; set; }
        public string? Title { get; set; }
    }
    public class Buyer {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string? FullName { get; set; }
        public DateTime Birth { get; set; }
        public string? Male { get; set; }
        public string? Email { get; set; }
    }
    public class Interest {
        [Column(Order = 0), ForeignKey("Buyer")]
        public int BuyerId { get; set; }
        [Column(Order = 1), ForeignKey("Chapter")]
        public int ChapterId { get; set; }

        public virtual Buyer? Buyer { get; set; }
        public virtual Chapter? Chapter { get; set; }
    }
    public class Promotional {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public int CountryId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class Product {
        public int Id { get; set; }
        public int PromotionalId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}