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
    public class EmployeesController : ControllerBase
    {
        private readonly CRUDContext _CRUDContext;

        public EmployeesController(CRUDContext CRUDContext)
        {
            _CRUDContext = CRUDContext;
        }

        // GET: api/Employees
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return _CRUDContext.Employees;
        }

        // GET api/Employees/5
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return _CRUDContext.Employees.SingleOrDefault(x => x.id == id);
        }

        // POST api/Employees
        [HttpPost]
        public void Post([FromBody] Employee employee)
        {
            _CRUDContext.Employees.Add(employee);
            _CRUDContext.SaveChanges();
        }

        // PUT api/Employees/5
        [HttpPut("{id}")]
        public void Put([FromBody] Employee employee)
        {
            _CRUDContext.Employees.Update(employee);
            _CRUDContext.SaveChanges();
        }

        // DELETE api/Employees/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var item = _CRUDContext.Employees.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                _CRUDContext.Employees.Remove(item);
                _CRUDContext.SaveChanges();
            }
        }
    }
}
