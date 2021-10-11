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
    public class EmployeesController : ControllerBase
    {
        private readonly CRUDContext _CRUDContext;

        public EmployeesController(CRUDContext CRUDContext)
        {
            _CRUDContext = CRUDContext;
        }

        // GET: api/Employees
        [HttpGet]
        public object Get() => _CRUDContext.GetEmployees_select();

        // GET api/LogIn?username=a&password=b
        [HttpGet("logIn")]
        public object Login(string username, string password)
        {
            var v = _CRUDContext.Login_admin(username, password);
            if (v == null) return BadRequest("Nombre de usuario o contraseña incorrectos.");
            return v;
        }

     
        // GET api/Employees/5
        [HttpGet("{cedula}")]
        public object Get(int cedula) => _CRUDContext.GetEmployee_select(cedula);
        

        // POST api/Employees
        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            int x = _CRUDContext.Post_employee(employee);
            if (x == 0)
            {
                return BadRequest("Ya existe un empleado asociado a esta cedula.");
            }
            else if (x == 2)
            {
                return BadRequest("Este nombre de usuario ya se encuentra en uso.");
            }
            return Ok();
        }

        // PUT api/Employees/5
        [HttpPut("{cedula}")]
        public IActionResult Put(int cedula, [FromBody] Employee employee)
        {
            int x = _CRUDContext.Put_employee(employee, cedula);

            if (x == -1)
                return BadRequest("No se ha encontrado ningun empleado asociado a esta cedula.");
            return Ok();
        }

        // DELETE api/Employees/5
        [HttpDelete("{cedula}")]
        public IActionResult Delete(int cedula)
        {
            int x = _CRUDContext.Delete_employee(cedula);

            if (x == -1)
                return BadRequest("No existe el cliente con esta cedula.");
            return Ok();
        }
    }
}
