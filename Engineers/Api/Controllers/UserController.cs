using Microsoft.AspNetCore.Mvc;
using Engineers.Models;
using Engineers.Api.Models;
using Engineers.IService;
using Microsoft.AspNetCore.Http;

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

        [HttpGet("GetUsers")]
        public Response GetUsers() => _userService.GetUsers();

        [HttpGet("GetOrders/{id}")]
        public Response GetOrders(string id) => _userService.GetOrders(id);

        [HttpGet("GetReViews/{id}")]
        public Response GetReViews(string id) => _userService.GetReViews(id);

        [HttpGet("GetById/{id}")]
        public Response GetById(string id) => _userService.GetById(id);

        [HttpGet("GetByName/{userName}")]
        public Response GetByName(string userName) => _userService.GetByName(userName);

        [HttpPost("Registration")]
        public Response Registration([FromBody] ApiUser oApiUser, string password) => _userService.Register(oApiUser, password);

        [HttpPost("UpLoadImage/{id}")]
        public Response UpLoadImage(string id, IFormFile ImageFile)
        {
            User user = (User)_userService.GetById(id).Data;

            var path = _fileService.Upload(ImageFile);

            user.Image = path;

            ApiUser apiUser = user.ConverToApiUser();

            return  _userService.Update(id, apiUser);
        }

        [HttpPut("Update/{id}")]
        public Response Update(string id, [FromBody] ApiUser oApiUser) => _userService.Update(id, oApiUser);

        [HttpDelete("Delete/{id}")]
        public Response Delete(string id) => _userService.Delete(id);
    }
}