using fakeLook_models.Models;
using fakeLook_starter.Filters;
using fakeLook_starter.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        [TypeFilter(typeof(GetUserActionFilter))]
        public IEnumerable<User> Get()
        {
            return _repository.GetAll();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [TypeFilter(typeof(GetUserActionFilter))]
        public JsonResult Get(int id)
        {
            return new JsonResult(_repository.GetById(id));
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User user)
        {
            return await _repository.Add(user);
        }

        // PUT api/<UserController>/5
        //[HttpPut("{id}")]
        [HttpPut]
        public async Task<ActionResult<User>> Put([FromBody] User user)
        {
            return await _repository.Edit(user);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            return await _repository.Delete(id);
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
