﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CineTec.Context;
using CineTec.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Entity.Validation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CineTec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly CRUDContext _CRUDContext;

        public RoomsController(CRUDContext CRUDContext)
        {
            _CRUDContext = CRUDContext;
        }

        // GET: api/Rooms
        [HttpGet]
        public Object Get()
        {
            return _CRUDContext.GetRooms_special();
        }

        // GET api/Rooms/5
        [HttpGet("{id}")]
        public Object Get(int id) => _CRUDContext.GetRoom_special(id);

        // POST api/Rooms
        [HttpPost]
        public IActionResult Post([FromBody] Room room)
        {

            if (!(6 <= room.row_quantity && room.row_quantity <= 10))
                return BadRequest("El valor de filas debe ser entre 6 - 10.");

            if (!(20 <= room.column_quantity && room.column_quantity <= 26 && room.column_quantity % 2 == 0))
                return BadRequest("El valor de columnas debe ser entre 20 - 26 y debe ser par.");

            _CRUDContext.Post_room(room);
            return Ok();
        }


        //// DELETE api/Rooms/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    int x = _CRUDContext.Delete_room_and_seats(id);
        //    return x switch
        //    {
        //        2 => BadRequest("No se puede eliminar una sala que tiene projecciones relacionadas."),
        //        -1 => BadRequest("No se ha encontrado esta sala."),
        //        _ => Ok(), // Se elimina correctamente.
        //    };
        //}



    } 
}
