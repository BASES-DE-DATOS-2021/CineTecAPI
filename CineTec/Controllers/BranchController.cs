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
    public class BranchController : ControllerBase
    {
        private readonly IBranchProvider _dataAccessProvider;

        public BranchController(IBranchProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }


       
        [HttpGet]
        public IEnumerable<Branch> Get()
        {
            return _dataAccessProvider.GetAllBranchs();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Branch branch)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.InsertBranch(branch);
                return Ok();
            }
            return BadRequest();
        }


        [HttpGet("{cinema_name}")]
        public Branch Details(string cinema_name)
        {
            return _dataAccessProvider.GetBranch(cinema_name);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Branch branch)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateBranch(branch);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{cinema_name}")]
        public IActionResult DeleteConfirmed(string cinema_room)
        {
            var data = _dataAccessProvider.GetBranch(cinema_room);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteBranch(cinema_room);
            return Ok();
        }

        
    }

        
}
