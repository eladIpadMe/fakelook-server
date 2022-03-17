using fakeLook_models.Models;
using fakeLook_starter.Filters;
using fakeLook_starter.Interfaces;
using fakeLook_starter.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fakeLook_starter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly IPostRepository _repository;

        public PostController(IPostRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<PostController>
        [HttpGet]
        public IEnumerable<Post> Get()
        {
            return _repository.GetAll();
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            try
            {
            return new JsonResult(_repository.GetById(id));

            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
                throw;
            }
        }

        // POST api/<PostController>
        [HttpPost]
        [Route("Post")]
        //[Authorize]
        //[TypeFilter(typeof(GetUserActionFilter))]
        public async Task<ActionResult<Post>> Post([FromBody] Post post)
        {
            return await _repository.Add(post);
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> Put(int id, [FromBody] Post post)
        {
            return await _repository.Edit(post);
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> Delete(int id)
        {
            return await _repository.Delete(id);
        }
    }
}
