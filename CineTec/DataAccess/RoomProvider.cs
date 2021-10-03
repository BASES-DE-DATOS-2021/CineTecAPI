using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTec.Models;

namespace CineTec.DataAccess
{
    public class RoomProvider : IRoomProvider
    {
        private readonly PostgreSqlContext _context;

        public RoomProvider(PostgreSqlContext context)
        {
            _context = context;
        }


        public List<Room> GetAllRooms()
        {

            return _context.room.ToList();
   
        }

        public Room GetRoom(int id)
        {
            return _context.room.First(c => c.id == id);
        }

        public void DeleteRoom(int id)
        {
            var room = _context.room.First(c => c.id == id);
            _context.room.Remove(room);
            _context.SaveChanges();
        }

        public void UpdateRoom(Room room)
        {
            _context.room.Update(room);
            _context.SaveChanges();
        }

        public void InsertRoom(Room room)
        {
            _context.room.Add(room);
            _context.SaveChanges();
        }

        public List<Room> GetRoomByBranchName(string branch_name)
        {

            var rooms = _context.room.ToList();
            return rooms.Where(room => room.branch_name == branch_name).ToList();


        }

      



    }
        
}
