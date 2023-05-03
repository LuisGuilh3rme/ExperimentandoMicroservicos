using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoAPIExterna.Models;
using AndreTurismoAPIExterna.PacoteService.Mapping;

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
            new PacoteMap().Configure(modelBuilder.Entity<Pacote>());
            base.OnModelCreating(modelBuilder);
        }
    }
}
