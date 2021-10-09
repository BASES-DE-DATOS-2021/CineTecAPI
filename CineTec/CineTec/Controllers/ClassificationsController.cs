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
    public class ClassificationsController : ControllerBase
    {
        private readonly CRUDContext _CRUDContext;

        public ClassificationsController(CRUDContext CRUDContext)
        {
            _CRUDContext = CRUDContext;
        }

        // GET: api/Classifications
        [HttpGet]
        public IEnumerable<Classification> Get() => _CRUDContext.Classifications;

        // GET api/Classifications/5
        [HttpGet("{code}")]
        public Classification Get(string code) => _CRUDContext.Classifications.SingleOrDefault(x => x.code == code);

        // POST api/Classifications
        [HttpPost]
        public IActionResult Post([FromBody] Classification classification)
        {
            int x = _CRUDContext.Post_classification(classification);
            return x switch
            {
                0 => BadRequest("Este codigo ya se encuentra en uso. Por favor ingrese otro."),
                _ => Ok(),
            };
        }

        // PUT api/Classifications/R
        [HttpPut("{code}")]
        public IActionResult Put(string code, [FromBody] Classification classification)
        {
            classification.code = code;
            int x = _CRUDContext.Put_classification(classification);
            return x switch
            {
                0 => BadRequest("El rango de edad ingresado ya se encuentra en uso. Por favor ingrese otro."),
                -1 => BadRequest("No se ha encontrado una clasificacion con este codigo."),
                _ => Ok(),
            };
        }

        // DELETE api/Classifications/5
        [HttpDelete("{code}")]
        public ActionResult Delete(string code)
        {
            int x = _CRUDContext.Delete_classification(code);
            return x switch
            {
                0 => BadRequest("No se puede eliminar una clasificacion que se encuentra asignada a una pelicula."),
                -1 => BadRequest("No se ha encontrado esta clasificacion."),
                _ => Ok(), // Se elimina correctamente.
            };
        }
    }
}
