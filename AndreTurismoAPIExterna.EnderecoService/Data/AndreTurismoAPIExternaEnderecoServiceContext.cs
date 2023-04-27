using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoAPIExterna.Models;

namespace AndreTurismoAPIExterna.EnderecoService.Data
{
    public class AndreTurismoAPIExternaEnderecoServiceContext : DbContext
    {
        public AndreTurismoAPIExternaEnderecoServiceContext (DbContextOptions<AndreTurismoAPIExternaEnderecoServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoAPIExterna.Models.Endereco> Endereco { get; set; } = default!;
    }
}
