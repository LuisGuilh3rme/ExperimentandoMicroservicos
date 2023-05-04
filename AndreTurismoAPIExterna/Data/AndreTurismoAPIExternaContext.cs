using AndreTurismoAPIExterna.Models;
using Microsoft.EntityFrameworkCore;

namespace AndreTurismoAPIExterna.Data
{
    public class AndreTurismoAPIExternaContext : DbContext
    {
        public AndreTurismoAPIExternaContext(DbContextOptions<AndreTurismoAPIExternaContext> options)
    : base(options)
        {
        }

        public DbSet<Endereco> Endereco { get; set; } = default!;
    }
}
