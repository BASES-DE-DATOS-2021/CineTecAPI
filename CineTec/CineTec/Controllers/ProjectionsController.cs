using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CineTec.Context;
using CineTec.Models;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CineTec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectionsController : ControllerBase
    {
        private readonly CRUDContext _CRUDContext;

        public ProjectionsController(CRUDContext CRUDContext)
        {
            _CRUDContext = CRUDContext;
        }


        // GET api/Projections/5
        [HttpGet()]
        public IEnumerable<Projection> Get() => _CRUDContext.Projections;


        //// GET api/Projections/byIds/?movie_id=a&room_id=b
        //[HttpGet("/{movie_id}")]
        //public IEnumerable<Projection> Get(int movie_id, int room_id) => _CRUDContext.GetProjection(movie_id, room_id);

        // GET api/Projections/byMovieId/
        [HttpGet("byMovieId/{movie_id}")]
        public IEnumerable<Projection> Get_byMovieId(int movie_id) => _CRUDContext.GetProjections_byMovieId(movie_id);

        // GET api/Projections/byRoomId?room_id=a
        [HttpGet("byRoomId/{room_id}")]
        public IEnumerable<Projection> Get_byRoomId(int room_id) => _CRUDContext.GetProjections_byRoomId(room_id);
        

        // POST api/Projections
        [HttpPost]
        public IActionResult Post([FromBody] Projection projection)
        {
            string x = _CRUDContext.Post_projection(projection);
            if (x == "") return Ok();
            return BadRequest(x);
        }

        // PUT api/Projections/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Projection projection)
        {
            projection.id = id;
            string x = _CRUDContext.Put_projection(projection);
            if (x == "") return Ok();
            return BadRequest(x);
        }

        // DELETE api/Projections/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string x = _CRUDContext.Delete_projection(id);
            if (x == "") return Ok();
            return BadRequest(x);
        }
    }
}
