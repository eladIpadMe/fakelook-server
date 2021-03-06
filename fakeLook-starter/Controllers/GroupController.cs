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
    public class GroupController : ControllerBase
    {

        private readonly IGroupRepository _repository;

        public GroupController(IGroupRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<GroupController>
        [HttpGet]
        [TypeFilter(typeof(GetUserActionFilter))]
        public IEnumerable<Group> Get()
        {
            return  _repository.GetAll();
        }

        // GET api/<GroupController>/5
        [HttpGet("{id}")]
        [TypeFilter(typeof(GetUserActionFilter))]
        public JsonResult Get(int id)
        {
            return new JsonResult(_repository.GetById(id));
        }

        // POST api/<GroupController>
        [HttpPost]
        [TypeFilter(typeof(GetUserActionFilter))]
        public async Task<ActionResult<Group>> Post([FromBody] Group group)
        {
            return await _repository.Add(group);
        }


        // PUT api/<GroupController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Group>> Put(int id, [FromBody] Group group)
        {
            return await _repository.Edit(group);
        }

       

        // DELETE api/<GroupController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Group>> Delete(int id)
        {
            return await _repository.Delete(id);

        }
    }
}
