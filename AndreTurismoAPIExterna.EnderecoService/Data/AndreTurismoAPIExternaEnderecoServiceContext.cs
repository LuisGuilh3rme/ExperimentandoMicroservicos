using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoAPIExterna.Models;
using AndreTurismoAPIExterna.EnderecoService.Mapping;

namespace AndreTurismoAPIExterna.EnderecoService.Data
{
    public class AndreTurismoAPIExternaEnderecoServiceContext : DbContext
    {
        public AndreTurismoAPIExternaEnderecoServiceContext(DbContextOptions<AndreTurismoAPIExternaEnderecoServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoAPIExterna.Models.Endereco> Endereco { get; set; } = default!;
        public DbSet<AndreTurismoAPIExterna.Models.Cidade> Cidade { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new EnderecoMap().Configure(modelBuilder.Entity<Endereco>());
            new CidadeMap().Configure(modelBuilder.Entity<Cidade>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
