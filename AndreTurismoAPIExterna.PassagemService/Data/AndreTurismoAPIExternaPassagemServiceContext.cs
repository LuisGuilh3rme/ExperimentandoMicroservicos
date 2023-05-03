using AndreTurismoAPIExterna.Models;
using AndreTurismoAPIExterna.PassagemService.Mapping;
using Microsoft.EntityFrameworkCore;

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
            new PassagemMap().Configure(modelBuilder.Entity<Passagem>());
            base.OnModelCreating(modelBuilder);
        }
    }
}
