using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoAPIExterna.Models;

namespace AndreTurismoAPIExterna.HotelService.Data
{
    public class AndreTurismoAPIExternaHotelServiceContext : DbContext
    {
        public AndreTurismoAPIExternaHotelServiceContext (DbContextOptions<AndreTurismoAPIExternaHotelServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoAPIExterna.Models.Hotel> Hotel { get; set; } = default!;
    }
}
