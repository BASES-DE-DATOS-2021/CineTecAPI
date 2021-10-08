using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CineTec.Context;
using CineTec.Models;
using Microsoft.EntityFrameworkCore;
using System;

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

        // GET api/Branches/Cinectec Cartago
        [HttpGet("{cinema_name}")]
        public Branch Get(string cinema_name)
        {
            return _CRUDContext.Branches.SingleOrDefault(x => x.cinema_name == cinema_name);
        }


        // GET api/Branches/all_rooms?cinema_name=a
        [HttpGet("all_rooms")]
        public IList<Room> Get_all_rooms(string cinema_name)
        {
            return _CRUDContext.Get_all_rooms_of_a_branch(cinema_name);
        }

        // GET api/Branches/all_projections_dates?cinema_name=a
        [HttpGet("all_projections_dates")]
        public IList<DateTime> Get_all_projections_dates_byBranch(string cinema_name)
        {
            return _CRUDContext.GetProjections_dates_byBranch(cinema_name);
        }

        // POST api/Branches
        [HttpPost]
        public void Post([FromBody] Branch branch)
        {
            branch.rooms_quantity = 0; // Por si llega algun valor setearlo a 0.
            _CRUDContext.Branches.Add(branch);
            _CRUDContext.SaveChanges();
        }

        // PUT api/Branches/Cinectec Cartago
        [HttpPut("{cinema_name}")]
        public void Put(string cinema_name, [FromBody] Branch branch)
        {
            branch.cinema_name = cinema_name;
            _CRUDContext.Branches.Update(branch);
            _CRUDContext.SaveChanges();
        }


        // DELETE api/Branches/Cinectec Cartago
        [HttpDelete("{cinema_name}")]
        public ActionResult Delete(string cinema_name)
        {
            return BadRequest(_CRUDContext.Delete_cinema_and_rooms(cinema_name));
        }
    }
}
