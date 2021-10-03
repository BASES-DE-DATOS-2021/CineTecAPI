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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeProvider _dataAccessProvider;

        public EmployeeController(IEmployeeProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return _dataAccessProvider.GetAllEmployees();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.InsertEmployee(employee);
                return Ok();
            }
            return BadRequest();
        }

   
        [HttpGet("{id}")]
        public Employee Details(int id)
        {
            return _dataAccessProvider.GetEmployee(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateEmployee(employee);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetEmployee(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteEmployee(id);
            return Ok();
        }
    }
}
