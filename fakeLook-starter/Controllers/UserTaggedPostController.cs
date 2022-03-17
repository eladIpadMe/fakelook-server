using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fakeLook_starter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTaggedPostController : Controller
    {
        private readonly IUserTaggedPostRepository _repository;

        public UserTaggedPostController(IUserTaggedPostRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<UserTaggedPostController>
        [HttpGet]
        public IEnumerable<UserTaggedPost> Get()
        {
            return _repository.GetAll();
        }

        // GET api/<UserTaggedPostController>/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            return new JsonResult(_repository.GetById(id));
        }

        // POST api/<UserTaggedPostController>
        [HttpPost]
        public async Task<ActionResult<UserTaggedPost>> Post([FromBody] UserTaggedPost userTaggedPost)
        {
            return await _repository.Add(userTaggedPost);
        }

        // PUT api/<UserTaggedPostController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserTaggedPost>> Put(int id, [FromBody] UserTaggedPost userTaggedPost)
        {
            return await _repository.Edit(userTaggedPost);
        }

        // DELETE api/<UserTaggedPostController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserTaggedPost>> Delete(int id)
        {
            return await _repository.Delete(id);
        }
    }
}
