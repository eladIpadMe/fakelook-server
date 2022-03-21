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
    public class UserTaggedCommentController : Controller
    {
        private readonly IUserTaggedCommentRepository _repository;
        public UserTaggedCommentController(IUserTaggedCommentRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<UserTaggedCommentController>
        [HttpGet]
        [TypeFilter(typeof(GetUserActionFilter))]
        public IEnumerable<UserTaggedComment> Get()
        {
            return _repository.GetAll();
        }

        // GET api/<UserTaggedCommentController>/5
        [HttpGet("{id}")]
        [TypeFilter(typeof(GetUserActionFilter))]
        public JsonResult Get(int id)
        {
            return new JsonResult(_repository.GetById(id));
        }

        // POST api/<UserTaggedCommentController>
        [HttpPost]
        [TypeFilter(typeof(GetUserActionFilter))]
        public async Task<ActionResult<UserTaggedComment>> Post([FromBody] UserTaggedComment userTaggedComment)
        {
            return await _repository.Add(userTaggedComment);
        }

        // PUT api/<UserTaggedCommentController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserTaggedComment>> Put(int id, [FromBody] UserTaggedComment userTaggedComment)
        {
            return await _repository.Edit(userTaggedComment);
        }

        // DELETE api/<UserTaggedCommentController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserTaggedComment>> Delete(int id)
        {
            return await _repository.Delete(id);
        }
    }
}
