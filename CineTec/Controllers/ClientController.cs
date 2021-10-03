using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTec.Models;
using CineTec.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CineTec.Controllers
{
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientProvider _dataAccessProvider;

        public ClientController(IClientProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Client> Get()
        {
            return _dataAccessProvider.GetAllClients();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Client client)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.InsertClient(client);
                return Ok();
            }
            return BadRequest();
        }

   
        [HttpGet("clients/{id}")]
        public Client Details(int id)
        {
            return _dataAccessProvider.GetClient(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Client client)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateClient(client);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetClient(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteClient(id);
            return Ok();
        }
    }
}
