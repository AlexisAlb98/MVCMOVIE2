using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MvcMovie.Data;

public partial class MoviesContext : IdentityDbContext<IdentityUser>
{
    public MoviesContext()
    {
    }

    public MoviesContext(DbContextOptions<MoviesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Movie> Movies { get; set; }
    public DbSet<Movie> Movie { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Movies__3214EC07659BF9C6");

            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            entity.Property(e => e.ReleaseDate).HasColumnType("date");
        });
        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Movie)
                  .WithMany()
                  .HasForeignKey(e => e.MovieId);
        });
        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.Property(p => p.Total)
                  .HasColumnType("decimal(18, 2)");
        });


        // Configuración para las entidades de Identity
        modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(x => new { x.LoginProvider, x.ProviderKey });
        modelBuilder.Entity<IdentityUserRole<string>>().HasKey(x => new { x.UserId, x.RoleId });
        modelBuilder.Entity<IdentityUserToken<string>>().HasKey(x => new { x.UserId, x.LoginProvider, x.Name });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<MvcMovie.Models.Pedido>? Pedido { get; set; }
}
