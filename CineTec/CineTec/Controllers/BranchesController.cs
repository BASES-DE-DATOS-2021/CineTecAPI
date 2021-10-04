using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTec.Context;
using CineTec.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CineTec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly CRUDContext _CRUDContext;

        public BranchesController(CRUDContext CRUDContext)
        {
            _CRUDContext = CRUDContext;
        }

        // GET: api/Branches
        [HttpGet]
        public IEnumerable<Branch> Get()
        {
            return _CRUDContext.Branches;
        }

        // GET api/Branches/5
        [HttpGet("{id}")]
        public Branch Get(string id)
        {
            return _CRUDContext.Branches.SingleOrDefault(x => x.cinema_name == id);
        }

        // POST api/Branches
        [HttpPost]
        public void Post([FromBody] Branch branch)
        {
            _CRUDContext.Branches.Add(branch);
            _CRUDContext.SaveChanges();
        }

        // PUT api/Branches/5
        [HttpPut("{id}")]
        public void Put([FromBody] Branch branch)
        {
            _CRUDContext.Branches.Update(branch);
            _CRUDContext.SaveChanges();
        }

        // DELETE api/Branches/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var item = _CRUDContext.Branches.FirstOrDefault(x => x.cinema_name == id);
            if (item != null)
            {
                _CRUDContext.Branches.Remove(item);
                _CRUDContext.SaveChanges();
            }
        }
    }
}
