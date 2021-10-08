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
        [HttpGet("byMovieId/{movie_id}")]
        public IEnumerable<Projection> Get_byMovieId(int movie_id)
        {
            return _CRUDContext.GetProjections_byMovieId(movie_id);
        }

        // GET api/Projections/byRoomId?room_id=a
        [HttpGet("byRoomId/{room_id}")]
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
        public IActionResult Delete(int id)
        {
            string resp = "";
            int x = _CRUDContext.Delete_projection(id);
            switch (x)
            {
                case 0:
                    resp = "No se puede eliminar una proyeccion que se encuentra asignada a una pelicula.";
                    break;

                case -1:
                    resp = "No se ha encontrado esta proyeccion.";
                    break;

                default: // Se elimina correctamente.
                    return Ok();
            }
            return BadRequest(resp);
        }
    }
}
