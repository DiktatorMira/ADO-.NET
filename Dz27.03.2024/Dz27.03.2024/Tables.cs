using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz27._03._2024 {
    public class Product {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
    public class Manager {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
    }
    public class Company {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Email { get; set; }
    }
    public class Sale {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int ManagerId { get; set; }
        public Manager? Manager { get; set; }
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
        public int SalesNumber { get; set; }
        public decimal Price { get; set; }
        public DateTime SaleDate { get; set; }
    }
}