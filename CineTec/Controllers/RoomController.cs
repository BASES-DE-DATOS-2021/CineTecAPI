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
    public class RoomController : ControllerBase
    {
        private readonly IRoomProvider _dataAccessProvider;

        public RoomController(IRoomProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }


       
        [HttpGet]
        public IEnumerable<Room> Get()
        {
            return _dataAccessProvider.GetAllRooms();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Room room)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.InsertRoom(room);
                return Ok();
            }
            return BadRequest();
        }


        [HttpGet("{id}")]
        public Room Details(int id)
        {
            return _dataAccessProvider.GetRoom(id);
        }



        [HttpGet("branch={branch_name}")]
        public IEnumerable<Room> GetRoomsByBranchName(string branch_name)
        {
            return _dataAccessProvider.GetRoomByBranchName(branch_name);
        }



        [HttpPut]
        public IActionResult Edit([FromBody] Room room)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateRoom(room);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetRoom(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteRoom(id);
            return Ok();
        }

        
    }

        
}
