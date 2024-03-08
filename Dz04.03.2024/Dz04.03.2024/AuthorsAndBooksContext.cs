using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dz04._03._2024;

public partial class AuthorsAndBooksContext : DbContext {
    public AuthorsAndBooksContext() {}
    public AuthorsAndBooksContext(DbContextOptions<AuthorsAndBooksContext> options) : base(options){}
    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<Book> Books { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-D5SHCUS\\MSSQLSERVER2022;Database=AuthorsAndBooks;Integrated Security=SSPI;TrustServerCertificate=true");
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Author>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__Authors__3213E83FC213CB1F");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FullName).HasColumnName("fullname");
        });
        modelBuilder.Entity<Book>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__Books__3213E83FCF18AF7D");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__author_id__398D8EEE");
        });
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
