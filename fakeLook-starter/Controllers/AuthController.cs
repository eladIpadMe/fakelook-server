using fakeLook_models.Models;
using fakeLook_starter.Filters;
using fakeLook_starter.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fakeLook_starter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserRepository _repo;
        private ITokenService _tokenService { get; }

        public AuthController(IUserRepository repo, ITokenService tokenService)
        {
            _repo = repo;
            _tokenService = tokenService;
        }


        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] User user)
        {
            var dbUser = _repo.findItem(user);
            if (dbUser == null) return Problem("user not in system");
            var token = _tokenService.CreateToken(dbUser);
            return Ok(new {token= token,userId= dbUser.Id });
        }
        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp([FromBody] User user)
        {
            var dbUser = _repo.Add(user).Result;
            if (dbUser == null) return Problem("Username exist already");
            var token = _tokenService.CreateToken(dbUser);
            
            return Ok(new { token });
        }
        
        [HttpGet]
        [Route("TestAll")]
        [TypeFilter(typeof(GetUserActionFilter))]

        public IActionResult TestAll()
        {
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("TestAdmin")]
        public IActionResult TestAdmin()
        {
            return Ok();
        }
    }
}
