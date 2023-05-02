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
    }
}
