using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CineTec.Context;
using CineTec.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CineTec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly CRUDContext _CRUDContext;

        public ActorsController(CRUDContext CRUDContext)
        {
            _CRUDContext = CRUDContext;
        }

        // GET: api/Actors
        [HttpGet]
        public IEnumerable<Actor> Get() => _CRUDContext.Actors;

        // GET api/Actors/5
        [HttpGet("byId/{id}")]
        public Actor Get_byId(int id) => _CRUDContext.GetActor(id);

        // GET api/Actors/Kevin Hart
        [HttpGet("byName/{name}")]
        public Actor Get_byName(string name) => _CRUDContext.GetActor(name);

        // PUT api/Actors/Kevin Hart
        [HttpPut("{name}")]
        public ActionResult Put(string name, [FromBody] Actor actor)
        {
            var x = _CRUDContext.Put_actor(actor, name);
            if (x == "") return Ok();
            return BadRequest(x);
        }

        // DELETE api/Actors/Kevin Hart
        [HttpDelete("{name}")]
        public ActionResult Delete(string name)
        {
            var x = _CRUDContext.Delete_actor(name);
            if (x == "") return Ok();
            return BadRequest(x);
        }
    }
}
