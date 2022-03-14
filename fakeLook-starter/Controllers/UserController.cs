﻿using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fakeLook_starter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        private readonly IUserRepository  _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            return new JsonResult(_repository.GetById(id));
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    //private readonly IRepository<Person> _repository;

    //public UsersController(IRepository<Person> repository)
    //{
    //    _repository = repository;
    //}

    //[HttpGet]
    //public JsonResult Get(int id)
    //{
    //    return new JsonResult(_repository.Get(id));
    //}
}
