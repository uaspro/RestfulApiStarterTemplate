using Microsoft.EntityFrameworkCore;
using RestfulApiStarterTemplate.Models.Entities;

namespace RestfulApiStarterTemplate.DataStore.Context
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
            : base(options)
        {
        }

        public DbSet<Test> Tests { get; set; }

        public DbSet<TestChild> TestChildren { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Test>()
                .HasMany(e => e.TestChildren)
                .WithOne(e => e.Test)
                .HasForeignKey(e => e.TestId)
                .HasConstraintName("FK_tblTestChildren_tblTests");

            modelBuilder.Entity<TestChild>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<TestChild>()
                .HasOne(e => e.Test)
                .WithMany(e => e.TestChildren)
                .HasForeignKey(e => e.TestId)
                .HasConstraintName("FK_tblTestChildren_tblTests");
        }
    }
}
