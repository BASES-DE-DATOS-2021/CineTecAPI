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
    public class BillsController : ControllerBase
    {
        private readonly CRUDContext _CRUDContext;

        public BillsController(CRUDContext CRUDContext)
        {
            _CRUDContext = CRUDContext;
        }

        // GET: api/Bills
        [HttpGet]
        public IEnumerable<Bill> Get()
        {
            return _CRUDContext.Bills;
        }

        // GET api/Bills/5
        [HttpGet("{id}")]
        public Bill Get(int id)
        {
            return _CRUDContext.Bills.SingleOrDefault(x => x.id == id);
        }

        // GET api/Bills/byClientId/
        [HttpGet("byClientId/{cedula}")]
        public IEnumerable<Bill> Get_byClientId(int cedula)
        {
            return _CRUDContext.GetBills_byClientId(cedula);
        }

        // POST api/Bills
        [HttpPost]
        public ActionResult Post([FromBody] Bill bill)
        {
            _CRUDContext.Bills.Add(bill);
            _CRUDContext.SaveChanges();
            return Ok();
        }

        // PUT api/Bills/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Bill bill)
        {
            bill.id = id;
            _CRUDContext.Bills.Update(bill);
            _CRUDContext.SaveChanges();
            return Ok();
        }

        // DELETE api/Bills/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var item = _CRUDContext.Bills.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                _CRUDContext.Bills.Remove(item);
                _CRUDContext.SaveChanges();
                return Ok();
            }
            return BadRequest("No se encuentra ninguna factura que coincida.");
        }
    }
}
