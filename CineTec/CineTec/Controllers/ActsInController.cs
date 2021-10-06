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
    public class ActsInController : ControllerBase
    {
        private readonly CRUDContext _CRUDContext;

        public ActsInController(CRUDContext CRUDContext)
        {
            _CRUDContext = CRUDContext;
        }

        // GET: api/ActsIn
        [HttpGet]
        public IEnumerable<Acts> Get()
        {
            return _CRUDContext.ActsIn;
        }

        // GET api/ActsIn/actsIn?movie_id=a&actor_id=b
        [HttpGet("actsIn")]
        public Acts Get(int movie_id, int actor_id)
        {
            return _CRUDContext.ActsIn
                    .Where(f => f.movie_id == movie_id && f.actor_id == actor_id)
                    .FirstOrDefault();
        }

        // POST api/ActsIn/
        [HttpPost]
        public void Post([FromBody] Acts Acts)
        {
            _CRUDContext.ActsIn.Add(Acts);
            _CRUDContext.SaveChanges();
        }

        // PUT api/ActsIn/actsIn?movie_id=a&actor_id=b
        [HttpPut("actsIn")]
        public void Put(int movie_id, int actor_id, [FromBody] Acts acts)
        {
            acts.movie_id = movie_id;
            acts.actor_id = actor_id;
            _CRUDContext.ActsIn.Update(acts);
            _CRUDContext.SaveChanges();
        }

        // DELETE api/ActsIn/actsIn?movie_id=a&actor_id=b
        [HttpDelete("actsIn")]
        public void Delete(int movie_id, int actor_id)
        {
            var item = _CRUDContext.ActsIn
                        .Where(f => f.movie_id == movie_id && f.actor_id == actor_id)
                        .FirstOrDefault();

            if (item != null)
            {
                _CRUDContext.ActsIn.Remove(item);
                _CRUDContext.SaveChanges();
            }
        }
    }
}
