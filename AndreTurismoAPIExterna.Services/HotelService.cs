using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoAPIExterna.Models;
using AndreTurismoAPIExterna.Repositories;

namespace AndreTurismoAPIExterna.Services
{
    public class HotelService
    {
        public int InserirHotel (Hotel hotel) => HotelRepository.InserirHotel(hotel);
        public Hotel RetornarHotel(int id) => HotelRepository.RetornarHotel(id);
    }
}
