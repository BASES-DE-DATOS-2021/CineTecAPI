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
    public class TimesController : ControllerBase
    {
        private readonly CRUDContext _CRUDContext;

        public TimesController(CRUDContext CRUDContext)
        {
            _CRUDContext = CRUDContext;
        }

        // GET: api/<TimesController>
        [HttpGet]
        public IEnumerable<Times> Get() => _CRUDContext.Times;


        // GET api/<TimesController>/5
        [HttpGet("byProjectionId/{projection_id}")]
        public IEnumerable<Times> GetTimes_byProjectionId(int projection_id) => _CRUDContext.GetTimes_byProjectionId(projection_id);


        [HttpGet("{id}")]
        public Times GetTimes_byId(int id) => _CRUDContext.GetTimes_byId(id);

        //// POST api/<TimesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<TimesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<TimesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
