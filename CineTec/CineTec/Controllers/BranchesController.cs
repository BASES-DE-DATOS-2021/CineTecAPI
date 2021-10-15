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
        public Object Get() => _CRUDContext.GetBranches();


        // GET api/Branches/Cinectec Cartago
        [HttpGet("{cinema_name}")]
        public Object Get(string cinema_name) => _CRUDContext.GetBranch_select(cinema_name);



        // GET api/Branches/all_rooms?cinema_name=a
        [HttpGet("all_rooms")]
        public Object Get_all_rooms(string cinema_name) => _CRUDContext.Get_all_rooms_of_a_branch(cinema_name);



        // GET: api/Branches/projections_by_date?cinema_name=a&date=b
        [HttpGet("projections_by_date")]
        public Object Get(string cinema_name, string date)
        {
            return _CRUDContext.GetBranches_Movie_Projection_select(cinema_name, date);
        }

        // GET: api/Branches/projections_by_date?cinema_name=a&date=b
        [HttpGet("projections_by_branch")]
        public Object Get_projections_by_branch(string cinema_name)
        {
            return _CRUDContext.GetBranches_Movie_Projection_no_date_select(cinema_name);
        }



        // GET api/Branches/all_projections_dates?cinema_name=a
        [HttpGet("all_projections_dates")]
        public List<string> Get_all_projections_dates_byBranch(string cinema_name) => _CRUDContext.GetProjections_dates_byBranch(cinema_name);

        // POST api/Branches
        [HttpPost]
        public IActionResult Post([FromBody] Branch branch)
        {
            int x = _CRUDContext.Post_branch(branch);
            if (x == 0)
                return BadRequest("Ya existe una sucursal con este nombre");
            return Ok();
        }

        // PUT api/Branches/Cinectec Cartago
        [HttpPut("{cinema_name}")]
        public IActionResult Put(string cinema_name, [FromBody] Branch branch)
        {
            branch.cinema_name = cinema_name;
            int x = _CRUDContext.Put_branch(branch);

            if (x == -1)
                return BadRequest("No se ha encontrado ninguna sucursal con este nombre.");
            return Ok();
        }

        // DELETE api/Branches/Cinectec Cartago
        [HttpDelete("{cinema_name}")]
        public ActionResult Delete(string cinema_name)
        {
            int x = _CRUDContext.Delete_branch(cinema_name);
            return x switch
            {
                2 => BadRequest("No se puede eliminar una sucursal que tiene empleados relacionados."),
                3 => BadRequest("Existen salas dentro de esta sucursal."),
                -1 => BadRequest("No se ha encontrado esta sucursal."),
                _ => Ok(), // Se elimina correctamente.
            };
        }
    }
}
