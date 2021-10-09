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
    public class DirectorsController : ControllerBase
    {
        private readonly CRUDContext _CRUDContext;

        public DirectorsController(CRUDContext CRUDContext)
        {
            _CRUDContext = CRUDContext;
        }

        // GET: api/Directors
        [HttpGet]
        public IEnumerable<Director> Get() => _CRUDContext.Directors;

        // GET api/Directors/5
        [HttpGet("byId/{id}")]
        public Director Get_byId(int id) => _CRUDContext.GetDirector(id);

        // GET api/Directors/Kevin Hart
        [HttpGet("byName/{name}")]
        public Director Get_byName(string name) => _CRUDContext.GetDirector(name);

        // PUT api/Directors/Kevin Hart
        [HttpPut("{name}")]
        public ActionResult Put(string name, [FromBody] Director director)
        {
            int x = _CRUDContext.Put_director(director, name);
            return x switch
            {
                0 => BadRequest("El nombre al que desea actualizar ya se encuentra en uso. Por favor ingrese otro."),
                -1 => BadRequest("No se ha encontrado un director con este nombre."),
                _ => Ok(),
            };
        }

        // DELETE api/Directors/Kevin Hart
        [HttpDelete("{name}")]
        public ActionResult Delete(string name)
        {
            int x = _CRUDContext.Delete_director(name);
            return x switch
            {
                0 => BadRequest("No se puede eliminar un director que se encuentra asignado a una pelicula."),
                -1 => BadRequest("No se ha encontrado este director."),
                _ => Ok(),// Se elimina correctamente.
            };
        }
    }
}
