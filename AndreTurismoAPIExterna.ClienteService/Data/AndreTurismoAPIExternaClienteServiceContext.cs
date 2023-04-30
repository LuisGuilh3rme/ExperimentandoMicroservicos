using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoAPIExterna.Models;

namespace AndreTurismoAPIExterna.ClienteService.Data
{
    public class AndreTurismoAPIExternaClienteServiceContext : DbContext
    {
        public AndreTurismoAPIExternaClienteServiceContext (DbContextOptions<AndreTurismoAPIExternaClienteServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoAPIExterna.Models.Cliente> Cliente { get; set; } = default!;
    }
}
