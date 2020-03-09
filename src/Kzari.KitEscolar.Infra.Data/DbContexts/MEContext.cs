using Kzari.KitEscolar.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kzari.KitEscolar.Infra.Data.DbContexts
{
    public class MEContext : DbContext
    {
        //Mock
        public MEContext()
        {

        }

        public MEContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder opt)
        {
            opt.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kit>(buildAction =>
            {
                buildAction.ToTable(nameof(Kit));
                buildAction.HasKey(p => p.Id);
                buildAction.Property(p => p.Id).ValueGeneratedOnAdd();
                buildAction.Property(p => p.Nome).HasMaxLength(50);

            });

            modelBuilder.Entity<Produto>(buildAction =>
            {
                buildAction.ToTable(nameof(Produto));
                buildAction.HasKey(p => p.Id);
                buildAction.Property(p => p.Id).ValueGeneratedOnAdd();
                buildAction.Property(p => p.Nome).HasMaxLength(50);
            });

            modelBuilder.Entity<Item>(buildAction =>
            {
                buildAction.ToTable(nameof(Item));
                buildAction
                    .HasKey(p => new { p.IdKit, p.IdProduto });
                buildAction
                    .HasOne<Kit>(p => p.Kit)
                    .WithMany(p => p.Itens)
                    .HasForeignKey(p => p.IdKit);
                buildAction
                    .HasOne<Produto>(p => p.Produto)
                    .WithMany(p => p.Items)
                    .HasForeignKey(p => p.IdProduto);
            });
        }

        public DbSet<Kit> Kits { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Item> Itens { get; set; }
    }
}
