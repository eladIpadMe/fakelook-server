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

    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _repository;

        public CommentController(ICommentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [TypeFilter(typeof(GetUserActionFilter))]
        public IEnumerable<Comment> Get()
        {
            return _repository.GetAll();
        }

        // GET api/<CommentController>/5
        [HttpGet("{id}")]
        [TypeFilter(typeof(GetUserActionFilter))]
        public JsonResult Get(int id)
        {
            return new JsonResult(_repository.GetById(id));
        }

        // POST api/<CommentController>
        [HttpPost]
        [TypeFilter(typeof(GetUserActionFilter))]
        public async Task<ActionResult<Comment>> Post([FromBody] Comment comment)
        {
            return await _repository.Add(comment);
        }

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Comment>> Put(int id, [FromBody] Comment comment)
        {
            return await _repository.Edit(comment);
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Comment>> Delete(int id)
        {
            return await _repository.Delete(id);
        }





    }
}
