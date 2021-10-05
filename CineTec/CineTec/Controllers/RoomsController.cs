﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CineTec.Context;
using CineTec.Models;

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

        // POST api/Rooms
        [HttpPost]
        public void Post([FromBody] Room room)
        {
            _CRUDContext.Rooms.Add(room);
            _CRUDContext.SaveChanges();

            // Llena una sala con la cantidad de sillas que debe tener. Todas estan vacias.
            SeatsController seatControl = new SeatsController(_CRUDContext);
            for (int i = 1; i < room.capacity; i++)
                seatControl.Post(new Seat(room.id, i, "EMPTY"));
        }

        // PUT api/Rooms/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Room room)
        {
            room.id = id;
            _CRUDContext.Rooms.Update(room);
            _CRUDContext.SaveChanges();
        }

        // DELETE api/Rooms/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var item = _CRUDContext.Rooms.FirstOrDefault(x => x.id == id);
            if (item != null)
            {

                // Elimina las sillas de una sala.
                SeatsController seatControl = new SeatsController(_CRUDContext);
                for (int i = 0; i < item.capacity; i++)
                    seatControl.Delete(item.id, i+1);

                _CRUDContext.Rooms.Remove(item);
                _CRUDContext.SaveChanges();
            
            }
        }
    }
}
