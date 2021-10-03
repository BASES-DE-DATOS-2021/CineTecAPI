using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTec.Models;
using CineTec.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CineTec.Controllers
{

    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieProvider _dataAccessProvider;

        public MovieController(IMovieProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }



        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return _dataAccessProvider.GetAllMovies();
        }

        [HttpPost]
        public IActionResult Create([FromBody] MovieComplete movie)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.InsertMovie(movie);
                return Ok();
            }
            return BadRequest();
        }


        [HttpGet("{id}")]
        public Movie Details(int id)
        {
            return _dataAccessProvider.GetMovie(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateMovie(movie);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetMovie(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteMovie(id);
            return Ok();
        }


    }


}
