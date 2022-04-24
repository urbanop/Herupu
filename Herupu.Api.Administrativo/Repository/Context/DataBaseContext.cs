using Herupu.Api.Administrativo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Oracle.EntityFrameworkCore;


namespace Herupu.Api.Administrativo.Repository.Context
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Atividade> Atividade { get; set; }
        public DbSet<AtividadeItem> AtividadeItem { get; set; }

        public DataBaseContext()
        {
           // Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("FiapDbConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Atividade>().ToTable("T_ATIVIDADE");
            modelBuilder.Entity<AtividadeItem>().ToTable("CD_ATIVIDADE_ITEM");


            modelBuilder.Entity<AtividadeItem>()
                       .HasOne<Atividade>()
                       .WithMany()
                       .HasForeignKey(p => p.IdAtividade);
        }
    }
}
