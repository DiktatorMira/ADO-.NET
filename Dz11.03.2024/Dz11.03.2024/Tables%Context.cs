using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz11._03._2024 {
    public partial class Company {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Email { get; set; } = null!;
        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
    public partial class Manager {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
    public partial class Product {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
    public partial class Sale {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ManagerId { get; set; }
        public int CompanyId { get; set; }
        public int SalesNumber { get; set; }
        public decimal Price { get; set; }
        public DateOnly SaleDate { get; set; }
        public virtual Company Company { get; set; } = null!;
        public virtual Manager Manager { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
    public partial class SalesView {
        public int SaleId { get; set; }
        public string ProductTitle { get; set; } = null!;
        public string ProductType { get; set; } = null!;
        public int SalesNumber { get; set; }
        public decimal Price { get; set; }
        public DateOnly SaleDate { get; set; }
        public string ManagerName { get; set; } = null!;
        public string ManagerSurname { get; set; } = null!;
        public string CompanyTitle { get; set; } = null!;
        public string CompanyEmail { get; set; } = null!;
    }
    public partial class Context : DbContext {
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<SalesView> SalesViews { get; set; }
        public Context() { }
        public Context(DbContextOptions<Context> options)
            : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=DESKTOP-D5SHCUS\\MSSQLSERVER2022;Database=StationeryCompany;Integrated Security=SSPI;TrustServerCertificate=true");
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Company>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__Companie__3213E83F66E7996D");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Email).HasMaxLength(50).HasColumnName("email");
                entity.Property(e => e.Title).HasMaxLength(100).HasColumnName("title");
            });
            modelBuilder.Entity<Manager>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__Managers__3213E83FF00CFAF3");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Email).HasMaxLength(50).HasColumnName("email");
                entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name");
                entity.Property(e => e.Surname).HasMaxLength(50).HasColumnName("surname");
            });
            modelBuilder.Entity<Product>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__Products__3213E83F23E855B8");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Amount).HasColumnName("amount");
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)").HasColumnName("price");
                entity.Property(e => e.Title).HasMaxLength(50).HasColumnName("title");
                entity.Property(e => e.Type).HasMaxLength(50).HasColumnName("type");
            });
            modelBuilder.Entity<Sale>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__Sales__3213E83F024947BB");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CompanyId).HasColumnName("company_id");
                entity.Property(e => e.ManagerId).HasColumnName("manager_id");
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)").HasColumnName("price");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.SaleDate).HasColumnName("sale_date");
                entity.Property(e => e.SalesNumber).HasColumnName("sales_number");
                entity.HasOne(d => d.Company).WithMany(p => p.Sales).HasForeignKey(d => d.CompanyId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Sales__company_i__3F466844");
                entity.HasOne(d => d.Manager).WithMany(p => p.Sales).HasForeignKey(d => d.ManagerId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Sales__manager_i__3E52440B");
                entity.HasOne(d => d.Product).WithMany(p => p.Sales).HasForeignKey(d => d.ProductId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Sales__product_i__3D5E1FD2");
            });
            modelBuilder.Entity<SalesView>(entity => {
                entity.HasNoKey().ToView("SalesView");
                entity.Property(e => e.CompanyEmail).HasMaxLength(50).HasColumnName("company_email");
                entity.Property(e => e.CompanyTitle).HasMaxLength(100).HasColumnName("company_title");
                entity.Property(e => e.ManagerName).HasMaxLength(50).HasColumnName("manager_name");
                entity.Property(e => e.ManagerSurname).HasMaxLength(50).HasColumnName("manager_surname");
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)").HasColumnName("price");
                entity.Property(e => e.ProductTitle).HasMaxLength(50).HasColumnName("product_title");
                entity.Property(e => e.ProductType).HasMaxLength(50).HasColumnName("product_type");
                entity.Property(e => e.SaleDate).HasColumnName("sale_date");
                entity.Property(e => e.SaleId).HasColumnName("sale_id");
                entity.Property(e => e.SalesNumber).HasColumnName("sales_number");
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
