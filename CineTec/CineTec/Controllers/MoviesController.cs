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
        public IEnumerable<Movie> Get()
        {
            return _CRUDContext.Movies;
        }

        // GET api/Movies/5
        [HttpGet("{id}")]
        public Movie Get(int id)
        {
            return _CRUDContext.Movies.SingleOrDefault(x => x.id == id);
        }

        // POST api/Movies
        [HttpPost]
        public void Post([FromBody] Movie movie)
        {
            _CRUDContext.Movies.Add(movie);
            _CRUDContext.SaveChanges();
        }

        // PUT api/Movies/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Movie movie)
        {
            movie.id = id;
            _CRUDContext.Movies.Update(movie);
            _CRUDContext.SaveChanges();
        }

        // DELETE api/Movies/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var item = _CRUDContext.Movies.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                _CRUDContext.Movies.Remove(item);
                _CRUDContext.SaveChanges();
            }
        }
    }
}
