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
        public IEnumerable<Actor> Get()
        {
            return _CRUDContext.Actors;
        }

        // GET api/Actors/5
        [HttpGet("{id}")]
        public Actor Get(int id)
        {
            return _CRUDContext.Actors.SingleOrDefault(x => x.id == id);
        }

        // POST api/Actors
/*        [HttpPost]
        public void Post([FromBody] Actor actor)
        {
            _CRUDContext.Actors.Add(actor);
            _CRUDContext.SaveChanges();
        }*/

        // PUT api/Actors/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Actor actor)
        {
            actor.id = id;
            _CRUDContext.Actors.Update(actor);
            _CRUDContext.SaveChanges();
        }

        // DELETE api/Actors/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var item = _CRUDContext.Actors.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                _CRUDContext.Actors.Remove(item);
                _CRUDContext.SaveChanges();
            }
        }
    }
}
