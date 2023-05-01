using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoAPIExterna.Models;

namespace AndreTurismoAPIExterna.PacoteService.Data
{
    public class AndreTurismoAPIExternaPacoteServiceContext : DbContext
    {
        public AndreTurismoAPIExternaPacoteServiceContext(DbContextOptions<AndreTurismoAPIExternaPacoteServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoAPIExterna.Models.Pacote> Pacote { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pacote>().HasOne(p => p.Hotel).WithOne().HasForeignKey<Pacote>("HotelId").OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Pacote>().HasOne(p => p.Passagem).WithOne().HasForeignKey<Pacote>("PassagemId").OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Pacote>().HasOne(p => p.Cliente).WithOne().HasForeignKey<Pacote>("ClienteId").OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Passagem>().HasOne(p => p.Origem).WithOne().HasForeignKey<Passagem>("OrigemId").OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Passagem>().HasOne(p => p.Destino).WithOne().HasForeignKey<Passagem>("DestinoId").OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Passagem>().HasOne(p => p.Cliente).WithOne().HasForeignKey<Passagem>("ClienteId").OnDelete(DeleteBehavior.NoAction);

        }
    }
}
