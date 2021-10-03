using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTec.Models;

namespace CineTec.DataAccess
{
    public interface IRoomProvider
    {

       
        List<Room> GetAllRooms();
        Room GetRoom(int id);
        void DeleteRoom(int id);
        void UpdateRoom(Room room);
        void InsertRoom(Room room);
        public List<Room> GetRoomByBranchName(string branch_name);





    }
}
