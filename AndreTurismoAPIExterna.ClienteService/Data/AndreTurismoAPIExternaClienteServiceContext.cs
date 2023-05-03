using AndreTurismoAPIExterna.ClienteService.Mapping;
using AndreTurismoAPIExterna.Models;
using Microsoft.EntityFrameworkCore;

namespace AndreTurismoAPIExterna.ClienteService.Data
{
    public class AndreTurismoAPIExternaClienteServiceContext : DbContext
    {
        public AndreTurismoAPIExternaClienteServiceContext (DbContextOptions<AndreTurismoAPIExternaClienteServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoAPIExterna.Models.Cliente> Cliente { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ClienteMap().Configure(modelBuilder.Entity<Cliente>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
