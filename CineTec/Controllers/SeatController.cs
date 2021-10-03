using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTec.Models;
using CineTec.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CineTec.Controllers
{
    [Route("[controller]")]
    public class SeatController : ControllerBase
    {
        private readonly ISeatProvider _dataAccessProvider;

        public SeatController(ISeatProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }





        [HttpGet("{id}")]
        public IEnumerable<Seat> GetSeats(int room_id)
        {
            return _dataAccessProvider.GetAllSeatByRoom(room_id);
        }


    }

        
}
