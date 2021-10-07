using Microsoft.AspNetCore.Mvc;
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

            //BranchesController branchControl = new BranchesController(_CRUDContext);
            //if (branchControl.EvalRooms(room.branch_name);
            _CRUDContext.Branches.
            _CRUDContext.Rooms.Add(room);
            _CRUDContext.SaveChanges();

            // Llena una sala con la cantidad de sillas que debe tener. Todas estan vacias.
            SeatsController seatControl = new SeatsController(_CRUDContext);
            for (int i = 1; i < room.capacity+1; i++)
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
            var room = _CRUDContext.Rooms.FirstOrDefault(x => x.id == id);
            if (room != null)
            {
                //// Elimina las sillas de una sala.
                SeatsController seatControl = new SeatsController(_CRUDContext);
                for (int i = 1; i < room.capacity + 1; i++)
                    seatControl.Delete(room.id, i);

                _CRUDContext.Rooms.Remove(room);
                _CRUDContext.SaveChanges();
            }
        }


        //// DELETE ALL ROOMS FROM A BRANCH
        //public void Delete_from_branch(string cinema_name)
        //{
        //    var query = from r in _CRUDContext.Rooms
        //                where r.branch_name == cinema_name
        //                select r;

        //    if (query != null)
        //    {
        //        // Elimina cada sala donde el nombre de sucursal coincida.
        //        foreach (Room room in query)
        //        {
        //            Delete(room.id);
        //        }
        //        _CRUDContext.SaveChanges();
        //    }
        //}
    }
}
