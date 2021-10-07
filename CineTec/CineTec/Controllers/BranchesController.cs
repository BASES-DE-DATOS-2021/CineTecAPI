using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CineTec.Context;
using CineTec.Models;
using Microsoft.EntityFrameworkCore;

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

        // POST api/Branches
        [HttpPost]
        public void Post([FromBody] Branch branch)
        {
            try
            {
                _CRUDContext.Branches.Add(branch);
                _CRUDContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                ////This either returns a error string, or null if it can’t handle that error
                //var error = CheckHandleError(e);
                //if (error != null)
                //{
                //    return error; //return the error string
                //}
                throw; //couldn’t handle that error, so rethrow
            }

        }

        // PUT api/Branches/Cinectec Cartago
        [HttpPut("{cinema_name}")]
        public void Put(string cinema_name, [FromBody] Branch branch)
        {
            branch.cinema_name = cinema_name;
            _CRUDContext.Branches.Update(branch);
            _CRUDContext.SaveChanges();
        }

        private bool EvalRooms(string cinema_name)
        {
            var branch = _CRUDContext.Branches.SingleOrDefault(x => x.cinema_name == cinema_name);
            if (branch != null)
            {
                // Encontrar la cantidad de salas referentes a esta sucursal en el momento.
                RoomsController roomControl = new RoomsController(_CRUDContext);
                var rooms = _CRUDContext.Rooms
                    .Where(r => r.branch_name == cinema_name);

                // Evaluar si aun hay espacio para poder agregar salas en la sucursal.
                if (rooms != null) {
                    return rooms.Count() < branch.room_quantity;
                } 
            }
            return false;
        }






        // DELETE api/Branches/Cinectec Cartago
        [HttpDelete("{cinema_name}")]
        public void Delete(string cinema_name)
        {
            var item = _CRUDContext.Branches.FirstOrDefault(x => x.cinema_name == cinema_name);
            if (item != null)
            {
                //// Elimina las salas de una sucursal.
                //RoomsController roomControl = new RoomsController(_CRUDContext);
                //var rooms = _CRUDContext.Rooms
                //    .Where(r => r.branch_name == cinema_name);
                //_CRUDContext.SaveChanges();
                //foreach (Room room in rooms) {
                //    roomControl.Delete(room.id);
                //}

                _CRUDContext.Branches.Remove(item);
                _CRUDContext.SaveChanges();

            }
        }
    }
}
