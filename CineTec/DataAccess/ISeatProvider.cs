using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTec.Models;

namespace CineTec.DataAccess
{
    public interface ISeatProvider
    {

       
        List<Seat> GetAllSeatByRoom(int id);
      
        void UpdateStateSeat(int number, string status);





    }
}
