using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CineTec.Context;
using CineTec.Models;
using Microsoft.EntityFrameworkCore;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CineTec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly CRUDContext _CRUDContext;

        public RoomsController(CRUDContext CRUDContext)
        {
            _CRUDContext = CRUDContext;
        }

        // GET: api/Rooms
        [HttpGet]
        public IEnumerable<Room> Get()
        {
            return _CRUDContext.Rooms;
        }

        // GET api/Rooms/5
        [HttpGet("{id}")]
        public Room Get(int id)
        {
            return _CRUDContext.Rooms.SingleOrDefault(x => x.id == id);
        }

        // GET api/Rooms/all_seats?cinema_name=a&room_id=b
        [HttpGet("all_seats")]
        public IList<Seat> Get_all_seats(string cinema_name, int room_id)
        {
            return _CRUDContext.Get_all_seats_of_a_room(cinema_name, room_id);
        }


        // POST api/Rooms
        [HttpPost]
        public void Post([FromBody] Room room)
        {
            try
            {
                _CRUDContext.Rooms.Add(room);
                _CRUDContext.SaveChanges();
                _CRUDContext.Add_seats_in_a_room(room.id, room.capacity);
                //_CRUDContext.Update_branch_rooms_quantity(room.branch_name);
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.GetType()); // what is the real exception?
            }
        }


        // PUT api/Rooms/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Room room)
        {
            _CRUDContext.Update_Room(id, room);
            // manejar excepcion

        }


        // DELETE api/Rooms/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _CRUDContext.Delete_room_and_seats(id);
        }



    } 
}
