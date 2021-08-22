using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Engineers.Models;
using Engineers.IService;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Engineers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespondController : ControllerBase
    {
        private readonly IRespondService _respondService;

        public RespondController(IRespondService respondService)
        {
            _respondService = respondService;
        }

        [HttpGet("List")]
        public Response Get() => _respondService.Get();

        [HttpGet("ByUser")]
        public Response GetByUser(string userId) => _respondService.GetByUserId(userId);

        [HttpGet("ByOrder")]
        public Response GetByOrder(int orderId) => _respondService.GetByOrderId(orderId);

        [HttpGet("Get/{id}")]
        public Response GetById(int id) => _respondService.GetById(id);

        [HttpPost("Send")]
        public Response Add(Respond respond) => _respondService.Create(respond);

        [HttpPost("Delete")]
        public Response Delete(int id) => _respondService.Delete(id);
    }
}
