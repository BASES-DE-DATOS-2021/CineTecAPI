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
        public void Delete(int cedula)
        {
            var item = _CRUDContext.Clients.FirstOrDefault(x => x.cedula == cedula);
            if (item != null)
            {
                _CRUDContext.Clients.Remove(item);
                _CRUDContext.SaveChanges();
            }
        }
    }
}
