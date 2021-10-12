using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CineTec.Context;
using CineTec.Models;
using CineTec.JSON_Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CineTec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly CRUDContext _CRUDContext;

        public MoviesController(CRUDContext CRUDContext)
        {
            _CRUDContext = CRUDContext;
        }

        // GET: api/Movies
        [HttpGet]
        public IEnumerable<Movie> Get() => _CRUDContext.Movies;

        // GET api/Movies/5
        [HttpGet("{id}")]
        public Movie Get(int id) => _CRUDContext.GetMovie_by_id(id);

        // GET api/Movies/
        [HttpGet("special_all")]
        public object GetAll_Select() => _CRUDContext.GetMovie_select_All();


        // GET api/Movies/5
        [HttpGet("special/{name}")]
        public object Get_Select(string name) => _CRUDContext.GetMovie_select(name);



        // GET api/Movies/byName/Shrek
        [HttpGet("byName/{name}")]
        public Movie GetByName(string name) => _CRUDContext.GetMovie_by_name(name);


        // POST api/Movies
        [HttpPost]
        public IActionResult Post([FromBody] MovieCreation movie_stats)
        {
            string x = _CRUDContext.Post_movie(movie_stats);
            if (x == "") return Ok();
            return BadRequest(x);
        }

        // PUT api/Movies/5
        [HttpPut("byName/{name}")]
        public IActionResult PutByName(string name, [FromBody] MovieCreation movie_stats)
        {
            string x = _CRUDContext.Put_movie_by_name(name, movie_stats);
            if (x == "") return Ok();
            return BadRequest(x);
        }

        // PUT api/Movies/5
        [HttpPut("byId/{id}")]
        public IActionResult Put(int id, [FromBody] MovieCreation movie_stats)
        {

            string x = _CRUDContext.Put_movie(id, movie_stats);
            if (x == "") return Ok();
            return BadRequest(x);
        }


        // DELETE api/Movies/byName/Shuek
        [HttpDelete("byName/{name}")]
        public IActionResult DeleteByName(string name)
        {
            string x = _CRUDContext.Delete_movie(name);
            if (x == "") return Ok();
            return BadRequest(x);
        }

        // DELETE api/Movies/1
        [HttpDelete("byId/{id}")]
        public IActionResult DeleteById(int id)
        {
            string x = _CRUDContext.Delete_movie_by_id(id);
            if (x == "") return Ok();
            return BadRequest(x);
        }
    }
}
