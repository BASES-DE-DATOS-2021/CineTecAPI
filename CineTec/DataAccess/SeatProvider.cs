using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTec.Models;

namespace CineTec.DataAccess
{
    public class SeatProvider : ISeatProvider
    {
        private readonly PostgreSqlContext _context;

        public SeatProvider(PostgreSqlContext context)
        {
            _context = context;
        }

        public List<Seat> GetAllSeatByRoom(int id)
        {


            return _context.seat.ToList().Where(seat => seat.Room_id == id).ToList();

        }

        public void UpdateStateSeat(int number, string status)
        {

        

        }



    }




}
