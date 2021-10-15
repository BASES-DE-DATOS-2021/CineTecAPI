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
        public Object Get() => _CRUDContext.GetProjections();

        // GET api/Projections/seats/
        [HttpGet("seats/{id}")]
        public List<Seat> Get(int id) => _CRUDContext.Get_all_seats_assgined_to_projection(id);

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
