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
    public class ClassificationsController : ControllerBase
    {
        private readonly CRUDContext _CRUDContext;

        public ClassificationsController(CRUDContext CRUDContext)
        {
            _CRUDContext = CRUDContext;
        }

        // GET: api/Classifications
        [HttpGet]
        public IEnumerable<Classification> Get()
        {
            return _CRUDContext.Classifications;
        }

        // GET api/Classifications/5
        [HttpGet("{code}")]
        public Classification Get(string code)
        {
            return _CRUDContext.Classifications.SingleOrDefault(x => x.code == code);
        }

        // POST api/Classifications
        [HttpPost]
        public void Post([FromBody] Classification Classification)
        {
            _CRUDContext.Classifications.Add(Classification);
            _CRUDContext.SaveChanges();
        }

        // PUT api/Classifications/R
        [HttpPut("{code}")]
        public void Put(string code, [FromBody] Classification Classification)
        {
            Classification.code = code;
            _CRUDContext.Classifications.Update(Classification);
            _CRUDContext.SaveChanges();
        }

        // DELETE api/Classifications/5
        [HttpDelete("{code}")]
        public void Delete(string code)
        {
            var item = _CRUDContext.Classifications.FirstOrDefault(x => x.code == code);
            if (item != null)
            {
                _CRUDContext.Classifications.Remove(item);
                _CRUDContext.SaveChanges();
            }
        }
    }
}
