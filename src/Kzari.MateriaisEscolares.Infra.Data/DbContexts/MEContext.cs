using Kzari.MaterialEscolar.Domain.Entities;
using Kzari.MaterialEscolar.Domain.Interfaces.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Kzari.MateriaisEscolares.Infra.Data.DbContexts
{
    public class MEContext : DbContext, IApplicationDbContext
    {
        public MEContext(DbContextOptions options) : base(options)
        {

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
                    .WithMany(p => p.Items)
                    .HasForeignKey(p => p.IdKit);
                buildAction
                    .HasOne<Produto>(p => p.Produto)
                    .WithMany(p => p.Items)
                    .HasForeignKey(p => p.IdProduto);
            });
        }

        public DbSet<Kit> Kits { get; set; }
        public DbSet<Produto> Item { get; set; }
        public DbSet<Item> ItemKit { get; set; }
    }
}
