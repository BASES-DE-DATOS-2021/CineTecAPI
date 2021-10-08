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
            return _CRUDContext.GetDirector(id);
        }

        // GET api/Directors/byName/Pablo
        [HttpGet("byName/{name}")]
        public Director Get(string name)
        {
            return _CRUDContext.GetDirector(name);
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
        public ActionResult Delete(string name)
        {
            
            string resp = "";
            int x = _CRUDContext.Delete_director(name);
            switch (x)
            {
                case 0:
                    resp = "No se puede eliminar un director que se encuentra asignado a una pelicula.";
                    break;

                case -1:
                    resp = "No se ha encontrado este director.";
                    break;

                default: // Se elimina correctamente.
                    return Ok();
            }
            return BadRequest(resp);
        }
    }
}
