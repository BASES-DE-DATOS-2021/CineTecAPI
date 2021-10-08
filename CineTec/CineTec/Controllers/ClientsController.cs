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
        public IEnumerable<Client> Get()
        {
            return _CRUDContext.Clients;
        }

        // GET api/Clients/5
        [HttpGet("{cedula}")]
        public Client Get(int cedula)
        {
            return _CRUDContext.Clients.SingleOrDefault(x => x.cedula == cedula);
        }

        // POST api/Clients
        [HttpPost]
        public void Post([FromBody] Client client)
        {
            _CRUDContext.Clients.Add(client);
            _CRUDContext.SaveChanges();
        }

        // PUT api/Clients/5
        [HttpPut("{cedula}")]
        public void Put(int cedula, [FromBody] Client client)
        {
            client.cedula = cedula;
            _CRUDContext.Clients.Update(client);
            _CRUDContext.SaveChanges();
        }

        // DELETE api/Clients/5
        [HttpDelete("{cedula}")]
        public ActionResult Delete(int cedula)
        {
            string resp = "";
            int x = _CRUDContext.Delete_client(cedula);
            switch (x)
            {
                case 0:
                    resp = "No se puede eliminar un cliente que se encuentra asignado a una factura.";
                    break;

                case -1:
                    resp = "No se ha encontrado este cliente.";
                    break;

                default: // Se elimina correctamente.
                    return Ok();
            }
            return BadRequest(resp);
        }
    }
}
