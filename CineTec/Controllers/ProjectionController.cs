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
    public class ProjectionController : ControllerBase
    {
        private readonly IProjectionProvider _dataAccessProvider;

        public ProjectionController(IProjectionProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }


       
        [HttpGet]
        public IEnumerable<Projection> Get()
        {
            return _dataAccessProvider.GetAllProjections();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Projection projection)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.InsertProjection(projection);
                return Ok();
            }
            return BadRequest();
        }


        [HttpGet("{id}")]
        public Projection Details(int id)
        {
            return _dataAccessProvider.GetProjection(id);
        }

        [HttpGet("room={room_id}/movie={movie_id}")]
        public IEnumerable<Projection> GetByRoomAndMovie(int room_id, int movie_id)
        {
            return _dataAccessProvider.GetProjectionByBranchAndMovie(room_id, movie_id);
        }



        [HttpPut]
        public IActionResult Edit([FromBody] Projection projection)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateProjection(projection);
                return Ok();
            }
            return BadRequest();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetProjection(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteProjection(id);
            return Ok();
        }

        
    }

        
}
