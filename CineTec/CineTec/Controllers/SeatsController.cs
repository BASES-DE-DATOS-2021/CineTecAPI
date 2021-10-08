﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CineTec.Context;
using CineTec.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CineTec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly CRUDContext _CRUDContext;

        public SeatsController(CRUDContext CRUDContext)
        {
            _CRUDContext = CRUDContext;
        }

        // GET: api/Seats
        [HttpGet]
        public IEnumerable<Seat> Get()
        {
            return _CRUDContext.Seats;
        }

        // GET api/Seats/byId?room_id=a&number=b
        [HttpGet("byId")]
        public Seat Get(int room_id, int number)
        {
            return _CRUDContext.GetSeat(room_id, number);
        }


        // POST api/Seats
        //[HttpPost]
        //public void Post([FromBody] Seat Seat)
        //{
        //    _CRUDContext.Seats.Add(Seat);
        //    _CRUDContext.SaveChanges();
        //}

        // PUT api/Seats/byId?room_id=a&number=b
        [HttpPut("byId")]
        public void Put(int room_id, int number, [FromBody] Seat Seat)
        {
            Seat s = _CRUDContext.GetSeat(room_id, number);
            s.status = Seat.status;
            _CRUDContext.Seats.Update(s);
            _CRUDContext.SaveChanges();
        }
    }
}
