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
    public class SeatsController : ControllerBase
    {
        private readonly CRUDContext _CRUDContext;

        public SeatsController(CRUDContext CRUDContext)
        {
            _CRUDContext = CRUDContext;
        }

        // GET: api/Seats
        [HttpGet]
        public IEnumerable<Seat> Get()
        {
            return _CRUDContext.Seats;
        }

        // GET api/Seats/byId?room_id=a&number=b
        [HttpGet("byId")]
        public Seat Get(int projection_id, int number)
        {
            return _CRUDContext.GetSeat(projection_id, number);
        }

        // PUT api/Seats/byId?_id=a&number=b
        [HttpPut("byId")]
        public IActionResult Put(int projection_id, int number, [FromBody] Seat Seat)
        {

            var s = _CRUDContext.GetSeat(projection_id, number);
            if (s == null)
                return BadRequest("No se encuentra ninguna silla que coincida.");

            s.status = Seat.status;
            _CRUDContext.Seats.Update(s);
            _CRUDContext.SaveChanges();
            return Ok();
        }



        // DELETE api/Seats/seat_key?room_id=a&number=b
        [HttpDelete("id")]
        public IActionResult Delete(int projection_id, int number)
        {
            int  x = _CRUDContext.Delete_seat(projection_id, number);
            if (x==-1) return BadRequest("No se encuentra ninguna silla que coincida.");
            return Ok();
        }
    }
    
}
