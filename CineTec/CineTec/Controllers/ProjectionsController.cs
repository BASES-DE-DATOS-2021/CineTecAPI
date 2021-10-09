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
        public IEnumerable<Projection> Get() => _CRUDContext.Projections;
        

        // GET api/Projections/5
        [HttpGet("{id}")]
        public Projection Get(int id) => _CRUDContext.GetProjection(id);


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
            int x = _CRUDContext.Post_projection(projection);
            if (x == 0)
                return BadRequest("Ya existe una proyeccion de la pelicula en esta sala a la misma hora.");
            return Ok();
        }

        // PUT api/Projections/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Projection projection)
        {
            projection.id = id;
            int x = _CRUDContext.Put_projection(projection);
            return x switch
            {
                0 => BadRequest(" Ya existe una proyeccion a esta hora de la misma pelicula en la misma sala."),
                -1 => BadRequest("No se ha encontrado una proyeccion existente."),
                _ => Ok(),
            };
        }

        // DELETE api/Projections/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int x = _CRUDContext.Delete_projection(id);
            return x switch
            {
                0 => BadRequest("No se puede eliminar una proyeccion que se encuentra asignada a una pelicula."),
                -1 => BadRequest("No se ha encontrado esta proyeccion."),
                _ => Ok(),// Se elimina correctamente.
            };
        }
    }
}
