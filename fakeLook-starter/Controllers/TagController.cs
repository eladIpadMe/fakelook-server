using fakeLook_models.Models;
using fakeLook_starter.Filters;
using fakeLook_starter.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace fakeLook_starter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : Controller
    {
        private readonly ITagRepository _repository;

        public TagController(ITagRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<TagController>
        [HttpGet]
        [TypeFilter(typeof(GetUserActionFilter))]
        public IEnumerable<Tag> Get()
        {
            return _repository.GetAll();
        }

        // GET api/<TagController>/5
        [HttpGet("{id}")]
        [TypeFilter(typeof(GetUserActionFilter))]
        public JsonResult Get(int id)
        {
            return new JsonResult(_repository.GetById(id));
        }

        // POST api/<TagController>
        [HttpPost]
        [TypeFilter(typeof(GetUserActionFilter))]
        public async Task<ActionResult<Tag>> Post([FromBody] Tag tag)
        {
            return await _repository.Add(tag);
        }

        // PUT api/<TagController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Tag>> Put(int id, [FromBody] Tag tag)
        {
            return await _repository.Edit(tag);
        }

        // DELETE api/<TagController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tag>> Delete(int id)
        {
            return await _repository.Delete(id);
        }
    }
}
