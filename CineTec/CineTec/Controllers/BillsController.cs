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

        // POST api/Bills
        [HttpPost]
        public void Post([FromBody] Bill bill)
        {
            _CRUDContext.Bills.Add(bill);
            _CRUDContext.SaveChanges();
        }

        // PUT api/Bills/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Bill bill)
        {
            bill.id = id;
            _CRUDContext.Bills.Update(bill);
            _CRUDContext.SaveChanges();
        }

        // DELETE api/Bills/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var item = _CRUDContext.Bills.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                _CRUDContext.Bills.Remove(item);
                _CRUDContext.SaveChanges();
            }
        }
    }
}
