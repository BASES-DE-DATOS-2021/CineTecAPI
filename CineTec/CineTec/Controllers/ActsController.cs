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
        public IEnumerable<Acts> Get() => _CRUDContext.Acts;
       
        // GET api/Acts/byActorsId/
        [HttpGet("byMovieId/{movie_id}")]
        public IEnumerable<Acts> GetActs_byMovieId(int movie_id) => _CRUDContext.GetActs_byMovieId(movie_id);
        
        // GET api/Acts/actsIn?actor_id=a
        [HttpGet("byActorsId/{actor_id}")]
        public IEnumerable<Acts> GetActs_byActorsId(int actor_id) => _CRUDContext.GetActs_byActorsId(actor_id);
    }
}
