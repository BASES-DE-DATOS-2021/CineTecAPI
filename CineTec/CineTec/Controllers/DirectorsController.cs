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
    public class DirectorsController : ControllerBase
    {
        private readonly CRUDContext _CRUDContext;

        public DirectorsController(CRUDContext CRUDContext)
        {
            _CRUDContext = CRUDContext;
        }

        // GET: api/Directors
        [HttpGet]
        public IEnumerable<Director> Get()
        {
            return _CRUDContext.Directors;
        }

        // GET api/Directors/5
        [HttpGet("{id}")]
        public Director Get(int id)
        {
            return _CRUDContext.Directors.SingleOrDefault(x => x.id == id);
        }

        // POST api/Directors
        [HttpPost]
        public void Post([FromBody] Director director)
        {
            _CRUDContext.Directors.Add(director);
            _CRUDContext.SaveChanges();
        }

        // PUT api/Directors/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Director director)
        {
            director.id = id;
            _CRUDContext.Directors.Update(director);
            _CRUDContext.SaveChanges();
        }


        // DELETE api/Directors/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var item = _CRUDContext.Directors.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                _CRUDContext.Directors.Remove(item);
                _CRUDContext.SaveChanges();
            }
        }
    }
}
