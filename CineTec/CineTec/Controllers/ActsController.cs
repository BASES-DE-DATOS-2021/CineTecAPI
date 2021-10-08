using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTec.Context;
using CineTec.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CineTec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActsController : ControllerBase
    {
        private readonly CRUDContext _CRUDContext;

        public ActsController(CRUDContext CRUDContext)
        {
            _CRUDContext = CRUDContext;
        }

        // GET: api/Acts
        [HttpGet]
        public IEnumerable<Acts> Get()
        {
            return _CRUDContext.Acts;
        }

        // GET api/Acts/byActorsId/
        [HttpGet("acts/byMovieId/{movie_id}")]
        public IEnumerable<Acts> GetActs_byMovieId(int movie_id)
        {
            return _CRUDContext.Acts
                    .Where(f => f.movie_id == movie_id);
        }

        // GET api/Acts/actsIn?movie_id=a&actor_id=b
        [HttpGet("acts/byActorsId/{actor_id}")]
        public IEnumerable<Acts> GetActs_byActorsId(int actor_id)
        {
            return _CRUDContext.Acts
                    .Where(f => f.actor_id == actor_id);
        }

        // POST api/Acts/
        [HttpPost]
        public void Post([FromBody] Acts Acts)
        {
            _CRUDContext.Acts.Add(Acts);
            _CRUDContext.SaveChanges();
        }

        // PUT api/Acts/actsIn?movie_id=a&actor_id=b
        [HttpPut("acts")]
        public void Put(int movie_id, int actor_id, [FromBody] Acts acts)
        {
            acts.movie_id = movie_id;
            acts.actor_id = actor_id;
            _CRUDContext.Acts.Update(acts);
            _CRUDContext.SaveChanges();
        }

        // DELETE api/Acts/actsIn?movie_id=a&actor_id=b
        [HttpDelete("acts")]
        public void Delete(int movie_id, int actor_id)
        {
            var item = _CRUDContext.Acts
                        .Where(f => f.movie_id == movie_id && f.actor_id == actor_id)
                        .FirstOrDefault();

            if (item != null)
            {
                _CRUDContext.Acts.Remove(item);
                _CRUDContext.SaveChanges();
            }
        }
    }
}
