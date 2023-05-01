using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoAPIExterna.Models;

namespace AndreTurismoAPIExterna.PassagemService.Data
{
    public class AndreTurismoAPIExternaPassagemServiceContext : DbContext
    {
        public AndreTurismoAPIExternaPassagemServiceContext (DbContextOptions<AndreTurismoAPIExternaPassagemServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoAPIExterna.Models.Passagem> Passagem { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passagem>().HasOne(p => p.Origem).WithOne().HasForeignKey<Passagem>("OrigemId").OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Passagem>().HasOne(p => p.Destino).WithOne().HasForeignKey<Passagem>("DestinoId").OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Passagem>().HasOne(p => p.Cliente).WithOne().HasForeignKey<Passagem>("ClienteId").OnDelete(DeleteBehavior.NoAction);
        }
    }
}
