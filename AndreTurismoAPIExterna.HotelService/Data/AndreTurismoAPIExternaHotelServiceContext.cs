using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoAPIExterna.Models;
using AndreTurismoAPIExterna.HotelService.Mapping;

namespace AndreTurismoAPIExterna.HotelService.Data
{
    public class AndreTurismoAPIExternaHotelServiceContext : DbContext
    {
        public AndreTurismoAPIExternaHotelServiceContext (DbContextOptions<AndreTurismoAPIExternaHotelServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoAPIExterna.Models.Hotel> Hotel { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new HotelMap().Configure(modelBuilder.Entity<Hotel>());
            base.OnModelCreating(modelBuilder);
        }
    }
}
