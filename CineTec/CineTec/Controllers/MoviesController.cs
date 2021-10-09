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
        public Movie Get(int id) => _CRUDContext.GetMovie(id);

        // GET api/Movies/5
        [HttpGet("special/{name}")]
        public object Get_Select(string name) => _CRUDContext.GetMovie_select(name);


        // GET api/Movies/byName/Shrek
        [HttpGet("byName/{name}")]
        public Movie GetByName(string name) => _CRUDContext.GetMovie(name);

        //// POST api/Movies
        //[HttpPost]
        //public IActionResult Post([FromBody] Movie movie)
        //{
        //    int x = _CRUDContext.Post_movie(movie);
        //    return x switch
        //    {
        //        0 => BadRequest("Esta pelicula ya se encuentra registrada."),
        //        _ => Ok(),
        //    };
        //}

        //// PUT api/Movies/5
        //[HttpPut("{id}")]
        //public IActionResult Put(string name, [FromBody] Movie movie)
        //{
        //    movie.original_name = name;
        //    int x = _CRUDContext.Put_movie(movie);
        //    return x switch
        //    {
        //        0 => BadRequest("Esta pelicula ya se encuentra registrada."),
        //        -1 => BadRequest("No se ha encontrado una pelicula con este nombre."),
        //        _ => Ok(),
        //    };
        //}

        //// PUT api/Movies/5
        //[HttpPut("byName/{name}")]
        //public void PutByName(string name, [FromBody] Movie movie)
        //{
        //    _CRUDContext.Put_Movie(name, movie);
        //}

        //// DELETE api/Movies/byName/Shuek
        //[HttpDelete("byName/{original_name}")]
        //public void DeleteByName(string original_name)
        //{
        //    var item = _CRUDContext.Movies.FirstOrDefault(x => x.original_name == original_name);
        //    if (item != null)
        //    {
        //        _CRUDContext.Movies.Remove(item);
        //        _CRUDContext.SaveChanges();
        //    }
        //}
    }
}
