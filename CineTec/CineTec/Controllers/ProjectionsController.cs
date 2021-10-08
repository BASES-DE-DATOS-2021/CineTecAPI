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
    public class ProjectionsController : ControllerBase
    {
        private readonly CRUDContext _CRUDContext;

        public ProjectionsController(CRUDContext CRUDContext)
        {
            _CRUDContext = CRUDContext;
        }

        // GET: api/Projections
        [HttpGet]
        public IEnumerable<Projection> Get()
        {
            return _CRUDContext.Projections;
        }

        // GET api/Projections/5
        [HttpGet("{id}")]
        public Projection Get(int id)
        {
            return _CRUDContext.GetProjection(id);
        }


        // GET api/Projections/byMovieId/
        [HttpGet("acts/byMovieId/{movie_id}")]
        public IEnumerable<Projection> Get_byMovieId(int movie_id)
        {
            return _CRUDContext.GetProjections_byMovieId(movie_id);
        }

        // GET api/Projections/byRoomId?room_id=a
        [HttpGet("acts/byRoomId/{room_id}")]
        public IEnumerable<Projection> Get_byRoomId(int room_id)
        {
            return _CRUDContext.GetProjections_byRoomId(room_id);
        }


        // POST api/Projections
        [HttpPost]
        public void Post([FromBody] Projection Projection)
        {
            _CRUDContext.Projections.Add(Projection);
            _CRUDContext.SaveChanges();
        }

        // PUT api/Projections/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Projection projection)
        {
            projection.id = id;
            _CRUDContext.Projections.Update(projection);
            _CRUDContext.SaveChanges();
        }

        // DELETE api/Projections/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var item = _CRUDContext.Projections.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                _CRUDContext.Projections.Remove(item);
                _CRUDContext.SaveChanges();
            }
        }
    }
}
