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
    public class ClientsController : ControllerBase
    {
        private readonly CRUDContext _CRUDContext;

        public ClientsController(CRUDContext CRUDContext)
        {
            _CRUDContext = CRUDContext;
        }

        // GET: api/Clients
        [HttpGet]
        public object Get() => _CRUDContext.GetClients_select();

        // GET api/Clients/5
        [HttpGet("{cedula}")]
        public object Get(int cedula) => _CRUDContext.GetClient_select(cedula);

        // GET api/Clients/logIn?username=a&password=b
        [HttpGet("logIn")]
        public object Login(string username, string password)
        {
            var v = _CRUDContext.Login_client(username, password);
            if (v == null) return BadRequest("Nombre de usuario o contraseña incorrectos.");
            return v;
        }


        // POST api/Clients
        [HttpPost]
        public IActionResult Post([FromBody] Client client)
        {
            int x = _CRUDContext.Post_client(client);
            if (x == 0)
            {
                return BadRequest("Ya existe un cliente asociado a esta cedula.");
            }
            else if (x == 2)
            {
                return BadRequest("Este nombre de usuario ya se encuentra en uso.");
            }
            return Ok();
        }

        // PUT api/Clients/5
        [HttpPut("{cedula}")]
        public IActionResult Put(int cedula, [FromBody] Client client)
        {
            client.cedula = cedula;
            int x = _CRUDContext.Put_client(client);

            if (x == -1)
                return BadRequest("No se ha encontrado ninguna sucursal con este nombre.");
            return Ok();
        }

        // DELETE api/Clients/5
        [HttpDelete("{cedula}")]
        public ActionResult Delete(int cedula)
        {
            int x = _CRUDContext.Delete_client(cedula);
            return x switch
            {
                0 => BadRequest("No se puede eliminar un cliente que tiene facturas asignadas."),
                -1 => BadRequest("No se ha encontrado este cliente."),
                _ => Ok(), // Se elimina correctamente.
            };
        }
    }
}
