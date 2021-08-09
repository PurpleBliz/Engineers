using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Engineers.Models;
using Engineers.IService;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System;

namespace Engineers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public UserController(IUserService userService, IFileService fileService)
        {
            _fileService = fileService;
            _userService = userService;
        }

        // GET: api/<UserController>
        [HttpGet("GetUsers")]
        public IEnumerable<User> GetUsers()
        {
            return _userService.GetUsers();
        }

        // GET api/<UserController>/5
        [HttpGet("GetById/{id}")]
        public Task<User> GetById(string id)
        {
            return _userService.GetById(id);
        }

        // GET api/<UserController>/5
        [HttpGet("GetByName/{userName}")]
        public Task<User> GetByName(string userName)
        {
            return _userService.GetByName(userName);
        }

        // POST api/<UserController>
        [HttpPost("Registration")]
        public Task<string> Registration([FromBody] ApiUser oApiUser, string password)
        {
            return _userService.Register(oApiUser, password);
        }

        [HttpPost("UpLoadImage")]
        public async Task<string> UpLoadImage([FromBody] string userId, IFormFile ImageFile)
        {
            User user = await _userService.GetById(userId);

            var path = _fileService.Upload(ImageFile);

            user.Image = path;

            ApiUser apiUser = user.ConverToApiUser();

            return await _userService.Update(userId, apiUser);
        }

        // PUT api/<UserController>/5
        [HttpPut("Update/{id}")]
        public Task<string> Update(string id, [FromBody] ApiUser oApiUser)
        {
            return _userService.Update(id, oApiUser);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("Delete/{id}")]
        public Task<string> Delete(string id)
        {
            return _userService.Delete(id);
        }
    }
}