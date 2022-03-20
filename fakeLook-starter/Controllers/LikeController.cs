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
    public class LikeController : Controller
    {
        private readonly ILikeRepository _repository;

        public LikeController(ILikeRepository repository)
        {
            _repository = repository;
        }
        // GET api/<LikeController>
        [HttpGet]

        public IEnumerable<Like> Get()
        {
            return _repository.GetAll();
        }

        // GET api/<LikeController>/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            return new JsonResult(_repository.GetById(id));
        }

        // POST api/<LikeController>
        [HttpPost]
        [TypeFilter(typeof(GetUserActionFilter))]
        public async Task<ActionResult<Like>> Post([FromBody] Like like)
        {
            return await _repository.Add(like);
        }

        // PUT api/<LikeController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Like>> Put(int id, [FromBody] Like like)
        {
            return await _repository.Edit(like);
        }

        // DELETE api/<LikeController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Like>> Delete(int id)
        {
            return await _repository.Delete(id);
        }
    }
}
